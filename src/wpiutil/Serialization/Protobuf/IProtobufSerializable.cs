using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufSerializable<T> : IWPISerializable<T>
{
    public static abstract IProtobuf<T> Proto { get; }
}

public interface IProtobufSerializable<T, TProto> : IProtobufSerializable<T> where TProto : IMessage<TProto>
{
    public static new abstract IProtobuf<T, TProto> Proto { get; }
}
