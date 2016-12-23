using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static string MD5Hash(string value, int repeatHash = 0)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                string hash = string.Join("", (bytes.Select(b => b.ToString("x2")).ToArray()));
                return hash;
            }
        }
        class Hit
        {
            public int Minutes { get; set; }
            public int Seconds { get; set; }
            public char Character { get; set; }
            public int Index { get; set; }


        }
        static void Main(string[] args)
        {
            for (int secondWind = 0; secondWind < 2; secondWind++)
            {
                DateTime start = DateTime.Now;
                MD5 md5Hash = MD5.Create();
                var savedHits = new List<Hit>();
                var prospectHits = new List<Hit>();
                string salt = "ahsbgdzn";
                Regex tripRegex = new Regex(@"(.)\1\1");
                for (int i = 0; true; i++)
                {
                    string hash = $"{salt}{i}";
                    for (int j = 0; j < (1 + (secondWind == 1 ? 2016 : 0)); j++)
                    {
                        byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(hash));
                        hash = string.Join("", (bytes.Select(b => b.ToString("x2")).ToArray()));
                    }
                    //prospect checks
                    if (prospectHits.Count > 0)
                    {
                        prospectHits.RemoveAll(hit => (hit.Index + 1000) < i);
                        var match = Regex.Match(hash, $@"(.)\1\1\1\1").Value;
                        char c = '\0';
                        if (match.Length > 0)
                        {
                            c = match[0];
                            var hits = prospectHits.Where(prospect => prospect.Character == c).ToList();
                            //var hits = prospectHits.Where(prospect => Regex.IsMatch(hash, $@"({prospect.Character})\1\1\1\1")).ToList();
                            if (hits.Count > 0)
                            {
                                savedHits.AddRange(hits);
                                Console.WindowHeight = savedHits.Count + 2;
                                Console.Clear();
                                savedHits = savedHits.OrderBy(index => index.Index).ToList();
                                savedHits.ForEach(hit => Console.WriteLine($"{savedHits.IndexOf(hit)}: {hit.Index} - {hit.Character}\t\t{(hit.Minutes > 9 ? $"{hit.Minutes}" : $"0{hit.Minutes}")}:{(hit.Seconds > 9 ? $"{hit.Seconds}" : $"0{hit.Seconds}")}"));
                            }
                            if (savedHits.Count >= 64)
                            {
                                TimeSpan time = DateTime.Now - start;
                                Console.WriteLine($"64th index = {savedHits.ElementAt(63).Index}\t{(time.Minutes > 9 ? $"{time.Minutes}" : $"0{time.Minutes}")}:{(time.Seconds > 9 ? $"{time.Seconds}" : $"0{time.Seconds}")}");
                                break;
                            }
                            prospectHits.RemoveAll(pros => hits.Contains(pros));
                            hits.Clear();
                        }
                    }
                    //adding prospects
                    var tri = tripRegex.Match(hash);
                    if (tri.Success)
                    {
                        TimeSpan time = DateTime.Now - start;
                        prospectHits.Add(new Hit() { Index = i, Character = tri.Value[0], Minutes = time.Minutes, Seconds = time.Seconds });
                    }
                }
                Console.WindowHeight = savedHits.Count + 3;
                Console.Write($"Press 'K' to {(secondWind == 0 ? "run B" : "End")}");
                while (Console.ReadKey().KeyChar != 'k')
                {

                }
                Console.Clear();
            }
        }
    }
}
