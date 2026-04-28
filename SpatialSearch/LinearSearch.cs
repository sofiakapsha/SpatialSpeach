using System;

namespace SpatialSearch;

public class LinearSearch
{
    private const double R = 6371e3;
    
    public void ShowPlacesNear(double lat, double lon, int size = 20)
    {
        var startPoint = new Point(lat, lon);
        
        var filteredPonts = FileReader.allPoints
            .Select(point => (point, distance: HaversineCalculate(startPoint, point)))
            .Where(p => p.distance <= size)
            .OrderBy(p => p.distance)
            .ToList();

        if (filteredPonts.Count > 0)
        {
            foreach (var (point, distance) in filteredPonts)
            {
                Console.WriteLine($"{point} | Дистанція: {distance:F0}m");
            }
        }
        else Console.WriteLine("Місць не знайдено.");
    }
    

    private double HaversineCalculate(Point startPoint, Point point)
    {

        double lat1 = double.DegreesToRadians(startPoint.Latitude);
        double long1 = double.DegreesToRadians(startPoint.Longitude);
        
        double lat2 = double.DegreesToRadians(point.Latitude);
        double long2 = double.DegreesToRadians(point.Longitude);

        double h = Math.Pow(Math.Sin((lat2 - lat1) / 2), 2) +
                   Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin((long2 - long1) / 2), 2);
        double c = 2 * Math.Atan2(Math.Sqrt(h), Math.Sqrt(1 - h)) * R;
        return c;
    }
}
