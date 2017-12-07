using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] rawStrings = File.ReadAllLines("Triangles.txt").ToArray();
            string[][] stringArrays = rawStrings.Select(x => x.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();
            int[][] intArrays = stringArrays.Select(x => x.Select(y => Int32.Parse(y.Trim())).ToArray()).ToArray();
            //part 1
            Console.WriteLine(intArrays.Where(x => (x.Sum() - x.Max()) > x.Max()).Count());
            //part 2
            List<int[]> intArrayList = new List<int[]>();
            for (int i = 0; i < intArrays.Length; i += 3)
            {
                for (int j = 0; j < 3; j++)
                {
                    intArrayList.Add(new int[] { intArrays[i][j], intArrays[i + 1][j], intArrays[i + 2][j] });
                }
            }
            Console.WriteLine(intArrayList.Where(x => (x.Sum() - x.Max()) > x.Max()).Count());
            Console.ReadKey();
        }
    }
}
