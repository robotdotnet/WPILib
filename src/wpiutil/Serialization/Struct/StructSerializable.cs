namespace WPIUtil.Serialization.Struct;

public interface IStructSerializable<T> : IWPISerializable<T>
{
    public static abstract IStruct<T> Struct { get; }
}