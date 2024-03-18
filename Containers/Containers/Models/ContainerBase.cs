using Containers.Exceptions;

namespace Containers.Models;

public abstract class ContainerBase
{
    private static int _nextId = 1;
    public double CurrCargoWeight { get; protected set; }
    public int Height { get; }      // cm
    public double Weight { get; }   // kg
    public int Depth { get; }       // cm
    public string SerialNumber { get; }
    public double MaxCapacity { get; }

    protected ContainerBase(string type, double currCargoWeight, int height,
        double weight, int depth, double maxCapacity)
    {
        CurrCargoWeight = currCargoWeight;
        Height = height;
        Weight = weight;
        Depth = depth;
        SerialNumber = GenerateSerialNumber(type);
        MaxCapacity = maxCapacity;
    }

    private static string GenerateSerialNumber(string type)
    {
        var serialNumber = "KON-" + type + "-" + _nextId;
        _nextId++;
        return serialNumber;
    }

    public double UnloadCargo()
    {
        var weightToUnload = CurrCargoWeight;
        CurrCargoWeight = 0;
        return weightToUnload;
    }

    public double LoadCargo(double weightToLoad)
    {
        double newWeight = CurrCargoWeight + weightToLoad;
        if (newWeight > MaxCapacity) throw new OverfillException("Masa ładunku jest za duża.");
        CurrCargoWeight = newWeight;
        return newWeight;
    }
}