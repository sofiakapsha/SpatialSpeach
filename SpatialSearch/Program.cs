using System.Diagnostics;

namespace SpatialSearch;

public static class SpatialSearch
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        FileReader.ReadFromFile();

        Console.WriteLine("Введіть координату Latitude: ");
        var userLat = Console.ReadLine();

        Console.WriteLine("Введіть координату Longtitude: ");
        var userLon = Console.ReadLine();

        Console.WriteLine("Введіть радіус: ");
        var userR = Console.ReadLine();

        double lat = 0, lon = 0;
        var size = 0;

        try
        {
            lat = double.Parse(userLat.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
            lon = double.Parse(userLon.Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
            size = int.Parse(userR, System.Globalization.CultureInfo.InvariantCulture);
            
            var linear = new LinearSearch();
            var sw = Stopwatch.StartNew();

            linear.ShowPlacesNear(lat, lon, size);
        
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Лінійний пошук: {sw.Elapsed}");
            Console.WriteLine();

            var kdtree = new KdTree();
        
            sw.Restart();
            kdtree.BuildTree();
            sw.Stop();
            Console.WriteLine();
            Console.WriteLine($"Побудова KD-Tree: {sw.Elapsed}");
            Console.WriteLine();

            sw.Restart();
            kdtree.FindPlacesNear(kdtree.Root, lat, lon, 0, size);
            kdtree.ShowPlaces();
            sw.Stop();
            
            Console.WriteLine();
            Console.WriteLine($"Тільки пошук по KD-Tree: {sw.Elapsed}");
            Console.WriteLine();
            
            Console.WriteLine("Введіть кількість вершин багатокутника:");
            var userE = Console.ReadLine();
            
            var e = int.Parse(userE, System.Globalization.CultureInfo.InvariantCulture);

            var polygon = new List<Point>();
            for (int i = 0; i < e; i++)
            {
                Console.WriteLine($"Введіть Latitude для вершини {i + 1}:");
                double pLat = double.Parse(Console.ReadLine().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
    
                Console.WriteLine($"Введіть Longitude для вершини {i + 1}:");
                double pLon = double.Parse(Console.ReadLine().Replace(',', '.'), System.Globalization.CultureInfo.InvariantCulture);
    
                polygon.Add(new Point(pLat, pLon));
            }
            

            var bagatokutnik = new Bagatokutnik();
        
            sw.Restart();
            bagatokutnik.CheckInBagatokutnik(polygon);
            bagatokutnik.ShowAllPoint();
            sw.Stop();

            Console.WriteLine();
            Console.WriteLine($"Пошук у довільному багатокутнику: {sw.Elapsed}");
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Сталась помилка: {e}");
        }
    }
}
