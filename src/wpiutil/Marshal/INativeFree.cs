namespace WPIUtil.Marshal;

public interface INativeFree<T> where T : unmanaged
{
    static abstract unsafe void Free(T* ptr);
}
