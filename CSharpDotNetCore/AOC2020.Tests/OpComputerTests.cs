using FluentAssertions;
using NUnit.Framework;
using static AOC2020.OpComputer;
using static AOC2020.OpComputer.OpCodes;
using static AOC2020.OpComputer.ParameterMode;

namespace AOC2020.Tests
{
    public class OpComputerTests
    {
        [Test]
        [TestCase(1, OpAdd, Position, Position, Position)]
        [TestCase(2, OpMultiply, Position, Position, Position)]
        [TestCase(3, OpInput, Position, Position, Position)]
        [TestCase(4, OpOutput, Position, Position, Position)]
        [TestCase(5, OpJumpIfTrue, Position, Position, Position)]
        [TestCase(6, OpJumpIfFalse, Position, Position, Position)]
        [TestCase(7, OpIsLessThan, Position, Position, Position)]
        [TestCase(8, OpIsEqualTo, Position, Position, Position)]
        [TestCase(9, OpAdjustRelativeBase, Position, Position, Position )]
        [TestCase(99, OpHalt, Position, Position, Position)]
        [TestCase(101, OpAdd, Immediate, Position, Position)]
        [TestCase(10002, OpMultiply, Position, Position, Immediate)]
        public void OperationTests(
            int input,
            OpCodes opCode,
            ParameterMode mode1,
            ParameterMode mode2,
            ParameterMode mode3)
        {
            var operation = new Operation(input);
            operation.opCode.Should().Be(opCode);
            operation.parameterModes.Length.Should().Be(3);
            operation.parameterModes[0].Should().Be(mode1);
            operation.parameterModes[1].Should().Be(mode2);
            operation.parameterModes[2].Should().Be(mode3);
        }

        [Test]
        [TestCase("99", null, null, "99")]

        //Day02
        [TestCase(
            "1,9,10,3,2,3,11,0,99,30,40,50",
            null, null,
            "3500,9,10,70,2,3,11,0,99,30,40,50")]
        [TestCase("1,0,0,0,99", null, null, "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", null, null, "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", null, null, "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", null, null, "30,1,1,4,2,5,6,0,99")]

        //Day05
        [TestCase("3,0,4,0,99", "1", "1", null)]
        [TestCase("1002,4,3,4,33", null, null, "1002,4,3,4,99")]
        [TestCase("1101,100,-1,4,0", null, null, "1101,100,-1,4,99")]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", "7", "0", null)]
        [TestCase("3,9,8,9,10,9,4,9,99,-1,8", "8", "1", null)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", "7", "1", null)]
        [TestCase("3,9,7,9,10,9,4,9,99,-1,8", "8", "0", null)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", "7", "0", null)]
        [TestCase("3,3,1108,-1,8,3,4,3,99", "8", "1", null)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", "7", "1", null)]
        [TestCase("3,3,1107,-1,8,3,4,3,99", "8", "0", null)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", "0", "0", null)]
        [TestCase("3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9", "1", "1", null)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", "0", "0", null)]
        [TestCase("3,3,1105,-1,9,1101,0,0,12,4,12,99,1", "1", "1", null)]
        [TestCase(
            "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99",
            "7", "999", null)]
        [TestCase(
            "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99",
            "8", "1000", null)]
        [TestCase(
            "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99",
            "9", "1001", null)]

        //Day09
        [TestCase("109,2000,99", null, null, null)]
        [TestCase(
            "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", null,
            "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99", null)]
        [TestCase("1102,34915192,34915192,7,4,7,99,0", null, null, null)]
        [TestCase("104,1125899906842624,99", null, "1125899906842624", null)]
        public void ProgramTests(string program, string programInputs, string expectedOutputs, string programMemory)
        {
            var computer = new OpComputer(program);

            if (!string.IsNullOrEmpty(programInputs))
                computer.Input(programInputs.ParseCommaSeparatedIntegers());

            var programResult = computer.RunUntilHalt();

            if (!string.IsNullOrEmpty(expectedOutputs))
            {
                var expectedOutputsParsed = expectedOutputs.ParseCommaSeparatedIntegers();
                programResult.Count.Should().Be(expectedOutputsParsed.Count);
                programResult.Should().BeEquivalentTo(expectedOutputsParsed);
            }

            if (!string.IsNullOrEmpty(programMemory))
                computer.ReadMemory().Should().Be(programMemory);
        }

        [Test]
        public void BUG_ComputerShouldRetryReadInputIfNoInputAvailable()
        {
            //This didn't cause an issue until Day 13!
            //Adding this test case for this bug

            var computer = new OpComputer("3,100,99");
            computer.Run();
            var result = computer.Run();

            //This is what was failing
            //The program would resume from the next command instead
            //of retrying the read input command
            result.Item1.Should().Be(Response.AwaitingInput);
        }
    }
}
