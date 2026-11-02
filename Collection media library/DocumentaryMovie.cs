using System;

namespace ConsoleApp1
{
    public class DocumentaryMovie : MediaItem
    {
        public string Investigator { get; set; } 
        public string Topic { get; set; }       

        public DocumentaryMovie(string title, int year, int rating, string investigator, string topic) : base(title, year, rating)
        {
            Investigator = investigator;
            Topic = topic;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($", Документальный фильм ведущий: {Investigator,-2}, Тема: {Topic}");
        }
    }
}