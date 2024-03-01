using System.Buffers.Binary;
using CommunityToolkit.Diagnostics;

namespace WPIUtil.Serialization.Struct;

public ref struct StructPacker(Span<byte> data)
{
    private readonly Span<byte> m_data = data;
    private int m_length = 0;

    public StructPacker() : this(default)
    {

    }

    public readonly ReadOnlySpan<byte> Filled => m_data[..m_length];
    public readonly Span<byte> Remaining => m_data[m_length..];

    public void Advance(int toAdvance)
    {
        int newValue = checked(toAdvance + m_length);
        if (newValue < 0 || newValue > m_data.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException($"{nameof(toAdvance)} with value {toAdvance} would cause length to go out of range");
        }
        m_length = newValue;
    }

    public void Write8(byte value)
    {
        Remaining[0] = value;
        m_length++;
    }

    public void Write16(short value)
    {
        BinaryPrimitives.WriteInt16LittleEndian(Remaining, value);
        m_length += 2;
    }

    public void Write32(int value)
    {
        BinaryPrimitives.WriteInt32LittleEndian(Remaining, value);
        m_length += 4;
    }

    public void Write64(long value)
    {
        BinaryPrimitives.WriteInt64LittleEndian(Remaining, value);
        m_length += 8;
    }

    public void WriteFloat(float value)
    {
        BinaryPrimitives.WriteSingleLittleEndian(Remaining, value);
        m_length += 4;
    }

    public void WriteDouble(double value)
    {
        BinaryPrimitives.WriteDoubleLittleEndian(Remaining, value);
        m_length += 8;
    }
}
