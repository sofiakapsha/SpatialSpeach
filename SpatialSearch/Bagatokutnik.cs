using System;
using System.Security.Principal;

namespace SpatialSearch;
  public class Bagatokutnik
{
    private const double R = 6371e3;
    private List<Point> _result = new List<Point>();

    public void CheckInBagatokutnik(List<Point> polygon)
    {
        _result.Clear();
        
        if (FileReader.allPoints.Count == 0) return;
        
        double minLat = polygon.Min(p => p.Latitude);
        double maxLat = polygon.Max(p => p.Latitude);
        double minLon = polygon.Min(p => p.Longitude);
        double maxLon = polygon.Max(p => p.Longitude);

        foreach (var point in FileReader.allPoints)
        {
            
            if (point.Latitude < minLat || point.Latitude > maxLat || 
                point.Longitude < minLon || point.Longitude > maxLon)
                continue;

            bool isInside = false;
            int j = polygon.Count - 1;

            for (int i = 0; i < polygon.Count; i++)
            {
                
                if (polygon[i].Latitude == point.Latitude && polygon[i].Longitude == point.Longitude)
                {
                    isInside = true;
                    break;
                }

               
                if ((polygon[i].Latitude > point.Latitude) != (polygon[j].Latitude > point.Latitude) &&
                    (point.Longitude < (polygon[j].Longitude - polygon[i].Longitude) * (point.Latitude - polygon[i].Latitude) / 
                    (polygon[j].Latitude - polygon[i].Latitude) + polygon[i].Longitude))
                {
                    isInside = !isInside;
                }
                j = i;
            }

            if (isInside)
                _result.Add(point);
        }
    }

    public void ShowAllPoint()
    {
        if (_result.Count == 0)
        {
            System.Console.WriteLine("Місць не знайдено.");
            return;
        }

        foreach (var point in _result)
        {
            System.Console.WriteLine(point);
        }
    }
}
