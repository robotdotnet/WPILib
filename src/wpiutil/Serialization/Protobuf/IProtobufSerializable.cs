using Google.Protobuf;

namespace WPIUtil.Serialization.Protobuf;

public interface IProtobufSerializable<T> : IWPISerializable<T>
{
    public static abstract IProtobuf<T> Proto { get; }
}
