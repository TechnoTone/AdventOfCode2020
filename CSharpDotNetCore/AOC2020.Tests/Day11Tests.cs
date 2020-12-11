using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day11Tests : TestBase
    {
        private readonly List<string> data;

        public Day11Tests() : base(11) { data = input.linesOfStrings(); }

        [Test]
        public void Part1Example1()
        {
            var testData =
                "L.LL.LL.LL\nLLLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLLL\nL.LLLLLL.L\nL.LLLLL.LL"
                    .Lines();
            Day11.Part1(testData).Should().Be(37);
        }
        
        [Test]
        public void Part1() => Day11.Part1(data).Should().Be(2354);

        [Test]
        public void Part2Example()
        {
            var testData =
                "L.LL.LL.LL\nLLLLLLL.LL\nL.L.L..L..\nLLLL.LL.LL\nL.LL.LL.LL\nL.LLLLL.LL\n..L.L.....\nLLLLLLLLLL\nL.LLLLLL.L\nL.LLLLL.LL"
                    .Lines();
            Day11.Part2(testData).Should().Be(26);
        }

        [Test]
        public void Part2() => Day11.Part2(data).Should().Be(2072);

    }
}
