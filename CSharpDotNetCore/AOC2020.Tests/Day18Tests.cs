using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day18Tests : TestBase
    {
        private readonly List<string> data;

        public Day18Tests() : base(18) { data = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            const string testData = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Day18.Evaluate1(testData).Should().Be(12240);
        }
        
        [Test]
        public void Part1() => Day18.Part1(data).Should().Be(464478013511);

        [Test]
        public void Part2Example()
        {
            const string testData = "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))";
            Day18.Evaluate2(testData).Should().Be(669060);
        }
        
        [Test]
        public void Part2() => Day18.Part2(data).Should().Be(85660197232452);

    }
}
