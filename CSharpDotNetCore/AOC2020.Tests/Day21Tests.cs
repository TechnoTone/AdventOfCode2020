using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day21Tests : TestBase
    {
        private readonly List<string> data;
        
        public Day21Tests() : base(21) { data = input.linesOfStrings(); }

        private const string testData =
            @"mxmxvkd kfcds sqjhc nhms (contains dairy, fish)
trh fvjkl sbzzf mxmxvkd (contains dairy)
sqjhc fvjkl (contains soy)
sqjhc mxmxvkd sbzzf (contains fish)";

        [Test]
        public void Part1Example() => 
            Day21.Part1(testData.Lines()).Should().Be(5);

        [Test]
        public void Part1() => Day21.Part1(data).Should().Be(2428);

        [Test]
        public void Part2Example()
        {
            Day21.Part2(testData.Lines()).Should().Be("mxmxvkd,sqjhc,fvjkl");
        }
        
        [Test]
        public void Part2() => Day21.Part2(data).Should().Be("bjq,jznhvh,klplr,dtvhzt,sbzd,tlgjzx,ctmbr,kqms");
        
    }
}
