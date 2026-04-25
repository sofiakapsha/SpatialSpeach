namespace SpatialSearch;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    
    public Point(List<string> entries)
    {
        if (entries.Count >= 2 && 
        double.TryParse(entries[0], out double latitude) &&
        double.TryParse(entries[1], out double longitude))
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        else
        {
            throw new ArgumentException("Invalid entries.");
        }
    }
    
    public Point(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public override string ToString()
    {
        return $"latitude: {Latitude} | longtitude: {Longitude}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Point other)
            return false;
        
        return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
    }

    public override int GetHashCode()
    {
        return GetHashCode.Combine(Latitude, Longitude);
    }
}