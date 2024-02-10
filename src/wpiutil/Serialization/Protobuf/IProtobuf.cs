using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufBase
{
    string TypeString => $"proto:{Descriptor.FullName}";
    MessageDescriptor Descriptor { get; }
    IProtobufBase[] Nested => [];

    void ForEachDescriptor(Func<string, bool> exists, Action<string, byte[]> fn)
    {
        ForEachDescriptorImpl(Descriptor.File, exists, fn);
    }

    private static void ForEachDescriptorImpl(FileDescriptor desc, Func<string, bool> exists, Action<string, byte[]> fn)
    {
        string name = $"proto:{desc.Name}";
        if (exists(name))
        {
            return;
        }
        foreach (var d in desc.Dependencies)
        {
            ForEachDescriptorImpl(d, exists, fn);
        }
        fn(name, desc.ToProto().ToByteArray());
    }
}

public interface IProtobuf<T> : IProtobufBase
{
    IMessage CreateMessage();

    T Unpack(IMessage msg);

    void Pack(IMessage msg, T value);

    void UnpackInto(ref T value, IMessage msg);
}

public interface IProtobuf<T, MessageType> : IProtobufBase where MessageType : IMessage<MessageType>
{
    internal class ProtobufWrapper(IProtobuf<T, MessageType> proto) : IProtobuf<T>
    {
        private readonly IProtobuf<T, MessageType> m_proto = proto;

        public MessageDescriptor Descriptor => m_proto.Descriptor;

        public IMessage CreateMessage() => m_proto.CreateMessage();

        public void Pack(IMessage msg, T value) => m_proto.Pack((MessageType)msg, value);

        public T Unpack(IMessage msg) => m_proto.Unpack((MessageType)msg);

        public void UnpackInto(ref T value, IMessage msg) => m_proto.UnpackInto(ref value, (MessageType)msg);
    }

    public IProtobuf<T> UntypedProto => new ProtobufWrapper(this);

    MessageType CreateMessage();

    T Unpack(MessageType msg);

    void Pack(MessageType msg, T value);

    void UnpackInto(ref T value, MessageType msg)
    {

        throw new NotSupportedException("Object does not support UnpackInto");
    }
}
