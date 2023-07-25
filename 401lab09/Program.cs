using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ManhattanNeighborhoods
{
    class Program
    {
        static void Main(string[] args)
        {
            string jsonFilePath = "data.json";
            string jsonData = File.ReadAllText(jsonFilePath);

            // Deserialize JSON data into C# objects
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(jsonData);

            // Extract the features list from the root object
            List<Feature> features = rootObject.features;

            // Perform LINQ queries
            var neighborhoods = features.Select(f => f.properties.neighborhood).ToList();
            var neighborhoodsWithNames = neighborhoods.Where(neigh => !string.IsNullOrEmpty(neigh)).ToList();
            var uniqueNeighborhoods = neighborhoodsWithNames.Distinct().ToList();
            var consolidatedQuery = features
                .Select(f => f.properties.neighborhood)
                .Where(neigh => !string.IsNullOrEmpty(neigh))
                .Distinct()
                .ToList();

            // Output the results to the console
            Console.WriteLine("Question 1 - All neighborhoods:");
            PrintList(neighborhoods);

            Console.WriteLine("\nQuestion 2 - Neighborhoods with names:");
            PrintList(neighborhoodsWithNames);

            Console.WriteLine("\nQuestion 3 - Unique neighborhoods:");
            PrintList(uniqueNeighborhoods);

            Console.WriteLine("\nQuestion 4 - Consolidated query:");
            PrintList(consolidatedQuery);

            Console.WriteLine("\nQuestion 5 - Using LINQ Query syntax:");
            var neighborhoodsQuerySyntax = (from f in features
                                            where !string.IsNullOrEmpty(f.properties.neighborhood)
                                            select f.properties.neighborhood)
                                           .Distinct()
                                           .ToList();
            PrintList(neighborhoodsQuerySyntax);
        }

        static void PrintList(List<string> list)
        {
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine($"Final Total: {list.Count} neighborhoods");
        }
    }

    // Define classes that match the structure of the JSON data
    class RootObject
    {
        public List<Feature> features { get; set; }
    }

    class Feature
    {
        public Properties properties { get; set; }
    }

    class Properties
    {
        public string neighborhood { get; set; }
    }
}
