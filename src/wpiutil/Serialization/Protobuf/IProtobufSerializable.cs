using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufSerializable<T> : IWPISerializable<T>
    where T : IProtobufSerializable<T>
{
    public static abstract IGenericProtobuf<T> Proto { get; }
}

public interface IProtobufSerializable<T, TProto> : IProtobufSerializable<T>
    where T : IProtobufSerializable<T, TProto>
    where TProto : IMessage<TProto>
{
    public static new abstract IProtobuf<T, TProto> Proto { get; }

    static IGenericProtobuf<T> IProtobufSerializable<T>.Proto => T.Proto;
}
