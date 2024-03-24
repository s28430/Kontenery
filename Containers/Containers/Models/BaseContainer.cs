using Containers.Products;

namespace Containers.Models;

public abstract class BaseContainer(
    string type,
    int height,
    double weight,
    int depth,
    double maxCapacity)
{
    private static int _nextId = 1;
    
    public double CurrCargoWeight { get; protected set; }   // kg
    public int Height { get; } = height; // cm
    public double Weight { get; } = weight; // kg
    public int Depth { get; } = depth; // cm
    public string SerialNumber { get; } = GenerateSerialNumber(type);
    public double MaxCapacity { get; } = maxCapacity;   // kg

    public bool IsOnShip { get; set; } = false;

    private static string GenerateSerialNumber(string type)
    {
        var serialNumber = "KON-" + type + "-" + _nextId;
        _nextId++;
        return serialNumber;
    }
    
    // returns a double indicating how much weight has been unloaded
    public abstract double UnloadCargo();

    // returns a new weight of the container
    public abstract double LoadCargo(double weightToLoad, Product? product);

    public override string ToString()
    {
        return "Container " + SerialNumber + " (max capacity=" + MaxCapacity 
               + ", own weight=" + Weight + ", curr cargo weight=" + CurrCargoWeight;
    }
    
    
}