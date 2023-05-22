using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects;

public sealed class Location : ValueObject
{

    private Location(string name, string address, double latitude, double longitude)
    {
        Name = name;
        Address = address;
        Latitude = latitude;
        Longitude = longitude;
    }

    public string Name { get; private set; }
    public string Address { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    

    public static Location CreateNew(string name, string address, double latitude, double longitude)
    {
        return new(name, address, latitude, longitude);
    }

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Name;
        yield return Address;
        yield return Latitude;
        yield return Longitude;

    }
}