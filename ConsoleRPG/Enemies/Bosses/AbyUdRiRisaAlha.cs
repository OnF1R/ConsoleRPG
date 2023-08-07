using Spectre.Console;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Races;
using ConsoleRPG.Items.Armors.Capes;
using ConsoleRPG.Spells.DamageSpells;
using ConsoleRPG.Interfaces;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Items;

namespace ConsoleRPG.Enemies.Bosses
{
    internal class AbyUdRiRisaAlha : Enemy
    {
        public AbyUdRiRisaAlha(int level) : base(level)
        {
            Random random = new Random();
            MyRace = new Human();
            IsBoss = true;
            Equipment equipment = new Equipment();
            Name = "[red]БОСС[/] [bold]Абу Уд'ри Риса Аль-ха[/]";
            MaxHealth = random.Next(20, 45) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(2 + Level, Level + 6);

            //Экипировка

            Equipment.WearEquip(this, new RaAyatAlQaiwai(Level), EquipmentSlot.Cape);

            DropList = new Item[]
            {
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                new RainbowStone(),
                Equipment.Equip[EquipmentSlot.Cape],
            };
        }

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy > 6)
                {
                    ArdaBintBaaniAlTaa(Player);
                    Energy = 0;
                }
                else
                {
                    foreach (var entity in GetDamageEntities())
                        Attack(Player, entity);
                }

                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public override IDamageDealerEntity[] GetDamageEntities()
        {
            Type classType = typeof(NothingItem);
            Type interfaceType = typeof(IDamageDealerEntity);
            List<Weapon> items = new List<Weapon>();

            foreach (var equip in Equipment.Equip.Keys)
            {
                if (Equipment.Equip[equip].GetType() == classType)
                {
                    if (equip == EquipmentSlot.LeftHand)
                        if (Equipment.Equip[EquipmentSlot.RightHand].GetType() == classType)
                            items.Add(new HandsBaseWeapon(Level));

                }
                else if (Equipment.Equip[equip].GetType().GetInterfaces().Contains(interfaceType))
                {
                    items.Add((Weapon)Equipment.Equip[equip]);
                }
            }

            return items.ToArray();
        }

        public void ArdaBintBaaniAlTaa(Player Player)
        {
            BaseSpell Spell = new ArdaBintBaaniAlTaa();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }
    }
}
