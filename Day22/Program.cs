using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    class Program
    {
        static int StepsMade;
        static ConsoleKey Key;
        static List<Node> Nodes;
        static int LengthX { get { return Nodes.Max(node => node.X) + 1; } }
        static int LengthY { get { return Nodes.Max(node => node.Y) + 1; } }
        class Node
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Size { get; set; }
            public int Used { get; set; }
            public int Available { get { return Size - Used; } }
            public bool IsTarget { get; set; } = false;
            public int Use { get { return (Used * 100) / Size; } }
            //  root@ebhq-gridcenter# df -h
            //  Filesystem              Size  Used  Avail  Use%
            //  [0]                     [1]   [2]   [3]    [4]
            public Node(string raw)
            {
                var parts = raw.Split(" T-xy%".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Skip(1).ToList();
                X = int.Parse(parts[0]);
                Y = int.Parse(parts[1]);
                Size = int.Parse(parts[2]);
                Used = int.Parse(parts[3]);
                //Available = int.Parse(parts[4]); //not needed?
                //Use = int.Parse(parts[5]); //not needed either?
            }
        }
        static void RunA()
        {
            Nodes = File.ReadAllLines("discspace.txt").Select(raw => new Node(raw)).ToList();
            var pairs = new List<Tuple<Node, Node>>();
            foreach (var a in Nodes)
            {
                if (a.Used != 0)
                {
                    foreach (var b in Nodes.Where(node => a != node && a.Used <= node.Available))
                    {
                        pairs.Add(new Tuple<Node, Node>(a, b));
                    }
                }
            }
            Console.WriteLine($"{pairs.Count} viable pairs");
        }
        static bool IsWall(Node node, List<Node> nodes)
        {
            return node.Used > nodes.Min(n => n.Size);
        }
        enum Proceeding { Continue, Return, Print }
        static void Reset()
        {
            StepsMade = 0;
            Key = ConsoleKey.Spacebar;
            Nodes = File.ReadAllLines("discspace.txt").Select(raw => new Node(raw)).ToList();
            Nodes.Single(node => node.X == Nodes.Max(n => n.X) && node.Y == 0).IsTarget = true;
        }
        static Proceeding MakeAMove(List<Node> nodes)
        {
            var n1 = nodes.Single(node => node.Used == 0);
            var deltaX = n1.X;
            var deltaY = n1.Y;
            if (Key == ConsoleKey.UpArrow || Key == ConsoleKey.DownArrow || Key == ConsoleKey.LeftArrow || Key == ConsoleKey.RightArrow || Key == ConsoleKey.L)
            {
                if (Key == ConsoleKey.UpArrow && n1.Y > 0)
                    deltaY--;
                else if (Key == ConsoleKey.DownArrow && n1.Y < nodes.Max(node => node.Y))
                    deltaY++;
                else if (Key == ConsoleKey.LeftArrow && n1.X > 0)
                    deltaX--;
                else if (Key == ConsoleKey.RightArrow && n1.X < nodes.Max(node => node.X))
                    deltaX++;
                else if (Key == ConsoleKey.L && n1.X > 1 && n1.Y < nodes.Max(n => n.Y))
                {
                    Key = ConsoleKey.DownArrow;
                    MakeAMove(nodes);
                    Key = ConsoleKey.LeftArrow;
                    MakeAMove(nodes);
                    Key = ConsoleKey.LeftArrow;
                    MakeAMove(nodes);
                    Key = ConsoleKey.UpArrow;
                    MakeAMove(nodes);
                    Key = ConsoleKey.RightArrow;
                    MakeAMove(nodes);
                    Key = ConsoleKey.Spacebar;
                }
                else
                {
                    Key = Console.ReadKey(true).Key;
                    return Proceeding.Continue;
                }

            }
            else if (Key == ConsoleKey.R)
            {
                Reset();
                return Proceeding.Print;
            }
            else if (Key != ConsoleKey.Spacebar)
            {
                return Proceeding.Return;
            }
            var n2 = nodes.Single(node => node.X == deltaX && node.Y == deltaY);
            if (Key != ConsoleKey.Spacebar && !IsWall(n2, nodes))
            {
                StepsMade++;
                n1.Used = n2.Used;
                n1.IsTarget = n2.IsTarget;
                n2.Used = 0;
                n2.IsTarget = false;
            }
            return Proceeding.Print;
        }
        static void RunB()
        {
            Console.WindowHeight = 65;
            Reset();
            do
            {
                switch (MakeAMove(Nodes))
                {
                    case Proceeding.Continue:
                        continue;
                    case Proceeding.Return:
                        Key = Console.ReadKey(true).Key;
                        continue;
                    case Proceeding.Print:
                    default:
                        break;
                }
                Console.SetCursorPosition(0, 0);
                var sb = new StringBuilder();
                for (int y = 0; y <= Nodes.Max(node => node.Y); y++)
                {
                    sb.Length = 0;
                    for (int x = 0; x <= Nodes.Max(node => node.X); x++)
                    {
                        var node = Nodes.Single(n => n.X == x && n.Y == y);

                        if (IsWall(node, Nodes))
                            sb.Append("█");
                        else if (node.IsTarget)
                            sb.Append("☼");
                        else if (node.Used == 0)
                            sb.Append(" ");
                        else
                            sb.Append("o");
                    }
                    Console.WriteLine(sb.ToString());
                }
                Console.Write($"Number of steps made: {StepsMade}");
                var n1 = Nodes.Single(node => node.Used == 0);
                Console.SetCursorPosition(n1.X, n1.Y);
                Key = Console.ReadKey(true).Key;
            }
            while (Key != ConsoleKey.K);
        }
        static void Main(string[] args)
        {
            RunA();
            RunB();
        }
    }
}
