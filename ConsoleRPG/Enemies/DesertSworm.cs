using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Rings;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.StacableItems;
using ConsoleRPG.Items.Weapons.BaseEnemyWeapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.Enemies
{
    internal class DesertSworm : Enemy
    {
        public DesertSworm(int level) : base(level)
        {
            Random random = new Random();
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

        public override void FightLogic(Player Player)
        {
            if (!IsDead)
            {
                if (Energy % 2 == 0)
                {
					foreach (var entity in GetDamageEntities())
						Attack(Player, entity);
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
			Spell.Use(this, Player);
		}
    }
}
