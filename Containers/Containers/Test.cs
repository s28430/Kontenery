using Containers.Models;

public class Test
{
    public static void Main(string[] args)
    {
        LiquidContainer lContainer1 = new(10, 25, 5, 23, 100, false);
        LiquidContainer lContainer2 = new(10, 30, 10, 23, 125, true);
        Console.WriteLine(lContainer1.SerialNumber);
        Console.WriteLine(lContainer2.SerialNumber);
    }
}