using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    class Program
    {
        enum InstructionType { Draw, RotateRow, RotateColumn }
        class Instruction
        {
            public InstructionType Type { get; set; }
            public int A { get; set; }
            public int B { get; set; }
            public Instruction(string raw)
            {
                string[] segments = raw.Split(' ');
                if (segments[0] == "rect")
                {
                    Type = InstructionType.Draw;
                    var values = segments[1].Split('x');
                    A = int.Parse(values[0]);
                    B = int.Parse(values[1]);
                }
                else
                {
                    A = int.Parse(segments[2].Substring(2));
                    B = int.Parse(segments[4]);
                    if (segments[1] == "row")
                    {
                        Type = InstructionType.RotateRow;
                    }
                    else
                    {
                        Type = InstructionType.RotateColumn;
                    }
                }
            }
        }
        static char[][] CreateDisplay()
        {
            char[][] display = new char[50][];
            for (int a = 0; a < 50; a++)
            {
                display[a] = new char[6];
                for (int b = 0; b < 6; b++)
                {
                    display[a][b] = ' ';
                }
            }
            return display;
        }
        static void Main(string[] args)
        {
            Console.WindowWidth = 104;
            //List<Instruction> instructions = new List<Instruction>(){ new Instruction("rect 3x2"), new Instruction("rotate column y=0 by 1") };
            List<Instruction> instructions = File.ReadAllLines("Draw.txt").Select(l => new Instruction(l)).ToList();
            char[][] display = CreateDisplay();
            foreach (var instruction in instructions)
            {
                if (instruction.Type == InstructionType.Draw)
                {
                    for (int a = 0; a < instruction.A; a++)
                    {
                        for (int b = 0; b < instruction.B; b++)
                        {
                            display[a][b] = '#';
                        }
                    }
                }
                else if (instruction.Type == InstructionType.RotateRow)
                {
                    char[] temp = new char[50];
                    for (int i = 0; i < 50; i++)
                    {
                        temp[(i + instruction.B) % 50] = display[i][instruction.A];
                    }
                    for (int i = 0; i < 50; i++)
                    {
                        display[i][instruction.A] = temp[i];
                    }
                }
                else
                {
                    char[] temp = new char[6];
                    for (int i = 0; i < 6; i++)
                    {
                        temp[(i + instruction.B) % 6] = display[instruction.A][i];
                    }
                    for (int i = 0; i < 6; i++)
                    {
                        display[instruction.A][i] = temp[i];
                    }
                }
            }
            Console.WriteLine();
            var result = 0;
            for (int y = 0; y < 6; y++)
            {
                Console.Write("  ");
                for (int x = 0; x < 50; x++)
                {
                    Console.Write((display[x][y] != ' ') ? "██" : "  ");
                    if (y % 2 == 0 && display[x][y] != ' ')
                    {
                        result++;
                    }
                }
                Console.WriteLine();//"██");
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine($"Result = {result}");

            Console.ReadKey();
        }
    }
}
