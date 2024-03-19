using Containers.Enums;
using Containers.Interfaces;

namespace Containers.Models;

public class LiquidContainer
    (int height,
    double weight,
    int depth,
    double maxCapacity,
    bool storesDangerous)
    : ContainerBase("L", height, weight, depth, maxCapacity), IHazardNotifier
{
    public bool StoresDangerous { get; } = storesDangerous;

    public string Notify(DangerCause cause)
    {
        var msg = cause == DangerCause.DangerousCargoMoreThan50 ? 
            "Containers which store dangerous cargo cannot be filled with more than 50% of their capacity." :
            "Containers cannot be filled with more than 90% of their capacity.";
        
        return msg + " Loading failed.";
    }

    public override double UnloadCargo()
    {
        var weightToUnload = base.UnloadCargo();
        CurrCargoWeight = 0;
        return weightToUnload;
    }

    public override double LoadCargo(double weightToLoad)
    {
        var newWeight = base.LoadCargo(weightToLoad);
        
        if (StoresDangerous && newWeight > MaxCapacity * 0.5)
            Console.WriteLine(Notify(DangerCause.DangerousCargoMoreThan50));
            
        else if (newWeight > MaxCapacity * 0.9)
            Console.WriteLine(Notify(DangerCause.CargoMoreThen90));
        
        else CurrCargoWeight = newWeight;
        
        // current cargo weight stays the same if dangerous situation occurred
        return CurrCargoWeight;
    }
}