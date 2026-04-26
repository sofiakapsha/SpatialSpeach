namespace SpatialSearch;

public static class SpatialSearch
{
    public static void Main()
    {
        var linear = new LinearSearch();
        linear.ShowPlacesNear(50.43484, 30.5295, 100);
    }
}
