namespace DogCeoService.UserException
{
    public class DogCeoException : Exception
    {
        public DogCeoException()
        {
        }

        public DogCeoException(string message)
            : base(message)
        {
        }

        public DogCeoException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
