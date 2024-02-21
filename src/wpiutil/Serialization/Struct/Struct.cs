namespace WPIUtil.Serialization.Struct;

public interface IStructBase
{
    public const int SizeBool = 1;
    public const int SizeInt16 = 4;
    public const int SizeDouble = 8;

    string TypeString { get; }
    int Size { get; }
    string Schema { get; }

    IStructBase[] Nested => [];
}

public interface IStruct<T> : IStructBase
{
    T Unpack(ref StructUnpacker buffer);

    void Pack(ref StructPacker buffer, T value);

    void UnpackInto(ref T value, ref StructUnpacker buffer)
    {
        throw new NotSupportedException("Object does not support UnpackInto");
    }
}
