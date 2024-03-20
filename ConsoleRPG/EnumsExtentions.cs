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
            T[] allEnums = ArrayEnumValues<T>();
            T randomEnum = allEnums[new SerializableRandom().Next(allEnums.Length)];
            return randomEnum;
        }
    }
}
