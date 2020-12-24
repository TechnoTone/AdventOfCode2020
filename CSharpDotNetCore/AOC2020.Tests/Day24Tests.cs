using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace AOC2020.Tests
{
    public class Day24Tests : TestBase
    {
        private readonly List<string> data;

        public Day24Tests() : base(24)
        {
            data = input.linesOfStrings();
        }

        private const string testData = @"sesenwnenenewseeswwswswwnenewsewsw
neeenesenwnwwswnenewnwwsewnenwseswesw
seswneswswsenwwnwse
nwnwneseeswswnenewneswwnewseswneseene
swweswneswnenwsewnwneneseenw
eesenwseswswnenwswnwnwsewwnwsene
sewnenenenesenwsewnenwwwse
wenwwweseeeweswwwnwwe
wsweesenenewnwwnwsenewsenwwsesesenwne
neeswseenwwswnwswswnw
nenwswwsewswnenenewsenwsenwnesesenew
enewnwewneswsewnwswenweswnenwsenwsw
sweneswneswneneenwnewenewwneswswnese
swwesenesewenwneswnwwneseswwne
enesenwswwswneneswsenwnewswseenwsese
wnwnesenesenenwwnenwsewesewsesesew
nenewswnwewswnenesenwnesewesw
eneswnwswnwsenenwnwnwwseeswneewsenese
neswnwewnwnwseenwseesewsenwsweewe
wseweeenwnesenwwwswnew";

        [Test]
        public void Part1Example() => 
            Day24.Part1(testData.Lines()).Should().Be(10);

        [Test]
        public void Part1() =>
            Day24.Part1(data).Should().Be(346);

        [Test]
        public void Part2Example() =>
            Day24.Part2(testData.Lines()).Should().Be(2208);
        
        [Test]
        public void Part2() => Day24.Part2(data).Should().Be(3802);

    }
}
