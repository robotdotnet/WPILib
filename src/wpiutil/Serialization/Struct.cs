using System;
using System.Buffers;

namespace WPIUtil.Serialization;

public interface Struct<T>
{
    string TypeString { get; }
    int Size { get; }
    string Schema { get; }

    Struct<object>[] Nested => [];

    T Unpack(ref SequenceReader<byte> buffer);

    ReadOnlySpan<byte> Pack(Span<byte> buffer, T value);
}
