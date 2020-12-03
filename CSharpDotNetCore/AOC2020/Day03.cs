using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day03
    {
        public static long part1(IEnumerable<string> input) => traverse(input, 3, 1);

        public static long part2(IEnumerable<string> input) =>
            traverse(input, 1, 1) *
            traverse(input, 3, 1) *
            traverse(input, 5, 1) *
            traverse(input, 7, 1) *
            traverse(input, 1, 2);

        private static long traverse(IEnumerable<string> input, int dx, int dy)
        {
            var area = input.ToArray();
            var x = 0;
            var y = 0;
            var width = area[0].Length;
            var height = area.Length;
            var trees = 0;

            while (y < height)
            {
                x = (x + dx) % width;
                y+=dy;
                if (y < height && area[y][x] == '#')
                    trees++;
            }

            return trees;
        }

    }
}
