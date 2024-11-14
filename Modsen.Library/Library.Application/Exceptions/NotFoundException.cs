namespace Library.Application.Exceptions;
public class NotFoundException(string message = "") : Exception(message);