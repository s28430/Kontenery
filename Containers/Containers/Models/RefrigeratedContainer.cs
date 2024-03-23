using Containers.Exceptions;
using Containers.Products;

namespace Containers.Models;

public class RefrigeratedContainer
    (int height, double weight, int depth, double maxCapacity, ProductType productType, double temperature)
    : BaseContainer("C", height, weight, depth, maxCapacity)
{
    public List<Product> Products { get; } = [];
    public ProductType PType { get; } = productType;

    private double _innerTemperature = temperature;
    public double InnerTemperature
    {
        get => _innerTemperature;
        
        set
        {
            if (value < PType.StorageTemperature) 
                throw new TooLowTemperatureException("Cannot set the inner temperature to less than " 
                                                     + PType.StorageTemperature);
        }
    }

    public void AddProduct(Product product)
    {
        if (PType != product.Type)
            throw new UnsupportedProductType("Containers storing products of type <"
                                             + PType + "> cannot store products of any other type.");
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
        return base.ToString() + ", product type=" + PType.Name + ")";
    }
}