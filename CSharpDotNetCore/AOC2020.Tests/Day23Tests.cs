using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day23Tests : TestBase
    {
        private readonly string data;

        public Day23Tests() : base(23)
        {
            data = "853192647";
        }

        private const string testData = "389125467";

        [Test]
        public void Part1Example() => 
            Day23.Part1(testData).Should().Be(67384529);

        [Test]
        public void Part1() =>
            Day23.Part1(data).Should().Be(97624853);

        [Test]
        public void Part2Example() =>
            Day23.Part2(testData).Should().Be(149245887792);

        [Test]
        public void Part2() => Day23.Part2(data).Should().Be(664642452305);

    }
}
