using System;

namespace WPILib
{
    public readonly struct AnalogAccumulatorOutput : IEquatable<AnalogAccumulatorOutput>
    {
        public long Count { get; }
        public long Value { get; }

        public AnalogAccumulatorOutput(long count, long value)
        {
            Count = count;
            Value = value;
        }

        public override bool Equals(object? obj)
        {
            return obj is AnalogAccumulatorOutput output && Equals(output);
        }

        public bool Equals(AnalogAccumulatorOutput other)
        {
            return Count == other.Count &&
                   Value == other.Value;
        }

        public override int GetHashCode()
        {
            var hashCode = -1663980258;
            hashCode = hashCode * -1521134295 + Count.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(AnalogAccumulatorOutput left, AnalogAccumulatorOutput right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AnalogAccumulatorOutput left, AnalogAccumulatorOutput right)
        {
            return !(left == right);
        }
    }
}
