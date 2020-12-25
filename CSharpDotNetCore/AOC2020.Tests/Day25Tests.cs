using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day25Tests : TestBase
    {
        private readonly List<int> data;
        
        public Day25Tests() : base(25) { data = input.linesOfIntegers(); }

        private readonly List<int> testData =
            new List<int> {5764801, 17807724};

        [Test]
        public void Part1Example() => 
            Day25.Part1(testData[0],testData[1]).Should().Be(14897079);

        [Test]
        public void Part1() => 
            Day25.Part1(data[0],data[1]).Should().Be(10548634);
        
    }
}
