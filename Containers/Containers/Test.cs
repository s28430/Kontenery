using Containers.Models;

public class Test
{
    public static void Main(string[] args)
    {
        LiquidContainer lContainer1 = new(20, 40, 40, 100, false);

        Console.WriteLine("Cargo weight: " + lContainer1.CurrCargoWeight);
        var loaded = lContainer1.LoadCargo(30);
        Console.WriteLine("Loaded: " + loaded);
        Console.WriteLine("Cargo weight: " + lContainer1.CurrCargoWeight);

        var unloaded = lContainer1.UnloadCargo();
        Console.WriteLine("Unloaded: " + unloaded);
        Console.WriteLine("Cargo weight: " + lContainer1.CurrCargoWeight);
    }
}