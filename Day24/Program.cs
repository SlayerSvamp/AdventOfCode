using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Day24
{
    class Program
    {
        static List<Distance> GetShortestRoute(List<Distance> distances, List<Distance> route, char current, bool goBack)
        {
            var routes = new List<List<Distance>>();
            var distancesAvailable = distances.Where(x => x.From == current && !route.Any(y => y.From == x.To));
            if (!distancesAvailable.Any())
            {
                if (goBack)
                    route.Add(distances.First(x => x.From == current && x.To == '0'));
                return route;
            }
            foreach (var distance in distancesAvailable)
            {
                var newRoute = route.ToList();
                newRoute.Add(distance);
                routes.Add(GetShortestRoute(distances, newRoute, distance.To, goBack));
            }
            return routes.Aggregate((a, b) => a.Sum(x => x.Length) < b.Sum(x => x.Length) ? a : b);
        }
        static void Main(string[] args)
        {
            var points =
                //*
                File.ReadAllLines("input.txt")
                /*/
                File.ReadAllLines("testinput.txt")
                //*/
                .SelectMany((a, y) => a.Select((b, x) => new Point { X = x, Y = y, Position = b }))
                .Where(x => x.Position != '#')
                .ToList();

            var grid = new Grid(points);
            var distances = grid.GetDistances();

            var route = GetShortestRoute(distances, new List<Distance> { }, '0', false);

            var route2 = GetShortestRoute(distances, new List<Distance> { }, '0', true);

            Console.WriteLine();
            Console.Write(" Part 1:\r\n ");
            Console.WriteLine(route.Sum(x => x.Length));
            Console.WriteLine();
            foreach (var distance in route)
            {
                Console.WriteLine($"{distance.From} -> {distance.To} = {distance.Length} steps");
            }
            Console.WriteLine();

            Console.Write(" Part 2:\r\n ");
            Console.WriteLine(route2.Sum(x => x.Length));
            Console.WriteLine();
            foreach (var distance in route2)
            {
                Console.WriteLine($" {distance.From} -> {distance.To} = {distance.Length} steps");
            }

            Console.ReadLine();
        }

    }
}
