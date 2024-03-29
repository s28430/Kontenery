using Containers.Models;
using Containers.Products;
using Containers.Ship;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.

namespace Containers.Interface;

public class Cli
{
    private readonly List<BaseContainer> _containers;
    private readonly List<ContainerShip> _ships;
    private readonly List<ProductType> _productTypes;

    private readonly string _iterSeparator;

    private readonly Dictionary<string, bool> _actionsAvailability;

    public Cli()
    {
        _containers = [];
        _ships = [];
        _productTypes =
        [
            new ProductType("Fruit", 0),
            new ProductType("Vegetable", 15),
            new ProductType("Dairy", 2)
        ];
        _actionsAvailability = new Dictionary<string, bool>
        {
            { "rmSh", false },
            { "rmCo", false },
            { "loadCoToSh", false }
        };

        _iterSeparator = new string('*', 25);
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine(_iterSeparator);
            SetActionsAvailability();
            ShowMenu();
            
            var action = GetActionFromUser();
            switch (action)
            {
                case "addSh":
                    AddShip();
                    break;
                case "addCo":
                    AddContainer();
                    break;
                case "rmSh":
                    RemoveShip();
                    break;
                case "rmCo":
                    RemoveContainer();
                    break;
                case "loadCoToSh":
                    ContainerToShip();
                    break;
            }
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("The list of ships:");
        if (_ships.Count == 0) Console.WriteLine("No ships");
        else DisplayShips();
        Console.WriteLine();
        
        Console.WriteLine("The list of containers:");
        if (_containers.Count == 0) Console.WriteLine("No containers");
        else DisplayContainers();
        Console.WriteLine();
        
        Console.WriteLine("Possible actions:");

        Console.WriteLine("1 -> Add a ship");
        Console.WriteLine("2 -> Add a container");

        var actionNum = 3;
        
        if (_actionsAvailability["rmSh"])
        {
            Console.WriteLine(actionNum + " -> Delete a ship");
            actionNum++;
        }

        if (_actionsAvailability["rmCo"])
        {
            Console.WriteLine(actionNum + " -> Delete a container");
            actionNum++;
        }

        if (_actionsAvailability["loadCoToSh"]) Console.WriteLine(actionNum + " -> Load a container onto a ship");
        
    }

