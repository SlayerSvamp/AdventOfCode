using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    class Program
    {
        static bool Debug = false;
        static string Input;
        static bool unscramble = false;

        private static void Swap(string[] scramble)
        {
            int X;
            int Y;
            //swap position X with position Y means that the letters at indexes X and Y (counting from 0) should be swapped.
            if (scramble[1] == "position")
            {
                X = int.Parse(scramble[2]);
                Y = int.Parse(scramble[5]);
            }
            //swap letter X with letter Y means that the letters X and Y should be swapped (regardless of where they appear in the string).
            else
            {
                X = Input.IndexOf(scramble[2]);
                Y = Input.IndexOf(scramble[5]);
            }
            var swap = Input.ToCharArray();
            swap[X] = Input[Y];
            swap[Y] = Input[X];
            Input = new string(swap);
        }
        static string RotateString(string str, int num)
        {
            while (num < 0)
            {
                num += str.Length;
            }
            num %= str.Length;
            return $"{str.Substring(str.Length - num)}{str.Substring(0, str.Length - num)}";
        }
        static void LetterBasedRotation(string[] scramble)
        {
            var num = Input.IndexOf(scramble[6]);
            //rotate based on position of letter X means that the whole string should be rotated to the right based on the index of letter X (counting from 0) as determined before this instruction does any rotations. Once the index is determined, rotate the string to the right one time, plus a number of times equal to that index, plus one additional time if the index was at least 4.
            if (unscramble)
            {
                unscramble = false;
                var input = Input;
                int i = 0;
                do
                {
                    i--;
                    Input = input;
                    Input = RotateString(Input, i);
                    LetterBasedRotation(scramble);
                } while (input != Input) ;
                Input = RotateString(Input, i);
                unscramble = true;
            }
            else
            {
                num += 1;
                if (num > 4)
                {
                    num++;
                }
                Input = RotateString(Input, num);
            }
        }
        private static void Rotate(string[] scramble)
        {
            //rotate left/right X steps means that the whole string should be rotated; for example, one right rotation would turn abcd into dabc.
            var num = int.Parse(scramble[2]);

            if (unscramble != (scramble[1] == "left"))
            {
                num = Input.Length - num;
            }
            Input = RotateString(Input, num);
        }
        private static void Reverse(string[] scramble)
        {
            //reverse positions X through Y means that the span of letters at indexes X through Y (including the letters at X and Y) should be reversed in order.
            var from = int.Parse(scramble[2]);
            var to = int.Parse(scramble[4]);
            var sub = Input.Substring(from, to - from + 1);
            sub = new string(sub.Reverse().ToArray());
            Input = $"{Input.Substring(0, from)}{sub}{Input.Substring(to + 1)}";
        }
        private static void Move(string[] scramble)
        {
            //move position X to position Y means that the letter which is at index X should be removed from the string, then inserted such that it ends up at index Y.
            var X = int.Parse(scramble[2]);
            var Y = int.Parse(scramble[5]);
            if (unscramble)
            {
                var swap = X;
                X = Y;
                Y = swap;
            }
            var input = Input.ToList();
            var c = input[X];
            input.RemoveAt(X);
            Input = $"{string.Join("", input).Substring(0, Y)}{c}{string.Join("", input).Substring(Y)}";
        }
        static void Run()
        {
            var scrambles = File.ReadAllLines("scrambles.txt").Select(raw => raw.Split(' ')).ToList();
            if (unscramble)
            {
                scrambles.Reverse();
            }
            Scramble(scrambles);
            Console.WriteLine(Input);
            if (unscramble)
            {
                if (new string[] { "egchfdab", "fgachbde", "bdfecgah", "hefadgcb", "fgcaedbh", "fbgdceah", "defbahgc" }.Any(s => s == Input))
                {
                    Console.WriteLine("wrong!");
                }
            }
        }

        private static void Scramble(List<string[]> scrambles)
        {
            foreach (var scramble in scrambles)
            {
                switch (scramble[0])
                {
                    case "swap":
                        Swap(scramble);
                        break;
                    case "rotate":
                        if (scramble[1] == "based")
                        {
                            LetterBasedRotation(scramble);
                        }
                        else
                        {
                            Rotate(scramble);
                        }
                        break;
                    case "reverse":
                        Reverse(scramble);
                        break;
                    case "move":
                        Move(scramble);
                        break;
                }
            }
        }

        static void TestRun()
        {
            Input = "abcde";
            var scrambles = File.ReadAllLines("test.txt").Select(raw => raw.Split(' ')).ToList();

            Scramble(scrambles);
            Console.WriteLine(Input);

            scrambles.Reverse();
            unscramble = true;
            Scramble(scrambles);
            Console.WriteLine(Input);

        }
        static void Main(string[] args)
        {
            if (Debug)
            {
                TestRun();
            }
            else
            {
                Input = "abcdefgh";
                Run();
                unscramble = true;
                Input = "fbgdceah";
                Run();
            }

            Console.ReadLine();
        }
    }
}
