using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC2020
{

    public static class LongExtensions
    {
        public static bool IsDivisibleBy(this long i, long n) => i % n == 0;

        public static void Repeat(this long i, Action a)
        {
            for (var n = 0; n < i; n++) a();
        }
    }

    public static class IntExtensions
    {
        public static void Repeat(this int i, Action a)
        {
            for (var n = 0; n < i; n++) a();
        }
    }
    
    public static class StringExtensions
    {
        public static List<long> ParseCommaSeparatedIntegers(this string input) =>
            input.Split(',').ToList().ConvertAll(long.Parse);

        public static string[] Lines(this string input) =>
            input.Replace("\r", "").Split("\n");

        public static string ReversString(this string input) =>
            new string(input.Reverse().ToArray());
    }

    public static class PointExtensions
    {
        public static Point[] Surrounding4(this Point p) => new[]
        {
            p.Shift(Direction.Up),
            p.Shift(Direction.Left),
            p.Shift(Direction.Right),
            p.Shift(Direction.Down)
        };

        public static int ManhattanDistance(this Point p) => Math.Abs(p.X) + Math.Abs(p.Y);

        public static void Move(this ref Point p, WireDirection direction)
        {
            switch (direction)
            {
                case WireDirection.Up:
                    p.X--;
                    break;
                case WireDirection.Down:
                    p.X++;
                    break;
                case WireDirection.Left:
                    p.Y--;
                    break;
                case WireDirection.Right:
                    p.Y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static void Move(this ref Point p, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    p.Y--;
                    break;
                case Direction.Down:
                    p.Y++;
                    break;
                case Direction.Left:
                    p.X--;
                    break;
                case Direction.Right:
                    p.X++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static void Move(this ref Point p, CompassDirection direction) => 
            p.Move(direction, 1);

        public static void Move(this ref Point p, CompassDirection direction, int distance)
        {
            switch (direction)
            {
                case CompassDirection.North:
                    p.Y += distance;
                    break;
                case CompassDirection.South:
                    p.Y -= distance;
                    break;
                case CompassDirection.West:
                    p.X -= distance;
                    break;
                case CompassDirection.East:
                    p.X += distance;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public static Point Shift(this Point p, CompassDirection direction) =>
            direction switch
            {
                CompassDirection.North => p.Add(new Point(0, 1)),
                CompassDirection.South => p.Add(new Point(0, -1)),
                CompassDirection.West => p.Add(new Point(-1, 0)),
                CompassDirection.East => p.Add(new Point(1, 0)),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        public static Point Shift(this Point p, Direction direction) =>
            direction switch
            {
                Direction.Up => p.Add(new Point(0, -1)),
                Direction.Down => p.Add(new Point(0, 1)),
                Direction.Left => p.Add(new Point(-1, 0)),
                Direction.Right => p.Add(new Point(1, 0)),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        public static float Angle(this Point p) =>
            (float) (Math.Atan2(p.X, -p.Y) * (float) (180 / Math.PI) + 360) % 360;

        public static Point Subtract(this Point p, Point offset) =>
            new Point(p.X - offset.X, p.Y - offset.Y);

        public static Point Add(this Point p, Point offset) =>
            new Point(p.X + offset.X, p.Y + offset.Y);

        public static Point Reduce(this Point p)
        {
            var gcd = GCD(p.X, p.Y);
            return new Point(p.X / gcd, p.Y / gcd);
        }

        private static int GCD(int a, int b)
        {
            if (a == 0 && b == 0) return 1;

            a = Math.Abs(a);
            b = Math.Abs(b);

            while (a != 0 && b != 0)
                if (a > b) a %= b;
                else b %= a;

            return a == 0 ? b : a;
        }

        public static string ToSimpleString(this Point p) => $"{p.X},{p.Y}";
    }

    public static class DirectionExtensions
    {
        public static CompassDirection TurnLeft(this CompassDirection direction)
        {
            return direction switch
            {
                CompassDirection.North => CompassDirection.West,
                CompassDirection.South => CompassDirection.East,
                CompassDirection.West => CompassDirection.South,
                CompassDirection.East => CompassDirection.North,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
        public static CompassDirection TurnRight(this CompassDirection direction)
        {
            return direction switch
            {
                CompassDirection.North => CompassDirection.East,
                CompassDirection.South => CompassDirection.West,
                CompassDirection.West => CompassDirection.North,
                CompassDirection.East => CompassDirection.South,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }
    }
    
    
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new[] {t});

            return Permutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] {t2}));
        }
        
        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> sequence) => 
            sequence.Permutations(sequence.Count());

        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> sequence) =>
            Enumerable
                .Range(0, (int) Math.Pow(2, sequence.Count()))
                .Select(i =>
                    sequence
                        .Where((item, ix) => ((int) Math.Pow(2, ix) & i) > 0)
                        .ToList()
                );

        public static string JoinToStringNoSeparator(this IEnumerable<int> sequence) =>
            string.Join("", sequence);

        public static string JoinToString (this IEnumerable<int> sequence) =>
            string.Join(',', sequence);

        public static string JoinToString (this IEnumerable<long> sequence) =>
            string.Join(',', sequence);

        public static IEnumerable<string> Chunk(this string input, int chunkSize) =>
            Enumerable.Range(0, input.Length / chunkSize)
                .Select(i => input.Substring(i * chunkSize, chunkSize));

        public static IEnumerable<T> Except<T>(this IEnumerable<T> sequence, T item) =>
            sequence.Where(el => !el.Equals(item));

        public static (T, T) Range<T>(this IEnumerable<T> sequence) =>
            (sequence.Min(), sequence.Max());

        public static IEnumerable<string> IgnoreEmptyLines(this IEnumerable<string> sequence) => 
            sequence.Where(s => !string.IsNullOrEmpty(s));

        public static long Product(this IEnumerable<int> sequence) =>
            sequence.Select(x => (long) x).Aggregate((a, b) => a * b);

        public static long Product(this IEnumerable<long> sequence) =>
            sequence.Aggregate((a, b) => a * b);

        public static Queue<T> ToQueue<T>(this IEnumerable<T> sequence) => 
            new Queue<T>(sequence);

        public static int GetSequenceHashCode<T>(this IEnumerable<T> sequence) => 
            sequence.Select(x => x.GetHashCode()).Aggregate(HashCode.Combine);
    }

    public static class ListExtensions
    {
        public static List<T> Clone<T>(this List<T> source) => new List<T>(source);

        public static bool SameContentAs(this List<int> sequence, List<int> other)
        {
            if (sequence.Count != other.Count) return false;
            return !sequence.Where((t, i) => t != other[i]).Any();
        }

        public static bool SameContentAs(this List<long> sequence, List<long> other)
        {
            if (sequence.Count != other.Count) return false;
            return !sequence.Where((t, i) => t != other[i]).Any();
        }

        public static int SubListIndex<T>(this IList<T> list, int start, IList<T> sublist)
        {
            for (var listIndex = start; listIndex < list.Count - sublist.Count + 1; listIndex++)
            {
                var count = 0;
                while (count < sublist.Count && sublist[count].Equals(list[listIndex + count]))
                    count++;
                if (count == sublist.Count)
                    return listIndex;
            }

            return -1;
        }

    }

    public static class DictionaryExtensions
    {
        public static Dictionary<T1, T2> Clone<T1, T2>(this Dictionary<T1, T2> source) =>
            new Dictionary<T1, T2>(source);
    }


    public static class LinkedListExtension
    {
        public static LinkedListNode<T> NextCircular<T>(this LinkedListNode<T> current) =>
            current.Next ?? current.List.First;
    }
}
