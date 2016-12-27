using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    class Program
    {
        static void Main(string[] args)
        {
            long A, B, C, D;
            A = B = C = D = 0;
            Action<Register> multiply = (reg) =>
            {
                switch (reg)
                {
                    case Register.A:
                        A *= 2;
                        break;
                    case Register.B:
                        B *= 2;
                        break;
                    case Register.C:
                        C *= 2;
                        break;
                    case Register.D:
                        D *= 2;
                        break;
                    default:
                        throw new Exception("buuuurt");
                }
            };
            Action<Register> divide = (reg) =>
            {
                switch (reg)
                {
                    case Register.A:
                        A /= 2;
                        break;
                    case Register.B:
                        B /= 2;
                        break;
                    case Register.C:
                        C /= 2;
                        break;
                    case Register.D:
                        D /= 2;
                        break;
                    default:
                        throw new Exception("goooorp!");
                }
            };
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
            Action<Register, long> copy = (reg, value) =>
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
            Func<Variable, long> getValue = (variable) =>
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
            Action<Assembunny> toggle = (bunny) =>
             {
                 if (bunny.Instruction == Instruction.Toggle || bunny.Instruction == Instruction.Decrease)
                 {
                     bunny.Instruction = Instruction.Increase;
                 }
                 else if (bunny.Instruction == Instruction.Increase)
                 {
                     bunny.Instruction = Instruction.Decrease;
                 }
                 else if (bunny.Instruction == Instruction.JumpsNotZero)
                 {
                     bunny.Instruction = Instruction.Copy;
                 }
                 else
                 {
                     bunny.Instruction = Instruction.JumpsNotZero;
                 }
             };
            for (long c = 7; c < 13; c += 5)
            {
                var start = DateTime.Now;
                long loops = 0;
                A = c;
                var assembunnies = File.ReadAllLines("Assembunny.txt").Select(line => new Assembunny(line)).ToList();
                for (long i = 0; i < assembunnies.Count; i++)
                {
                    loops++;
                    Assembunny bunny = assembunnies.ElementAt((int)i);
                    switch (bunny.Instruction)
                    {
                        case Instruction.Increase:
                            increase(bunny.First.Name);
                            break;
                        case Instruction.Decrease:
                            decrease(bunny.First.Name);
                            break;
                        case Instruction.JumpsNotZero:
                            long jumpCheckValue = getValue(bunny.First);
                            if (jumpCheckValue == 0)
                            {
                                break;
                            }
                            i += getValue(bunny.Second) - 1;
                            break;
                        case Instruction.Copy:
                            long copyValue = getValue(bunny.First);
                            copy(bunny.Second.Name, copyValue);
                            break;
                        case Instruction.Toggle:
                            long index = assembunnies.IndexOf(bunny) + getValue(bunny.First);
                            if (index >= 0 && index < assembunnies.Count)
                            {
                                var toggleBunny = assembunnies.ElementAt((int)index);
                                toggle(toggleBunny);
                            }
                            break;
                        default:
                            throw new Exception("blaargh!");
                    }
                }
                Console.WriteLine($"In {c}: A = {A} - {DateTime.Now - start}");
            }
            Console.ReadLine();
        }
    }
}
