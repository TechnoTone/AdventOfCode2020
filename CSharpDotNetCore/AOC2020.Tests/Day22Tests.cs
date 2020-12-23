using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day22Tests : TestBase
    {
        private readonly string data;
        
        public Day22Tests() : base(22) { data = input.readAllText(); }

        private const string testData =
            "Player 1:\n9\n2\n6\n3\n1\n\nPlayer 2:\n5\n8\n4\n7\n10";

        [Test]
        public void Part1Example() => 
            Day22.Part1(testData).Should().Be(306);

        [Test]
        public void Part1() => Day22.Part1(data).Should().Be(34566);
        
        [Test]
        public void Part2Example()
        {
            Day22.Part2(testData).Should().Be(291);
        }
        
        [Test]
        public void Part2InfiniteLoopExample()
        {
            const string loopTestData = "Player 1:\n43\n19\n\nPlayer 2:\n2\n29\n14";
            Day22.Part2(loopTestData).Should().BePositive();
        }
        
        [Test]
        public void Part2() => Day22.Part2(data).Should().Be(31854);
        
    }
}
