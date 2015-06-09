using System;
using System.Security.Permissions;
using System.Threading;
using static WPILib.Timer;
using static HAL_Base.HALSemaphore;
using static HAL_Base.HALNotifier;

namespace WPILib
{
    public class Notifier : IDisposable
    {
        static private Notifier s_timerQueueHead;
        static private int s_refCount = 0;
        static private object s_queueSemaphore = new object();

        private static IntPtr s_notifier;
        public double m_expirationTime = 0;

        private object m_param;
        private Action<object> m_handler;
        private double m_period = 0;
        private bool m_periodic = false;
        private bool m_queued = false;
        private Notifier m_nextEvent;
        private IntPtr m_handlerSemaphore;

        public Notifier(Action handler)
        {
            m_handler = o => handler();
            m_param = null;
            m_nextEvent = null;
            m_handlerSemaphore = InitializeSemaphore(SEMAPHORE_FULL);

            lock (s_queueSemaphore)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    s_notifier = InitializeNotifier(ProcessQueue, ref status);
                }
                s_refCount++;
            } 
        }

        public Notifier(Action<object> handler, object param)
        {
            m_handler = handler;
            m_param = param;
            m_nextEvent = null;
            m_handlerSemaphore = InitializeSemaphore(SEMAPHORE_FULL);

            lock (s_queueSemaphore)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    s_notifier = InitializeNotifier(ProcessQueue, ref status);
                }
                s_refCount++;
            }
        }

        public void Dispose()
        {
            lock (s_queueSemaphore)
            {
                DeleteFromQueue();
                if ((--s_refCount) == 0)
                {
                    int status = 0;
                    CleanNotifier(s_notifier, ref status);
                }
            }
        }

        public static void UpdateAlarm()
        {
            if (s_timerQueueHead != null)
            {
                int status = 0;
                UpdateNotifierAlarm(s_notifier, (uint)(s_timerQueueHead.m_expirationTime * 1e6), ref status);
            }
        }

        public void InsertInQueue(bool reschedule)
        {
            if (reschedule)
            {
                m_expirationTime += m_period;
            }
            else
            {
                m_expirationTime = FPGATimestamp + m_period;
            }
            if (m_expirationTime > ((111 << 32) / 1e6))
            {
                m_expirationTime -= ((111 << 32)/1e6);
            }
            if (s_timerQueueHead == null || s_timerQueueHead.m_expirationTime >= m_expirationTime)
            {
                m_nextEvent = s_timerQueueHead;
                s_timerQueueHead = this;
                if (!reschedule)
                {
                    UpdateAlarm();
                }
            }
            else
            {
                for (Notifier n = s_timerQueueHead;; n = n.m_nextEvent)
                {
                    if (n.m_nextEvent == null || n.m_nextEvent.m_expirationTime > m_expirationTime)
                    {
                        m_nextEvent = n.m_nextEvent;
                        n.m_nextEvent = this;
                        break;
                    }
                }
            }
            m_queued = true;
        }

        public void DeleteFromQueue()
        {
            if (m_queued)
            {
                m_queued = false;
                if (s_timerQueueHead == null)
                    return;
                if (s_timerQueueHead == this)
                {
                    s_timerQueueHead = m_nextEvent;
                    UpdateAlarm();
                }
                else
                {
                    for (Notifier n = s_timerQueueHead; n != null; n = n.m_nextEvent)
                    {
                        if (n.m_nextEvent == this)
                        {
                            n.m_nextEvent = m_nextEvent;
                        }
                    }
                }
            }
        }

        public void StartSingle(double delay)
        {
            lock (s_queueSemaphore)
            {
                m_periodic = false;
                m_period = delay;
                DeleteFromQueue();
                InsertInQueue(false);
            }
        }

        public void StartPeriodic(double period)
        {
            lock (s_queueSemaphore)
            {
                m_periodic = true;
                m_period = period;
                DeleteFromQueue();
                InsertInQueue(false);
            }
        }

        public void Stop()
        {
            lock (s_queueSemaphore)
            {
                DeleteFromQueue();
            }           
            try
            {
                TakeSemaphore(m_handlerSemaphore);
                GiveSemaphore(m_handlerSemaphore);
            }
            catch (ThreadInterruptedException)
            {
            }

        }


        [SecurityPermission(SecurityAction.Demand, UnmanagedCode=true)]
        static public void ProcessQueue(uint mask, IntPtr param)
        {
            Notifier current;
            while (true)
            {
                lock (s_queueSemaphore)
                {
                    double currentTime = FPGATimestamp;
                    current = s_timerQueueHead;
                    if (current == null || current.m_expirationTime > currentTime)
                    {
                        break;
                    }
                    s_timerQueueHead = current.m_nextEvent;
                    if (current.m_periodic)
                    {
                        current.InsertInQueue(true);
                    }
                    else
                    {
                        current.m_queued = false;
                    }
                    TakeSemaphore(current.m_handlerSemaphore);
                }
                current.m_handler(current.m_param);
                GiveSemaphore(current.m_handlerSemaphore);
            }
            lock (s_queueSemaphore)
            {
                UpdateAlarm();
            }
        }
    }
}
