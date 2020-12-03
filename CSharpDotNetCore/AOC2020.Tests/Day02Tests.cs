using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day02Tests : TestBase
    {
        private readonly List<string> passwords;

        public Day02Tests() : base(2) { passwords = input.linesOfStrings(); }

        [Test]
        public void Part1Example()
        {
            var input = "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc".Split("\n");
            Day02.part1(input).Should().Be(2);
        }

        [Test]
        public void Part2Example()
        {
            var input = "1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc".Split("\n");
            Day02.part2(input).Should().Be(1);
        }


        [Test]
        public void Part1() =>
        Day02.part1(passwords).Should().Be(506);

        [Test]
        public void Part2() =>
            Day02.part2(passwords).Should().Be(443);

    }
}
