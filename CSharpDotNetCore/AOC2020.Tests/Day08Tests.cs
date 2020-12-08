using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day08Tests : TestBase
    {
        private readonly List<string> program;

        public Day08Tests() : base(8) { program = input.linesOfStrings(); }
       
        [Test]
        public void Part1Example()
        {
            var testData = "nop +0,acc +1,jmp +4,acc +3,jmp -3,acc -99,acc +1,jmp -4,acc +6".Split(",").ToList();
            new Day08(testData).part1().Should().Be(5);
        }

        [Test]
        public void Part1() => new Day08(program).part1().Should().Be(1179);

        [Test]
        public void Part2Example()
        {
            var testData = "nop +0,acc +1,jmp +4,acc +3,jmp -3,acc -99,acc +1,jmp -4,acc +6".Split(",").ToList();
            new Day08(testData).part2().Should().Be(8);
        }

        [Test]
        public void Part2() => new Day08(program).part2().Should().Be(1089);

    }
}
