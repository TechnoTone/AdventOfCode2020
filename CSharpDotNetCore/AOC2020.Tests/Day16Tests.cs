using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day16Tests : TestBase
    {
        private readonly string data;

        public Day16Tests() : base(16) { data = input.readAllText(); }

        [Test]
        public void Part1Example1()
        {
            const string testData = @"class: 1-3 or 5-7
row: 6-11 or 33-44
seat: 13-40 or 45-50

your ticket:
7,1,14

nearby tickets:
7,3,47
40,4,50
55,2,20
38,6,12";
            Day16.Part1(testData).Should().Be(71);
        }
        
        [Test]
        public void Part1() => Day16.Part1(data).Should().Be(21081);

        [Test]
        public void Part2Example()
        {
            const string testData = @"class: 0-1 or 4-19
row: 0-5 or 8-19
seat: 0-13 or 16-19

your ticket:
11,12,13

nearby tickets:
3,9,18
15,1,5
5,14,9";
            var ticket = Day16.yourTicket(testData);
            ticket.Should().ContainKey("class");
            ticket.Should().ContainKey("row");
            ticket.Should().ContainKey("seat");
            ticket["class"].Should().Be(12);
            ticket["row"].Should().Be(11);
            ticket["seat"].Should().Be(13);
        }
        
        [Test]
        public void Part2() => Day16.Part2(data).Should().Be(314360510573);

    }
}
