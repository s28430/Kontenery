using Containers.Models;
using Containers.Ship;

namespace Containers.Interface;

public class Cli
{
    private readonly List<BaseContainer> _containers;
    private readonly List<ContainerShip> _ships;

    private readonly string _iterSeparator;

    private readonly Dictionary<string, bool> _actionsAvailability;

    public Cli()
    {
        _containers = [];
        _ships = [];
        _actionsAvailability = new Dictionary<string, bool>
        {
            { "3", false },
            { "4", false },
            { "5", false }
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
                case "1":
                    AddShip();
                    break;
                default:
                    Console.Write("");
                    break;
            }
        }
    }
    

    private void ShowMenu()
    {
        Console.WriteLine("The list of ships:");
        if (_ships.Count == 0) Console.WriteLine("No ships");
        else foreach (var ship in _ships) Console.WriteLine(ship);
        Console.WriteLine();
        
        Console.WriteLine("The list of containers:");
        if (_containers.Count == 0) Console.WriteLine("No containers");
        foreach (var container in _containers) Console.WriteLine(container);
        Console.WriteLine();

        Console.WriteLine("Possible actions:");

        Console.WriteLine("1 -> Add a ship");
        Console.WriteLine("2 -> Add a container");
        
        if (_actionsAvailability["3"]) Console.WriteLine("3 -> Delete a ship");
        if (_actionsAvailability["4"]) Console.WriteLine("4 -> Delete a container");
        if (_actionsAvailability["5"]) Console.WriteLine("5 -> Load a container onto a ship");
    }

    private string? GetActionFromUser()
    {
        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
            case "2":
                return input;
            case "3":
            case "4":
            case "5":
                return _actionsAvailability[input] ? input : null;
            default:
                return null;
        }
    }
    
    private void SetActionsAvailability()
    {
        _actionsAvailability["3"] = _ships.Count > 0;
        _actionsAvailability["4"] = _containers.Count > 0;
        _actionsAvailability["5"] = _ships.Count > 0 && _containers.Count > 0;
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
}