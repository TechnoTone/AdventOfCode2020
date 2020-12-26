using FluentAssertions;
using NUnit.Framework;
// ReSharper disable InconsistentNaming

namespace AOC2020.Tests
{
    public class Day20Tests : TestBase
    {
        private readonly string data;
        
        public Day20Tests() : base(20) { data = input.readAllText(); }

        private const string TEST_DATA =
            "Tile 2311:\n..##.#..#.\n##..#.....\n#...##..#.\n####.#...#\n##.##.###.\n##...#.###\n.#.#.#..##\n..#....#..\n###...#.#.\n..###..###\n\nTile 1951:\n#.##...##.\n#.####...#\n.....#..##\n#...######\n.##.#....#\n.###.#####\n###.##.##.\n.###....#.\n..#.#..#.#\n#...##.#..\n\nTile 1171:\n####...##.\n#..##.#..#\n##.#..#.#.\n.###.####.\n..###.####\n.##....##.\n.#...####.\n#.##.####.\n####..#...\n.....##...\n\nTile 1427:\n###.##.#..\n.#..#.##..\n.#.##.#..#\n#.#.#.##.#\n....#...##\n...##..##.\n...#.#####\n.#.####.#.\n..#..###.#\n..##.#..#.\n\nTile 1489:\n##.#.#....\n..##...#..\n.##..##...\n..#...#...\n#####...#.\n#..#.#.#.#\n...#.#.#..\n##.#...##.\n..##.##.##\n###.##.#..\n\nTile 2473:\n#....####.\n#..#.##...\n#.##..#...\n######.#.#\n.#...#.#.#\n.#########\n.###.#..#.\n########.#\n##...##.#.\n..###.#.#.\n\nTile 2971:\n..#.#....#\n#...###...\n#.#.###...\n##.##..#..\n.#####..##\n.#..####.#\n#..#.#..#.\n..####.###\n..#.#.###.\n...#.#.#.#\n\nTile 2729:\n...#.#.#.#\n####.#....\n..#.#.....\n....#..#.#\n.##..##.#.\n.#.####...\n####.#.#..\n##.####...\n##..#.##..\n#.##...##.\n\nTile 3079:\n#.#.#####.\n.#..######\n..#.......\n######....\n####.#..#.\n.#...#.##.\n#.#####.##\n..#.###...\n..#.......\n..#.###...";

        private const string EXPECTED_TEST_MAP =
            @".#.#..#.##...#.##..#####
###....#.#....#..#......
##.##.###.#.#..######...
###.#####...#.#####.#..#
##.#....#.##.####...#.##
...########.#....#####.#
....#..#...##..#.#.###..
.####...#..#.....#......
#..#.##..#..###.#.##....
#.####..#.####.#.#.###..
###.#.#...#.######.#..##
#.####....##..########.#
##..##.#...#...#.#.#.#..
...#..#..#.#.##..###.###
.#.#....#.##.#...###.##.
###.#...#..#.##.######..
.#.#.###.##.##.#..#.##..
.####.###.#...###.#..#.#
..#.#..#..#.#.#.####.###
#..####...#.#.#.###.###.
#####..#####...###....##
#.##..#..#...#..####...#
.#.###..##..##..####.##.
...###...##...#...#..###";
        
        [Test]
        public void Part1Example() => 
            Day20.Part1(TEST_DATA).Should().Be(20899048083289);

        [Test]
        public void Part1() => Day20.Part1(data).Should().Be(140656720229539);

        [Test]
        public void Part2Assembled() =>
            Day20.Assemble(TEST_DATA).Should().BeEquivalentTo(EXPECTED_TEST_MAP.Lines());

        [Test]
        public void Part2Example()
        {
            Day20.Part2(TEST_DATA).Should().Be(273);
        }

