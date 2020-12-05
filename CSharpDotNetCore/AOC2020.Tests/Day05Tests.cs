using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day05Tests : TestBase
    {
        private readonly List<string> boardingPasses;

        public Day05Tests() : base(5) { boardingPasses = input.linesOfStrings(); }
       
        [Test]
        public void Part1Example()
        {
            const string testData = "FBFBBFFRLR,BFFFBBFRRR,FFFBBBFRRR,BBFFBBFRLL";
            var result = testData.Split(",").Select(Day05.decode).JoinToString();
            result.Should().Be("357,567,119,820");
        }

        [Test]
        public void Part1() =>
            Day05.part1(boardingPasses).Should().Be(861);
        
        [Test]
        public void Part2() =>
            Day05.part2(boardingPasses).Should().Be(633);

    }
}
