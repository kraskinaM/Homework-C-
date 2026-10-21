using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, bool> books = new Dictionary<string, bool>();
            books.Add("Halloween Kills", false);
            books.Add("A Nightmare on Elm Street", false);
            books.Add("The Conjuring", false);
            
            List<int> PagesOfTheFirstBook = new List<int> {69};
            List<int> PagesOfTheSecondBook = new List<int> {52};
            List<int> PagesOfTheThirdBook = new List<int> {42};
            
            List<double> RatingOfTheFirstBook = new List<double> {4.7};
            List<double> RatingOfTheSecondBook = new List<double> {3.8};
            List<double> RatingOfTheThirdBook = new List<double> {4.4};
            
            bool isRead = true;
            
            var bookNamesList = books.Keys.ToList();

            if (isRead)
            {
                Console.WriteLine($"Книга 1: {bookNamesList[0]}, Страниц: {PagesOfTheFirstBook[0]}, Рейтинг: {RatingOfTheFirstBook[0]}");
                Console.WriteLine($"Книга 2: {bookNamesList[1]}, Страниц: {PagesOfTheSecondBook[0]}, Рейтинг: {RatingOfTheSecondBook[0]}");
                Console.WriteLine($"Книга 3: {bookNamesList[2]}, Страниц: {PagesOfTheThirdBook[0]}, Рейтинг: {RatingOfTheThirdBook[0]}");
            }
            
            while (true)
            { 
                Console.WriteLine(); 
                Console.WriteLine("Меню:"); 
                Console.WriteLine("1.Выставить все книги."); 
                Console.WriteLine("2.Найти книгу с самым лучшим рейтингом."); 
                Console.WriteLine("3.Показать прочитанные книги."); 
                Console.WriteLine("4.Отметить как прочитанное/непрочитанное."); 
                Console.WriteLine("5.Добавить свою книгу."); 
                Console.WriteLine("6.Поиск книги по названию.");
                Console.WriteLine("7.Фильтрация книг."); 
                Console.WriteLine("8.Показать топ-3 лучших книг."); 
                Console.WriteLine("9.Показать статистику."); 
                Console.WriteLine("10.Ливнуть."); 
                Console.WriteLine(); 
                Console.Write("Выбери пункт:");
               
                string menuChoice = Console.ReadLine();

                switch(menuChoice)
                {
                    case "1": 
                        Console.WriteLine("Все книги в библиотеке:");
                        foreach (var book in books.Keys)
                        {
                            Console.WriteLine($"- {book}");
                        }
                        break;
                   
                    case "2":
                        if (bookNamesList.Count < 3 || RatingOfTheFirstBook.Count == 0 || RatingOfTheSecondBook.Count == 0 || RatingOfTheThirdBook.Count == 0)
                        {
                            Console.WriteLine("Недостаточно данных для поиска лучшей книги.");
                            break;
                        }

                        var bestBook = new List<KeyValuePair<string, double>>
                        {
                            new KeyValuePair<string, double>(bookNamesList[0], RatingOfTheFirstBook[0]),
                            new KeyValuePair<string, double>(bookNamesList[1], RatingOfTheSecondBook[0]),
                            new KeyValuePair<string, double>(bookNamesList[2], RatingOfTheThirdBook[0])
                        }
                        .OrderByDescending(x => x.Value)
                        .First();

                        Console.WriteLine($"Книга с самым лучшим рейтингом: {bestBook.Value}, Название: {bestBook.Key}");
                        break;
                   
                    case "3":
                        Console.WriteLine("Прочитанные книги:");
                        bool anyRead = false;
                       
                        foreach (var book in books)
                        {
                            if (book.Value) 
                            {
                                Console.WriteLine($"- {book.Key}");
                                anyRead = true;
                            }
                        }
                        if (!anyRead)
                        {
                            Console.WriteLine("У вас пока нет прочитанных книг.");
                        }
                        break;
                       
                    case "4":
                        Console.WriteLine("Выбери книги, которые отметить как прочитанные/непрочитанные:");
                       
                        var currentBooks = books.Keys.ToList();
                       
                        for (int i = 0; i < currentBooks.Count; i++)
                        {
                            string status = books[currentBooks[i]] ? "[Прочитана]" : "[Не прочитана]";
                            Console.WriteLine($"{i + 1}. {currentBooks[i]} {status}");
                        }
                        Console.Write("Введите номер книги для переключения статуса: ");
                        if (int.TryParse(Console.ReadLine(), out int bookChoice) && bookChoice > 0 && bookChoice <= currentBooks.Count)
                        {
                            string selectedBook = currentBooks[bookChoice - 1];
                           
                            books[selectedBook] = !books[selectedBook]; 
        
                            string newStatus = books[selectedBook] ? "прочитанной" : "непрочитанной";
                            Console.WriteLine($"Статус книги \"{selectedBook}\" изменен на: {newStatus}!");
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод. Переключение отменено.");
                        }
                        break;
                   
                    case "5":
                        Console.WriteLine("Добавление новой книги!");
                        Console.Write("Введите название книги:");
                        string title = Console.ReadLine();
                       
                        string fullBookName = title;

                        if (string.IsNullOrWhiteSpace(fullBookName))
                        {
                            Console.WriteLine("Ошибка: название не должно быть пустым.");
                            break;
                        }

                        if (books.ContainsKey(fullBookName))
                        {
                            Console.WriteLine("Такая книга уже есть.");
                            break;
                        }
                       
                        books.Add(fullBookName, false); 
    
                        Console.WriteLine("Книга успешно добавлена!");
                        break;
                   
                    case "6":
                        Console.Clear();
                        Console.WriteLine("Поиск книги по названию");
                        Console.Write("Введите название книги для поиска: ");
                        string searchTitle = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(searchTitle))
                        {
                            Console.WriteLine("Ошибка: запрос не должен быть пустым.");
                        }
                        else
                        {
                            var foundBooks = books.Keys
                                .Where(b => b.IndexOf(searchTitle, StringComparison.OrdinalIgnoreCase) >= 0)
                                .ToList();

                            if (foundBooks.Count > 0)
                            {
                                Console.WriteLine($"Найдено книг: {foundBooks.Count}");
                                foreach (var book in foundBooks)
                                {
                                    Console.WriteLine($"- {book}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Книги с таким названием не найдены.");
                            }
                        }

                        Console.WriteLine("Нажмите на любую клавишу для продолжения.");
                        Console.ReadKey();
                        break;
                   
                    case "7":
                        Console.Clear();
                        Console.WriteLine("1.Показать только прочитанные.");
                        Console.WriteLine("2.Показать только непрочитанные");
    
                        string filterChoice = Console.ReadLine();
                        Console.WriteLine();

                        if (filterChoice == "1")
                        {
                            Console.WriteLine("Прочитанные книги:");
                            bool hasRead = false;
                            foreach (var book in books)
                            {
                                if (book.Value)
                                {
                                    Console.WriteLine($"- {book.Key}");
                                    hasRead = true;
                                }
                            }
                            if (!hasRead) Console.WriteLine("У вас пока нет прочитанных книг.");
                        }
                        else if (filterChoice == "2")
                        {
                            Console.WriteLine("Непрочитанные книги:");
                            bool hasUnread = false;
                            foreach (var book in books)
                            {
                                if (!book.Value) 
                                {
                                    Console.WriteLine($"- {book.Key}");
                                    hasUnread = true;
                                }
                            }
                            if (!hasUnread) Console.WriteLine("Все книги из списка уже прочитанны.");
                        }
                        else
                        {
                            Console.WriteLine("Неверный ввод. Возврат в главное меню.");
                        }

                        Console.WriteLine("Нажмите любую клавишу для продолжения...");
                        Console.ReadKey();
                        break;
                    
                    case "8":
                        if (bookNamesList.Count < 3 || RatingOfTheFirstBook.Count == 0 || RatingOfTheSecondBook.Count == 0 || RatingOfTheThirdBook.Count == 0)
                        {
                            Console.WriteLine("Недостаточно данных для топ-3.");
                            break;
                        }

                        var topBooks = new List<KeyValuePair<string, double>>
                        {
                            new KeyValuePair<string, double>(bookNamesList[0], RatingOfTheFirstBook[0]),
                            new KeyValuePair<string, double>(bookNamesList[1], RatingOfTheSecondBook[0]),
                            new KeyValuePair<string, double>(bookNamesList[2], RatingOfTheThirdBook[0])
                        }
                        .OrderByDescending(x => x.Value)
                        .Take(3);

                        Console.WriteLine("Топ 3 лучших книг:");
                        foreach (var book in topBooks)
                        {
                            Console.WriteLine($"{book.Key}");
                        }
                        break;
                    
                    case "9":
                        if (books.Count == 0)
                        {
                            Console.WriteLine("В библиотеке пока нет книг.");
                            break;
                        }

                        if (PagesOfTheFirstBook.Count == 0 || PagesOfTheSecondBook.Count == 0 || PagesOfTheThirdBook.Count == 0 ||
                            RatingOfTheFirstBook.Count == 0 || RatingOfTheSecondBook.Count == 0 || RatingOfTheThirdBook.Count == 0)
                        {
                            Console.WriteLine("Недостаточно данных для статистики.");
                            break;
                        }

                        Console.WriteLine("Статистика:");
                        
                        var allPages = new List<int> { PagesOfTheFirstBook[0], PagesOfTheSecondBook[0], PagesOfTheThirdBook[0] };
                        var allRatings = new List<double> { RatingOfTheFirstBook[0], RatingOfTheSecondBook[0], RatingOfTheThirdBook[0] };
                        
                        int totalPages = allPages.Sum();
                        double averageRating = allRatings.Average();
                        
                        int readCount = books.Count(b => b.Value); 
                        int unreadCount = books.Count(b => !b.Value);
                        double readPercentage = ((double)readCount / books.Count) * 100;
                        
                        double readingTimeHours = (totalPages * 1.5) / 60;
                        
                        string rank = totalPages switch
                        {
                            < 50 => "Начинающий книголюб",
                            < 150 => "Опытный читатель",
                            _ => "Настоящий Книжный Червь"
                        };
                        
                        Console.WriteLine("Прогресс");
                        Console.WriteLine($"Прочитано книг: {readCount} из {books.Count} ({readPercentage:F0}%)");
                        Console.WriteLine($"Книг в очереди на чтение: {unreadCount}");
    
                        Console.WriteLine("Объемы");
                        Console.WriteLine($"Общее кол-во страниц: {totalPages} стр.");
                        Console.WriteLine($"Будет потрачено на чтение: ~{readingTimeHours:F1} ч.");
    
                        Console.WriteLine("Оценки и достижения");
                        Console.WriteLine($"Средний рейтинг книг: {averageRating:F2} / 5");
                        Console.WriteLine($"Ваш текущий ранг: {rank}");
                        break;

                    case "10":
                        Console.WriteLine("Пока!");
                        return; 

                    default:
                        Console.WriteLine("Выбран невернный пункт.");
                        break;
                }
            } 
        }
    }
}