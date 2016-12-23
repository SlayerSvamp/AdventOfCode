using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    class Program
    {
        class Disc
        {
            public int Delay { get; set; }
            public int Positions { get; set; }
            public int StartingPosition { get; set; }
            public bool IsOpenAtTime(int time)
            {
                return 0 == (StartingPosition + Delay + time) % Positions;
            }
        }
        static List<Disc> CreateDiscs()
        {
            return new List<Disc>()
            {
            //*///Linus Input
                new Disc() { Delay = 1, Positions = 17, StartingPosition = 1 },
                new Disc() { Delay = 2, Positions = 7, StartingPosition = 0 },
                new Disc() { Delay = 3, Positions = 19, StartingPosition = 2 },
                new Disc() { Delay = 4, Positions = 5, StartingPosition = 0 },
                new Disc() { Delay = 5, Positions = 3, StartingPosition = 0 },
                new Disc() { Delay = 6, Positions = 13, StartingPosition = 5 },
            /*///Niklas Input
                new Disc() { Delay = 1, Positions = 13, StartingPosition = 10 },
                new Disc() { Delay = 2, Positions = 17, StartingPosition = 15 },
                new Disc() { Delay = 3, Positions = 19, StartingPosition = 17 },
                new Disc() { Delay = 4, Positions = 7, StartingPosition = 1 },
                new Disc() { Delay = 5, Positions = 5, StartingPosition = 0 },
                new Disc() { Delay = 6, Positions = 3, StartingPosition = 1 },
            //*/
                new Disc() { Delay = 7, Positions = 11, StartingPosition = 0 }
            };
        }
        static void Main(string[] args)
        {
            DateTime start;
            var allDiscs = CreateDiscs();
            int time = 0;
            bool isRoundTwo = false;
            while (true)
            {
                while (true)
                {
                    Console.Write("\rSelect test '1' or '2' by pressing one of those keys ('K' to exit)");
                    char key = Console.ReadKey(true).KeyChar;
                    switch (key)
                    {
                        case '1':
                            isRoundTwo = false;
                            break;
                        case '2':
                            isRoundTwo = true;
                            break;
                        case 'k':
                            return;
                        default:
                            break;
                    }
                    if (key == '1' || key == '2')
                    {
                        break;
                    }
                }
                var discs = allDiscs.Where(disc => isRoundTwo || disc.Delay != 7);
                start = DateTime.Now;
                for (time = 0; true; time++)
                {
                    if (!discs.Any(disc => !disc.IsOpenAtTime(time)))
                    {
                        break;
                    }
                }
                var ms = (DateTime.Now - start).Milliseconds;
                Console.Clear();
                Console.WriteLine($"Falling through for the first time after {time} seconds");
                Console.WriteLine($"Program took {ms} ms");
            }
        }
    }
}

