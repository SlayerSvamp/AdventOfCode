using System;
using System.Collections.Generic;
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
            var elves = new List<bool>();
            for (int i = 0; i < numElves; i++)
            {
                elves.Add(true);
            }
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
                Console.Write($"{elves.Count(elf => elf == true)} - {loops++}         \r");
            }
            Console.WriteLine($"The winning elf is number {elves.IndexOf(true) + 1}");
        }
        static void RunB()
        {

            var numElves = 3005290;
            var elves = new LinkedList<int>();
            for (int i = 0; i < numElves; i++)
            {
                elves.AddLast(i);
            }
            var latest = DateTime.Now;
            while (elves.Count > 1)
            {
                for (var i = 0; i < elves.Count;)
                {
                    var opposingIndex = (i + elves.Count / 2) % elves.Count;
                    var isAfter = opposingIndex > i;
                    if (elves.Count % 1000 == 0)
                    {
                        Console.Write("\r                                                        \r");
                        Console.Write($"Removing {Math.Floor(1000000/(DateTime.Now-latest).TotalMilliseconds)} elves/second, {((numElves-elves.Count)*100)/numElves}% done, isAfter = {isAfter}");
                        latest = DateTime.Now;
                    }
                    elves.Remove(elves.ElementAt(opposingIndex));
                    if(isAfter)
                    {
                        i++;
                    }
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
