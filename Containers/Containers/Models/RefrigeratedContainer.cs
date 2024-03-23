using System.Collections.Immutable;
using Containers.Exceptions;
using Containers.Products;

namespace Containers.Models;

public class RefrigeratedContainer
    (int height, double weight, int depth, double maxCapacity, ProductType productType)
    : BaseContainer("C", height, weight, depth, maxCapacity)
{
    public ProductType PType { get; } = productType;

    private double _innerTemperature = productType.StorageTemperature;
    
    private readonly List<Product> _products = [];
    public double InnerTemperature
    {
        get => _innerTemperature;
        
        set
        {
            if (value < PType.StorageTemperature) 
                throw new TooLowTemperatureException("Cannot set the inner temperature to less than " 
                                                     + (PType.StorageTemperature - 0.5));
            _innerTemperature = value;
        }
    }

    public IList<Product> GetProducts()
    {
        return _products.ToImmutableList();
    }
    
    public override double UnloadCargo()
    {
        var weightToUnload = CurrCargoWeight;
        CurrCargoWeight = 0;
        _products.Clear();
   
        return weightToUnload;
    }

    public override double LoadCargo(double weightToLoad, Product? product)
    {
        if (product is null)
        {
            Console.WriteLine("You have to specify a product you are loading to a refrigerated container. " +
                              "Loading failed.");
            return CurrCargoWeight;
        }

        if (PType != product.Type)
        {
            Console.WriteLine("Container " + SerialNumber +
                              " can store only products with type <" + PType + ">. Loading failed");
            return CurrCargoWeight;
        }

        var newWeight = CurrCargoWeight + weightToLoad;
        if (newWeight > MaxCapacity)
            throw new OverfillException(
                "Cargo weight is bigger than the container's capacity. Loading failed.");

        CurrCargoWeight = newWeight;
        _products.Add(product);
        return CurrCargoWeight;
    }

    public override string ToString()
    {
        return base.ToString() + ", product type=" + PType + ")";
    }
}