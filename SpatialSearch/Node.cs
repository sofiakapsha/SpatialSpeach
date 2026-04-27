using System;

namespace SpatialSearch;

public class Node
{
    public Point Point;
    public Node? Left;
    public Node? Right;

    public Node(Point point)
    {
        Point = point;
    }
}
