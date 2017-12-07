using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{

    class Program
    {
        private static void DisplayState(State state, int moves)
        {
            Console.CursorTop = 4;
            Console.CursorLeft = 1;

            Console.WriteLine(moves);
            Console.WriteLine();
            for (int floor = 4; floor > 0; floor--)
            {
                Console.Write($" F{floor} {(state.Floor == floor ? "E" : ".")}  ");
                for (int atom = 0; atom < state.Items.Count / 2; atom++)
                {
                    Console.Write(state[(Atom)atom, ItemType.Generator].Floor == floor ? $"{((Atom)atom).ToString().Substring(0, 2)}G " : " .  ");
                    Console.Write(state[(Atom)atom, ItemType.Microchip].Floor == floor ? $"{((Atom)atom).ToString().Substring(0, 2)}M " : " .  ");
                }
                Console.WriteLine();
            }
        }

        private static void LoopSolver(InitialState init)
        {
            var root = State.InitialState(init);
            var states = new List<State> { root };
            var newStates = new List<State> { };
            var seen = new List<int>();
            var moves = 0;
            State done = null;
            while (!states.Any(x => x.Done))
            {
                Console.CursorLeft = 1;
                Console.Write($"Testing {++moves} moves...");
                if (!states.Any())
                {
                    Console.WriteLine("wtf?!");
                    Console.ReadLine();
                    break;
                }
                else if (moves >= 175)
                {
                    Console.WriteLine("too high! something is wrong.");
                    Console.ReadLine();
                    break;
                }

                foreach (var state in states.ToArray())
                {
                    foreach (var possible in state.GetPossibleStates())
                    {
                        if (possible.Done)
                        {
                            done = possible;
                            Console.Write(" Found!");
                            break;
                        }
                        if (!seen.Contains(possible.GetHashCode()))
                        {
                            seen.Add(possible.GetHashCode());
                            newStates.Add(possible);
                        }
                    }
                    if (done != null)
                        break;
                }
                if (done != null)
                    break;
                states = newStates;
            }

            var path = new List<State> { done };
            while (done?.Parent != null)
            {
                done = done.Parent;
                path.Add(done);
            }

            moves = 0;
            foreach (var state in path.Reverse<State>())
            {
                DisplayState(state, moves++);
                Console.ReadLine();
            }
            Console.WriteLine();
            Console.WriteLine(" Done!");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine(" Test:");
            LoopSolver(InitialState.Example);
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(" Part 1:");
            LoopSolver(InitialState.Listed);
            Console.Clear();

            Console.WriteLine();
            Console.WriteLine(" Part 2:");
            LoopSolver(InitialState.Real);
        }
    }
}