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
}
