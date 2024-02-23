namespace WPIHal.Marshal;

public interface INullTerminatedStringFree<T> where T : unmanaged
{
    static abstract unsafe void FreeString(T* str);
}
