namespace Containers.Exceptions;

public class OverfillException : Exception
{
    public OverfillException(string? message) : base(message)
    {
    }
}