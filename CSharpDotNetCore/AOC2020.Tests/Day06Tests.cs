using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day06Tests : TestBase
    {
        private readonly string answers;

        public Day06Tests() : base(6) { answers = input.readAllText(); }
       
        [Test]
        public void Part1Example()
        {
            const string testData = "abc,,a,b,c,,ab,ac,,a,a,a,a,,b";
            Day06.part1(testData.Replace(",","\n")).Should().Be(11);
        }

        [Test]
        public void Part1() => Day06.part1(answers).Should().Be(7027);

        [Test]
        public void Part2Example()
        {
            const string testData = "abc,,a,b,c,,ab,ac,,a,a,a,a,,b";
            Day06.part2(testData.Replace(",", "\n")).Should().Be(6);
        }


        [Test]
        public void Part2() => Day06.part2(answers).Should().Be(3579);

    }
}
