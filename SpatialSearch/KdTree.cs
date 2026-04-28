using System;
using System.Runtime.Serialization;

namespace SpatialSearch;

public class KdTree
{
    public Node? Root;
    public const int K = 2;

    private const double R = 6371e3;
    private List<(Point, double)> _result = new List<(Point, double)>();
    
    public Node? NewNode(List<Point> points, int depth)
    {
        if (points.Count == 0)
            return null;

        var axis = depth % K;

        if (axis == 0)
        {
            points.Sort((a, b) => a.Latitude.CompareTo(b.Latitude));
        }
        else
        {
            points.Sort((a, b) => a.Longitude.CompareTo(b.Longitude));
        }

        var indexMiddle = points.Count / 2;
        var newNode = new Node(points[indexMiddle]);
        
        var listLeft = points.GetRange(0, indexMiddle);
        var listRight = points.GetRange(indexMiddle + 1, points.Count - indexMiddle - 1);

        newNode.Left = NewNode(listLeft, depth + 1);
        newNode.Right = NewNode(listRight, depth + 1);

        return newNode;
    }

    public void BuildTree()
    {
        Root = NewNode(FileReader.allPoints, 0);
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

    public void FindPlacesNear(Node? node, double lat, double lon, int depth, int size = 20)
    {
        var sizeDegreesLat = size / 111000.0;
        var sizeDegreesLon = size / (111000.0 * Math.Cos(double.DegreesToRadians(lat)));


        if (node == null)
            return;

        var targetPoint = new Point(lat, lon);
        
        var distanceM = HaversineCalculate(targetPoint, node.Point);

        if (distanceM <= size)
        {
            _result.Add((node.Point, Math.Round(distanceM, 0)));
        }

        var axis = depth % K;

        var difference = 0.0;

        if (axis == 0)
        {
            difference = lat - node.Point.Latitude;
        }
        else
        {
            difference = lon - node.Point.Longitude;
        }

        if (difference < 0)
        {
            FindPlacesNear(node.Left, lat, lon, depth + 1, size);

            var LatOrLon = axis == 0 ? sizeDegreesLat : sizeDegreesLon;
            if (Math.Abs(difference) <= LatOrLon)
            {
                FindPlacesNear(node.Right, lat, lon, depth + 1, size);
            }
        }
        else
        {
            FindPlacesNear(node.Right, lat, lon, depth + 1, size);

            var LatOrLon = axis == 0 ? sizeDegreesLat : sizeDegreesLon;
            if (Math.Abs(difference) <= LatOrLon)
            {
                FindPlacesNear(node.Left, lat, lon, depth + 1, size);
            }
        }
    }

    public void ShowPlaces()
    {
        if (_result.Count == 0)
        {
            System.Console.WriteLine("Місць не знайдено.");
            return;
        }

        foreach (var point in _result)
        {
            System.Console.WriteLine($"{point.Item1} | Дистанція: {point.Item2}m");
        }
    }
}
