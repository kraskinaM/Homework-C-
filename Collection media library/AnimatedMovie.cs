using System;

namespace ConsoleApp1
{
    public class AnimatedMovie : MediaItem
    {
        public string AnimationStudio { get; set; } 
        public string AnimationType { get; set; }  

        public AnimatedMovie(string title, int year, int rating, string studio, string animationType) : base(title, year, rating)
        {
            AnimationStudio = studio;
            AnimationType = animationType;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine($", Мультфильм студия: {AnimationStudio,-2}, Тип: {AnimationType}");
        }
    }
}