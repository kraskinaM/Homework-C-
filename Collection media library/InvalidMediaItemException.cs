namespace ConsoleApp1
{
    public class InvalidMediaItemException : Exception
    {
        public InvalidMediaItemException(string message) : base(message) { }
    }
}