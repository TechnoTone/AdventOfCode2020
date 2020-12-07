using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day07Tests : TestBase
    {
        private const string TestData = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

        private readonly string rules;

        public Day07Tests() : base(7) { rules = input.readAllText(); }
       
        [Test]
        public void Part1Example()
        {
            new Day07(TestData).Part1().Should().Be(4);
        }

        [Test]
        public void Part1() => new Day07(rules).Part1().Should().Be(316);

        [Test]
        public void Part2Example()
        {
            new Day07(TestData).Part2().Should().Be(32);
        }
        
        
        [Test]
        public void Part2() => new Day07(rules).Part2().Should().Be(11310);

    }
}
