using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day09Tests : TestBase
    {
        private readonly List<long> data;

        public Day09Tests() : base(9) { data = input.linesOfLongs(); }
       
        [Test]
        public void Part1Example()
        {
            var testData = new List<long>
            {
                35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            };
            Day09.Part1(testData,5).Should().Be(127);
        }

        [Test]
        public void Part1() => Day09.Part1(data, 25).Should().Be(1504371145);

        [Test]
        public void Part2Example()
        {
            var testData = new List<long>
            {
                35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576
            };
            Day09.Part2(testData, 127).Should().Be(62);
        }

        [Test]
        public void Part2() => Day09.Part2(data, 1504371145).Should().Be(183278487);

    }
}
