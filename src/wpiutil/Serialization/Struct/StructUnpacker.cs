using System.Buffers.Binary;
using CommunityToolkit.Diagnostics;

namespace WPIUtil.Serialization.Struct;

public ref struct StructUnpacker(ReadOnlySpan<byte> data)
{
    private readonly ReadOnlySpan<byte> m_data = data;
    private int m_length = 0;

    public StructUnpacker() : this(default)
    {

    }

    public readonly ReadOnlySpan<byte> Used => m_data[..m_length];
    public readonly ReadOnlySpan<byte> Remaining => m_data[m_length..];

    public void Advance(int toAdvance)
    {
        int newValue = checked(toAdvance + m_length);
        if (newValue < 0 || newValue > m_data.Length)
        {
            ThrowHelper.ThrowArgumentOutOfRangeException($"{nameof(toAdvance)} with value {toAdvance} would cause length to go out of range");
        }
        m_length = newValue;
    }

    public byte Read8()
    {
        byte ret = Remaining[0];
        m_length++;
        return ret;
    }

    public short Read16()
    {
        short ret = BinaryPrimitives.ReadInt16LittleEndian(Remaining);
        m_length += 2;
        return ret;
    }

    public int Read32()
    {
        int ret = BinaryPrimitives.ReadInt32LittleEndian(Remaining);
        m_length += 4;
        return ret;
    }

    public long Read64()
    {
        long ret = BinaryPrimitives.ReadInt64LittleEndian(Remaining);
        m_length += 8;
        return ret;
    }

    public float ReadFloat()
    {
        float ret = BinaryPrimitives.ReadSingleLittleEndian(Remaining);
        m_length += 4;
        return ret;
    }

    public double ReadDouble()
    {
        double ret = BinaryPrimitives.ReadDoubleLittleEndian(Remaining);
        m_length += 8;
        return ret;
    }
}
