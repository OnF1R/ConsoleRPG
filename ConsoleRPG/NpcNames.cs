namespace ConsoleRPG
{
	internal class NpcNames
	{
		public static readonly List<string> Names = new List<string>()
		{
			"Мартех",
			"Синви",
			"Бульбех",
			"Арволь колдун",
			"Зущ",
			"Хлиб",
			"Курбек",
			"Хие",
			"Аликсандрик",
			"Хъ Су Ваб",
			"Орбу",
			"Микрик",
			"Чум, бродяга",
			"Бик, богач",
			"Смуг",
			"Шиншиг",
			"Уфуву",
			"Йор из банды воров",
			"Муха",
			"Кириешка",
			"Большой бук",
			"Жим-лё-жа",
			"Сом водный",
			"Папамам",
			"Куклик младший",
			"Рипстоп",
			"Белоглаз",
			"Червь",
			"Хэш",
			"Микро Фася",
		};

		public static string GetRandomName()
		{
			var random = new Random();

			return Names[random.Next(Names.Count)];
		}
	}
}
