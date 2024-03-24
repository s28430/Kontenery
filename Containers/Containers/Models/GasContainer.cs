using Containers.Enums;
using Containers.Exceptions;
using Containers.Interfaces;
using Containers.Products;

namespace Containers.Models;

public class GasContainer(int height, double weight, int depth, double maxCapacity, double pressure) :
    BaseContainer("G", height, weight, depth, maxCapacity), IHazardNotifier
{
    public double Pressure { get; } = pressure; // atm

    public override double UnloadCargo()
    {
        var weightToUnload = CurrCargoWeight * 0.95;
        CurrCargoWeight -= weightToUnload;
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
        if (newWeight > MaxCapacity * 0.9)
            Console.WriteLine(Notify(DangerCause.CargoMoreThen90));
        else CurrCargoWeight = newWeight;
        
        return newWeight;
    }

    public string Notify(DangerCause cause)
    {
        var msg = "Loading failed for container " + SerialNumber + ":\n";
        msg += cause == DangerCause.DangerousCargoMoreThan50 ? 
            "Containers which store dangerous cargo cannot be filled with more than 50% of their capacity." :
            "Containers cannot be filled with more than 90% of their capacity.";
        return msg;
    }

    public override string ToString()
    {
        return base.ToString() + ", pressure=" + Pressure + ")";
    }
}