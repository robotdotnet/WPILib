namespace WPIUtil.Marshal;

public interface INativeArrayFree<T> where T : unmanaged
{
    static abstract unsafe void FreeArray(T* array, int len);
}
