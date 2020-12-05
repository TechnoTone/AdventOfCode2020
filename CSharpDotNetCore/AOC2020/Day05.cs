using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day05
    {
        public static int part1(IEnumerable<string> input) => input.Max(decode);
        public static int part2(IEnumerable<string> input)
        {
            var allSeats = input.Select(decode).OrderBy(x => x).ToList();
            for (var i = 0; i < allSeats.Count-1; i++)
                if (allSeats[i] + 2 == allSeats[i + 1])
                    return allSeats[i] + 1;
            return 0;
        }

        private struct Seat
        {
            public int Row;
            public int Col;
            public int Id() => Row * 8 + Col;
        }
        
        public static int decode (string input) => decodeSeat(input).Id();

        private static Seat decodeSeat(string input) =>
            new Seat
            {
                Row = decodePart(input.Substring(0, 7)),
                Col = decodePart(input.Substring(7, 3))
            };

        private static int decodePart(string input)
        {
            var n = 1;
            var x = 0;
            var stack = new Stack<char>(input.ToCharArray());
            while (stack.Count>0)
            {
                var next = stack.Pop();
                if (next == 'B' || next == 'R') x += n;
                n *= 2;
            }

            return x;
        }
    }
}
