using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day01
{
    class Day01
    {
        bool Debug = false;
        enum RelativeDirection { Left = -1, Right = 1 }
        enum CardinalDirection { North = 0, East = 1, South = 2, West = 3 }
        class Instruction
        {
            public RelativeDirection RelativeDirection { get; private set; }
            public int Blocks { get; private set; }
            public Instruction(string raw)
            {
                if (raw[0] == 'R')
                {
                    RelativeDirection = RelativeDirection.Right;
                }
                else
                {
                    RelativeDirection = RelativeDirection.Left;
                }
                int blocks = Int32.Parse(raw.Substring(1));
                Blocks = blocks;
            }
        }
        int BlocksX { get; set; }
        int BlocksY { get; set; }
        int DoubleDistance { get; set; }
        bool DoubleRegistered = false;
        List<int[]> Positions { get; set; }
        CardinalDirection Direction { get; set; }
        List<Instruction> Instructions { get; set; }
        void WalkDistance(int blocks)
        {
            for (int i = 0; i < blocks; i++)
            {
                switch (Direction)
                {
                    case CardinalDirection.North:
                        BlocksY++;
                        break;
                    case CardinalDirection.East:
                        BlocksX++;
                        break;
                    case CardinalDirection.South:
                        BlocksY--;
                        break;
                    case CardinalDirection.West:
                    default:
                        BlocksX--;
                        break;
                }
                if (CheckIfDouble())
                {
                    if (!DoubleRegistered)
                    {
                        RegisterDoubleDistance();
                        DoubleRegistered = true;
                    }
                }
                RegisterPosition();
            }
        }
        void RegisterPosition()
        {
            Positions.Add(new int[] { BlocksX, BlocksY });
        }
        bool CheckIfDouble()
        {
            return Positions.Any(p => p[0] == BlocksX && p[1] == BlocksY);
        }
        void RegisterDoubleDistance()
        {

            Console.Write("First occurance of double position: ");
            PrintInstructions();
        }
        void SetDirection(RelativeDirection relativeDirection)
        {
            if (Direction == CardinalDirection.North)
            {
                if (relativeDirection == RelativeDirection.Left)
                {
                    Direction = CardinalDirection.West;
                    return;
                }
            }
            else if (Direction == CardinalDirection.West)
            {
                if (relativeDirection == RelativeDirection.Right)
                {
                    Direction = CardinalDirection.North;
                    return;
                }
            }
            Direction = (CardinalDirection)((int)Direction + (int)relativeDirection);
        }
        void PrintInstructions()
        {
            string xDirection = (BlocksX >= 0) ? "east" : "west";
            string yDirection = (BlocksY >= 0) ? "north" : "south";
            Console.WriteLine($"{Math.Abs(BlocksX)} blocks {xDirection} and {Math.Abs(BlocksY)} blocks {yDirection}. A total of {Math.Abs(BlocksX) + Math.Abs(BlocksY)} blocks");
        }
        public Day01()
        {
            Direction = CardinalDirection.North;
            BlocksX = 0;
            BlocksY = 0;
            DoubleDistance = 0;
            Positions = new List<int[]>();
            string input = "L4, L1, R4, R1, R1, L3, R5, L5, L2, L3, R2, R1, L4, R5, R4, L2, R1, R3, L5, R1, L3, L2, R5, L4, L5, R1, R2, L1, R5, L3, R2, R2, L1, R5, R2, L1, L1, R2, L1, R1, L2, L2, R4, R3, R2, L3, L188, L3, R2, R54, R1, R1, L2, L4, L3, L2, R3, L1, L1, R3, R5, L1, R5, L1, L1, R2, R4, R4, L5, L4, L1, R2, R4, R5, L2, L3, R5, L5, R1, R5, L2, R4, L2, L1, R4, R3, R4, L4, R3, L4, R78, R2, L3, R188, R2, R3, L2, R2, R3, R1, R5, R1, L1, L1, R4, R2, R1, R5, L1, R4, L4, R2, R5, L2, L5, R4, L3, L2, R1, R1, L5, L4, R1, L5, L1, L5, L1, L4, L3, L5, R4, R5, R2, L5, R5, R5, R4, R2, L1, L2, R3, R5, R5, R5, L2, L1, R4, R3, R1, L4, L2, L3, R2, L3, L5, L2, L2, L1, L2, R5, L2, L2, L3, L1, R1, L4, R2, L4, R3, R5, R3, R4, R1, R5, L3, L5, L5, L3, L2, L1, R3, L4, R3, R2, L1, R3, R1, L2, R4, L3, L3, L3, L1, L2";
            Instructions = input.Split(',').Select(s => new Instruction(s.Trim())).ToList();
            Instructions.ForEach(i => {
                SetDirection(i.RelativeDirection);
                WalkDistance(i.Blocks);
                if (Debug)
                {
                    PrintInstructions();
                }
            });
            if (!Debug)
            {
                PrintInstructions();
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Day01 main = new Day01();
            Console.ReadKey();
        }
    }
}
