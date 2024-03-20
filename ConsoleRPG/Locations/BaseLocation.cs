using ConsoleRPG.NPC_s;
using ConsoleRPG.Enums;

namespace ConsoleRPG.Locations
{
    [Serializable]
    abstract class BaseLocation
	{
		public string Name { get; set; }

		#region Location Effects
		public BaseLocation AddComponent(Characteristics component)
		{
			_components.Add(component);
			return this;
		}

		public T GetComponent<T>()
		{
			return (T)_components.OfType<T>().FirstOrDefault();
		}
		
		private List<Characteristics> _components = new List<Characteristics>();
		#endregion

		public LocationIdentifier locationId;

		public abstract string LocationDesription();

		public abstract string LocationEffects();

		public abstract void EnterLocation(Unit unit);

		public abstract void ExitLocation(Unit unit);

		public abstract void EnterLocation(Player player);

		public abstract void ExitLocation(Player player);

		public abstract void ApplyLocationEffect(Unit unit);

		public abstract void RemoveLocationEffect(Unit unit);

		public abstract Enemy GetRandomLocationEnemy(int playerLevel);

		//public int MinimalRequiredLevel { get; set; }
		//public int MaximumRequiredLevel { get; set; }
	}
}
