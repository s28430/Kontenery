namespace Containers.Products;

public class Product(ProductType type, string name)
{
    public string Name { get; } = name;
    public ProductType Type { get; } = type;
}