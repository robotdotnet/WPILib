using WPIUtil.Handles;
using WPIUtil.Natives;

namespace WPIUtil;

public class EventVector
{
    private readonly object m_lock = new();
    private readonly List<int> m_events = [];

    public void Add(int handle)
    {
        lock (m_lock)
        {
            m_events.Add(handle);
        }
    }

    public void Remove(int handle)
    {
        lock (m_lock)
        {
            m_events.Remove(handle);
        }
    }

    public void Wakeup()
    {
        lock (m_lock)
        {
            foreach (int handle in m_events)
            {
                SynchronizationNative.SetEvent(new WpiEventHandle(handle));
            }
        }
    }
}
