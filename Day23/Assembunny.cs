using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    enum Instruction { Increase, Decrease, Toggle, JumpsNotZero, Copy }
    enum Register { Value, A, B, C, D }
    class Variable
    {
        public int Value { get; set; }
        public Register Name { get; set; }
        public Variable(string raw)
        {
            int value;
            if (int.TryParse(raw, out value))
            {
                Value = value;
                Name = Register.Value;
                return;
            }
            switch (raw)
            {
                case "a":
                    Name = Register.A;
                    break;
                case "b":
                    Name = Register.B;
                    break;
                case "c":
                    Name = Register.C;
                    break;
                case "d":
                    Name = Register.D;
                    break;
            }
        }
    }
    class Assembunny
    {
        public Instruction Instruction { get; set; }
        public Variable First { get; set; }
        public Variable Second { get; set; }
        public Assembunny(string raw)
        {
            var segments = raw.Split(' ');
            if (segments[0] == "inc")
            {
                Instruction = Instruction.Increase;
            }
            else if (segments[0] == "dec")
            {
                Instruction = Instruction.Decrease;
            }
            else if (segments[0] == "jnz")
            {
                Instruction = Instruction.JumpsNotZero;
            }
            else if (segments[0] == "cpy")
            {
                Instruction = Instruction.Copy;
            }
            else if (segments[0] == "tgl")
            {
                Instruction = Instruction.Toggle;
            }
            First = new Variable(segments[1]);
            if (segments.Length == 3)
            {
                Second = new Variable(segments[2]);
            }
        }
    }
}
