using System.Globalization;

namespace SpatialSearch;

public class Point
{
    public readonly double Latitude;
    public readonly double Longitude;
    private readonly string RawLine;
    
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
            throw new ArgumentException("Неправильний ввід.");
        }
    }
    
    public Point(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
    
    public override string ToString()
    {
        var place = RawLine.Split(';');
        var lat = place[0];
        var lon = place[1];
        var type = place[3];

        var name = place[4];
        if (!string.IsNullOrEmpty(name)) return $"Координати: ({lat}; {lon}) | Тип: {type} | Назва: {name}";
        return $"Координати: ({lat}; {lon}) | Тип: {type}";
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