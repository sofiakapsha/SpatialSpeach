using System;
using System.Security.Principal;

namespace SpatialSearch;

public class Bagatokutnik
{
    private List<Point> _allPoints = new List<Point>();
    private const string _filePath = @"../../../Data/ukraine_poi.csv";
    private const double R = 6371e3;
    private List<Point> _result = new List<Point>();

    private void ReadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            System.Console.WriteLine("File wasn't found.");
            return;
        }

        var lines = File.ReadLines(_filePath, System.Text.Encoding.UTF8);

        foreach (var line in lines)
        {
            var parts = line.Split(";").ToList();
            var point = new Point(parts, line);

            _allPoints.Add(point);
        }
    }

    public void CheckInBagatokutnik(List<Point> polygon)
    {
        ReadFromFile();

        if (_allPoints.Count == 0)
        {
            System.Console.WriteLine("No points.");
            return;
        }

        foreach (var point in _allPoints)
        {
            var count = 0;

            for (var i = 0; i < polygon.Count; i++)
            {
                var one = polygon[i];

                var i2 = i + 1;

                if (i2 == polygon.Count)
                    i2 = 0;
                
                var two = polygon[i2];

                var lat1 = one.Latitude;
                var lon1 = one.Longitude;

                var lat2 = two.Latitude;
                var lon2 = two.Longitude;

                if (lat1 == point.Latitude && lon1 == point.Longitude)
                    _result.Add(point);

                var isIntersecting = false;

                if ((point.Longitude >= lon1 && point.Longitude < lon2) || (point.Longitude >= lon2 && point.Longitude < lon1))
                {
                    double xInt = lat1 + (double)(point.Longitude - lon1) * (lat1 - lat1) / (lon2 - lon1);

                    if (point.Latitude < xInt)
                        isIntersecting = true;
                }

                if (isIntersecting)
                        count++;
            }

            if (count % 2 != 0)
                _result.Add(point);
        }
    }

    public void ShowAllPoint()
    {
        if (_result.Count == 0)
        {
            System.Console.WriteLine("No points found.");
            return;
        }

        foreach (var point in _result)
        {
            System.Console.WriteLine(point);
        }
    }
}
