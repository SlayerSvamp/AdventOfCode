using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    class Program
    {
        static MD5 md5Hash = MD5.Create();
        static string Linus = "rrrbmfta";
        static string Niklas = "qljzarfv";
        static string Input = string.Empty;
        static List<string> solutions = new List<string>();
        static void Move(int X, int Y, string currentPath)
        {
            if (X == 3 && Y == 3)
            {
                solutions.Add(currentPath);
            }
            else
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes($"{Input}{currentPath}"));
                string hash = string.Join("", (bytes.Select(b => b.ToString("x2")).ToArray()));
                string opens = "bcdef";
                if (opens.Contains(hash[0]) && Y > 0)
                {
                    Move(X, Y - 1, $"{currentPath}{"U"}");
                }
                if (opens.Contains(hash[1]) && Y < 3)
                {
                    Move(X, Y + 1, $"{currentPath}{"D"}");
                }
                if (opens.Contains(hash[2]) && X > 0)
                {
                    Move(X - 1, Y, $"{currentPath}{"L"}");
                }
                if (opens.Contains(hash[3]) && X < 3)
                {
                    Move(X + 1, Y, $"{currentPath}{"R"}");
                }
            }
        }
        static void Run(string input)
        {
            Console.WriteLine($"With {(input == Linus ? "Linus" : "Niklas")} input:");
            Input = input;
            int X = 0;
            int Y = 0;
            Move(X, Y, "");
            string shortest = solutions.OrderBy(x => x.Length).First();
            string longest = solutions.OrderByDescending(x => x.Length).First();
            Console.WriteLine($"Shortest = {shortest} - {shortest.Length} long");
            Console.WriteLine($"Longest length = {longest.Length}");
            Console.WriteLine($"Total number of possible solutions = {solutions.Count}");
            solutions.Clear();
        }
        static void Main(string[] args)
        {
            Run(Linus);
            Console.WriteLine();
            Run(Niklas);
            Console.ReadLine();
        }
    }
}
