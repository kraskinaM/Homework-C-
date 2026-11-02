namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MediaItem> collection = new List<MediaItem>
            {
                new FeatureMovie(title: "Лох",year: 2014,rating: 10,director: "К.Нолан",genre: "Фантастика"),
                new DocumentaryMovie(title: "Даун",year: 2019,rating: 10,investigator: "Д.Аттенборо",topic: "Природа"),
                new AnimatedMovie(title: "Кукумбер",year: 1994,rating: 9,studio: "Disney",animationType: "2D")
            };
            
            while (true)
            {
                try
                {
                    Console.WriteLine("Меню:");
                    Console.WriteLine("1.Показать все.");
                    Console.WriteLine("2.Добавить фильм.");
                    Console.WriteLine("3.Найти по названию.");
                    Console.WriteLine("4.Средняя оценка.");
                    Console.WriteLine("5.Выход.");
                    Console.Write("Выбор:");

                    string choice = Console.ReadLine();

                    if (choice == "1")
                    {
                        foreach (var item in collection)
                        {
                            item.Describe();
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Название:");
                        string title = Console.ReadLine();

                        Console.Write("Год:");
                        int.TryParse(Console.ReadLine(), out int year);

                        Console.Write("Оценка:");
                        int.TryParse(Console.ReadLine(), out int rating);

                        MediaItem movie = new MediaItem(title, year, rating);
                        collection.Add(movie);
                        Console.WriteLine("Добавлено.");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Что искать:");
                        string search = Console.ReadLine();

                        foreach (var item in collection)
                        {
                            if (item.Title.Contains(search, StringComparison.OrdinalIgnoreCase))
                            {
                                item.Describe();
                            }
                        }
                    }
                    else if (choice == "4")
                    {
                        if (collection.Count == 0)
                        {
                            Console.WriteLine("Коллекция пуста.");
                            continue;
                        }

                        double sum = 0;
                        foreach (var item in collection)
                        {
                            sum += (double)item.Rating;
                        }

                        double average = sum / collection.Count;
                        Console.WriteLine("Средняя оценка:" + average);
                    }
                    else if (choice == "5")
                    {
                        break;
                    }
                }
                catch (InvalidMediaItemException ex)
                {
                    Console.WriteLine("Ошибка:" + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Системная ошибка:" + ex.Message);
                }
            }
        }
    }
}