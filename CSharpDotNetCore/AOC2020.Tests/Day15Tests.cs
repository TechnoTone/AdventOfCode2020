using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day15Tests : TestBase
    {
        private readonly List<long> data;

        public Day15Tests() : base(15) { data = input.commaSeparatedIntegers(); }

        [Test]
        public void Part1Example1()
        {
            var testData = "0,3,6".ParseCommaSeparatedIntegers();
            Day15.Part1(testData).Should().Be(436);
        }
        
        [Test]
        public void Part1() => Day15.Part1(data).Should().Be(706);

        [Test]
        public void Part2Example()
        {
            var testData = "0,3,6".ParseCommaSeparatedIntegers();
            Day15.Part2(testData).Should().Be(175594);
        }

        [Test]
        public void Part2() => Day15.Part2(data).Should().Be(19331);

    }
}
