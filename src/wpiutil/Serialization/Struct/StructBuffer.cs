namespace WPIUtil.Serialization.Struct;

public struct StructBuffer<T> where T : IStructSerializable<T>
{
    private readonly int m_structSize;
    private byte[] m_buf;
    public IStruct<T> Struct { get; }

    public readonly string TypeString => Struct.TypeString;

    public StructBuffer()
    {
        Struct = T.Struct;
        m_structSize = Struct.Size;
        m_buf = new byte[m_structSize];
    }

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

    public readonly T Read(ReadOnlySpan<byte> buffer)
    {
        StructUnpacker unpacker = new(buffer);
        return Struct.Unpack(ref unpacker);
    }

    public readonly void ReadInto(ref T output, ReadOnlySpan<byte> buffer)
    {
        StructUnpacker unpacker = new(buffer);
        Struct.UnpackInto(ref output, ref unpacker);
    }

    public readonly ReadOnlySpan<T> ReadInto(Span<T> output, ReadOnlySpan<byte> buffer, out bool copiedAll)
    {
        if ((buffer.Length % m_structSize) != 0)
        {
            throw new StructBufferException("Buffer size not a multiple of struct size");
        }
        int nelem = buffer.Length / m_structSize;
        int length = int.Min(nelem, output.Length);
        copiedAll = length == nelem;
        StructUnpacker unpacker = new(buffer);
        for (int i = 0; i < length; i++)
        {
            Struct.UnpackInto(ref output[0], ref unpacker);
        }
        return output[..length];
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

    public readonly T[] ReadArray(ReadOnlySpan<byte> buffer)
    {
        if ((buffer.Length % m_structSize) != 0)
        {
            throw new StructBufferException("Buffer size not a multiple of struct size");
        }
        int nelem = buffer.Length / m_structSize;
        T[] arr = new T[nelem];
        StructUnpacker unpacker = new(buffer);
        for (int i = 0; i < nelem; i++)
        {
            arr[i] = Struct.Unpack(ref unpacker);
        }
        return arr;
    }
}