        [Test]
        public void TileRotation()
        {
            const string TEST_INPUT =
                "Tile: 0000\n#.........\n#........#\n.........#\n.........#\n..........\n..........\n..........\n..........\n..........\n.####.....";
            const string EXPECTED_RESULT =
                "Tile 0:\n........##\n#.........\n#.........\n#.........\n#.........\n..........\n..........\n..........\n..........\n......###.\n";

            var tile = Day20.Tile.Parse(TEST_INPUT);
            var (t, r, b, l) = (tile.Edges.Top, tile.Edges.Right, tile.Edges.Bottom, tile.Edges.Left);
            var (_t, _r, _b, _l) = (tile.Edges._Top, tile.Edges._Right, tile.Edges._Bottom, tile.Edges._Left);

            tile.Rotate();

            tile.ToString().Replace("\r","").Should().Be(EXPECTED_RESULT);

            tile.Edges.Top.Should().Be(_l);
            tile.Edges.Right.Should().Be(t);
            tile.Edges.Bottom.Should().Be(_r);
            tile.Edges.Left.Should().Be(b);
            tile.Edges._Top.Should().Be(l);
            tile.Edges._Right.Should().Be(_t);
            tile.Edges._Bottom.Should().Be(r);
            tile.Edges._Left.Should().Be(_b);
        }

        [Test]
        public void TileFlipV()
        {
            const string TEST_INPUT =
                "Tile: 0000\n#.........\n#........#\n.........#\n.........#\n..........\n..........\n..........\n..........\n..........\n.####.....";
            const string EXPECTED_RESULT =
                "Tile 0:\n.####.....\n..........\n..........\n..........\n..........\n..........\n.........#\n.........#\n#........#\n#.........\n";

            var tile = Day20.Tile.Parse(TEST_INPUT);
            var (t, r, b, l) = (tile.Edges.Top, tile.Edges.Right, tile.Edges.Bottom, tile.Edges.Left);
            var (_t, _r, _b, _l) = (tile.Edges._Top, tile.Edges._Right, tile.Edges._Bottom, tile.Edges._Left);

            tile.FlipV();

            tile.ToString().Replace("\r","").Should().Be(EXPECTED_RESULT);

            tile.Edges.Top.Should().Be(b);
            tile.Edges.Right.Should().Be(_r);
            tile.Edges.Bottom.Should().Be(t);
            tile.Edges.Left.Should().Be(_l);
            tile.Edges._Top.Should().Be(_b);
            tile.Edges._Right.Should().Be(r);
            tile.Edges._Bottom.Should().Be(_t);
            tile.Edges._Left.Should().Be(l);
        }

        [Test]
        public void TileFlipH()
        {
            const string TEST_INPUT =
                "Tile: 0000\n#.........\n#........#\n.........#\n.........#\n..........\n..........\n..........\n..........\n..........\n.####.....";
            const string EXPECTED_RESULT =
                "Tile 0:\n.........#\n#........#\n#.........\n#.........\n..........\n..........\n..........\n..........\n..........\n.....####.\n";

            var tile = Day20.Tile.Parse(TEST_INPUT);
            var (t, r, b, l) = (tile.Edges.Top, tile.Edges.Right, tile.Edges.Bottom, tile.Edges.Left);
            var (_t, _r, _b, _l) = (tile.Edges._Top, tile.Edges._Right, tile.Edges._Bottom, tile.Edges._Left);

            tile.FlipH();

            tile.ToString().Replace("\r","").Should().Be(EXPECTED_RESULT);

            tile.Edges.Top.Should().Be(_t);
            tile.Edges.Right.Should().Be(l);
            tile.Edges.Bottom.Should().Be(_b);
            tile.Edges.Left.Should().Be(r);
            tile.Edges._Top.Should().Be(t);
            tile.Edges._Right.Should().Be(_l);
            tile.Edges._Bottom.Should().Be(b);
            tile.Edges._Left.Should().Be(_r);
        }
        
        [Test]
        public void Part2() => Day20.Part2(data).Should().Be(1885);
        
    }
}
