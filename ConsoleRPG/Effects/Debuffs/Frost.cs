namespace ConsoleRPG.Effects.Debuffs
{
    internal class Frost : BaseEffect
    {
        private int BaseDamage;
        private int FireResistanceBuff;
        private int WaterResistanceDebuff;
        private int FrostResistanceDebuff;
        private Dictionary<DamageTypes, string> Names;

        public Frost(int duration = 2)
        {
            Name = "[aqua]Обледенение[/]";
            Duration = duration;
            CurrentDuration = Duration;
            EffectType = EffectType.Debuff;

            Names = new DamageTypesNames().Names;
            AddComponent(new ElementalDamageCharacteristic
            {
                ElemDamage = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Frost, 2},
                    }
            });

            AddComponent(new ElementalResistanceCharacteristic
            {
                ElemResistance = new Dictionary<DamageTypes, int>()
                    {
                        { DamageTypes.Fire, 20},
                        { DamageTypes.Water, -5},
                        { DamageTypes.Frost, -10},
                    }
            });

            BaseDamage = GetComponent<ElementalDamageCharacteristic>().ElemDamage.FirstOrDefault(x => x.Key == DamageTypes.Fire).Value;
            FireResistanceBuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Fire).Value;
            WaterResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Water).Value;
            FrostResistanceDebuff = GetComponent<ElementalResistanceCharacteristic>().ElemResistance.FirstOrDefault(x => x.Key == DamageTypes.Frost).Value;
        }

        protected override void StartEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] += FireResistanceBuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] += WaterResistanceDebuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Frost] += FrostResistanceDebuff;
        }

        public override void ApplyEffect(Unit unit)
        {
            if (Duration == CurrentDuration)
                StartEffect(unit);

            CurrentDuration--;

            unit.DealDamage(unit, BaseDamage, DamageTypes.Frost, this);

            unit.ShowMessage(EffectDurationMessage());

            if (CurrentDuration <= 0)
                EndEffect(unit);
        }



        protected override void EndEffect(Unit unit)
        {
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Fire] -= FireResistanceBuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Water] -= WaterResistanceDebuff;
            unit.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Frost] -= FrostResistanceDebuff;
        }

        public override string DescriptionEffect()
        {
            string message = $"{Name}: Наносит {BaseDamage} урон от ({Names[DamageTypes.Frost]}) + элементальный урон от ({Names[DamageTypes.Frost]}) персонажа" +
                $", также уменьнает сопротивление ({Names[DamageTypes.Frost]}) на {FrostResistanceDebuff}% и ({Names[DamageTypes.Water]}) на {WaterResistanceDebuff}%, увеличивает сопротивление ({Names[DamageTypes.Fire]}) на {FireResistanceBuff}%";
            return message;
        }

        private string EffectMessage(Unit unit, int damage)
        {
            string message = "";
            if (damage > 0)
            {
                message += $"{unit.Name} получил {damage} урона ({Names[DamageTypes.Frost]}) от эффекта {Name} ";
            }
            else
            {
                message += $"{unit.Name} исцелился на {damage} от эффекта {Name} ";
            }

            if (unit.IsDie())
            {
                message += "и [red]умер[/]";
            }
            else
            {
                message += $", его здоровье [lime]{unit.CurrentHealth}[/]";

                if (Duration > 0)
                {
                    message += $", осталось ходов: {Duration}";
                }
                else
                {
                    message += ", последний ход!";
                }
            }

            return message;
        }
    }
}
