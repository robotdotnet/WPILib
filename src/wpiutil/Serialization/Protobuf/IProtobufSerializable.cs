using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufSerializable<T> : IWPISerializable<T>
    where T : IProtobufSerializable<T>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static abstract IGenericProtobuf<T> Proto { get; }
#pragma warning restore CA1000 // Do not declare static members on generic types
}

public interface IProtobufSerializable<T, TProto> : IProtobufSerializable<T>
    where T : IProtobufSerializable<T, TProto>
    where TProto : IMessage<TProto>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static new abstract IProtobuf<T, TProto> Proto { get; }
#pragma warning restore CA1000 // Do not declare static members on generic types

    static IGenericProtobuf<T> IProtobufSerializable<T>.Proto => T.Proto;
}
