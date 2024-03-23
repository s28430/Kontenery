namespace Containers.Exceptions;

public class TooLowTemperatureException : Exception
{
    public TooLowTemperatureException()
    {
    }

    public TooLowTemperatureException(string? message) : base(message)
    {
    }
}