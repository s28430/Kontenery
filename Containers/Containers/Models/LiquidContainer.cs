using Containers.Enums;
using Containers.Interfaces;

namespace Containers.Models;

public class LiquidContainer : ContainerBase , IHazardNotifier
{
    public bool StoresDangerous { get; }

    public LiquidContainer
        (double currCargoWeight, int height, double weight, int depth, double maxCapacity, bool storesDangerous) 
        : base("L", currCargoWeight, height, weight, depth, maxCapacity)
    {
        StoresDangerous = storesDangerous;
    }

    public string Notify(DangerCause cause)
    {
        throw new NotImplementedException();
    }

    public override double LoadCargo(double weightToLoad)
    {
        var newWeight = base.LoadCargo(weightToLoad);
        
        if (StoresDangerous && newWeight > MaxCapacity * 0.5)
            Console.WriteLine(Notify(DangerCause.DangerousCargoMoreThan50));
            
        else if (newWeight > MaxCapacity * 0.9)
            Console.WriteLine(Notify(DangerCause.CargoMoreThen90));
        
        else CurrCargoWeight = newWeight;
        
        return CurrCargoWeight;
    }
}