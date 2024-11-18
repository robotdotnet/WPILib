using UnitsNet;
using WPIHal.Handles;
using WPIHal.Natives;
using WPIUtil;

namespace WPILib;

public class Notifier : IDisposable
{
    private Thread m_thread;

    private readonly object m_processLock = new();

    private int m_notifier;

    private Action m_callback;

    private Duration m_expirationTime;

    private Duration m_period;

    private bool m_periodic;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        HalNotifierHandle handle = new(Interlocked.Exchange(ref m_notifier, 0));
        if (handle.Handle == 0)
        {
            return;
        }

        HalNotifier.StopNotifier(handle);
        if (m_thread.IsAlive)
        {
            m_thread.Interrupt();
            m_thread.Join();
        }
        HalNotifier.CleanNotifier(handle);
    }

    private void UpdateAlarm(long triggerTime)
    {
        HalNotifierHandle handle = new(Interlocked.CompareExchange(ref m_notifier, 0, 0));
        if (handle.Handle == 0)
        {
            return;
        }

        HalNotifier.UpdateNotifierAlarm(handle, (uint)triggerTime);
    }

    private void UpdateAlarm()
    {
        UpdateAlarm((long)m_expirationTime.Microseconds);
    }

    public Notifier(Action callback, string name = "Notifier")
    {
        m_callback = WpiGuard.RequireNotNull(callback, nameof(callback));

        var notifier = HalNotifier.InitializeNotifier();

        m_notifier = notifier.Handle;
        HalNotifier.SetNotifierName(notifier, name);

        m_thread = new(() =>
        {
            while (true)
            {
                HalNotifierHandle handle = new(Interlocked.CompareExchange(ref m_notifier, 0, 0));
                if (handle.Handle == 0)
                {
                    break;
                }
                ulong curTime = HalNotifier.WaitForNotifierAlarm(handle);
                if (curTime == 0)
                {
                    break;
                }

                Action? threadHandler;
                lock (m_processLock)
                {
                    threadHandler = m_callback;
                    if (m_periodic)
                    {
                        m_expirationTime += m_period;
                        UpdateAlarm();
                    }
                    else
                    {
                        UpdateAlarm(-1);
                    }
                }

                threadHandler?.Invoke();
            }
        })
        {
            Name = name,
            IsBackground = true,
        };
        m_thread.Start();
    }

    public void SetCallback(Action callback)
    {
        lock (m_processLock)
        {
            m_callback = WpiGuard.RequireNotNull(callback, nameof(callback));
        }
    }

    public void StartSingle(Duration delay)
    {
        lock (m_processLock)
        {
            m_periodic = false;
            m_period = delay;
            m_expirationTime = Timer.FPGATimestamp + delay;
            UpdateAlarm();
        }
    }

    public void StartPeriodic(Duration period)
    {
        lock (m_processLock)
        {
            m_periodic = true;
            m_period = period;
            m_expirationTime = Timer.FPGATimestamp + period;
            UpdateAlarm();
        }
    }

    public void Stop()
    {
        lock (m_processLock)
        {
            m_periodic = false;
            HalNotifier.CancelNotifierAlarm(new(Interlocked.CompareExchange(ref m_notifier, 0, 0)));
        }
    }

    public static bool SetHalThreadPriority(bool realTime, int priority)
    {
        return HalNotifier.SetNotifierThreadPriority(realTime, priority);
    }
}
