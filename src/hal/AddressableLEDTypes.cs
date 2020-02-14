using System;

namespace Hal
{
    public struct AddressableLEDData : IEquatable<AddressableLEDData>
    {
        public const int MaxLength = 5460;

#pragma warning disable CA1051 // Do not declare visible instance fields
        public byte B;
        public byte G;
        public byte R;
        public byte Padding;
#pragma warning restore CA1051 // Do not declare visible instance fields

        public override bool Equals(object? obj)
        {
            return obj is AddressableLEDData data && Equals(data);
        }

        public bool Equals(AddressableLEDData other)
        {
            return B == other.B &&
                   G == other.G &&
                   R == other.R;
        }

        public override int GetHashCode()
        {
            var hashCode = 376106656;
            hashCode = hashCode * -1521134295 + B.GetHashCode();
            hashCode = hashCode * -1521134295 + G.GetHashCode();
            hashCode = hashCode * -1521134295 + R.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AddressableLEDData left, AddressableLEDData right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AddressableLEDData left, AddressableLEDData right)
        {
            return !(left == right);
        }
    }
}
