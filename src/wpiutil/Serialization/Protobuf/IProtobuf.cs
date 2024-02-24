using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobuf
{
    string TypeString => $"proto:{Descriptor.FullName}";
    MessageDescriptor Descriptor { get; }
    IProtobuf[] Nested => [];

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

public interface IGenericProtobuf<T> : IProtobuf
{
    IMessage GenericCreateMessage();

    T GenericUnpack(IMessage msg);

    void GenericPack(IMessage msg, T value);

    void GenericUnpackInto(ref T value, IMessage msg);
}

public interface IProtobuf<T, TMessageType> : IGenericProtobuf<T> where TMessageType : IMessage<TMessageType>
{
    IMessage IGenericProtobuf<T>.GenericCreateMessage()
    {
        return CreateMessage();
    }

    T IGenericProtobuf<T>.GenericUnpack(IMessage msg)
    {
        return Unpack((TMessageType)msg);
    }

    void IGenericProtobuf<T>.GenericPack(IMessage msg, T value)
    {
        Pack((TMessageType)msg, value);
    }

    void IGenericProtobuf<T>.GenericUnpackInto(ref T value, IMessage msg)
    {
        UnpackInto(ref value, (TMessageType)msg);
    }

    TMessageType CreateMessage();

    T Unpack(TMessageType msg);

    void Pack(TMessageType msg, T value);

    void UnpackInto(ref T value, TMessageType msg)
    {

        throw new NotSupportedException("Object does not support UnpackInto");
    }
}
