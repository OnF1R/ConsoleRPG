
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG.Items.Weapons
{
    internal class FireSword : Weapon
    {
        public FireSword()
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[orangered1]Огненный[/] меч";
            int Chance = rand.Next(1, 101);

            ID = ItemIdentifier.FireSword;

            if (Chance <= 5)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(5, 12) });
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(1, 4) });
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(8, 17) });
                AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    {DamageTypes.Fire, rand.Next(1,4) },
                }));
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                RarityId = 0;
                Level = 1;
            }
            else if (Chance <= 85)
            {
                AddComponent(new ValueCharacteristic { Cost = rand.Next(11, 22) });
                AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    {DamageTypes.Fire, rand.Next(3,11) },
                }));
                int resist = rand.Next(-6, 6);
                if (resist != 0) AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(3, 11) });
                RarityId = 1;
                Level = 2;
            }
            else if(Chance > 85 || Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(20, 35) });
                AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    {DamageTypes.Fire, rand.Next(6,14) },
                }));
                int resist = rand.Next(1, 11);
                AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(6, 14) });
                RarityId = 2;
                Level = 3;
            }
            else if(Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                AddComponent(new ValueCharacteristic { Cost = rand.Next(27, 50) });
                AddComponent(new ElementalDamageCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    {DamageTypes.Fire, rand.Next(14, 22) },
                }));
                int resist = rand.Next(3, 15);
                AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(14, 22) });
                RarityId = 3;
                Level = 4;
            }


            Type = ItemType.Sword;
            
            DropChance = 5f;

            IsStacable = false;
            IsEquapable = true;
            IsEquiped = false;

            SetRarity(RarityId);
        }
    }
}
