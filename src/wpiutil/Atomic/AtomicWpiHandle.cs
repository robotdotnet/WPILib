using System.Runtime.CompilerServices;
using WPIUtil.Handles;

namespace WPIUtil.Atomic;

public sealed class AtomicWpiHandle<T> where T : struct, IWPIIntHandle
{
    private int m_value;

    public AtomicWpiHandle()
    {
        m_value = 0;
    }

    public AtomicWpiHandle(T initialValue)
    {
        m_value = initialValue.Handle;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Get()
    {
        T ret = default;
        ret.Handle = Interlocked.CompareExchange(ref m_value, 0, 0);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetAndSet(T newValue)
    {
        T ret = default;
        ret.Handle = Interlocked.Exchange(ref m_value, newValue.Handle);
        return ret;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(T newValue)
    {
        Interlocked.Exchange(ref m_value, newValue.Handle);
    }
}
