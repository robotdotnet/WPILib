using System;
using System.Collections;

namespace WPIUtil.Serialization;

public struct StructBuffer<T>
{
    private readonly int m_structSize;
    private byte[] m_buf;

    private StructBuffer(IStruct<T> value)
    {
        m_structSize = value.Size;
        m_buf = new byte[m_structSize];
        Struct = value;
    }

    public static StructBuffer<T> Create(IStruct<T> value) => new(value);

    public IStruct<T> Struct { get; }

    public readonly string TypeString => Struct.TypeString;

    public void Reserve(int nelem)
    {
        if ((nelem * m_structSize) > m_buf.Length)
        {
            m_buf = new byte[(nelem * m_structSize)];
        }
    }

    public readonly ReadOnlySpan<byte> Write(T value)
    {
        StructPacker packer = new(m_buf);
        Struct.Pack(ref packer, value);
        return packer.Filled;
    }

    public ReadOnlySpan<byte> WriteArray(ReadOnlySpan<T> values)
    {
        if ((values.Length * m_structSize) > m_buf.Length)
        {
            m_buf = new byte[values.Length * m_structSize];
        }
        StructPacker packer = new(m_buf);
        foreach (T v in values)
        {
            Struct.Pack(ref packer, v);
        }
        return packer.Filled;
    }
}
