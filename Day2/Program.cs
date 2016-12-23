using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Day2
{
    public class Day2
    {
        char[][] keypad { get; set; }
        int indexY { get; set; }
        int indexX { get; set; }
        char PositionValue(int deltaY, int deltaX) { return keypad[indexY + deltaY][indexX + deltaX]; }

        public Day2(char part)
        {
            switch (part)
            {
                case 'A':
                    A();
                    break;
                case 'B':
                default:
                    B();
                    break;
            }
        }
        void A()
        {
            indexY = 1;
            indexX = 1;
            keypad = new char[][]{
            new char[]{'1','2','3'},
            new char[]{'4','5','6'},
            new char[]{'7','8','9'}};
            StringBuilder result = new StringBuilder();
            foreach (string s in File.ReadAllLines("KeypadMoves.txt"))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 'U')
                    {
                        if (indexY > 0)
                        {
                            indexY--;
                        }
                    }
                    if (s[i] == 'R')
                    {
                        if (indexX < 2)
                        {
                            indexX++;
                        }
                    }
                    if (s[i] == 'D')
                    {
                        if (indexY < 2)
                        {
                            indexY++;
                        }
                    }
                    if (s[i] == 'L')
                    {
                        if (indexX > 0)
                        {
                            indexX--;
                        }
                    }
                }
                result.Append(PositionValue(0, 0));
            }
            Console.WriteLine(result.ToString());
        }
        void B()
        {
            indexY = 2;
            indexX = 0;
            keypad = new char[][]{
            new char[]{' ',' ','1',' ',' '},
            new char[]{' ','2','3','4',' '},
            new char[]{'5','6','7','8','9'},
            new char[]{' ','A','B','C',' '},
            new char[]{' ',' ','D',' ',' '}};
            StringBuilder result = new StringBuilder();
            foreach (string s in File.ReadAllLines("KeypadMoves.txt"))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 'U')
                    {
                        if (indexY > 0 && PositionValue(-1, 0) != ' ')
                        {
                            indexY--;
                        }
                    }
                    if (s[i] == 'R')
                    {
                        if (indexX < 4 && PositionValue(0, 1) != ' ')
                        {
                            indexX++;
                        }
                    }
                    if (s[i] == 'D')
                    {
                        if (indexY < 4 && PositionValue(1, 0) != ' ')
                        {
                            indexY++;
                        }
                    }
                    if (s[i] == 'L')
                    {
                        if (indexX > 0 && PositionValue(0, -1) != ' ')
                        {
                            indexX--;
                        }
                    }
                }
                result.Append(PositionValue(0, 0));
            }
            Console.WriteLine(result.ToString());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Day2 main = new Day2('A');
            main = new Day2('B');
            Console.ReadKey();

        }
    }
}
