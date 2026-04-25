using System;

namespace SpatialSearch;

public class LinearSearch
{
    private List<Point> _allPoints = new List<Point>();
    private const string _filePath = @"../../../Data/ukraine_poi.csv";

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
            var point = new Point(parts);

            _allPoints.Add(point);
        }
    }

    public void ShowList()
    {
        ReadFromFile();

        foreach (var point in _allPoints)
        {
            System.Console.WriteLine(point);
        }
    }
}
