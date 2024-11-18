using UnitsNet;
using UnitsNet.NumberExtensions.NumberToDuration;
using WPIHal.Handles;
using WPIHal.Natives;
using WPIUtil;

namespace WPILib;

public sealed class Watchdog : IDisposable
{
    private static readonly Duration MinPrintPeriod = 1.Seconds();

    private Duration m_startTime;
    private Duration m_timeout;
    private Duration m_expirationTime;
    private readonly Action m_callback;
    private Duration m_lastTimeoutPrint;

    internal bool m_isExpired;

    private readonly Tracer m_tracer;

    private static readonly PriorityQueue<Watchdog, Duration> m_watchdogs = new();
    private static readonly object m_queueMutex = new();
    private static HalNotifierHandle m_notifier;

    static Watchdog()
    {
        m_notifier = HalNotifier.InitializeNotifier();
        HalNotifier.SetNotifierName(m_notifier, "Watchdog");
        StartDaemonThread(SchedulerFunc);
    }

    public Watchdog(Duration timeout, Action callback)
    {
        m_timeout = timeout;
        m_callback = WpiGuard.RequireNotNull(callback);
        m_tracer = new();
    }

    public void Dispose()
    {
        Disable();
    }

    public Duration Time => Timer.FPGATimestamp - m_startTime;

    public Duration Timeout
    {
        get
        {
            lock (m_queueMutex)
            {
                return m_timeout;
            }
        }
        set
        {
            m_startTime = Timer.FPGATimestamp;
            m_tracer.ClearEpochs();

            lock (m_queueMutex)
            {
                m_timeout = value;
                m_isExpired = false;

                // TODO remove from queue
                m_expirationTime = m_startTime + m_timeout;
                m_watchdogs.Enqueue(this, m_expirationTime);
                UpdateAlarm();
            }
        }
    }

    public bool IsExpired
    {
        get
        {
            lock (m_queueMutex)
            {
                return m_isExpired;
            }
        }
    }

    public void AddEpoch(string epochName)
    {
        m_tracer.AddEpoch(epochName);
    }

    public void PrintEpochs()
    {
        m_tracer.PrintEpochs();
    }

    public void Reset()
    {
        Enable();
    }

    public void Enable()
    {
        m_startTime = Timer.FPGATimestamp;
        m_tracer.ClearEpochs();

        lock (m_queueMutex)
        {
            m_isExpired = false;

            // TODO remove
            m_expirationTime = m_startTime + m_timeout;
            m_watchdogs.Enqueue(this, m_expirationTime);
            UpdateAlarm();
        }
    }

    public void Disable()
    {
        lock (m_queueMutex)
        {
            GC.KeepAlive(this);
            UpdateAlarm();
        }
    }

    public bool SuppressTimeoutMessage { get; set; }

    private static void UpdateAlarm()
    {
        if (m_watchdogs.Count == 0)
        {
            HalNotifier.CancelNotifierAlarm(m_notifier);
        }
        else
        {
            HalNotifier.UpdateNotifierAlarm(m_notifier, (ulong)m_watchdogs.Peek().m_expirationTime.Microseconds);
        }
    }

    private static Thread StartDaemonThread(ThreadStart target)
    {
        Thread inst = new(target)
        {
            IsBackground = true
        };
        inst.Start();
        return inst;
    }

    private static void SchedulerFunc()
    {
        while (true)
        {
            ulong curTime = HalNotifier.WaitForNotifierAlarm(m_notifier);
            if (curTime == 0)
            {
                break;
            }

            Watchdog watchdog;

            lock (m_queueMutex)
            {
                if (m_watchdogs.Count == 0)
                {
                    continue;
                }

                // If the condition variable timed out, that means a Watchdog timeout
                // has occurred, so call its timeout function.
                watchdog = m_watchdogs.Dequeue();

                Duration now = curTime.Microseconds();
                if (now - watchdog.m_lastTimeoutPrint > MinPrintPeriod)
                {
                    watchdog.m_lastTimeoutPrint = now;
                    if (!watchdog.SuppressTimeoutMessage)
                    {
                        DriverStation.ReportWarning($"Watchdog not fed within {watchdog.m_timeout}", false);
                    }
                }

                // Set expiration flag before calling the callback so any
                // manipulation of the flag in the callback (e.g., calling
                // Disable()) isn't clobbered.
                watchdog.m_isExpired = true;
            }
            watchdog.m_callback();
            lock (m_queueMutex)
            {
                UpdateAlarm();
            }
        }
    }
}
