namespace SpatialSearch;

public static class SpatialSearch
{
    public static void Main()
    {
        var linear = new LinearSearch();
        linear.ShowPlacesNear(50.43484, 30.5295, 100);

        System.Console.WriteLine();
        System.Console.WriteLine();

        var kdtree = new KdTree();
        kdtree.BuildTree();
        
        kdtree.FindPlacesNear(kdtree.Root, 50.43484, 30.5295, 0, 100);

        kdtree.ShowPlaces();
    }
}
