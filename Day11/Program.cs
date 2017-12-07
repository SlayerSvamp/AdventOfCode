using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{

    class Program
    {
        private static void DisplayState(State state, int moves)
        {
            Console.CursorTop = 5;
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
            var clock = new Stopwatch();
            clock.Start();
            while (!states.Any(x => x.Done))
            {
                Console.CursorLeft = 1;
                Console.Write($"Testing {++moves} moves...");

                Parallel.ForEach(states.ToArray(), (state, loopState) => //var state in states.ToArray())
                //states.ToList().ForEach((state) =>
                {
                    foreach (var possible in state.GetPossibleStates())
                    {
                        if (possible.Done && done == null)
                        {
                            done = possible;
                            Console.WriteLine(" Found!");
                            Console.WriteLine($" It took {(clock.Elapsed.Seconds > 10 ? $"{clock.Elapsed.Minutes}m {clock.Elapsed.Seconds}s" : $"{clock.ElapsedMilliseconds / 1000d} seconds")}");

                            loopState.Break();
                        }
                        if (!seen.Contains(possible.GetHashCode()))
                        {
                            seen.Add(possible.GetHashCode());
                            newStates.Add(possible);
                        }
                    }
                });

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
                if (path.First() != state)
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