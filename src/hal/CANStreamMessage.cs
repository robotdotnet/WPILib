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
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
        private byte _element0;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
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
#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
        private byte _element0;
#pragma warning restore IDE0051 // Remove unused private members
#pragma warning restore IDE0044 // Add readonly modifier
    }
}
