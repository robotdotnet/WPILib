using System;
using System.Collections.Generic;
using System.Threading;

namespace WPILib
{
    public class Watchdog : IDisposable, IComparable<Watchdog>
    {
        private static readonly TimeSpan MinPrintPeriod = TimeSpan.FromSeconds(1);

        private TimeSpan m_startTime;
        private TimeSpan m_timeout;
        private TimeSpan m_expirationTime;
        private readonly Action m_callback;
        private TimeSpan m_lastTimeoutPrintTime;
        private TimeSpan m_lastEpochsPrintTime;

        private readonly Dictionary<string, TimeSpan> m_epochs = new Dictionary<string, TimeSpan>();
        private bool m_isExpired;

        private static readonly Lazy<Thread> m_lazyThread = new Lazy<Thread>(() =>
        {
            Thread inst = new Thread(SchedulerFunc);
            inst.IsBackground = true;
            inst.Name = "Watchdog Thread";
            inst.Start();
            return inst;
        }, LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly LinkedList<Watchdog> m_watchdogs = new LinkedList<Watchdog>();
        private static readonly object m_lockObject = new object();

        public Watchdog(TimeSpan timeout, Action callback)
        {
            m_timeout = timeout;
            m_callback = callback;
            var tmp = m_lazyThread.Value;
        }

        public void Dispose()
        {
            Disable();
        }

        public int CompareTo(Watchdog other)
        {
            return m_expirationTime.CompareTo(other.m_expirationTime);
        }

        public TimeSpan Time => Timer.FPGATimestamp - m_startTime;

        public TimeSpan Timeout
        {
            get
            {
                lock (m_lockObject)
                {
                    return m_timeout;
                }
            }
            set
            {
                m_startTime = Timer.FPGATimestamp;
                m_epochs.Clear();

                lock (m_lockObject)
                {
                    m_timeout = value;
                    m_isExpired = false;
                    m_watchdogs.Remove(this);
                    m_expirationTime = m_startTime + m_timeout;
                    m_watchdogs.AddLast(this);
                    Monitor.PulseAll(m_lockObject);
                }
            }
        }

        public bool IsExpired
        {
            get
            {
                lock (m_lockObject)
                {
                    return m_isExpired;
                }
            }
        }

        public void AddEpoch(string epochName)
        {
            var currentTime = Timer.FPGATimestamp;
            m_epochs.Add(epochName, currentTime - m_startTime);
            m_startTime = currentTime;
        }

        public void PrintEpochs()
        {
            var now = Timer.FPGATimestamp;
            if (now - m_lastEpochsPrintTime > MinPrintPeriod)
            {
                m_lastEpochsPrintTime = now;
                foreach (var epoch in m_epochs)
                {
                    Console.WriteLine($"\t{epoch.Key}: {epoch.Value.ToString()}");
                }
            }
        }

        public void Reset()
        {
            Enable();
        }

        public void Enable()
        {
            m_startTime = Timer.FPGATimestamp;

            m_epochs.Clear();

            lock (m_lockObject)
            {
                m_isExpired = false;

                m_watchdogs.Remove(this);
                m_expirationTime = m_startTime + m_timeout;
                m_watchdogs.AddLast(this);
                Monitor.PulseAll(m_lockObject);
            }
        }

        public void Disable()
        {
            lock (m_lockObject)
            {
                m_watchdogs.Remove(this);
                Monitor.PulseAll(m_lockObject);
            }
        }

        public bool SuppressTimeoutMessage { get; set; }

        private static void SchedulerFunc()
        {
            lock (m_lockObject)
            {
                while (true)
                {
                    if (m_watchdogs.Count > 0)
                    {
                        bool timedOut = Monitor.Wait(m_lockObject, m_watchdogs.First.Value.m_expirationTime - Timer.FPGATimestamp);
                        if (timedOut)
                        {
                            if (m_watchdogs.Count == 0 || m_watchdogs.First.Value.m_expirationTime > Timer.FPGATimestamp)
                            {
                                continue;
                            }
                        }

                        var watchdogNode = m_watchdogs.First;
                        Watchdog watchdog = watchdogNode.Value;
                        m_watchdogs.Remove(watchdogNode);

                        var now = Timer.FPGATimestamp;
                        if (now - watchdog.m_lastTimeoutPrintTime > MinPrintPeriod)
                        {
                            watchdog.m_lastTimeoutPrintTime = now;
                            if (!watchdog.SuppressTimeoutMessage)
                            {
                                Console.WriteLine($"Watchdog not fed within {watchdog.m_timeout}");
                            }
                        }

                        // Set expiration flag before calling the callback so any
                        // manipulation of the flag in the callback (e.g., calling
                        // Disable()) isn't clobbered.
                        watchdog.m_isExpired = true;

                        Monitor.Exit(m_lockObject);
                        watchdog.m_callback?.Invoke();
                        Monitor.Enter(m_lockObject);

                    }
                    // Otherwise, a Watchdog removed itself from the queue (it notifies
                    // the scheduler of this) or a spurious wakeup occurred, so just
                    // rewait with the soonest watchdog timeout.
                    else
                    {
                        while (m_watchdogs.Count == 0)
                        {
                            Monitor.Wait(m_lockObject);
                        }
                    }
                }
            }
        }
    }
}
