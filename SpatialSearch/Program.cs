namespace SpatialSearch;

public static class SpatialSearch
{
    public static void Main()
    {
        // var linear = new LinearSearch();
        // linear.ShowPlacesNear(50.43484, 30.5295, 100);

        // System.Console.WriteLine();
        // System.Console.WriteLine();

        // var kdtree = new KdTree();
        // kdtree.BuildTree();
        
        // kdtree.FindPlacesNear(kdtree.Root, 50.43484, 30.5295, 0, 100);

        // kdtree.ShowPlaces();

        var point1 = new Point(48.56989, 39.3401);
        var point2 = new Point(51.40678, 29.45697);
        var point3 = new Point(45.34338, 36.23596);

        var polygon = new List<Point> {point1, point2, point3};

        var bagatokutnik = new Bagatokutnik();

        bagatokutnik.CheckInBagatokutnik(polygon);
        bagatokutnik.ShowAllPoint();
    }
}
