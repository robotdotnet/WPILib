using System;
using UnitsNet;

namespace WPILib.Geometry
{
    public readonly struct Twist2d : IEquatable<Twist2d>
    {
        public Length dX { get; }
        public Length dY { get; }
        public Angle dTheta { get; }

        public Twist2d(in Length dx, in Length dy, in Angle dtheta)
        {
            dX = dx;
            dY = dy;
            dTheta = dtheta;
        }

        public static bool operator ==(in Twist2d left, in Twist2d right)
        {
            return left.dX == right.dX &&
                   left.dY == right.dY &&
                   left.dTheta == right.dTheta;
        }

        public static bool operator !=(in Twist2d left, in Twist2d right)
        {
            return !(left == right);
        }

        public bool Equals(Twist2d other)
        {
            return this == other;
        }

        public override int GetHashCode()
        {
            return 0; // HashCode.Combine(dX, dY, dTheta);
        }

        public override bool Equals(object? obj)
        {
            return obj is Twist2d t && this == t;
        }
    }
}
