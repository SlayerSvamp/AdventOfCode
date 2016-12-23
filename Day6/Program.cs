using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder resultA = new StringBuilder();
            StringBuilder resultB = new StringBuilder();
            List<string> inputList = File.ReadAllLines("Signals.txt").ToList();
            int length = inputList.First().Length;
            for (int i = 0; i < length; i++)
            {
                List<char> letters = inputList
                    .Select(s => s[i])
                    .GroupBy(c => c)
                    .OrderByDescending(chars => chars.Count())
                    .Select(chars => chars.Key).ToList();
                resultA.Append(letters.First());
                resultB.Append(letters.Last());
            }
            Console.WriteLine(resultA.ToString());
            Console.WriteLine(resultB.ToString());
            Console.ReadKey();
        }
    }
}
