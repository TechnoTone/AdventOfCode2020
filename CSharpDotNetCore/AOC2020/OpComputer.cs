using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public class OpComputer
    {
        private readonly List<long> memory;
        private int pos;
        private int relativeBase;
        private readonly Queue<long> inputQueue = new Queue<long>();

        public string ReadMemory() => memory.JoinToString();
        public long ReadMemoryPosition(int pos) => memory[pos];
        public void WriteMemoryPosition(int pos, long value) => memory[pos] = value;

        public enum OpCodes
        {
            OpAdd = 1,
            OpMultiply = 2,
            OpInput = 3,
            OpOutput = 4,
            OpJumpIfTrue = 5,
            OpJumpIfFalse = 6,
            OpIsLessThan = 7,
            OpIsEqualTo = 8,
            OpAdjustRelativeBase = 9,
            OpHalt = 99
        }

        public enum ParameterMode
        {
            Position = 0,
            Immediate = 1,
            Relative = 2
        }

        public struct Operation
        {
            public readonly OpCodes opCode;
            public readonly ParameterMode[] parameterModes;
            public long[] parameters;

            public Operation(long value)
            {
                opCode = (OpCodes) (value % 100);
                parameters = Array.Empty<long>();

                if (!Enum.IsDefined(typeof(OpCodes), opCode))
                    throw new UnknownOperationException(opCode);

                parameterModes =
                    value
                        .ToString("00000").ToCharArray().ToList()
                        .ConvertAll(m => (ParameterMode) m - 48)
                        .Take(3)
                        .Reverse()
                        .ToArray();
            }

            public override string ToString() =>
                string.Format($"{opCode}({string.Join(", ", parameters)})");
        }

        public enum Response
        {
            AwaitingInput,
            Output,
            Halt
        }

        public static class RespondWith
        {
            public static Tuple<Response, long> AwaitingInput() =>
                new Tuple<Response, long>(Response.AwaitingInput, 0);

            public static Tuple<Response, long> Output(long value) =>
                new Tuple<Response, long>(Response.Output, value);

            public static Tuple<Response, long> Halt() =>
                new Tuple<Response, long>(Response.Halt, 0);
        }

        public OpComputer(string program)
        {
            memory = program.ParseCommaSeparatedIntegers();
        }

        public OpComputer(List<long> program)
        {
            memory = program;
        }

        public List<long> RunUntilHalt()
        {
            var outputs = new List<long>();
            while (true)
            {
                switch (Run())
                {
                    case (Response.Halt, _):
                        return outputs;

                    case (Response.Output, long value):
                        outputs.Add(value);
                        break;

                    case (Response.AwaitingInput, _):
                        throw new NoInputException();
                }
            }
        }

        public List<long> RunUntilHalt(long input)
        {
            Input(input);
            return RunUntilHalt();
        }

        public List<long> RunUntilHalt(List<long> inputs)
        {
            Input(inputs);
            return RunUntilHalt();
        }

        public (Response, List<long>) RunUntilHaltOrAwaitingInput()
        {
            var outputs = new List<long>();
            while (true)
            {
                switch (Run())
                {
                    case (Response.Halt, _):
                        return (Response.Halt, outputs);

                    case (Response.Output, long value):
                        outputs.Add(value);
                        break;

                    case (Response.AwaitingInput, _):
                        return (Response.AwaitingInput, outputs);
                }
            }
        }

        public Tuple<Response, long> Run()
        {
            while (true)
            {
                // var tmpPos = pos;
                var op = readOp();

                // HelperFunctions.Log("Memory::" + memory.JoinToString());
                // HelperFunctions.Log("Inputs::" + inputQueue.JoinToString());
                // HelperFunctions.Log($"{tmpPos}/{relativeBase}:{op.ToString()}");

                switch (op.opCode)
                {
                    case OpCodes.OpAdd:
                        write(op.parameters[0] + op.parameters[1], op.parameters[2]);
                        break;

                    case OpCodes.OpMultiply:
                        write(op.parameters[0] * op.parameters[1], op.parameters[2]);
                        break;

                    case OpCodes.OpInput:
                        if (hasInput)
                            write(inputQueue.Dequeue(), op.parameters[0]);
                        else
                        {
                            pos -= 2;
                            return RespondWith.AwaitingInput();
                        }
                        break;

                    case OpCodes.OpOutput:
                        return RespondWith.Output(op.parameters[0]);

                    case OpCodes.OpJumpIfTrue:
                        if (op.parameters[0] != 0)
                            jump(op.parameters[1]);
                        break;

                    case OpCodes.OpJumpIfFalse:
                        if (op.parameters[0] == 0)
                            jump(op.parameters[1]);
                        break;

                    case OpCodes.OpIsLessThan:
                        write(op.parameters[0] < op.parameters[1] ? 1 : 0, op.parameters[2]);
                        break;

                    case OpCodes.OpIsEqualTo:
                        write(op.parameters[0] == op.parameters[1] ? 1 : 0, op.parameters[2]);
                        break;

                    case OpCodes.OpAdjustRelativeBase:
                        adjustRelativeBase(op.parameters[0]);
                        break;

                    case OpCodes.OpHalt:
                        return RespondWith.Halt();

                    default:
                        throw new UnknownOperationException(op.opCode);
                }
            }
        }

        private bool hasInput => inputQueue.Count > 0;

        public void Input(long value) => inputQueue.Enqueue(value);
        public void Input(List<long> values) => values.ForEach(Input);

        private Operation readOp()
        {
            var operation = new Operation(read(ParameterMode.Immediate));

            switch (operation.opCode)
            {
                case OpCodes.OpAdd:
                case OpCodes.OpMultiply:
                case OpCodes.OpIsLessThan:
                case OpCodes.OpIsEqualTo:
                    operation.parameters = new[]
                    {
                        read(operation.parameterModes[0]),
                        read(operation.parameterModes[1]),
                        readAddress(operation.parameterModes[2])
                    };
                    break;
                case OpCodes.OpJumpIfTrue:
                case OpCodes.OpJumpIfFalse:
                    operation.parameters = new[]
                    {
                        read(operation.parameterModes[0]),
                        read(operation.parameterModes[1])
                    };
                    break;
                case OpCodes.OpOutput:
                    operation.parameters = new[]
                    {
                        read(operation.parameterModes[0])
                    };
                    break;
                case OpCodes.OpInput:
                    operation.parameters = new[]
                    {
                        readAddress(operation.parameterModes[0])
                    };
                    break;
                case OpCodes.OpAdjustRelativeBase:
                    operation.parameters = new[]
                    {
                        read(operation.parameterModes[0])
                    };
                    break;
                case OpCodes.OpHalt:
                    break;
                default:
                    throw new UnknownOperationException(operation.opCode);
            }

            return operation;
        }

        private long safeRead(long ix) => safeRead((int) ix);
        private long safeRead(int ix) => ix < memory.Count ? memory[ix] : 0;

        private long read(ParameterMode mode) =>
            mode switch
            {
                ParameterMode.Position => safeRead(memory[pos++]),
                ParameterMode.Immediate => safeRead(pos++),
                ParameterMode.Relative => safeRead(relativeBase + memory[pos++]),
                _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
            };

        private long readAddress(ParameterMode mode) =>
            mode switch
            {
                ParameterMode.Relative => read(ParameterMode.Immediate) + relativeBase,
                _ => read(ParameterMode.Immediate)
            };

        private void write(long value, long address) => write(value, (int) address);
        private void write(long value, int address)
        {
            if (memory.Count - 1 < address)
                memory.AddRange(new long[address - memory.Count + 1 ]);

            memory[address] = value;
        }

        private void jump(long newPos)
        {
            if (newPos < 0 || newPos >= memory.Count)
                throw new JumpAddressOutOfRange(newPos);

            pos = (int) newPos;
        }

        private void adjustRelativeBase(long offset) => relativeBase += (int)offset;
    }

    public class UnknownOperationException : Exception
    {
        public OpComputer.OpCodes OpCode { get; }

        public UnknownOperationException(OpComputer.OpCodes opCode) :
            base($"Unknown OperationCode ({opCode}).")
        {
            OpCode = opCode;
        }
    }

    public class JumpAddressOutOfRange : Exception
    {
        public long Address { get; }

        public JumpAddressOutOfRange(long address) :
            base($"Jump operation to invalid address ({address})")
        {
            Address = address;
        }
    }

    public class NoInputException : Exception
    {
    }
}
