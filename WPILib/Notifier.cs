

using System;
using System.Security.Permissions;
using WPILib.Interfaces;
using System.Threading;
using HAL_Base;
using System.Runtime.InteropServices;

namespace WPILib
{

    public delegate void TimerEventHandlerDelegate(object o);
    public class Notifier
    {
        static private Notifier s_timerQueueHead;
        static private int s_refCount = 0;
        static private object s_queueSemaphore = new object();

        private static IntPtr s_notifier;
        public double m_expirationTime = 0;

        private Action<uint, IntPtr> _delegate;

        private object m_param;
        private TimerEventHandlerDelegate m_handler;
        private double m_period = 0;
        private bool m_periodic = false;
        private bool m_queued = false;
        private Notifier m_nextEvent;
        private IntPtr m_handlerSemaphore;

        public Notifier(TimerEventHandlerDelegate handler, object param)
        {
            m_handler = handler;
            m_param = param;
            m_nextEvent = null;
            m_handlerSemaphore = HALSemaphore.InitializeSemaphore(HALSemaphore.SEMAPHORE_FULL);

            _delegate = ProcessQueue;

            lock (s_queueSemaphore)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    s_notifier = HALNotifier.InitializeNotifier(_delegate, ref status);
                }
                s_refCount++;
            }
        }

        public void Free()
        {
            lock (s_queueSemaphore)
            {
                DeleteFromQueue();
                if ((--s_refCount) == 0)
                {
                    int status = 0;
                    HALNotifier.CleanNotifier(s_notifier, ref status);
                }
            }
        }

        public static void UpdateAlarm()
        {
            if (s_timerQueueHead != null)
            {
                int status = 0;
                HALNotifier.UpdateNotifierAlarm(s_notifier, (uint)(s_timerQueueHead.m_expirationTime * 1e6), ref status);
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
                m_expirationTime = Timer.GetFPGATimestamp() + m_period;
            }
            if (m_expirationTime > ((111 << 32) / 1e6))
            {
                m_expirationTime -= ((111 << 32)/1e6);
            }
            if (s_timerQueueHead == null || s_timerQueueHead.m_expirationTime >= this.m_expirationTime)
            {
                this.m_nextEvent = s_timerQueueHead;
                s_timerQueueHead = this;
                if (!reschedule)
                {
                    UpdateAlarm();
                }
            }
            else
            {
                Notifier last = s_timerQueueHead;
                Notifier cur = last.m_nextEvent;
                bool looking = true;

                while (looking)
                {
                    cur = last.m_nextEvent;
                    if (cur == null || cur.m_expirationTime > this.m_expirationTime)
                    {
                        last.m_nextEvent = this;
                        this.m_nextEvent = cur;
                        looking = false;
                    }
                    last = last.m_nextEvent;
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
                    s_timerQueueHead = this.m_nextEvent;
                    UpdateAlarm();
                }
                else
                {
                    for (Notifier n = s_timerQueueHead; n != null; n = n.m_nextEvent)
                    {
                        if (n.m_nextEvent == this)
                        {
                            n.m_nextEvent = this.m_nextEvent;
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
                HALSemaphore.TakeSemaphore(m_handlerSemaphore);
                HALSemaphore.GiveSemaphore(m_handlerSemaphore);
            }
            catch (ThreadInterruptedException e)
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
                    double currentTime = Timer.GetFPGATimestamp();
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
                    HALSemaphore.TakeSemaphore(current.m_handlerSemaphore);
                }
                current.m_handler(current.m_param);
                HALSemaphore.GiveSemaphore(current.m_handlerSemaphore);
            }
            lock (s_queueSemaphore)
            {
                UpdateAlarm();
            }
        }
    }
}
