using System;

namespace SpatialSearch;

public class Node
{
    public Point Point;
    public Point Left;
    public Point Right;

    public Node(Point point, Point left, Point right)
    {
        Point = point;
        Left = left;
        Right = right;
    }
}
