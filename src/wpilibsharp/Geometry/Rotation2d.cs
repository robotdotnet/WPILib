using System;
using UnitsNet;

namespace WPILib.Geometry
{
    public readonly struct Rotation2d : IEquatable<Rotation2d>, IComparable<Rotation2d>
    {
        public Angle Angle { get; }
        public double Cos { get; }
        public double Sin { get; }


        public double Tan => Sin / Cos;

        public Rotation2d(Angle value)
        {
            Angle = value;
            Cos = Math.Cos(Angle.Radians);
            Sin = Math.Sin(Angle.Radians);
        }

        public Rotation2d(double x, double y)
        {

            var magnitude = Math.Sqrt(x * x + y * y);
            if (magnitude > 1e-6)
            {
                Sin = y / magnitude;
                Cos = x / magnitude;
            }
            else
            {
                Sin = 0;
                Cos = 1;
            }
            Angle = Angle.FromRadians(Math.Atan2(Sin, Cos));

        }

        public Rotation2d RotateBy(in Rotation2d other)
        {
            return new Rotation2d(Cos * other.Cos - Sin * other.Sin, Cos * other.Sin + Sin * other.Cos);
        }

        public override bool Equals(object? obj)
        {
            return obj is Rotation2d d &&
                   Angle == d.Angle;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Angle.Degrees);
        }

        public bool Equals(Rotation2d other)
        {
            return Angle == other.Angle;
        }

        public int CompareTo(Rotation2d other)
        {
            return Angle.CompareTo(other.Angle);
        }

        public static bool operator >(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle > right.Angle;
        }

        public static bool operator >=(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle >= right.Angle;
        }

        public static bool operator <(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle < right.Angle;
        }

        public static bool operator <=(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle <= right.Angle;
        }



        public static Rotation2d operator +(in Rotation2d left, in Rotation2d right)
        {
            return left.RotateBy(right);
        }

        public static Rotation2d operator -(in Rotation2d left, in Rotation2d right)
        {
            return left + -right;
        }

        public static Rotation2d operator -(in Rotation2d left)
        {
            return new Rotation2d(-left.Angle);
        }

        public static Rotation2d operator *(in Rotation2d value, double scalar)
        {
            return new Rotation2d(value.Angle * scalar);
        }

        public static bool operator ==(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle == right.Angle;
        }
        public static bool operator !=(in Rotation2d left, in Rotation2d right)
        {
            return left.Angle != right.Angle;
        }
    }
}
