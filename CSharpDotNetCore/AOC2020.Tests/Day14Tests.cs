using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day14Tests : TestBase
    {
        private readonly List<string> data;

        public Day14Tests() : base(14) { data = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            var testData = "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X\nmem[8] = 11\nmem[7] = 101\nmem[8] = 0".Lines();
            Day14.Part1(testData).Should().Be(165);
        }
        
        [Test]
        public void Part1() => Day14.Part1(data).Should().Be(4886706177792);

        [Test]
        public void Part2Example()
        {
            var testData = "mask = 000000000000000000000000000000X1001X\nmem[42] = 100\nmask = 00000000000000000000000000000000X0XX\nmem[26] = 1".Lines();
            Day14.Part2(testData).Should().Be(208);
        }

        [Test]
        public void Part2() => Day14.Part2(data).Should().Be(3348493585827);

    }
}
