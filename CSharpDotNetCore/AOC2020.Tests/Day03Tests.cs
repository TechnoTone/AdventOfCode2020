using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day03Tests : TestBase
    {
        private readonly List<string> map;

        public Day03Tests() : base(3) { map = input.linesOfStrings(); }

        [Test]
        public void Part1Example()
        {
            IEnumerable<string> map = "..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#".Split("\n");
            Day03.part1(map).Should().Be(7);
        }

        [Test]
        public void Part2Example()
        {
            IEnumerable<string> map = "..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#".Split("\n");
            Day03.part2(map).Should().Be(336);
        }


        [Test]
        public void Part1() =>
            Day03.part1(map).Should().Be(232);

        [Test]
        public void Part2() =>
            Day03.part2(map).Should().Be(3952291680);

    }
}
