using System.Globalization;

namespace SpatialSearch;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    public readonly string RawLine;
    
    public Point(List<string> entries, string rawLine = "")
    {
        if (entries.Count >= 2 && 
        double.TryParse(entries[0].Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture,  out double latitude) &&
        double.TryParse(entries[1].Replace(',', '.'), System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out double longitude))
        {
            Latitude = latitude;
            Longitude = longitude;
            RawLine = rawLine;
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
        return RawLine;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Point other)
            return false;
        
        return Latitude.Equals(other.Latitude) && Longitude.Equals(other.Longitude);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude);
    }
}