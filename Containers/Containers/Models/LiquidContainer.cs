using Containers.Enums;
using Containers.Exceptions;
using Containers.Interfaces;
using Containers.Products;

namespace Containers.Models;

public class LiquidContainer
    (int height,
    double weight,
    int depth,
    double maxCapacity,
    bool storesDangerous)
    : BaseContainer("L", height, weight, depth, maxCapacity), IHazardNotifier
{
    public bool StoresDangerous { get; } = storesDangerous;

    public string Notify(DangerCause cause)
    {
        var msg = "Loading failed for container " + SerialNumber + ":\n";
        msg += cause == DangerCause.DangerousCargoMoreThan50 ? 
            "Containers which store dangerous cargo cannot be filled with more than 50% of their capacity." :
            "Containers cannot be filled with more than 90% of their capacity.";
        
        return msg;
    }

    public override double UnloadCargo()
    {
        var weightToUnload = CurrCargoWeight;
        CurrCargoWeight = 0;
        return weightToUnload;
    }

    public double LoadCargo(double weightToLoad)
    {
        return LoadCargo(weightToLoad, null);
    }

    public override double LoadCargo(double weightToLoad, Product? product)
    {
        var newWeight = CurrCargoWeight + weightToLoad;
        if (newWeight > MaxCapacity) 
            throw new OverfillException("Cargo weight is bigger than the container's capacity. Loading failed.");
        
        if (StoresDangerous && newWeight > MaxCapacity * 0.5)
            Console.WriteLine(Notify(DangerCause.DangerousCargoMoreThan50));
            
        else if (newWeight > MaxCapacity * 0.9)
            Console.WriteLine(Notify(DangerCause.CargoMoreThen90));
        
        else CurrCargoWeight = newWeight;
        
        // current cargo weight stays the same if dangerous situation occurred
        return CurrCargoWeight;
    }

    public override string ToString()
    {
        return base.ToString() + ", stores dangerous=" + StoresDangerous + ")";
    }
}