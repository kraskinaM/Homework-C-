using System;

namespace ConsoleApp1
{
    public class Movie : MediaItem
    {
        public string Director { get; set; }
        public int DurationMinutes { get; set; }

        public Movie(string title, int year, int rating, string director, int durationMinutes) : base(title, year, rating)
        {
            Director = director;
            DurationMinutes = durationMinutes;
        }
        public override void Describe()
        {
            base.Describe(); 
            Console.WriteLine($", Режиссёр: {Director,-15} Длительность: {DurationMinutes} мин.");
        }
    }
}