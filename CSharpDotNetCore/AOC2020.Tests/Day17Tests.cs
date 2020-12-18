using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day17Tests : TestBase
    {
        private readonly List<string> data;

        public Day17Tests() : base(17) { data = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            var testData = ".#.\n..#\n###".Lines();
            Day17.Part1(testData).Should().Be(112);
        }
        
        [Test]
        public void Part1() => Day17.Part1(data).Should().Be(286);

        [Test]
        public void Part2Example()
        {
            var testData = ".#.\n..#\n###".Lines();
            Day17.Part2(testData).Should().Be(848);
        }
        
        [Test]
        public void Part2() => Day17.Part2(data).Should().Be(960);

    }
}
