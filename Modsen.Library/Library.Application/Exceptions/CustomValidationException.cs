namespace Library.Application.Exceptions
{
    public class CustomValidationException : Exception
    {
        public CustomValidationException() : base() { }
        public CustomValidationException(string message) : base(message) { }
        public CustomValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
