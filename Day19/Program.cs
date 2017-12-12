using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        static void RunA()
        {
            var numElves = 3005290;
            var elves = Enumerable.Range(0, numElves).Select(x => true).ToList();
            var loops = 1;
            var isTaking = false;
            while (elves.Count(elf => elf) > 1)
            {
                for (var i = 0; i < elves.Count; i++)
                {
                    if (elves[i])
                    {
                        if (isTaking)
                        {
                            elves[i] = false;
                            isTaking = false;
                        }
                        else
                        {
                            isTaking = true;
                        }
                    }
                }
                Console.Write($"{elves.Count(elf => elf)} - {loops++}         \r");
            }
            Console.WriteLine($"The winning elf is number {elves.IndexOf(true) + 1}");
        }
        static void RunB()
        {

            var numElves = 3005290;
            var elves = Enumerable.Range(0, numElves).ToList();
            var clock = new Stopwatch();
            clock.Start();
            while (elves.Count > 1)
            {
                for (var i = 0; i < elves.Count;)
                {
                    var opposingIndex = (i + elves.Count / 2) % elves.Count;
                    if (elves.Count % 1000 == 0)
                    {
                        Console.Write("\r                                                        \r");
                        Console.Write($"Removing {1000000 / (clock.ElapsedMilliseconds + 1)} elves/second, {((numElves - elves.Count) * 1000) / numElves / 10d}% done");
                        clock.Restart();
                    }
                    elves.RemoveAt(opposingIndex);
                    if (opposingIndex > i) // if the opposing index is lower, prevents stepping over an elf
                        i++;
                }
            }
            Console.WriteLine($"\r                                                             \rThe winning elf is number {elves.First() + 1}");
        }
        static void Main(string[] args)
        {
            RunA();
            RunB();
            Console.ReadLine();
        }
    }
}
