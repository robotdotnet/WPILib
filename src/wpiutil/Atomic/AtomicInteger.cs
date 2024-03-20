using System.Runtime.CompilerServices;

namespace WPIUtil.Atomic;

public sealed class AtomicInteger
{
    private int m_value;

    public AtomicInteger()
    {
        m_value = 0;
    }

    public AtomicInteger(int initialValue)
    {
        m_value = initialValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Get()
    {
        return Interlocked.CompareExchange(ref m_value, 0, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetAndSet(int newValue)
    {
        return Interlocked.Exchange(ref m_value, newValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Set(int newValue)
    {
        Interlocked.Exchange(ref m_value, newValue);
    }
}
