using Containers.Models;
using Containers.Ship;

namespace Containers.Interface;

public class Cli
{
    private readonly List<BaseContainer> _containers = [];
    private readonly List<ContainerShip> _ships = [];

    private readonly Dictionary<string, bool> _actionsAvailability = new ()
    {
        { "3", false },
        { "4", false },
        { "5", false }
    };

    public void Run()
    {
        while (true)
        {
            SetActionsAvailability();
            ShowMenu();
            
            var action = GetActionFromUser();
            if (action is null) continue;
            
        }
    }

    private void ShowMenu()
    {
        Console.WriteLine("The list of ships");
        if (_ships.Count == 0) Console.WriteLine("No ships");
        else foreach (var ship in _ships) Console.WriteLine(ship);
        
        Console.WriteLine("The list of containers:");
        if (_containers.Count == 0) Console.WriteLine("No containers");
        foreach (var container in _containers) Console.WriteLine(container);

        Console.WriteLine("\nPossible actions:");

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
}