using Google.Protobuf;
using Google.Protobuf.Reflection;
using WPIUtil.Function;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobuf
{
    string TypeString => $"proto:{Descriptor.FullName}";
    MessageDescriptor Descriptor { get; }
    IProtobuf[] Nested => [];

    void ForEachDescriptor(Function<string, bool> exists, BiConsumer<string, byte[]> fn)
    {
        ForEachDescriptorImpl(Descriptor.File, exists, fn);
    }

    private static void ForEachDescriptorImpl(FileDescriptor desc, Function<string, bool> exists, BiConsumer<string, byte[]> fn)
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

public interface IGenericProtobuf<T> : IProtobuf
{
    IMessage GenericCreateMessage();

    T GenericUnpack(IMessage msg);

    void GenericPack(IMessage msg, T value);

    void GenericUnpackInto(ref T value, IMessage msg);
}

public interface IProtobuf<T, MessageType> : IGenericProtobuf<T> where MessageType : IMessage<MessageType>
{
    IMessage IGenericProtobuf<T>.GenericCreateMessage()
    {
        return CreateMessage();
    }

    T IGenericProtobuf<T>.GenericUnpack(IMessage msg)
    {
        return Unpack((MessageType)msg);
    }

    void IGenericProtobuf<T>.GenericPack(IMessage msg, T value)
    {
        Pack((MessageType)msg, value);
    }

    void IGenericProtobuf<T>.GenericUnpackInto(ref T value, IMessage msg)
    {
        UnpackInto(ref value, (MessageType)msg);
    }

    MessageType CreateMessage();

    T Unpack(MessageType msg);

    void Pack(MessageType msg, T value);

    void UnpackInto(ref T value, MessageType msg)
    {

        throw new NotSupportedException("Object does not support UnpackInto");
    }
}
