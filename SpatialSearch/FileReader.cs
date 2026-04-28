namespace SpatialSearch;

public static class FileReader
{
    public static List<Point> allPoints = new List<Point>();
    private const string _filePath = @"../../../Data/ukraine_poi.csv";
    
    public static void ReadFromFile()
    {
        if (!File.Exists(_filePath))
        {
            System.Console.WriteLine("Файл не знайдено.");
            return;
        }

        var lines = File.ReadLines(_filePath, System.Text.Encoding.UTF8);

        foreach (var line in lines)
        {
            var parts = line.Split(";").ToList();
            var point = new Point(parts, line);

            allPoints.Add(point);
        }
    }
}