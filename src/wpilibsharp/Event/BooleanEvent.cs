using CommunityToolkit.Diagnostics;
using UnitsNet;
using WPIMath.Filter;
using WPIUtil;
using WPIUtil.Atomic;

namespace WPILib.Event;

public class BooleanEvent
{
    protected EventLoop Loop { get; }

    private readonly Func<bool> m_signal;
    private readonly AtomicBool m_state = new(false);

    public BooleanEvent(EventLoop loop, Func<bool> signal)
    {
        Loop = WpiGuard.RequireNotNull(loop);
        m_signal = WpiGuard.RequireNotNull(signal);
        m_state.Set(m_signal());
        loop.Bind(() => m_state.Set(m_signal()));
    }

    public bool Get()
    {
        return m_state.Get();
    }

    public void IfHigh(Action action)
    {
        Loop.Bind(() =>
        {
            if (m_state.Get())
            {
                action();
            }
        });
    }

    public BooleanEvent Rising()
    {
        bool previous = m_state.Get();
        return new BooleanEvent(Loop, () =>
        {
            bool present = m_state.Get();
            bool ret = !previous && present;
            previous = present;
            return ret;
        });
    }

    public BooleanEvent Falling()
    {
        bool previous = m_state.Get();
        return new BooleanEvent(Loop, () =>
        {
            bool present = m_state.Get();
            bool ret = previous && !present;
            previous = present;
            return ret;
        });
    }

    public BooleanEvent Debounce(Duration duration, Debouncer.DebounceType type = Debouncer.DebounceType.Rising)
    {
        Debouncer debouncer = new(duration, type);
        return new BooleanEvent(Loop, () =>
        {
            return debouncer.Calculate(m_state.Get());
        });
    }

    public BooleanEvent Negate()
    {
        return new BooleanEvent(Loop, () => !m_state.Get());
    }

    public BooleanEvent And(Func<bool> other)
    {
        Guard.IsNotNull(other);
        return new BooleanEvent(Loop, () => m_state.Get() && other());
    }

    public BooleanEvent Or(Func<bool> other)
    {
        Guard.IsNotNull(other);
        return new BooleanEvent(Loop, () => m_state.Get() || other());
    }

    public T CastTo<T>(Func<EventLoop, Func<bool>, T> ctor)
    {
        return ctor(Loop, m_state.Get);
    }
}
