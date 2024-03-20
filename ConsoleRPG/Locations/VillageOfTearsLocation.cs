using ConsoleRPG.Enums;

namespace ConsoleRPG.Locations
{
    [Serializable]
    internal class VillageOfTearsLocation : BaseLocation
    {
        public VillageOfTearsLocation()
        {
            Name = "[steelblue1]Деревня слёз[/]";
            locationId = LocationIdentifier.VillageOfTears;
            AddComponent(new ElementalResistanceCharacteristic()
            {
                ElemResistance = new Dictionary<DamageTypes, int>()
                {
                    { DamageTypes.Nature, -35 },
                }
            });
        }

        public override Enemy GetRandomLocationEnemy(int playerLevel)
        {
            return ExistableEnemies.GetRandomEnemy(locationId, playerLevel);
        }
        public override void ApplyLocationEffect(Unit unit) { }

        public override void RemoveLocationEffect(Unit unit) { }

        public void ApplyLocationEffect(Player player)
        {
            player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature] +=
                GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature];
        }

        public void RemoveLocationEffect(Player player)
        {
            player.GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature] -=
                GetComponent<ElementalResistanceCharacteristic>().ElemResistance[DamageTypes.Nature];
        }

        public override void EnterLocation(Unit unit)
        {
            ApplyLocationEffect(unit);
        }

        public override void ExitLocation(Unit unit)
        {
            RemoveLocationEffect(unit);
        }

        public override void EnterLocation(Player player)
        {
            //ConsoleMessages.Message(LocationDesription());
            player.ShowMessage(LocationEffects());
            ApplyLocationEffect(player);
            player.ShowMessage($"{player.Name} прибыл в локацию [bold]{Name}[/]");
        }

        public override void ExitLocation(Player player)
        {
            RemoveLocationEffect(player);
            player.ShowMessage($"{player.Name} покинул локацию {Name}.");
        }

        public override string LocationDesription()
        {
            string description = "";
            return description;
        }

        public override string LocationEffects()
        {
            string effects = $"[bold]Деревня сокрытая в глубине леса, всего пара разрушенных домиков, но почему холод пробивает до самых костей?[/] \n" +
                $"Прибывая в [steelblue1]'Деревне Слёз'[/] игрок получает -35% к сопротивлению [{new DamageTypesNames().Color[DamageTypes.Nature]}]природе[/].";
            return effects;
        }
    }
}
