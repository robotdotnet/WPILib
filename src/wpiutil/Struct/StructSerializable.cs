namespace WPIUtil;

public interface IStructSerializable<T> : IWPISerializable<T> {
    public static abstract Struct<T> Struct {get;}
}