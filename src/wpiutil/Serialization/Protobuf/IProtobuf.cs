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
    abstract IMessage CreateMessage();

    T Unpack(IMessage msg);

    void Pack(IMessage msg, T value);

    void UnpackInto(ref T value, IMessage msg);
}

public interface IProtobuf<T, MessageType> : IProtobuf<T> where MessageType : IMessage<MessageType>
{
    IMessage IProtobuf<T>.CreateMessage()
    {
        return CreateMessage();
    }

    void IProtobuf<T>.Pack(IMessage msg, T value)
    {
        Pack((MessageType)msg, value);
    }

    T IProtobuf<T>.Unpack(IMessage msg)
    {
        return Unpack((MessageType)msg);
    }

    void IProtobuf<T>.UnpackInto(ref T value, IMessage msg)
    {
        UnpackInto(ref value, (MessageType)msg);
    }

    new MessageType CreateMessage();

    T Unpack(MessageType msg);

    void Pack(MessageType msg, T value);

    void UnpackInto(ref T value, MessageType msg)
    {

        throw new NotSupportedException("Object does not support UnpackInto");
    }
}
