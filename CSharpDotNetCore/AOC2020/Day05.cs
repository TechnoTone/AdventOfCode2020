using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day05
    {
        public static int part1(IEnumerable<string> input) => input.Max(decode);

        public static int part2(IEnumerable<string> input)
        {
            var allSeats = input.Select(decode).ToList();
            var sumOfSeats = allSeats.Sum();
            var min = allSeats.Min();
            var max = allSeats.Max();
            var sumOfRange = Enumerable.Range(min, max - min + 1).Sum();
            return sumOfRange - sumOfSeats;
        }

        public static int decode(string input) => decodeSeat(input);

        private static int decodeSeat(string input) =>
            decodePart(input.Substring(0, 7)) * 8 +
            decodePart(input.Substring(7, 3));

        private static int decodePart(string input) =>
            Convert.ToInt32(
                input
                    .Replace("B", "1")
                    .Replace("R", "1")
                    .Replace("F", "0")
                    .Replace("L", "0")
                , 2);
    }
}
