using CommunityToolkit.Diagnostics;

namespace WPILib.Event;

public sealed class EventLoop
{
    private readonly List<Action> m_bindings = [];

    private bool m_running;

    public EventLoop() { }

    public void Bind(Action action)
    {
        if (m_running)
        {
            ThrowHelper.ThrowInvalidOperationException("Cannot bind EventLoop while it is running");
        }
        m_bindings.Add(action);
    }

    public void Pool()
    {
        try
        {
            m_running = true;
            foreach (var action in m_bindings)
            {
                action();
            }
        }
        finally
        {
            m_running = false;
        }
    }

    public void Clear()
    {
        if (m_running)
        {
            ThrowHelper.ThrowInvalidOperationException("Cannot bind EventLoop while it is running");
        }
        m_bindings.Clear();
    }
}
