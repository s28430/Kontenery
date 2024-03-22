using Containers.Models;

namespace Containers.Ship;

public class ContainerShip
{
    private static int _nextId = 1;
    public int Id { get; }
    private readonly List<BaseContainer> _containers;
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

    public double GetCurrTotalCargoWeight()
    {
        return _containers.Sum(container => container.CurrCargoWeight + container.Weight);
    }

    private bool CanLoad(BaseContainer container)
    {
        return _containers.Count + 1 <= MaxNumberOfContainers
               && GetCurrTotalCargoWeight() + container.CurrCargoWeight + container.Weight <= MaxWeight;
    }
    
    public bool LoadContainerToShip(BaseContainer container)
    {
        if (FindContainer(container.SerialNumber) is not null)
        {
            Console.WriteLine("The container " + container.SerialNumber 
                                               + " is already on the ship with id " + Id + ".");
            return false;
        }
        
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
        for (var i = 0; i < containers.Count; i++)
        {
            if (!LoadContainerToShip(containers[i]))
            {
                Console.WriteLine("Loading failed for the ship " +
                                  Id + ". All previously added containers will be removed");
                for (var j = 0; j < i; j++)
                    RemoveContainerFromShip(containers[j]);
                return false;
            }
        }
        
        return true;
    }

    public int GetCurrNumberOfContainers()
    {
        return _containers.Count;
    }

    public BaseContainer? FindContainer(string serialNumber)
    {
        foreach (var container in _containers)
            if (container.SerialNumber == serialNumber)
                return container;

        return null;
    }

    public bool RemoveContainerFromShip(BaseContainer container)
    {
        return _containers.Remove(container);
    }
    
    public bool RemoveContainerFromShip(string containerSerialNumber)
    {
        var containerToRemove = FindContainer(containerSerialNumber);

        if (containerToRemove is null)
        {
            Console.WriteLine("Container " + containerSerialNumber + " is not on the ship " + Id + ".");
            return false;
        }

        return _containers.Remove(containerToRemove);
    }
    
    public void RemoveAllContainers()
    {
        _containers.Clear();
    }

    public bool SwitchContainer(string firstSerialNumber, BaseContainer second)
    {
        var first = FindContainer(firstSerialNumber);
        
        var removed = RemoveContainerFromShip(firstSerialNumber);
        if (removed && CanLoad(second))
        {
            LoadContainerToShip(second);
            Console.WriteLine("Switched container " + firstSerialNumber + " with " + second.SerialNumber);
            return true;
        }

        if (first != null) LoadContainerToShip(first);
        return false;
    }


    public override string ToString()
    {
        return "Ship " + Id + " (speed=" + MaxSpeed + ", maxContainerNum=" 
               + MaxNumberOfContainers + ", maxWeight=" + MaxWeight 
               + ", currentWeight=" + GetCurrTotalCargoWeight() + ")";
    }
}