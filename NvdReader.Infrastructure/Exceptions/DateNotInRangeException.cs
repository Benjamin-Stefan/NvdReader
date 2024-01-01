namespace NvdReader.Infrastructure.Exceptions;

[Serializable]
public class DateNotInRangeException : Exception
{
    public DateNotInRangeException()
    {
    }
    
    public DateNotInRangeException(string? message) : base(message)
    {
    }

    public DateNotInRangeException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}