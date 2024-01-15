using System;
using System.Collections;

namespace WPIUtil.Serialization;

public struct StructBuffer<T>
{
    private readonly int m_structSize;
    private byte[] m_buf;

    private StructBuffer(Struct<T> value)
    {
        m_structSize = value.Size;
        m_buf = new byte[m_structSize];
        Struct = value;
    }

    public static StructBuffer<T> Create(Struct<T> value) => new(value);

    public Struct<T> Struct { get; }

    public readonly string TypeString => Struct.TypeString;

    public void Reserve(int nelem)
    {
        if ((nelem * m_structSize) > m_buf.Length)
        {
            m_buf = new byte[(nelem * m_structSize)];
        }
    }

    public readonly ReadOnlySpan<byte> Write(T value) => Struct.Pack(m_buf, value);

    public ReadOnlySpan<byte> WriteArray(ReadOnlySpan<T> values)
    {
        if ((values.Length * m_structSize) > m_buf.Length)
        {
            m_buf = new byte[values.Length * m_structSize];
        }
        Span<byte> buf = m_buf;
        int writeLength = 0;
        foreach (T v in values)
        {
            writeLength += Struct.Pack(buf[writeLength..], v).Length;
        }
        return m_buf.AsSpan()[..writeLength];
    }
}
