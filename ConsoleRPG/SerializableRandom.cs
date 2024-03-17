namespace ConsoleRPG
{
    [Serializable]
    internal class SerializableRandom
    {
        private int seed;

        [NonSerialized]
        private Random random;

        public SerializableRandom()
        {
            this.random = new Random();
        }

        public SerializableRandom(int seed)
        {
            this.seed = seed;
            this.random = new Random(seed);
        }

        public int Next()
        {
            return random.Next();
        }
        public int Next(int maxValue)
        {
            return random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }

        // Методы сериализации и десериализации
        //protected SerializableRandom(SerializationInfo info, StreamingContext context) : base(info, context) { }

        //public override void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    base.GetObjectData(info, context);
        //}
    }
}
