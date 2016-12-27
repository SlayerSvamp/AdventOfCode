using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    class Program
    {
        static void RunA()
        {
            int X = 70;
            int Y = 50;
            Console.WindowWidth = X * 2 + 1;
            Console.WindowHeight = Y + 1 <= Console.LargestWindowHeight ? Y + 1 : Console.LargestWindowHeight;
            var input = 1350;
            Func<int, int, int> algorithm = (x, y) => (x * x + 3 * x + 2 * x * y + y + y * y) + input;
            Func<int, int> countBits = (i) => Convert.ToString(i, 2).ToCharArray().Count(c => c == '1');
            Func<int, int, bool> isOpenSpace = (x, y) => countBits(algorithm(x, y)) % 2 == 0;
            int[,] landscape = new int[X, Y];
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    landscape[x, y] =
                        (x == 31 && y == 39) ? 0
                        : (x == 1 && y == 1) ? -1
                        : isOpenSpace(x, y) ? -2
                        : -3;
                }
            }
            bool changeWasMade = true;
            bool done = false;
            while (changeWasMade && done == false)
            {
                changeWasMade = false;
                for (int y = 0; y < Y; y++)
                {
                    for (int x = 0; x < X; x++)
                    {
                        int value = landscape[x, y];
                        if (value >= 0)
                        {
                            if (x < X - 1 && (landscape[x + 1, y] == -2 || landscape[x + 1, y] == -1))
                            {
                                landscape[x + 1, y] = value + 1;
                                changeWasMade = true;
                            }
                            if (x != 0 && (landscape[x - 1, y] == -2 || landscape[x - 1, y] == -1))
                            {
                                landscape[x - 1, y] = value + 1;
                                changeWasMade = true;
                            }
                            if (y < Y - 1 && (landscape[x, y + 1] == -2 || landscape[x, y + 1] == -1))
                            {
                                landscape[x, y + 1] = value + 1;
                                changeWasMade = true;
                            }
                            if (y != 0 && (landscape[x, y - 1] == -2 || landscape[x, y - 1] == -1))
                            {
                                landscape[x, y - 1] = value + 1;
                                changeWasMade = true;
                            }
                            if ((x < X - 1 && landscape[x + 1, y] == -1) || (x != 0 && landscape[x - 1, y] == -1) || (y < Y - 1 && landscape[x, y + 1] == -1) || (y != 0 && landscape[x, y - 1] == -1))
                            {
                                done = true;
                            }
                        }
                    }
                }
            }
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    int value = landscape[x, y];
                    Console.Write((value < 0) ? ((value == -2) ? "  " : "██") : (value > 9) ? $"{value}" : $" {value}");

                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }
        static void RunB()
        {
            int X = 70;
            int Y = 50;
            Console.WindowWidth = X * 2 + 1;
            Console.WindowHeight = Y + 1;
            var input = 1350;
            Func<int, int, int> algorithm = (x, y) => (x * x + 3 * x + 2 * x * y + y + y * y) + input;
            Func<int, int> countBits = (i) => Convert.ToString(i, 2).ToCharArray().Count(c => c == '1');
            Func<int, int, bool> isOpenSpace = (x, y) => countBits(algorithm(x, y)) % 2 == 0;
            int[,] landscape = new int[X, Y];
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    landscape[x, y] =
                        (x == 1 && y == 1) ? 0
                        //: (x == 1 && y == 1) ? -1
                        : isOpenSpace(x, y) ? -2
                        : -3;
                }
            }
            bool changeWasMade = true;
            bool done = false;
            while (changeWasMade && done == false)
            {
                changeWasMade = false;
                for (int y = 0; y < Y; y++)
                {
                    for (int x = 0; x < X; x++)
                    {
                        int value = landscape[x, y];
                        if (value >= 0 && value < 50)
                        {
                            if (x < X - 1 && (landscape[x + 1, y] == -2 || landscape[x + 1, y] == -1))
                            {
                                landscape[x + 1, y] = value + 1;
                                changeWasMade = true;
                            }
                            if (x != 0 && (landscape[x - 1, y] == -2 || landscape[x - 1, y] == -1))
                            {
                                landscape[x - 1, y] = value + 1;
                                changeWasMade = true;
                            }
                            if (y < Y - 1 && (landscape[x, y + 1] == -2 || landscape[x, y + 1] == -1))
                            {
                                landscape[x, y + 1] = value + 1;
                                changeWasMade = true;
                            }
                            if (y != 0 && (landscape[x, y - 1] == -2 || landscape[x, y - 1] == -1))
                            {
                                landscape[x, y - 1] = value + 1;
                                changeWasMade = true;
                            }
                        }
                    }
                }
            }
            int num = 0;
            for (int y = 0; y < Y; y++)
            {
                for (int x = 0; x < X; x++)
                {
                    int value = landscape[x, y];
                    if (value >= 0)
                    {
                        num++;
                    }
                    Console.Write((value < 0) ? ((value == -2) ? "  " : "██") : (value > 9) ? $"{value}" : $" {value}");

                }
                Console.WriteLine();
            }
            Console.WriteLine($"places reachable in 50 steps or less = {num}");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            RunA();
            RunB();
        }
    }
}
