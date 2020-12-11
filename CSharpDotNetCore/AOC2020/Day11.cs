using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2020
{
    public static class Day11
    {
        public static int Part1(IEnumerable<string> data)
        {
            var seats = parse(data);
            var history = new HashSet<int>();

            while (true)
            {
                seats = applyRules1(seats);
                var hash = getHash(seats);
                if (history.Contains(hash)) return occupiedSeats(seats);
                history.Add(hash);
            }
        }

        public static int Part2(IEnumerable<string> data)
        {
            var seats = parse(data);
            var history = new HashSet<int>();

            while (true)
            {
                seats = applyRules2(seats);
                var hash = getHash(seats);
                if (history.Contains(hash)) return occupiedSeats(seats);
                history.Add(hash);
            }
        }


        private static int getHash(Position[][] seats) =>
            seats.Select(row =>
                row.Select(pos => pos.GetHashCode())
                    .Aggregate(HashCode.Combine)
            ).Aggregate(HashCode.Combine);

        private static int occupiedSeats(Position[][] seats) =>
            seats.Select(row => row.Count(s => s == Position.OccupiedSeat)).Sum();

        enum Position
        {
            Floor, EmptySeat, OccupiedSeat
        }

        private static Position[][] parse(IEnumerable<string> data) =>
            data.Select(s =>
                s.ToCharArray().Select(c => c switch
                {
                    'L' => Position.EmptySeat,
                    _ => Position.Floor
                }).ToArray()
            ).ToArray();

        private static Position[][] applyRules1(Position[][] seats)
        {
            var width = seats[0].Length;
            var height = seats.Length;

            Position seat(int x, int y) =>
                x < 0 || y < 0 || x >= width || y >= height
                    ? Position.Floor
                    : seats[y][x];

            int seatVal(int x, int y) =>
                seat(x, y) switch
                {
                    Position.OccupiedSeat => 1,
                    _ => 0
                };

            int adjacent(int x, int y) =>
                seatVal(x - 1, y - 1) +
                seatVal(x, y - 1) +
                seatVal(x + 1, y - 1) +
                seatVal(x - 1, y) +
                seatVal(x + 1, y) +
                seatVal(x - 1, y + 1) +
                seatVal(x, y + 1) +
                seatVal(x + 1, y + 1);

            var result = new List<Position[]>();
            for (var y = 0; y < seats.Length; y++)
            {
                var newRow = new List<Position>();
                for (var x = 0; x < seats[0].Length; x++)
                {
                    var newSeat = seat(x, y);
                    newSeat = newSeat switch
                    {
                        Position.Floor => Position.Floor,
                        Position.EmptySeat => adjacent(x, y) == 0 ? Position.OccupiedSeat : Position.EmptySeat,
                        Position.OccupiedSeat => adjacent(x, y) >= 4 ? Position.EmptySeat : Position.OccupiedSeat,
                        _ => newSeat
                    };
                    newRow.Add(newSeat);
                }

                result.Add(newRow.ToArray());
            }

            return result.ToArray();
        }
        private static Position[][] applyRules2(Position[][] seats)
        {
            var width = seats[0].Length;
            var height = seats.Length;

            Position seat(int x, int y) =>
                x < 0 || y < 0 || x >= width || y >= height
                    ? Position.Floor
                    : seats[y][x];
            
            bool occupiedDirection(int x, int y, int dx, int dy) =>
                x + dx >= 0 &&
                y + dy >= 0 &&
                x + dx < width &&
                y + dy < height &&
                seat(x + dx, y + dy) switch
                {
                    Position.OccupiedSeat => true,
                    Position.EmptySeat => false,
                    _ => occupiedDirection(x + dx, y + dy, dx, dy)
                };

            int occupiedDirectionVal(int x, int y, int dx, int dy) =>
                occupiedDirection(x, y, dx, dy) ? 1 : 0;
            
            int adjacent(int x, int y) =>
                occupiedDirectionVal(x, y, -1, -1) +
                occupiedDirectionVal(x, y, 0, -1) +
                occupiedDirectionVal(x, y, 1, -1) +
                occupiedDirectionVal(x, y, -1, 0) +
                occupiedDirectionVal(x, y, 1, 0) +
                occupiedDirectionVal(x, y, -1, 1) +
                occupiedDirectionVal(x, y, 0, 1) +
                occupiedDirectionVal(x, y, 1, 1);

            var result = new List<Position[]>();
            for (var y = 0; y < seats.Length; y++)
            {
                var newRow = new List<Position>();
                for (var x = 0; x < seats[0].Length; x++)
                {
                    var newSeat = seat(x, y);
                    newSeat = newSeat switch
                    {
                        Position.Floor => Position.Floor,
                        Position.EmptySeat => adjacent(x, y) == 0 ? Position.OccupiedSeat : Position.EmptySeat,
                        Position.OccupiedSeat => adjacent(x, y) >= 5 ? Position.EmptySeat : Position.OccupiedSeat,
                        _ => newSeat
                    };
                    newRow.Add(newSeat);
                }

                result.Add(newRow.ToArray());
            }

            return result.ToArray();
        }
    }
}
