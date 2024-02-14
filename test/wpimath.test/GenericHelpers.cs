using Google.Protobuf;
using WPIUtil.Serialization.Protobuf;
using WPIUtil.Serialization.Struct;

namespace WPIMath.Test;

public static class GenericHelpers
{
    public static T StructRoundTrip<T>(T start) where T : IStructSerializable<T>
    {
        StructPacker packer = new(new byte[T.Struct.Size]);
        T.Struct.Pack(ref packer, start);
        StructUnpacker unpacker = new StructUnpacker(packer.Filled);
        return T.Struct.Unpack(ref unpacker);
    }

    public static T ProtoRoundTrip<T>(T start) where T : IProtobufSerializable<T>
    {
        var proto = T.ProtoGeneric.GenericCreateMessage();
        T.ProtoGeneric.GenericPack(proto, start);
        return T.ProtoGeneric.GenericUnpack(proto);
    }

    public static T ProtoTypedRoundTrip<T, U>(T start) where T : IProtobufSerializable<T, U> where U : IMessage<U>
    {
        var proto = T.Proto.CreateMessage();
        T.Proto.Pack(proto, start);
        return T.Proto.Unpack(proto);
    }
}
