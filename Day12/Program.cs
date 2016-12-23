using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            int A, B, C, D;
            A = B = C = D = 0;
            Action<Register> increase = (reg) =>
            {
                switch (reg)
                {
                    case Register.A:
                        A++;
                        break;
                    case Register.B:
                        B++;
                        break;
                    case Register.C:
                        C++;
                        break;
                    case Register.D:
                        D++;
                        break;
                    default:
                        throw new Exception("buuuurt");
                }
            };
            Action<Register> decrease = (reg) =>
            {
                switch (reg)
                {
                    case Register.A:
                        A--;
                        break;
                    case Register.B:
                        B--;
                        break;
                    case Register.C:
                        C--;
                        break;
                    case Register.D:
                        D--;
                        break;
                    default:
                        throw new Exception("goooorp!");
                }
            };
            Action<Register, int> copy = (reg, value) =>
            {
                switch (reg)
                {
                    case Register.A:
                        A = value;
                        break;
                    case Register.B:
                        B = value;
                        break;
                    case Register.C:
                        C = value;
                        break;
                    case Register.D:
                        D = value;
                        break;
                    default:
                        throw new Exception("meeeep!");
                }
            };
            Func<Variable, int> getValue = (variable) =>
                {
                    switch (variable.Name)
                    {
                        case Register.A:
                            return A;
                        case Register.B:
                            return B;
                        case Register.C:
                            return C;
                        case Register.D:
                            return D;
                        case Register.Value:
                            return variable.Value;
                        default:
                            throw new Exception("wrooong");
                    }
                };
            for (int c = 0; c < 2; c++)
            {
                C = c;
                var assembunnies = File.ReadAllLines("Assembunny.txt").Select(line => new Assembunny(line)).ToList();
                for (int i = 0; i < assembunnies.Count; i++)
                {
                    Assembunny bunny = assembunnies[i];
                    switch (bunny.Instruction)
                    {
                        case Instruction.Increase:
                            increase(bunny.First.Name);
                            break;
                        case Instruction.Decrease:
                            decrease(bunny.First.Name);
                            break;
                        case Instruction.JumpsNotZero:
                            int jumpCheckValue = getValue(bunny.First);
                            if (jumpCheckValue == 0)
                            {
                                break;
                            }
                            i += bunny.Second.Value - 1;
                            break;
                        case Instruction.Copy:
                            int copyValue = getValue(bunny.First);
                            copy(bunny.Second.Name, copyValue);
                            break;
                        default:
                            throw new Exception("blaargh!");
                    }
                }
                Console.WriteLine($"A = {A}");
            }
            Console.ReadKey();
        }
    }
}
