using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AOC2020
{
    public static class Day20
    {
        public static long Part1(string data)
        {
            var tileData = data.Replace("\r", "").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var tiles = parseTileData(tileData).ToDictionary(t => t.ID);
            var edgeCounts = getEdgeCounts(tiles);
            var tileEdgeCounts = getTileEdgeCountTotals(tiles, edgeCounts);

            return tileEdgeCounts.Take(4).Select(x => x.ID).Product();
        }

        public static int Part2(string data)
        {
            var assembledMap = Assemble(data);
            var total = assembledMap.Sum(s => s.Count(c => c == '#'));
            var monsters = monsterCount(assembledMap);
            return total - monsters * 15;
        }

        private static int monsterCount(string[] map)
        {
            var monster = "                  # \n#    ##    ##    ###\n #  #  #  #  #  #   ".Lines();
            var monsterWidth = monster[0].Length;
            var monsterHeight = monster.Length;
            
            var monsterOffsets = new List<(int x, int y)>();
            
            for (var y = 0; y < monsterHeight; y++) 
            for (var x = 0; x < monsterWidth; x++)
                if (monster[y][x] == '#')
                    monsterOffsets.Add((x, y));

            void rotate() =>
                map = map
                    .Select((_, y) =>
                        new string(map
                            .Select(s => s[map.Length - y - 1])
                            .ToArray()))
                    .ToArray();

            void flip() =>
                map = map.Reverse().ToArray();

            int monsters()
            {
                var monsterCount = 0;
                for (var y = 0; y < map.Length-monsterHeight; y++)
                for (var x = 0; x < map[0].Length - monsterWidth; x++)
                    // ReSharper disable AccessToModifiedClosure
                    if (monsterOffsets.All(offset => map[y + offset.y][x + offset.x] == '#'))
                        // ReSharper restore AccessToModifiedClosure
                        monsterCount++;

                return monsterCount;
            }

            var count = 0;
            while (monsters() == 0)
            {
                rotate();
                if (++count == 4)
                    flip();
            }

            return monsters();
        }

        public static string[] Assemble(string data)
        {
            var tileData = data.Replace("\r", "").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);
            var tiles = parseTileData(tileData).ToDictionary(t => t.ID);
            var edgeCounts = getEdgeCounts(tiles);
            var tileEdgeCounts = getTileEdgeCountTotals(tiles, edgeCounts);

            var size = (int) Math.Sqrt(tiles.Count);
            var tileGrid = new Tile[size, size];
            
            //top left corner
            var firstCorner = tileEdgeCounts.First();
            tileGrid[0, 0] = tiles[firstCorner.ID];

            if (edgeCounts[tileGrid[0, 0].Edges.Right]==1) tileGrid[0, 0].FlipH();
            if (edgeCounts[tileGrid[0, 0].Edges.Bottom]==1) tileGrid[0, 0].FlipV();
            
            //Remaining top row
            for (var x = 1; x < size; x++)
            {
                var tileLeft = tileGrid[x - 1, 0];
                tileGrid[x, 0] = tiles.Values
                    .Single(t => t.Edges.All().Contains(tileLeft.Edges.Right) &&
                                t.ID != tileLeft.ID);
                tileGrid[x, 0].AlignLeftEdge(tileLeft.Edges.Right);
            }

            //Remaining rows
            for (var y = 1; y < size; y++)
            for (var x = 0; x < size; x++)
            {
                var tileAbove = tileGrid[x, y - 1];
                tileGrid[x, y] = tiles.Values
                    .Single(t => t.Edges.All().Contains(tileAbove.Edges.Bottom) &&
                                t.ID != tileAbove.ID);
                tileGrid[x, y].AlignTopEdge(tileAbove.Edges.Bottom);
            }

            var result = new List<string>();
            for (var y = 0; y < size; y++)
            for (var subY = 1; subY < 9; subY++)
            {
                var row = "";
                for (var x = 0; x < size; x++)
                for (var subX = 1; subX < 9; subX++)
                    row += tileGrid[x, y].Map.Contains(new Point(subX, subY)) ? "#" : ".";

                result.Add(row);
            }

            return result.ToArray();
        }

        private static Dictionary<int, int> getEdgeCounts(Dictionary<int, Tile> tiles) =>
            tiles
                .SelectMany(t => t.Value.Edges.All())
                .GroupBy(e => e)
                .OrderBy(g => g.Count())
                .ToDictionary(g => g.Key, g => g.Count());

        private static List<(int ID, int Count)> getTileEdgeCountTotals(Dictionary<int, Tile> tiles, Dictionary<int, int> edgeCounts) =>
            tiles
                .Select(t => (ID: t.Key, Count: t.Value.Edges.All().Sum(e => edgeCounts[e])))
                .OrderBy(t => t.Count)
                .ToList();

        [DebuggerDisplay("{ToString()}")]
        public class Tile
        {
            public int ID { get; }
            public HashSet<Point> Map;
            public TileEdges Edges { get; }

            private Tile(in int id, HashSet<Point> map, TileEdges edges)
            {
                ID = id;
                Edges = edges;
                Map = map;
            }
            
            public static Tile Parse(string data)
            {
                static int binary(string bits) => Convert.ToInt32(bits, 2);

                var id = int.Parse(data.Substring(5, 4));
                var map = new HashSet<Point>();
    
                var rows = data.Replace(".", "0").Replace("#", "1").Lines().IgnoreEmptyLines().Skip(1).ToList();
                for (var y = 0; y < 10; y++)
                for (var x = 0; x < 10; x++)
                    if (rows[y][x] == '1')
                        map.Add(new Point(x, y));
    
                var left = "";
                var right = "";
                for (var y = 0; y < 10; y++)
                {
                    left += rows[y][0];
                    right += rows[y][9];
                }

                var edges = new TileEdges(
                    binary(rows.First()),
                    binary(right),
                    binary(rows.Last()),
                    binary(left),
                    binary(rows.First().ReversString()),
                    binary(right.ReversString()),
                    binary(rows.Last().ReversString()),
                    binary(left.ReversString())
                );

                return new Tile(id, map, edges);
            }

            public class TileEdges
            {
                public int Top;
                public int Right;
                public int Bottom;
                public int Left;
                // ReSharper disable InconsistentNaming
                public int _Top;
                public int _Right;
                public int _Bottom;
                public int _Left;

                public TileEdges(int t, int r, int b, int l, int _t, int _r, int _b, int _l)
                {
                    Top = t;
                    Right = r;
                    Bottom = b;
                    Left = l;
                    _Top = _t;
                    _Right = _r;
                    _Bottom = _b;
                    _Left = _l;
                }
                // ReSharper restore InconsistentNaming

                public IEnumerable<int> All()
                {
                    yield return Top;
                    yield return Right;
                    yield return Bottom;
                    yield return Left;
                    yield return _Top;
                    yield return _Right;
                    yield return _Bottom;
                    yield return _Left;
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                sb.AppendLine($"Tile {ID}:");
                for (var y = 0; y < 10; y++)
                {
                    for (var x = 0; x < 10; x++)
                        sb.Append(Map.Contains(new Point(x, y)) ? "#" : ".");
                
                    sb.AppendLine();
                }
                
                return sb.ToString();
            }
    
            public void FlipH()
            {
                Map = Map.Select(p => new Point(9 - p.X, p.Y)).ToHashSet();
                (Edges.Left, Edges.Right) = (Edges.Right, Edges.Left);
                (Edges._Left, Edges._Right) = (Edges._Right, Edges._Left);
                (Edges.Top, Edges._Top) = (Edges._Top, Edges.Top);
                (Edges.Bottom, Edges._Bottom) = (Edges._Bottom, Edges.Bottom);
            }

            public void FlipV()
            {
                Map = Map.Select(p => new Point(p.X, 9 - p.Y)).ToHashSet();
                (Edges.Top, Edges.Bottom) = (Edges.Bottom, Edges.Top);
                (Edges._Top, Edges._Bottom) = (Edges._Bottom, Edges._Top);
                (Edges.Left, Edges._Left) = (Edges._Left, Edges.Left);
                (Edges.Right, Edges._Right) = (Edges._Right, Edges.Right);
            }

            public void Rotate()
            {
                Map = Map.Select(p => new Point(9 - p.Y, p.X)).ToHashSet();
                var (t, r, b, l) = (Edges.Top, Edges.Right, Edges.Bottom, Edges.Left);
                // ReSharper disable InconsistentNaming
                var (_t, _r, _b, _l) = (Edges._Top, Edges._Right, Edges._Bottom, Edges._Left);
                // ReSharper restore InconsistentNaming

                (Edges.Top, Edges.Right, Edges.Bottom, Edges.Left) = (_l, t, _r, b);
                (Edges._Top, Edges._Right, Edges._Bottom, Edges._Left) = (l, _t, r, _b);
            }

            public void AlignLeftEdge(int edge)
            {
                if (Edges._Top == edge) FlipH();
                if (Edges._Right == edge) Rotate();
                if (Edges._Bottom == edge) FlipH();
                if (Edges.Top == edge) FlipV();
                if (Edges.Right == edge) FlipH();
                if (Edges.Bottom == edge) Rotate();
                if (Edges._Left == edge) FlipV();
                Debug.Assert(Edges.Left == edge);
            }

            public void AlignTopEdge(int edge)
            {
                if (Edges.Right == edge) FlipV();
                if (Edges._Right == edge) Rotate();
                if (Edges.Bottom == edge) FlipV();
                if (Edges._Bottom == edge) Rotate();
                if (Edges.Left == edge) FlipV();
                if (Edges._Left == edge) Rotate();
                if (Edges._Top == edge) FlipH();
                Debug.Assert(Edges.Top == edge);
            }
        }
    
        private static IEnumerable<Tile> parseTileData(IEnumerable<string> tileData) =>
            tileData.Select(Tile.Parse);

        // private class TileVariant
        // {
        //     public int TileId { get; }
        //     public int Variant { get; }
        //
        //     public TileVariant(int tileId, int variant)
        //     {
        //         TileId = tileId;
        //         Variant = variant;
        //     }
        // }
        
        // private static TileVariant[,] assemble(Dictionary<int, Tile> tiles, Dictionary<int, List<string>> edges, int size)
        // {
        //     var allTiles = tiles
        //         .Select((tile, index) => (tile, index))
        //         .ToImmutableDictionary(x => x.index, x => x.tile);
        //
        //     // var 
        //     
        //     var bigMap = new TileVariant[size, size];
        //     var x = 0;
        //     var y = 0;
        //
        //     while (true)
        //     {
        //         if (bigMap[x, y] == null)
        //         {
        //             if (x == 0 && y == 0)
        //             {
        //                 bigMap[x, y] = new TileVariant(tiles.Keys.First(), 0);
        //             }
        //
        //             y += x == 9 ? 1 : 0;
        //             x = x == 9 ? 0 : x + 1;
        //         }
        //         else
        //         {
        //             if (x == size && y == size)
        //                 return bigMap;
        //         }
        //     }
        //
        //     
        //     var corners = tiles
        //         .Where(t =>
        //             2 == tiles.Count(t2 =>
        //                 t.Value.Edges().Any(e =>
        //                     t2.Value.Edges().Contains(e))));
        //     
        //     return null;
        // }
    }
}
