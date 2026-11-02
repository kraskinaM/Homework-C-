using System;

namespace ConsoleApp1
{
    public class FeatureMovie : MediaItem
    {
        public string Director { get; set; }
        public string Genre { get; set; }

        public FeatureMovie(string title, int year, int rating, string director, string genre) : base(title, year, rating)
        {
            Director = director;
            Genre = genre;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($", Художественный фильм режиссёр: {Director,-2}, Жанр: {Genre}");
        }
    }
}