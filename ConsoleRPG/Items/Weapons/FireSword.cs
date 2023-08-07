﻿
using ConsoleRPG.Effects;
using ConsoleRPG.Effects.Debuffs;

namespace ConsoleRPG.Items.Weapons
{
    internal class FireSword : Weapon
    {
        public FireSword(int level) : base(level)
        {
            Random rand = new Random();
            Quality Quality = new Quality();
            Name = "[orangered1]Огненный[/] меч";
            int Chance = rand.Next(1, 101);

            AddComponent(new PhysicalDamageCharacteristic { PhysicalDamage = rand.Next(Level, Level + Level) });
            AddComponent(new ValueCharacteristic { Cost = rand.Next(Level, Level + Level) });

            ID = ItemIdentifier.FireSword;

            if (Chance <= 5)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                RarityId = 0;
            }
            else if (Chance <= 10)
            {
                string QualityName = Quality.GetBadQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(-12, 2);
                if (resist != 0) AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                RarityId = 0;
            }
            else if (Chance <= 85)
            {
                int resist = rand.Next(-6, 6);
                if (resist != 0)
                    AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + 2);
                AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
                {
                    { new Burn(), 5 },
                }));
                RarityId = 1;
            }
            else if (Chance > 85 && Chance != 100)
            {
                string QualityName = Quality.GetGoodQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(1, 11);
                AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
                {
                    { new Burn(), 10 },
                }));
                RarityId = 2;
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level / 2);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level / 2);
            }
            else if (Chance == 100)
            {
                string QualityName = Quality.GetBestQuality();
                Name = QualityName + " " + Name;
                int resist = rand.Next(3, 15);
                AddComponent(new ElementalResistanceCharacteristic(new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Fire, resist },
                }));
                GetComponent<PhysicalDamageCharacteristic>().PhysicalDamage += rand.Next(Level, Level + Level);
                GetComponent<ValueCharacteristic>().Cost += rand.Next(Level, Level + Level);
                AddComponent(new StatusEffectsCharacteristic(new Dictionary<BaseEffect, double>()
                {
                    { new Burn(), 15 },
                }));
                RarityId = 3;
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
