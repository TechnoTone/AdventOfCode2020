using System;
using static System.Math;

namespace AOC2020
{
    public class Vector3 : IEquatable<Vector3>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }

        public int AbsSum => Abs(X) + Abs(Y) + Abs(Z);

        public static Vector3 Zero => new Vector3(0, 0, 0);

        public static Vector3 operator +(Vector3 left, Vector3 right) =>
            new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        public Vector3(in int x, in int y, in int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3 Add(int x, int y, int z) => new Vector3(X + x, Y + y, Z + z);

        public override string ToString()
        {
            static string f(int n) => n >= 0 ? $" {n}" : $"{n}";
            return $"{f(X)}, {f(Y)}, {f(Z)}";
        }

        public bool Equals(Vector3 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vector3) obj);
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z);
    }
}
