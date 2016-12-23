using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day5
{
    class Program
    {
        static void A()
        {
            Func<string, string> MD5Hash =
                (value) =>
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                        string hash = string.Join("", (bytes.Select(b => b.ToString("x2")).ToArray()));
                        return hash;
                    }
                };
            string puzzleInput = "uqwqemis";
            int length = puzzleInput.Length;
            StringBuilder password = new StringBuilder();
            StringBuilder input = new StringBuilder(puzzleInput);
            for (int i = 0; password.Length < 8; i++)
            {
                input.Length = length;
                input.Append(i);
                string subHash = MD5Hash(input.ToString()).Substring(0, 6);
                if (subHash.Substring(0, 5) == "00000")
                {
                    password.Append(subHash[5]);
                }
            }
            Console.WriteLine(password.ToString()); //1a3099aa
        }
        static void B()
        {
            Func<string, string> MD5Hash =
                (value) =>
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                        string hash = string.Join("", (bytes.Select(b => b.ToString("x2")).ToArray()));
                        return hash;
                    }
                };
            string puzzleInput = "uqwqemis";
            int length = puzzleInput.Length;
            char[] password = new char[8];
            StringBuilder input = new StringBuilder(puzzleInput);
            for (int i = 0; password.Contains('\0'); i++)
            {
                input.Length = length;
                input.Append(i);
                string subHash = MD5Hash(input.ToString()).Substring(0, 7);
                if (subHash.Substring(0, 5) == "00000")
                {
                    if ("01234567".Contains(subHash[5]))
                    {
                        int index = int.Parse(subHash[5].ToString());
                        if (password[index] == 0)
                        {
                            password[index] = subHash[6];
                        }
                    }
                }
            }
            Console.WriteLine(new string(password)); //694190cd
        }
        static void Main(string[] args)
        {
            bool isTooHeavy = false;
            if (isTooHeavy)
            {
                Console.WriteLine("Program too heavy for glot.io... Aborting. (-_-) *sigh*");
                Console.WriteLine("(But i ran it outside of glot.io; A: 1a3099aa, B: 694190cd)");
            }
            else
            {
                A();
                B();
            }
            Console.ReadKey();
        }
    }
}
