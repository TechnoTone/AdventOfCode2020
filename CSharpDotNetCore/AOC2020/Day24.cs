using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day24
    {
        public static int Part1(IEnumerable<string> data)
        {
            var grid = parseGrid(data);

            return grid.Count(t => t.Value);
        }

        public static int Part2(IEnumerable<string> data)
        {
            var grid = parseGrid(data);

            100.Repeat(() => flip(grid));

            return grid.Count(t => t.Value);
        }

        private static void draw(Dictionary<(int X, int Y), bool> grid)
        {
            var minX = grid.Keys.Min(k => k.X);
            var maxX = grid.Keys.Max(k => k.X);
            var minY = grid.Keys.Min(k => k.Y);
            var maxY = grid.Keys.Max(k => k.Y);

            bool isActive(int x, int y) =>
                grid.ContainsKey((x, y)) && grid[(x, y)];

            for (var y = minY - 2; y < maxY + 2; y++)
            {
                var s = new string(' ', y - minY + 2);
                for (var x = minX - 2; x < maxX + 2; x++)
                    s += isActive(x, y) ? "# " : ". ";

                HelperFunctions.Log(s);
            }

            HelperFunctions.Log("Count: " + grid.Count(t => t.Value));
            HelperFunctions.Log("");
        }

        private static void flip(Dictionary<(int X, int Y), bool> grid)
        {
            bool isActive(int x, int y) =>
                grid.ContainsKey((x, y)) && grid[(x, y)];

            int activeNeighbours(int x, int y) =>
                neighbours(x, y).Count(pos => isActive(pos.X, pos.Y));

            static IEnumerable<(int X, int Y)> neighbours(int X, int Y)
            {
                yield return (X + 1, Y);
                yield return (X - 1, Y);
                yield return (X, Y + 1);
                yield return (X, Y - 1);
                yield return (X + 1, Y + 1);
                yield return (X - 1, Y - 1);
            }

            var minX = grid.Keys.Min(k => k.X);
            var maxX = grid.Keys.Max(k => k.X);
            var minY = grid.Keys.Min(k => k.Y);
            var maxY = grid.Keys.Max(k => k.Y);

            var toFlip = new HashSet<(int X, int Y)>();

            for (var x = minX - 2; x < maxX + 2; x++)
            for (var y = minY - 2; y < maxY + 2; y++)
            {
                var count = activeNeighbours(x, y);
                if (isActive(x, y) && (count == 0 || count > 2))
                    toFlip.Add((x, y));
                if (!isActive(x, y) && count == 2)
                    toFlip.Add((x, y));
            }

            foreach (var key in toFlip)
            {
                if (grid.ContainsKey(key))
                    grid[key] = !grid[key];
                else
                    grid[key] = true;
            }
        }

        private static Dictionary<(int X, int Y), bool> parseGrid(IEnumerable<string> data)
        {
            var grid = new Dictionary<(int X, int Y), bool>();

            static string read(ref string line)
            {
                var next = line.Substring(0, line.StartsWith("s") || line.StartsWith("n") ? 2 : 1);
                line = line.Remove(0, next.Length);
                return next;
            }

            foreach (var l in data)
            {
                var x = 0;
                var y = 0;
                var line = l;
                while (line != "")
                {
                    var next = read(ref line);
                    switch (next)
                    {
                        case "e":
                            x++;
                            break;
                        case "w":
                            x--;
                            break;
                        case "sw":
                            y++;
                            break;
                        case "ne":
                            y--;
                            break;
                        case "se":
                            x++;
                            y++;
                            break;
                        case "nw":
                            x--;
                            y--;
                            break;
                        default:
                            throw new Exception("oops");
                    }
                }

                var key = (x, y);
                if (grid.ContainsKey(key))
                    grid[key] = !grid[key];
                else
                    grid[key] = true;
            }

            return grid;
        }
    }
}
