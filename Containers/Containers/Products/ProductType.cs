namespace Containers.Products;

public class ProductType(string name, double storageTemperature)
{
    public string Name { get; } = name.ToLower();
    public double StorageTemperature { get; } = storageTemperature;

    public static bool operator ==(ProductType left, ProductType right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ProductType left, ProductType right)
    {
        return !(left == right);
    }

    public override string ToString()
    {
        return "Product type(name=" + Name + ", storageTemp=" + StorageTemperature + ")";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        
        if (ReferenceEquals(this, obj)) return true;
        
        if (obj.GetType() != GetType()) return false;
        var another = (ProductType)obj;

        return Name == another.Name
               && Math.Abs(StorageTemperature - another.StorageTemperature) < 0.5;
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode() + StorageTemperature.GetHashCode();
    }
}