using System;
using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public struct ProtobufBuffer<T, MessageType> where MessageType : IMessage
                                                   where T : IProtobufSerializable<T, MessageType>
{
    public ProtobufBuffer()
    {
        Proto = T.Proto;
        m_msg = Proto.CreateMessage();
    }

    public IProtobuf<T, MessageType> Proto { get; }

    public readonly string TypeString => Proto.TypeString;

    public ReadOnlySpan<byte> Write(T value)
    {
        Proto.Pack(m_msg, value);
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
        return Proto.Unpack(m_msg);
    }

    public readonly void ReadInto(ref T output, ReadOnlySpan<byte> buffer)
    {
        m_msg.MergeFrom(buffer);
        Proto.UnpackInto(ref output, m_msg);
    }

    private readonly MessageType m_msg;
    private byte[] m_buf = new byte[1024];
}
