using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufSerializable<T, MessageType> : IWPISerializable<T> where MessageType : IMessage
{
    public static abstract IProtobuf<T, MessageType> Proto { get; }
}
