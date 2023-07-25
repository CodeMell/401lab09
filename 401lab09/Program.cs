using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

public class FeatureCollection
{
    public List<Feature> Features { get; set; }
}

public class Feature
{
    public Geometry Geometry { get; set; }
    public Properties Properties { get; set; }
}

public class Geometry
{
    public string Type { get; set; }
    public List<double> Coordinates { get; set; }
}

public class Properties
{
    public string Zip { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Address { get; set; }
    public string Borough { get; set; }
    public string Neighborhood { get; set; }
    public string County { get; set; }
}

class Program
{
    static void Main()
    {
        string jsonFilePath = "data.json";
        string jsonData = File.ReadAllText(jsonFilePath);

        FeatureCollection featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(jsonData);

        // Question 1: Output all of the neighborhoods in this data list
        List<string> allNeighborhoods = featureCollection.Features.Select(f => f.Properties.Neighborhood).ToList();
        Console.WriteLine($"Question 1: Total neighborhoods: {allNeighborhoods.Count}, Neighborhoods: {string.Join(", ", allNeighborhoods)}");

        // Question 2: Filter out all the neighborhoods that do not have any names
        List<string> neighborhoodsWithNames = featureCollection.Features
            .Where(f => !string.IsNullOrEmpty(f.Properties.Neighborhood))
            .Select(f => f.Properties.Neighborhood)
            .ToList();
        Console.WriteLine($"Question 2: Total neighborhoods with names: {neighborhoodsWithNames.Count}, Neighborhoods: {string.Join(", ", neighborhoodsWithNames)}");

        // Question 3: Remove duplicates
        List<string> uniqueNeighborhoods = featureCollection.Features
            .Where(f => !string.IsNullOrEmpty(f.Properties.Neighborhood))
            .Select(f => f.Properties.Neighborhood)
            .Distinct()
            .ToList();
        Console.WriteLine($"Question 3: Total unique neighborhoods: {uniqueNeighborhoods.Count}, Neighborhoods: {string.Join(", ", uniqueNeighborhoods)}");

        // Question 4: Rewrite the queries and consolidate into one
        List<string> allNeighborhoodsConsolidated = featureCollection.Features
            .Where(f => !string.IsNullOrEmpty(f.Properties.Neighborhood))
            .Select(f => f.Properties.Neighborhood)
            .ToList();
        List<string> uniqueNeighborhoodsConsolidated = allNeighborhoodsConsolidated.Distinct().ToList();

        Console.WriteLine($"Question 4: Total neighborhoods: {allNeighborhoodsConsolidated.Count}, Total unique neighborhoods: {uniqueNeighborhoodsConsolidated.Count}");

        // Question 5: Rewrite using the opposing method (Method Syntax -> Query Syntax)
        List<string> neighborhoodsWithNamesQuerySyntax = (from f in featureCollection.Features
                                                          where !string.IsNullOrEmpty(f.Properties.Neighborhood)
                                                          select f.Properties.Neighborhood).ToList();
        Console.WriteLine($"Question 5: Total neighborhoods with names using Query Syntax: {neighborhoodsWithNamesQuerySyntax.Count}, Neighborhoods: {string.Join(", ", neighborhoodsWithNamesQuerySyntax)}");
    }
}
