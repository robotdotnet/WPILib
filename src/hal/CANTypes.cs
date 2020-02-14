using System;

namespace Hal
{
    public unsafe struct CANStreamMessage : IEquatable<CANStreamMessage>
    {
#pragma warning disable CA1051 // Do not declare visible instance fields
        public uint MessageId;
        public uint TimeStamp;
        public fixed byte Data[8];
        public byte DataSize;
#pragma warning restore CA1051 // Do not declare visible instance fields

        public override bool Equals(object? obj)
        {
            return obj is CANStreamMessage message && Equals(message);
        }

        public bool Equals(CANStreamMessage other)
        {
            return MessageId == other.MessageId &&
                   TimeStamp == other.TimeStamp &&
                   DataSize == other.DataSize;
        }

        public override int GetHashCode()
        {
            var hashCode = -1376383683;
            hashCode = hashCode * -1521134295 + MessageId.GetHashCode();
            hashCode = hashCode * -1521134295 + TimeStamp.GetHashCode();
            hashCode = hashCode * -1521134295 + DataSize.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(CANStreamMessage left, CANStreamMessage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(CANStreamMessage left, CANStreamMessage right)
        {
            return !(left == right);
        }
    }
}
