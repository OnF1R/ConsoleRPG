using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class ExplosiveBug : Enemy
    {
        public ExplosiveBug(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[orangered1]Взрывной жук[/]";
            MaxHealth = random.Next(4, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.ExplosiveBug;
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

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy >= 4)
                {
                    NecroticExplosion(unit);
                    Energy = 0;
                }
                else
                {
                    Spit(unit);
                    Energy++;
                }
            }
            
            else
            {
                Death(Player);
            }
        }

        public void Spit(Unit unit)
        {
            BaseSpell Spell = new PoisonSpit();
			Spell.Use(this, unit);
		}

        public void NecroticExplosion(Unit unit)
        {
            BaseSpell Spell = new NecroticExplosion();
			Spell.Use(this, unit);
		}
    }
}
