namespace Containers.Products;

public class Product(string type, double storageTemperature)
{
    public string Type { get; } = type;
    
    public double StorageTemperature { get; } = storageTemperature;
}