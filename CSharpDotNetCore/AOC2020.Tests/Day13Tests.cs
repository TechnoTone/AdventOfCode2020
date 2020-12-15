using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day13Tests : TestBase
    {
        private readonly List<string> data;

        public Day13Tests() : base(13) { data = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            var testData = "939\n7,13,x,x,59,x,31,19".Lines();
            Day13.Part1(testData).Should().Be(295);
        }
        
        [Test]
        public void Part1() => Day13.Part1(data).Should().Be(1915);

        [Test]
        public void Part2Example1()
        {
            var testData = "939\n7,13,x,x,59,x,31,19".Lines();
            Day13.Part2(testData).Should().Be(1068781);
        }

        [Test]
        public void Part2Example2()
        {
            var testData = "\n1789,37,47,1889".Lines();
            Day13.Part2(testData).Should().Be(1202161486);
        }
        
        [Test]
        public void Part2() => Day13.Part2(data).Should().Be(294354277694107);

    }
}
