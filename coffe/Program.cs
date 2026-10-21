using System;

namespace CoffeeTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            CoffeeStorage storage = new CoffeeStorage();
        
            while (true) 
            {
                Console.WriteLine();
                Console.WriteLine("Трекер кофе");
                Console.WriteLine("1.Показать все записи.");
                Console.WriteLine("2.Добавить запись.");
                Console.WriteLine("3.Найти по названию.");
                Console.WriteLine("4.Среднее по кофеину.");
                Console.WriteLine("5.Выход.");
                Console.Write("Выбор:");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            if (storage.Count == 0)
                            {
                                Console.WriteLine("Записей пока нет.");
                                break;
                            }
                            Console.WriteLine($"{"Название"} {"Кофеин,мг"} {"Молоко"}");
                            for (int i = 0; i < storage.Count; i++)
                            {
                                CoffeeRecord r = storage[i];
                                string milkText = r.WithMilk ? "да" : "нет";
                                Console.WriteLine($"{r.Name} {r.Caffeine} {milkText}");
                            }
                            break;

                        case "2":
                            Console.Write("Название напитка:");
                            string name = Console.ReadLine();

                            Console.Write("Кофеин (мг):");
                            string input = Console.ReadLine();
                            int caffeine;
                            if (!int.TryParse(input, out caffeine))
                            {
                                Console.WriteLine("Это не целое число!");
                                break;
                            }

                            Console.Write("С молоком? (да/нет): ");
                            bool milk = Console.ReadLine().ToLower() == "да";
                            
                            CoffeeRecord record = new CoffeeRecord(name, caffeine, milk);
                            storage.Add(record);
                            Console.WriteLine("Запись добавлена.");
                            break;

                        case "3":
                            Console.Write("Что ищем:");
                            string search = Console.ReadLine().ToLower();
                            bool found = false;
                            for (int i = 0; i < storage.Count; i++)
                            {
                                if (storage[i].Name.ToLower().Contains(search))
                                {
                                    Console.WriteLine(storage[i].Name + ", " + storage[i].Caffeine + " мг");
                                    found = true;
                                }
                            }
                            if (!found)
                                Console.WriteLine("Ничего не найдено.");
                            break;

                        case "4":
                            if (storage.Count == 0)
                            {
                                Console.WriteLine("Записей пока нет.");
                                break;
                            }
                            int sum = 0;
                            for (int i = 0; i < storage.Count; i++)
                            {
                                sum = sum + storage[i].Caffeine;
                            }
                            double average = (double)sum / storage.Count;
                            Console.WriteLine($"Средний кофеин: {average:F1} мг");
                            break;

                        case "5":
                            return; 

                        default:
                            Console.WriteLine("Такого пункта нет.");
                            break;
                    }
                }
                catch (BadCoffeeException ex)
                {
                    Console.WriteLine("Ошибка валидации: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Произошла ошибка: " + ex.Message);
                }
            } 
        } 
    } 
    
    class BadCoffeeException : Exception
    {
        public BadCoffeeException(string message) : base(message) { }
    }
    
    class CoffeeRecord
    {
        private string name;
        private int caffeine;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new BadCoffeeException("Название не может быть пустым.");
                name = value;
            }
        }

        public int Caffeine
        {
            get { return caffeine; }
            set
            {
                if (value < 0 || value > 500)
                    throw new BadCoffeeException("Кофеин должен быть от 0 до 500 мг.");
                caffeine = value;
            }
        }

        public bool WithMilk { get; set; }   

        public CoffeeRecord(string name, int caffeine, bool withMilk)
        {
            Name = name;
            Caffeine = caffeine;
            WithMilk = withMilk;
        }
    }
    
    class CoffeeStorage
    {
        private CoffeeRecord[] records;
        private int count;

        public int Count
        {
            get { return count; }
        }

        public CoffeeStorage()
        {
            records = new CoffeeRecord[4];
            count = 0;
        }

        public CoffeeRecord this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Нет записи с таким номером.");
                return records[index];
            }
        }

        public void Add(CoffeeRecord record)
        {
            if (count == records.Length)
            {
                CoffeeRecord[] bigger = new CoffeeRecord[records.Length * 2];
                for (int i = 0; i < records.Length; i++)
                {
                    bigger[i] = records[i];
                }
                records = bigger;
            }
            records[count] = record;
            count++;
        }

        public static CoffeeStorage operator +(CoffeeStorage a, CoffeeStorage b)
        {
            CoffeeStorage result = new CoffeeStorage();
            for (int i = 0; i < a.Count; i++)
            {
                result.Add(a[i]);
            }
            for (int i = 0; i < b.Count; i++)
            {
                result.Add(b[i]);
            }
            return result;
        }
    } 
}