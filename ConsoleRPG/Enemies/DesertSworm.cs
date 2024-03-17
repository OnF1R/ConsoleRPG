using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    [Serializable]
    internal class DesertSworm : Enemy
    {
        public DesertSworm(int level) : base(level)
        {
            SerializableRandom random = new SerializableRandom();
            Equipment equipment = new Equipment();
            Name = "[orange1]Пустынный[/] червь";
            MaxHealth = random.Next(6, 11) * Level;
            CurrentHealth = MaxHealth;
			ID = EnemyIdentifierEnum.DesertSworm;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, 4 + Level);

			//Экипировка
			Equipment.WearEquip(this, new TeethBaseWeapon(Level), EquipmentSlot.LeftHand);

			DropList = new Item[]
            {
                new DesertRune(),
                new SwormTooth(),
                new SwormSkin(),
            };
        }

        public override void FightLogic(Player Player, Unit unit)
        {
            if (!IsDead)
            {
                if (Energy % 2 == 0)
                {
					foreach (var entity in GetDamageEntities())
						Attack(unit, entity);
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
    }
}
