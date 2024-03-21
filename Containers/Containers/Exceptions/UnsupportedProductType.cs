namespace Containers.Exceptions;

public class UnsupportedProductType : Exception
{
    public UnsupportedProductType()
    {}

    public UnsupportedProductType(string? message) : base(message)
    {}
}