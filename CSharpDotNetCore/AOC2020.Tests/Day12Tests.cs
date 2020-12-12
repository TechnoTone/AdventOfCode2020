using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day12Tests : TestBase
    {
        private readonly List<string> instructions;

        public Day12Tests() : base(12) { instructions = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            var testData = "F10\nN3\nF7\nR90\nF11".Lines();
            Day12.Part1(testData).Should().Be(25);
        }
        
        [Test]
        public void Part1() => Day12.Part1(instructions).Should().Be(820);

        [Test]
        public void Part2Example1()
        {
            var testData = "F10\nN3\nF7\nR90\nF11".Lines();
            Day12.Part2(testData).Should().Be(286);
        }

        [Test]
        public void Part2() => Day12.Part2(instructions).Should().Be(66614);

    }
}
