using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day18
{
    class Program
    {

        static void Main(string[] args)
        {
            var rows = new List<string>() { "^.....^.^^^^^.^..^^.^.......^^..^^^..^^^^..^.^^.^.^....^^...^^.^^.^...^^.^^^^..^^.....^.^...^.^.^^.^" };
            var rowLength = rows.First().Length;
            Console.WindowWidth = rowLength + 1;
            Console.WindowHeight = 40 + 2;
            while (rows.Count < 400 * 1000)
            {
                var newRow = new StringBuilder();
                for (int i = 0; i < rowLength; i++)
                {
                    string row = rows.Last();
                    //is a trap
                    char left, center, right;
                    if (i > 0)
                    {
                        left = row[i - 1];
                    }
                    else
                    {
                        left = '.';
                    }
                    center = row[i];
                    if (i < rowLength - 1)
                    {
                        right = row[i + 1];
                    }
                    else
                    {
                        right = '.';
                    }
                    string lcr = new string(new char[] { left, center, right });
                    newRow.Append(Regex.IsMatch(lcr, @"\.\.\^|\^\^\.|\^\.\.|\.\^\^") ? "^" : ".");
                }
                rows.Add(newRow.ToString());
            }

            rows.Take(40).ToList().ForEach(row => Console.WriteLine(row));
            
            Console.WriteLine($"There are {string.Join("", rows.Take(40)).Count(c => c == '.')} safe tiles in 40 rows");
            Console.WriteLine($"There are {string.Join("", rows).Count(c => c == '.')} safe tiles in 400 000 rows");
            Console.ReadLine();
        }
    }
}
