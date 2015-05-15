

using System;
using WPILib.Interfaces;
using System.Threading;
using HAL_FRC;
using System.Runtime.InteropServices;

namespace WPILib
{
    public class Notifier
    {
        static private Notifier s_timerQueueHead;
        static private int s_refCount = 0;
        static private object s_queueSemaphore = new object();

        protected static IntPtr s_notifier = IntPtr.Zero;
        public double m_expirationTime = 0;

        public void s_notifier_process_queue(uint param0, IntPtr param1)
        {
            Notifier.ProcessQueue((int)param0);
        }

        //private NotifierDelegate _delegate;
        private IntPtr NotifierDelegate;
        private NotifierDelegate notDel;

        protected object m_param;
        protected TimerEventHandler m_handler;
        public double m_period = 0;
        public bool m_periodic = false;
        public bool m_queued = false;
        public Notifier m_nextEvent;
        //public Semaphore m_handlerSemaphore = new Semaphore(1, 1);

        public IntPtr m_handlerSemaphore;



        public Notifier(TimerEventHandler handler, object param)
        {
            m_handler = handler;
            m_param = param;

            notDel = s_notifier_process_queue;
            Marshal.GetFunctionPointerForDelegate(notDel);
            m_handlerSemaphore = HALSemaphore.initializeSemaphore(1);
            lock (s_queueSemaphore)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    s_notifier = HALNotifier.initializeNotifier(NotifierDelegate , ref status);
                }
                s_refCount++;
            }
        }

        public static void UpdateAlarm()
        {
            if (s_timerQueueHead != null)
            {
                int status = 0;
                HALNotifier.updateNotifierAlarm(s_notifier, (uint)(s_timerQueueHead.m_expirationTime * 1e6), ref status);
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
                Notifier cur;
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
                HALSemaphore.takeSemaphore(m_handlerSemaphore);
                HALSemaphore.giveSemaphore(m_handlerSemaphore);
            }
            catch (ThreadInterruptedException e)
            {
            }
        }

        static public void ProcessQueue(int mask)//, object param)
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
                    try
                    {
                        //current.m_handlerSemaphore.WaitOne();
                        HALSemaphore.takeSemaphore(current.m_handlerSemaphore);
                    }
                    catch (ThreadInterruptedException e)
                    {
                    }
                }
                current.m_handler.Update(current.m_param);//.Update(current.m_param);
                HALSemaphore.giveSemaphore(current.m_handlerSemaphore);
                //current.m_handlerSemaphore.Release();
            }
            lock (s_queueSemaphore)
            {
                UpdateAlarm();
            }
        }
    }
}
