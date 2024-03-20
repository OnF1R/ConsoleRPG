using ConsoleRPG.Enums;
using ConsoleRPG.Items.Armors.Helmets;
using ConsoleRPG.Items.Currencies;
using ConsoleRPG.Items.Shields;
using ConsoleRPG.Items.Weapons;
using ConsoleRPG.Spells.DamageSpells;

namespace ConsoleRPG.NPC_s
{
    [Serializable]
    internal class MageNpc : BaseNpc
	{
		public MageNpc(int level) : base(level)
		{
			SerializableRandom random = new SerializableRandom();

			var npcType = random.Next(1, 101);
			
			if (npcType > 80)
			{
				Type = NpcTypeEnum.Standart;
			}

			if (npcType >= 80 && npcType <= 90)
			{
				Type = NpcTypeEnum.Merchant;
			}

			if (npcType > 90)
			{
				Type = NpcTypeEnum.QuestGiver;
			}

			Equipment equipment = new Equipment();
			Name = NpcNames.GetRandomName();
			MaxHealth = random.Next(7, 11) * Level;
			CurrentHealth = MaxHealth;
			GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage = random.Next(Level, Level + 3);

			//Экипировка
			Equipment.WearEquip(this, new EnchantedHat(Level), EquipmentSlot.Helmet);
			Equipment.WearEquip(this, new RunicShield(Level), EquipmentSlot.RightHand);
			Equipment.WearEquip(this, new MagicStaff(Level), EquipmentSlot.LeftHand);

			DropList = new Item[]
			{
				new Gold(),
				Equipment.Equip[EquipmentSlot.LeftHand],
				Equipment.Equip[EquipmentSlot.Helmet],
				Equipment.Equip[EquipmentSlot.RightHand]
			};
		}

		public override void FightLogic(Player Player, Unit unit)
        {
			if (!IsDead)
			{
				{
					foreach (var entity in GetDamageEntities())
						Attack(Player, entity);
				}

				Energy++;
			}
			else
			{
				Death(Player);
			}
		}
	}
}
