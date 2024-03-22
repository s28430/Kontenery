using Containers.Models;

namespace Containers.Ship;

public class ContainerShip
{
    private static int _nextId = 1;
    public int Id { get; }
    private List<BaseContainer> _containers;
    public double MaxSpeed { get; }
    public int MaxNumberOfContainers { get; }
    public double MaxWeight { get; }

    public ContainerShip(double maxSpeed, int maxNumberOfContainers, double maxWeight)
    {
        Id = _nextId;
        _nextId++;
        _containers = new List<BaseContainer>();
        MaxSpeed = maxSpeed;
        MaxNumberOfContainers = maxNumberOfContainers;
        MaxWeight = maxWeight;
    }

    public double GetCurrentTotalCargoWeight()
    {
        return _containers.Sum(container => container.CurrCargoWeight + container.Weight);
    }

    private bool CanLoad(BaseContainer container)
    {
        return _containers.Count <= MaxNumberOfContainers
               && GetCurrentTotalCargoWeight() + container.CurrCargoWeight + container.Weight <= MaxWeight;
    }
    
    public bool LoadContainerToShip(BaseContainer container)
    {
        if (!CanLoad(container))
        {
            Console.WriteLine("The ship with id " + Id + 
                              " cannot be loaded with container " + container.SerialNumber + '.');
            return false;
        }

        _containers.Add(container);
        return true;
    }

    public bool LoadContainersToShip(List<BaseContainer> containers)
    {
        foreach(var container in _containers)
            if (!CanLoad(container))
            {
                Console.WriteLine("Loading failed.");
                return false;
            }
        _containers.AddRange(containers);
        return true;
    }

}