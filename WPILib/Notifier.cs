using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_FRC;
using WPILib.Interfaces;
using System.Threading;

namespace WPILib
{
    //public delegate void TimerEventHandler(object param);

    public class Notifier
    {
        static private Notifier s_timer_queue_head;
        static private int s_ref_count = 0;
        static private object s_queue_semaphore = new object();

        protected static IntPtr s_notifier = IntPtr.Zero;
        public double m_expiration_time = 0;

        public static void s_notifier_process_queue(uint param0, IntPtr param1)
        {
            Notifier.ProcessQueue((int)param0);
        }

        protected object m_param;
        protected TimerEventHandler m_handler;
        public double m_period = 0;
        public bool m_periodic = false;
        public bool m_queued = false;
        public Notifier m_next_event;
        public Semaphore m_handler_semaphore = new Semaphore(1, 1);


        public Notifier(TimerEventHandler handler, object param)
        {
            m_handler = handler;
            m_param = param;
            lock (s_queue_semaphore)
            {
                if(s_ref_count == 0)
                {
                    
                    int status = 0;
                    HALNotifier.initializeNotifier(s_notifier_process_queue, ref status);
                    //HALNotifier.initializeNotifier(handler, ref status);
                }
            }
        }

        public static void UpdateAlarm()
        {
            if (s_timer_queue_head != null)
            {
                int status = 0;
                HALNotifier.updateNotifierAlarm(s_notifier, (uint)(s_timer_queue_head.m_expiration_time * 1e6), ref status);
            }
        }

        public void InsertInQueue(bool reschedule)
        {
            if (reschedule)
            {
                m_expiration_time += m_period;
            }
            else
            {
                m_expiration_time = Timer.GetFPGATimestamp() + m_period;
            }
            if (s_timer_queue_head == null || s_timer_queue_head.m_expiration_time >= this.m_expiration_time)
            {
                this.m_next_event = s_timer_queue_head;
                s_timer_queue_head = this;
                if (!reschedule)
                {
                    UpdateAlarm();
                }
            }
            else
            {
                Notifier last = s_timer_queue_head;
                Notifier cur = s_timer_queue_head.m_next_event;
                bool looking = true;
                while (looking)
                {
                    if (cur == null || cur.m_expiration_time > this.m_expiration_time)
                    {
                        last.m_next_event = this;
                        this.m_next_event = cur;
                        looking = false;
                    }
                }
            }
            m_queued = false;
        }

        public void DeleteFromQueue()
        {
            if (m_queued)
            {
                m_queued = false;
                if (s_timer_queue_head == null)
                    return;
                if (s_timer_queue_head == this)
                {
                    s_timer_queue_head = this.m_next_event;
                    UpdateAlarm();
                }
                else
                {
                    for (Notifier n = s_timer_queue_head; n != null; n = n.m_next_event)
                    {
                        if(n.m_next_event == this)
                        {
                            n.m_next_event = this.m_next_event;
                        }
                    }
                }
            }
        }

        public void StartSingle(double delay)
        {
            lock (s_queue_semaphore)
            {
                m_periodic = false;
                m_period = delay;
                DeleteFromQueue();
                InsertInQueue(false);
            }
        }

        public void StartPeriodic(double period)
        {
            lock (s_queue_semaphore)
            {
                m_periodic = true;
                m_period = period;
                DeleteFromQueue();
                InsertInQueue(false);
            }
        }

        public void Stop()
        {
            lock (s_queue_semaphore)
            {
                DeleteFromQueue();
            }
            try
            {
                m_handler_semaphore.WaitOne();
                m_handler_semaphore.Release();
            }
            catch (ThreadInterruptedException e)
            {

            }
        }

        static public void ProcessQueue(int mask)//, object param)
        {
            Notifier current;
            while(true)
            {
                lock(s_queue_semaphore)
                {
                    double current_time = Timer.GetFPGATimestamp();
                    current = s_timer_queue_head;
                    if (current == null || current.m_expiration_time > current_time)
                    {
                        break;
                    }
                    s_timer_queue_head = current.m_next_event;
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
                        current.m_handler_semaphore.WaitOne();
                    }
                    catch (ThreadInterruptedException e)
                    {

                    }
                }
                current.m_handler.Update(current.m_param);//.Update(current.m_param);
                current.m_handler_semaphore.Release();
            }
            lock(s_queue_semaphore)
            {
                UpdateAlarm();
            }
        }
    }
}
