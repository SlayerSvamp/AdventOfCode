using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day07
{
    class Program
    {
        class SSL
        {
            string Data { get; set; }
            List<string> Supers { get; set; }
            List<string> Hypers { get; set; }
            List<string> ABAs { get; set; }

            string ABAtoBAB(string aba)
            {
                return new string(new char[] { aba[1], aba[0], aba[1] });
            }
            public bool IsValid()
            {
                foreach (string super in Supers)
                {
                    for (int i = 0; i < (super.Length - 2); i++)
                    {
                        if (super[i] == super[i + 2] && super[i] != super[i + 1])
                        {
                            ABAs.Add(super.Substring(i, 3));
                        }
                    }
                }
                foreach (string aba in ABAs)
                {
                    if (Hypers.Any(hyper => hyper.Contains(ABAtoBAB(aba))))
                    {
                        return true;
                    }
                }
                return false;
            }
            public SSL(string data)
            {
                Data = data;
                Supers = new List<string>();
                Hypers = new List<string>();
                ABAs = new List<string>();
                bool isSuper = true;
                data.Split(new char[] { '[', ']' }).ToList().ForEach(s => {
                    if (isSuper)
                    {
                        Supers.Add(s);
                    }
                    else
                    {
                        Hypers.Add(s);
                    }
                    isSuper = !isSuper;
                });
            }
        }
        static void Main(string[] args)
        {
            List<string> abbas = new List<string>();
            List<SSL> abas = new List<SSL>();
            foreach (string data in File.ReadAllLines("IPv7.txt"))
            {
                if ((Regex.IsMatch(data, @"\][^\[]*([^\[])([^\[])\2\1(?<!\1\1\1\1)|([^\[])([^\[])\4\3(?<!\3\3\3\3)[^\]]*\["))
                && !Regex.IsMatch(data, @"\[[^\]]*([^\]])([^\]])\2\1(?<!\1\1\1\1)"))
                {
                    abbas.Add(data);
                }
                SSL ssl = new SSL(data);
                if (ssl.IsValid())
                {
                    abas.Add(ssl);
                }
            }
            Console.WriteLine($"Abbas = {abbas.Count()}");
            Console.WriteLine($"Abas = {abas.Count()}");
            /*
            Console.WriteLine("------------------------");
            abbas.ForEach(a => Console.WriteLine(a));
            */

            Console.ReadKey();
        }
    }
}
