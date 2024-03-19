using Containers.Exceptions;

namespace Containers.Models;

public abstract class ContainerBase(
    string type,
    int height,
    double weight,
    int depth,
    double maxCapacity)
{
    private static int _nextId = 1;
    public double CurrCargoWeight { get; protected set; }   // 0
    public int Height { get; } = height; // cm
    public double Weight { get; } = weight; // kg
    public int Depth { get; } = depth; // cm
    public string SerialNumber { get; } = GenerateSerialNumber(type);
    public double MaxCapacity { get; } = maxCapacity;

    private static string GenerateSerialNumber(string type)
    {
        var serialNumber = "KON-" + type + "-" + _nextId;
        _nextId++;
        return serialNumber;
    }
    
    // returns a double indicating how much weight has been unloaded
    public virtual double UnloadCargo()
    {
        return CurrCargoWeight;
    }

    // returns a double indicating how much weight has been loaded
    public virtual double LoadCargo(double weightToLoad)
    {
        var newWeight = CurrCargoWeight + weightToLoad;
        if (newWeight > MaxCapacity) 
            throw new OverfillException("Cargo weight is bigger than the container's capacity. Loading failed.");
        
        return newWeight;
    }
}