using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public struct ProtobufBuffer<T> where T : IProtobufSerializable<T>
{
    public ProtobufBuffer()
    {
        Proto = T.Proto;
        m_msg = Proto.GenericCreateMessage();
    }

    public IGenericProtobuf<T> Proto { get; }

    public readonly string TypeString => Proto.TypeString;

    public ReadOnlySpan<byte> Write(T value)
    {
        Proto.GenericPack(m_msg, value);
        int size = m_msg.CalculateSize();
        if (size > m_buf.Length)
        {
            m_buf = new byte[size];
        }
        m_msg.WriteTo(m_buf.AsSpan()[..size]);
        return m_buf.AsSpan()[..size];
    }

    public readonly T Read(ReadOnlySpan<byte> buffer)
    {
        m_msg.MergeFrom(buffer);
        return Proto.GenericUnpack(m_msg);
    }

    public readonly void ReadInto(ref T output, ReadOnlySpan<byte> buffer)
    {
        m_msg.MergeFrom(buffer);
        Proto.GenericUnpackInto(ref output, m_msg);
    }

    private readonly IMessage m_msg;
    private byte[] m_buf = new byte[1024];
}
