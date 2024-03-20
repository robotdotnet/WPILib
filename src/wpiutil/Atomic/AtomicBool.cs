using System.Runtime.CompilerServices;

namespace WPIUtil.Atomic;

public sealed class AtomicBool
{
    private int m_value;

    public AtomicBool()
    {
        m_value = 0;
    }

    public AtomicBool(bool initialValue)
    {
        m_value = initialValue ? 1 : 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Get()
    {
        return Interlocked.CompareExchange(ref m_value, 0, 0) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool GetAndSet(bool newValue)
    {
        return Interlocked.Exchange(ref m_value, newValue ? 1 : 0) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(bool newValue)
    {
        Interlocked.Exchange(ref m_value, newValue ? 1 : 0);
    }
}
