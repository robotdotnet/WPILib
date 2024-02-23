namespace WPIUtil.Marshal;

public interface INativeArrayFree<T> where T : unmanaged
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    static abstract unsafe void FreeArray(T* array, int len);
#pragma warning restore CA1000 // Do not declare static members on generic types
}
