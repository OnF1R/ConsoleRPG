namespace ConsoleRPG
{
    internal class EnumsExtentions
    {
        public static T[] ArrayEnumValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToArray();
        }

        public static T RandomsEnumValue<T>()
        {
            T[] AllDamageTypes = ArrayEnumValues<T>();
            T DamageType = AllDamageTypes[new SerializableRandom().Next(AllDamageTypes.Length)];
            return DamageType;
        }
    }
}
