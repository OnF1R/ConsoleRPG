using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    internal class FireMage : Enemy
    {
        public FireMage(int level) : base(level)
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Огненный[/] маг";
            MaxHealth = random.Next(7, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

            //Экипировка
            Equipment.WearEquip(this, new EnchantedHat(Level), EquipmentSlot.Helmet);
            Equipment.WearEquip(this, new FireSword(Level), EquipmentSlot.LeftHand);
            Equipment.WearEquip(this, new RunicShield(Level), EquipmentSlot.RightHand);

            DropList = new Item[]
            {
                new Gold(),
                Equipment.Equip[EquipmentSlot.LeftHand],
                Equipment.Equip[EquipmentSlot.Helmet],
                Equipment.Equip[EquipmentSlot.RightHand]
            };
        }

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy >= 3)
                {
                    Pyroblast(Player);
                    Energy = 0;
                }
                else
                {
                    if (new Random().Next(0, 2) == 0)
                    {
                        FireBall(Player);
                    }
                    else
                    {
                        foreach (var entity in GetDamageEntities())
                            Attack(Player, entity);
                    }

                }
                Energy++;
            }
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void FireBall(Player Player)
        {
            BaseSpell Spell = new FireBall();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }

        }

        public void Pyroblast(Player Player)
        {
            BaseSpell Spell = new Pyroblast();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }
    }
}
