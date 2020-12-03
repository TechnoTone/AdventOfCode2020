using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day01Tests : TestBase
    {
        private readonly List<int> expenses;

        public Day01Tests() : base(1) { expenses = input.linesOfIntegers(); }

        [Test]
        public void Part1Example()
        {
            var expenses = new List<int>
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };

            Day01.findSumOfPair(expenses).Should().Be(514579);
        }

        [Test]
        public void Part2Example()
        {
            var expenses = new List<int>
            {
                1721,
                979,
                366,
                299,
                675,
                1456
            };

            Day01.findSumOfTriplet(expenses).Should().Be(241861950);
        }


        [Test]
        public void Part1() =>
            Day01.findSumOfPair(expenses).Should().Be(444019);

        [Test]
        public void Part2() =>
            Day01.findSumOfTriplet(expenses).Should().Be(29212176);

    }
}
