namespace Containers.Models;

public class LiquidContainer : ContainerBase
{
    public bool StoresDangerous { get; }

    public LiquidContainer
        (double currCargoWeight, int height, double weight, int depth, double maxCapacity, bool storesDangerous) 
        : base("L", currCargoWeight, height, weight, depth, maxCapacity)
    {
        StoresDangerous = storesDangerous;
    }
}