namespace WPIUtil.Serialization.Struct;

public interface IStructSerializable<T> : IWPISerializable<T>
    where T : IStructSerializable<T>
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    public static abstract IStruct<T> Struct { get; }
#pragma warning restore CA1000 // Do not declare static members on generic types
}
