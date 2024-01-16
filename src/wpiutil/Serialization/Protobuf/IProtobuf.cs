using System;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufBase
{
    string TypeString => $"proto:{Descriptor.FullName}";
    MessageDescriptor Descriptor { get; }
    IProtobufBase[] Nested => [];
}

public interface IProtobuf<T, MessageType> : IProtobufBase where MessageType : IMessage
{
    MessageType CreateMessage();

    T Unpack(MessageType msg);

    void Pack(MessageType msg, T value);

    void UnpackInto(ref T value, MessageType msg)
    {
        throw new NotSupportedException("Object does not support UnpackInto");
    }

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
