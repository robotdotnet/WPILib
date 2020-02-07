using System;
using UnitsNet;

namespace WPILib.Geometry
{
    public readonly struct Pose2d : IEquatable<Pose2d>
    {
        public Translation2d Translation { get; }
        public Rotation2d Rotation { get; }

        public Pose2d(in Translation2d translation, in Rotation2d rotation)
        {
            Translation = translation;
            Rotation = rotation;
        }

        public Pose2d(Length x, Length y, Rotation2d rotation)
        {
            Translation = new Translation2d(x, y);
            Rotation = rotation;
        }

        public static Pose2d operator +(in Pose2d left, in Transform2d right)
        {
            return left.TransformBy(right);
        }

        public static Transform2d operator -(in Pose2d left, in Pose2d right)
        {
            var pose = left.RelativeTo(right);
            return new Transform2d(pose.Translation, pose.Rotation);
        }

        public static bool operator ==(in Pose2d left, in Pose2d right)
        {
            return left.Translation == right.Translation && left.Rotation == right.Rotation;
        }

        public static bool operator !=(in Pose2d left, in Pose2d right)
        {
            return left.Translation != right.Translation || left.Rotation != right.Rotation;
        }

        public Pose2d TransformBy(in Transform2d other)
        {
            throw new NotImplementedException();
        }

        public Pose2d RelativeTo(in Pose2d other)
        {
            throw new NotImplementedException();
        }

        public Pose2d Exp(in Twist2d twist)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Pose2d other)
        {
            return this == other;
        }

        public override bool Equals(object? other)
        {
            return other is Pose2d p && this == p;
        }

        public override int GetHashCode()
        {
            return 0;
            //return HashCode.Combine(Translation, Rotation);
        }
    }
}
