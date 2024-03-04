using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal;

[StructLayout(LayoutKind.Sequential)]
public struct CANStreamMessage
{
    public uint MessageID;
    public uint TimeStamp;
    public DataBuffer Data;
    public byte DataSize;

    [System.Runtime.CompilerServices.InlineArray(8)]
    public struct DataBuffer
    {
        private byte _element0;
    }
}

public readonly struct CANMessage
{
    private readonly ulong timestamp;
    private readonly DataBuffer data;
    private readonly int dataSize;

    public CANMessage(ReadOnlySpan<byte> buffer, ulong timestamp)
    {
        Debug.Assert(buffer.Length <= 8);
        buffer.CopyTo(data);
        this.timestamp = timestamp;
        this.dataSize = buffer.Length;
    }

    [UnscopedRef]
    public ReadOnlySpan<byte> Data => data[..dataSize];

    public ulong Timestamp => timestamp;

    [InlineArray(8)]
    public struct DataBuffer
    {
        private byte _element0;
    }
}