    private string? GetActionFromUser()
    {
        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                return "addSh";
            case "2":
                return "addCo";
            case "3":
                if (_actionsAvailability["rmSh"]) return "rmSh";
                return _actionsAvailability["rmCo"] ? "rmCo" : null;
            case "4":
                if (_actionsAvailability["rmSh"] && _actionsAvailability["rmCo"]) return "rmCo";
                return null;
            case "5":
                if (_actionsAvailability["rmSh"] && _actionsAvailability["rmCo"]) return "loadCoToSh";
                return null; 
            default:
                return null;
        }
    }
    
    private void SetActionsAvailability()
    {
        _actionsAvailability["rmSh"] = _ships.Count > 0;
        _actionsAvailability["rmCo"] = _containers.Count > 0;
        _actionsAvailability["loadCoToSh"] = _ships.Count > 0 && _containers.Count > 0;
    }

    private void AddShip()
    {
        try
        {
            Console.WriteLine("Enter a new ship's max speed (in knots):");
            var maxSpeed = double.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter a new ship's max number of containers it can hold:");
            var maxNumContainers = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Enter a new ship's max weight it can hold (in kg):");
            var maxWeight = double.Parse(Console.ReadLine());

            var newShip = new ContainerShip(maxSpeed, maxNumContainers, maxWeight); 
            _ships.Add(newShip);
            Console.WriteLine(newShip + " has been created.");
        }
        catch (Exception)
        {
            Console.WriteLine("Provided value is invalid. Operation failed.");
        }
    }

    private ProductType GetProductTypeFromUser()
    {
        Console.WriteLine("Choose the product type:");
        for (var i = 0; i < _productTypes.Count; i++)
            Console.WriteLine(i + 1 + " -> " + _productTypes[i]);
        
        var input = int.Parse(Console.ReadLine()) - 1;

        if (input > 0 && input < _productTypes.Count) return _productTypes[input];

        throw new Exception();
    }

    private void AddContainer()
    {
        try
        {
            Console.WriteLine("Choose the type of a new container:");
            Console.WriteLine("G -> Gas container");
            Console.WriteLine("L -> Liquid container");
            Console.WriteLine("C -> Refrigerated container");

            var type = Console.ReadLine().ToUpper();

            if (type is not ("G" or "L" or "C")) throw new Exception();

            Console.WriteLine("Enter a new container's height (in cm, decimal value):");
            var height = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new container's weight (in kg):");
            var weight = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new container's depth (in cm, decimal value):");
            var depth = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a new container's max capacity (in kg):");
            var maxCapacity = int.Parse(Console.ReadLine());

            BaseContainer newContainer;

            switch (type)
            {
                case "L":
                    Console.WriteLine("Do you want this liquid container to store 'dangerous' goods?");
                    Console.WriteLine("1 -> Yes, store dangerous goods");
                    Console.WriteLine("2 -> No, store safe goods");

                    var input = Console.ReadLine();
                    var storesDangerous = input switch
                    {
                        "1" => true,
                        "2" => false,
                        _ => throw new Exception()
                    };
                    newContainer = new LiquidContainer(height, weight, depth, maxCapacity, storesDangerous);
                    break;
                case "G":
                    Console.WriteLine("Enter this gas container's inner pressure (in atm):");
                    var pressure = double.Parse(Console.ReadLine());
                    newContainer = new GasContainer(height, weight, depth, maxCapacity, pressure);
                    break;
                case "C":
                    var productType = GetProductTypeFromUser();
                    newContainer = new RefrigeratedContainer(height, weight, depth, maxCapacity, productType);
                    break;
                default:
                    throw new Exception();
            }
            
            _containers.Add(newContainer);
            Console.WriteLine(newContainer + " has been created.");
        }
        catch (Exception)
        {
            Console.WriteLine("Provided value is invalid. Operation failed.");
        }
    }

    private void DisplayShips()
    {
        foreach (var ship in _ships) Console.WriteLine(ship);
    }

    private void DisplayContainers()
    {
        foreach (var container in _containers) Console.WriteLine(container);
    }
    
    private void RemoveShip()
    {
        Console.WriteLine("Choose a ship to remove by entering its id (it is specified as <id>):");
        DisplayShips();

        Console.WriteLine("Enter id:");
        try
        {
            var input = int.Parse(Console.ReadLine());
            for (var i = 0; i < _ships.Count; i++)
                if (_ships[i].Id == input)
                {
                    Console.WriteLine(_ships[i] + " has been removed");
                    _ships.RemoveAt(i);
                }
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid option. Operation failed.");
        }
    }
    
    private void RemoveContainer()
    {
        Console.WriteLine("Containers:");
        DisplayContainers();
        Console.WriteLine("Enter the serial number of the container be to removed (e.g. KON-G-1):");
        var serialNumber = Console.ReadLine().ToUpper();

        var containerToRemove = _containers.Find(container => container.SerialNumber.Equals(serialNumber));

        if (containerToRemove is null)
        {
            Console.WriteLine("Invalid option. Operation failed.");
            return;
        }

        _ships.ForEach(ship => ship.RemoveContainerFromShip(containerToRemove));
        
        _containers.Remove(containerToRemove);
        Console.WriteLine("Container " + containerToRemove + " has been removed.");
    }
    
    private void ContainerToShip()
    {
        Console.WriteLine("Enter the serial number of the container to load onto the ship (e.g. KON-G-1):");
        DisplayContainers();
        try
        {
            var serialNumber = Console.ReadLine().ToUpper();
            var containerToLoad = _containers.Find(container => container.SerialNumber == serialNumber);

            if (containerToLoad is null) throw new Exception();

            if (containerToLoad.IsOnShip)
            {
                Console.WriteLine("The chosen container is already on a ship.");
                throw new Exception();
            }
            
            Console.WriteLine("Choose a ship to load onto by entering its id (it is specified as <id>):");
            DisplayShips();
            
            var id = int.Parse(Console.ReadLine());
            var shipToLoadOnto = _ships.Find(ship => ship.Id == id);

            if (shipToLoadOnto is null) throw new Exception();

            shipToLoadOnto.LoadContainerToShip(containerToLoad);
            containerToLoad.IsOnShip = true;
        }
        catch (Exception)
        {
            Console.WriteLine("Operation failed.");
        }
        
    }
}