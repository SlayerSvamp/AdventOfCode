using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        static void Run(int diskSize)
        {
            var data = "11101000110010100";
            while (data.Length < diskSize)
            {
                data = data + "0" + new string(data.Reverse().Select(c => (c == '1') ? '0' : '1').ToArray());
            }
            data = data.Substring(0, diskSize);
            var checksum = new StringBuilder(data);

            while (checksum.Length % 2 == 0)
            {
                var old = new StringBuilder(checksum.ToString());
                checksum.Length = 0;
                for (int i = 0; i < old.Length; i += 2)
                {
                    checksum.Append((old[i] == old[i + 1]) ? "1" : "0");
                }
            }
            Console.WriteLine($"Checksum == {checksum}");
        }
        static void Main(string[] args)
        {
            Run(272);
            Run(35651584);
            Console.ReadLine();
        }
    }
}
