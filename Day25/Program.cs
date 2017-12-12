using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    enum Operation
    {
        Toggle,
        Increment,
        Decrement,
        Copy,
        JumpIfNotZero,
        Output
    }
    class Argument
    {
        public readonly long? Value = null;
        public char Register { get; }

        public Argument(string data)
        {
            if (long.TryParse(data, out long value))
                Value = value;
            else
                Register = data[0];
        }

    }
    class Instruction
    {
        readonly IDictionary<char, long> registers;
        public Argument Arg1 { get; }
        public Argument Arg2 { get; }
        public Operation Operation { get; set; }
        public long Value1
        {
            get => Arg1.Value ?? registers[Arg1.Register];
            set => registers[Arg1.Register] = value;
        }
        public long Value2
        {
            get => Arg2.Value ?? registers[Arg2.Register];
            set => registers[Arg2.Register] = value;
        }
        public bool TwoArgs => Arg2 != null;
        public Instruction(string raw, IDictionary<char, long> registers)
        {
            this.registers = registers;
            var parts = raw.Split(' ');
            switch (parts[0])
            {
                case "cpy": Operation = Operation.Copy; break;
                case "inc": Operation = Operation.Increment; break;
                case "dec": Operation = Operation.Decrement; break;
                case "jnz": Operation = Operation.JumpIfNotZero; break;
                case "tgl": Operation = Operation.Toggle; break;
                case "out": Operation = Operation.Output; break;
            }
            Arg1 = new Argument(parts[1]);

            if (parts.Length > 2)
                Arg2 = new Argument(parts[2]);
            else
                Arg2 = null;
        }
    }
    static class Program
    {
        static IEnumerable<long> Transmission(List<Instruction> instructions)
        {
            //cpy [0-9a-z] [a-z]
            //jnz [0-9a-z] [0-9]
            //inc [a-z]
            //dec [a-z]
            //out [a-z]
            for (int i = 0; i >= 0 && i < instructions.Count; i++)
            {
                var instruction = instructions[i];
                switch (instruction.Operation)
                {
                    case Operation.Toggle:
                        var index = (int)instruction.Value1 + i;
                        if (index >= 0 && index < instructions.Count)
                        {
                            var toggled = instructions[index];
                            if (toggled.TwoArgs)
                                if (toggled.Operation == Operation.JumpIfNotZero)
                                    toggled.Operation = Operation.Copy;
                                else
                                    toggled.Operation = Operation.JumpIfNotZero;
                            else if (toggled.Operation == Operation.Increment)
                                toggled.Operation = Operation.Decrement;
                            else
                                toggled.Operation = Operation.Increment;
                        }
                        break;
                    case Operation.Increment:
                        instruction.Value1++;
                        break;
                    case Operation.Decrement:
                        instruction.Value1--;
                        break;
                    case Operation.Copy:
                        instruction.Value2 = instruction.Value1;
                        break;
                    case Operation.JumpIfNotZero:
                        if (instruction.Value1 != 0)
                            i += (int)instruction.Value2 - 1;
                        break;
                    case Operation.Output:
                        yield return instruction.Value1;
                        break;
                }
            }
            yield return 2;
        }
        static bool TransmissionWorks(List<Instruction> instructions, int limit, out int i)
        {
            i = 0;
            var expected = 0L;
            foreach (var bit in Transmission(instructions).Take(limit))
            {
                i++;
                if (bit != expected)
                    return false;
                expected ^= 1;
            }

            return true;
        }
        static long GetLowestInitializer()
        {
            var registers = new Dictionary<char, long>();
            var instructions = File.ReadAllLines("input.txt").Select(x => new Instruction(x, registers)).ToList();
            var max = 0;
            for (long i = 1; ; i++)
            {
                registers['a'] = i;
                if (TransmissionWorks(instructions, 20, out int length))
                    return i;
            }
        }
        static void Main(string[] args)
        {
            var lowest = GetLowestInitializer();

            Console.WriteLine($"\n Part 1:\r\n {lowest}");

            Console.ReadLine();
        }
    }
}
