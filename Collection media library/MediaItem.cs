using System;

namespace ConsoleApp1
{
    public class MediaItem
    {
        private string _title;
        private int _rating;

        public int Year { get; set; }

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new InvalidMediaItemException("Прикинь, название фильма не может быть пустым.");
                }
                _title = value;
            }
        }

        public int Rating
        {
            get => _rating;
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new InvalidMediaItemException("Оценка фильма должна быть 10/10.");
                }
                _rating = value;
            }
        }

        public MediaItem(string title, int year, int rating)
        {
            Title = title;
            Year = year;
            Rating = rating;
        }

        public virtual void Describe()
        {
            Console.Write($"Название: {Title,-2}, Год: {Year}, Оценка: {Rating,-2}/10");
        }

        public override string ToString()
        {
            return $"{Title} ({Year}) — {Rating}/10";
        }
    }
}