namespace WPIUtil.Serialization.Struct;

public interface IStructSerializable<T> : IWPISerializable<T>
    where T : IStructSerializable<T>
{
    public static abstract IStruct<T> Struct { get; }
}
