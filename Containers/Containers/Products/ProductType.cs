namespace Containers.Products;

public class ProductType(string name, double storageTemperature)
{
    public string Name { get; } = name;
    public double StorageTemperature { get; } = storageTemperature;
}