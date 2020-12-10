using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day10Tests : TestBase
    {
        private readonly List<int> data;

        public Day10Tests() : base(10) { data = input.linesOfIntegers(); }
       
        [Test]
        public void Part1Example1()
        {
            var testData = new List<int>
            {
                16,
                10,
                15,
                5,
                1,
                11,
                7,
                19,
                6,
                12,
                4
            };
            Day10.Part1(testData).Should().Be(35); // 7 * 5
        }

        [Test]
        public void Part1Example2()
        {
            var testData = new List<int>
            {
                28,
                33,
                18,
                42,
                31,
                14,
                46,
                20,
                48,
                47,
                24,
                23,
                49,
                45,
                19,
                38,
                39,
                11,
                1,
                32,
                25,
                35,
                8,
                17,
                7,
                9,
                4,
                2,
                34,
                10,
                3
            };
            Day10.Part1(testData).Should().Be(220); // 22 * 10
        }

        [Test]
        public void Part1() => Day10.Part1(data).Should().Be(2240);

        [Test]
        public void Part2Example1()
        {
            var testData = new List<int>
            {
                16,
                10,
                15,
                5,
                1,
                11,
                7,
                19,
                6,
                12,
                4
            };
            Day10.Part2(testData).Should().Be(8); 
        }

        [Test]
        public void Part2Example2()
        {
            var testData = new List<int>
            {
                28,
                33,
                18,
                42,
                31,
                14,
                46,
                20,
                48,
                47,
                24,
                23,
                49,
                45,
                19,
                38,
                39,
                11,
                1,
                32,
                25,
                35,
                8,
                17,
                7,
                9,
                4,
                2,
                34,
                10,
                3
            };
            Day10.Part2(testData).Should().Be(19208); 
        }

        
        [Test]
        public void Part2() => Day10.Part2(data).Should().Be(99214346656768);

    }
}
