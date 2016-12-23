using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    class Range
    {
        public long Lower { get; set; }
        public long Upper { get; set; }
        public bool InRange(long number) { return (number >= Lower && number <= Upper); }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var start = DateTime.Now;
            var ranges = File.ReadAllLines("ip.txt").Select(raw =>
            {
                var segments = raw.Split('-');
                return new Range() { Lower = long.Parse(segments[0]), Upper = long.Parse(segments[1]) };
            }).OrderBy(range => range.Upper).ToList();
            var allowed = new List<long>();
            int numRanges;
            do
            {
                numRanges = ranges.Count();
                foreach (var range in ranges.ToList())
                {
                    if (ranges.Contains(range))
                    {
                        var entangled = ranges.Where(r => range.InRange(r.Lower-1) && r != range).ToList();
                        if (entangled.Count != 0)
                        {
                            long max = entangled.Max(r => r.Upper);
                            range.Upper = range.Upper < max ? max : range.Upper;
                            ranges.RemoveAll(r => entangled.Contains(r));
                        }
                    }
                }
            } while (ranges.Count != numRanges);
            Console.WriteLine($"Number of ranges = {ranges.Count}");
            for (long i = 0; i < 4294967295; i++)
            {
                ranges.RemoveAll(range => range.Upper < i);
                var blocked = ranges.LastOrDefault(range => range.InRange(i));
                if (blocked != null)
                {
                    i = blocked.Upper;
                }
                else
                {
                    allowed.Add(i);
                }
            }
            var end = DateTime.Now;
            Console.WriteLine();
            Console.WriteLine($"First allowed IP: {allowed.First()}");
            Console.WriteLine($"Number of allowed addresses: {allowed.Count}");
            Console.WriteLine();
            Console.WriteLine($"Runtime: {(int)(end-start).TotalMilliseconds} milliseconds");
            Console.ReadLine();
        }
    }
}
