using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    internal class ExplosiveBug : Enemy
    {
        public ExplosiveBug(int level) : base(level)
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

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
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
            }
            
            else
            {
                DeathDropLoot(Player);
            }
        }

        public void Spit(Player Player)
        {
            BaseSpell Spell = new PoisonSpit();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }

        public void NecroticExplosion(Player Player)
        {
            BaseSpell Spell = new NecroticExplosion();
            Dictionary<DamageTypes, int> elemDamage = Spell.GetComponent<ElementalDamageCharacteristic>().ElemDamage;
            foreach (DamageTypes type in elemDamage.Keys)
            {
                DealDamage(Player, elemDamage[type], type, Spell);
            }
        }
    }
}
