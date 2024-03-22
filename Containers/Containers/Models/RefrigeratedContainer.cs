using Containers.Exceptions;
using Containers.Products;

namespace Containers.Models;

public class RefrigeratedContainer
    (int height, double weight, int depth, double maxCapacity, string productType)
    : BaseContainer("C", height, weight, depth, maxCapacity)
{
    public List<Product> Products { get; } = [];
    public string ProductType { get; } = productType;

    public void AddProduct(Product product)
    {
        if (ProductType != product.Type)
            throw new UnsupportedProductType("Containers storing products of type <"
                                             + ProductType + "> cannot store products of any other type.");
        Products.Add(product);
    }
    
    public override double UnloadCargo()
    {
        throw new NotImplementedException();
    }

    public override double LoadCargo(double weightToLoad)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return base.ToString() + ", product type=" + ProductType + ")";
    }
}