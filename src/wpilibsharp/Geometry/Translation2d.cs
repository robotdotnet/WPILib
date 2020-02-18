using System;
using UnitsNet;

namespace WPILib.Geometry
{
    public readonly struct Translation2d : IEquatable<Translation2d>
    {
        public Length X { get; }
        public Length Y { get; }

        public Translation2d(Length x, Length y)
        {
            X = x;
            Y = y;
        }

        public Length Distance(in Translation2d other)
        {
            var left = other.X - X;
            var right = other.Y - Y;

            var leftArea = left * left;
            var rightArea = right * right;
            var combined = leftArea + rightArea;
            return Length.FromMeters(Math.Sqrt(combined.SquareMeters));
        }

        public Length Norm()
        {

            var leftArea = X * X;
            var rightArea = Y * Y;
            var combined = leftArea + rightArea;
            return Length.FromMeters(Math.Sqrt(combined.SquareMeters));
        }

        public static Translation2d operator *(in Translation2d other, double scalar)
        {
            throw new NotImplementedException();
        }

        public static Translation2d operator /(in Translation2d other, double scalar)
        {
            throw new NotImplementedException();
        }

        public Translation2d RotateBy(in Rotation2d other)
        {
            return new Translation2d(X * other.Cos - Y * other.Sin,
                    X * other.Sin + Y * other.Cos);
        }

        public static Translation2d operator +(in Translation2d left, in Translation2d right)
        {
            return new Translation2d(left.X + right.X, left.Y + right.Y);
        }

        public static Translation2d operator -(in Translation2d left, in Translation2d right)
        {
            return new Translation2d(left.X - right.X, left.Y - right.Y);
        }

        public static Translation2d operator -(in Translation2d other)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(in Translation2d left, in Translation2d right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(in Translation2d left, in Translation2d right)
        {
            return !(left == right);
        }


        public override bool Equals(object? obj)
        {
            return obj is Translation2d t && this == t;
        }

        public bool Equals(Translation2d other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return 0; // HashCode.Combine(X, Y);
        }
    }
}
