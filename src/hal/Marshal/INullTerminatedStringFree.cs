namespace WPIHal.Marshal;

public interface INullTerminatedStringFree<T> where T : unmanaged
{
#pragma warning disable CA1000 // Do not declare static members on generic types
    static abstract unsafe void FreeString(T* str);
#pragma warning restore CA1000 // Do not declare static members on generic types
}
