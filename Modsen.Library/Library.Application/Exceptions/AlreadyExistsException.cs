namespace Library.Application.Exceptions;
public class AlreadyExistsException(string message = "") : Exception(message);