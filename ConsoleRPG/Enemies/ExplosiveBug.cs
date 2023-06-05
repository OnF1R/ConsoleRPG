using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    internal class ExplosiveBug : Enemy
    {
        public ExplosiveBug(int playerLevel) : base(playerLevel)
        {
            Random random = new Random();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Взрывной жук[/]";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
            GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 4 + Level);

            //Экипировка
            Equipment.WearEquip(this, new ProtectionRing(Level), EquipmentSlot.FirstRing);

            DropList = new Item[]
            {
                new Gold(),
                new BloodStone(),
                Equipment.Equip[EquipmentSlot.FirstRing],
            };
        }

        public override void FightLogic(Player Player, Dictionary<DamageTypes, int> TakedDamage)
        {
            foreach (DamageTypes type in TakedDamage.Keys)
            {
                if (!IsDead)
                    TakeDamage(Player, TakedDamage[type], type);
            }

            if (!IsDead)
            {
                Player.AfterAttackBehaviour(this);

                if (Energy >= 4)
                {
                    NecroticExplosion(Player);
                    Energy = 0;
                }
                else
                {
                    Spit(Player);
                    Energy++;
                }

                AfterAttackBehaviour(Player);
            }
            
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Spit(Player Player)
        {
            Spell Spell = new PoisonSpit();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
        }

        public void NecroticExplosion(Player Player)
        {
            Spell Spell = new NecroticExplosion();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                Player.TakeDamage(this, elemDamage[type] + Level + GetPhysicalDamage(), type);
            }
        }
    }
}
