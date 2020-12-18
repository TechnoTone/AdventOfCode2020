using System;
using System.Diagnostics;

namespace AOC2020
{
    [DebuggerDisplay("{ToString()}")]
    public class Vector4 : IEquatable<Vector4>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int W { get; private set; }

        public int AbsSum => Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z) + Math.Abs(W);

        public static Vector4 Zero => new Vector4(0, 0, 0, 0);

        public static Vector4 operator +(Vector4 left, Vector4 right) =>
            new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        public Vector4(in int x, in int y, in int z, in int w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4 Add(int x, int y, int z, int w) => new Vector4(X + x, Y + y, Z + z, W + w);

        public override string ToString()
        {
            static string f(int n) => n >= 0 ? $" {n}" : $"{n}";
            return $"{f(X)}, {f(Y)}, {f(Z)}, {f(W)}";
        }

        
        public bool Equals(Vector4 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z && W == other.W;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Vector4) obj);
        }

        public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
    }
}
