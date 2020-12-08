using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public class OpComputer
    {
        private readonly List<Instruction> program;

        private int position;
        private int accumulator;
        
        public enum Operation
        {
            OpAcc,
            OpJmp,
            OpNoOp,
        }

        public OpComputer(IEnumerable<string> input)
        {
            program = input.Select(Instruction.read).ToList();
        }
        
        private static Operation readOperation(string opString) =>
            opString switch
            {
                "acc" => Operation.OpAcc,
                "jmp" => Operation.OpJmp,
                _ => Operation.OpNoOp
            };

        public readonly struct Instruction
        {
            public readonly Operation Operation;
            public readonly int Parameter;

            private Instruction(string instruction)
            {
                Operation = readOperation(instruction.Split(" ")[0]);
                Parameter = int.Parse(instruction.Split(" ")[1]);
            }

            private Instruction(Operation operation, in int parameter)
            {
                Operation = operation;
                Parameter = parameter;
            }

            public static Instruction read(string instruction) => new Instruction(instruction);

            private string Plus => Parameter > 0 ? "+" : "";

            public override string ToString() => 
                string.Format($"{Operation} {Plus}{Parameter}");

            public Instruction Fix(Operation operation) => new Instruction(operation, Parameter);
        }

        public int RunUntilLooped()
        {
            position = 0;
            accumulator = 0;
            var history = new HashSet<int>();
            while (true)
            {
                if (history.Contains(position)) return accumulator;
                history.Add(position);

                Step();
            }
        }

        public int FixCorruptionAndRun()
        {
            for (var i = 0; i < program.Count; i++)
            {
                var original = program[i];
                switch (program[i].Operation)
                {
                    case Operation.OpAcc:
                        continue;
                    
                    case Operation.OpJmp:
                        program[i] = program[i].Fix(Operation.OpNoOp);
                        break;
                    
                    case Operation.OpNoOp:
                        program[i] = program[i].Fix(Operation.OpJmp);
                        break;
                    
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                position = 0;
                accumulator = 0;
                var history = new HashSet<int>();

                while (!history.Contains(position))
                {
                    if(position>=program.Count) return accumulator;
                    history.Add(position);

                    Step();
                }

                program[i] = original;

            }

            return 0;
        }
        
        private void Step()
        {
            var instruction = program[position];
            switch (instruction.Operation)
            {
                case Operation.OpAcc:
                    accumulator += instruction.Parameter;
                    position++;
                    break;

                case Operation.OpJmp:
                    position += instruction.Parameter;
                    break;

                case Operation.OpNoOp:
                    position++;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

}
