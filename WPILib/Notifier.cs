using System;
using System.Security.Permissions;
using System.Threading;
using static WPILib.Timer;
using static HAL_Base.HALSemaphore;
using static HAL_Base.HALNotifier;

namespace WPILib
{
    /// <summary>
    /// The Notifier class is used to create alarms from the FPGA.
    /// </summary>
    public class Notifier : IDisposable
    {
        static private Notifier s_timerQueueHead;
        static private int s_refCount = 0;
        static private object s_queueSemaphore = new object();

        private static IntPtr s_notifier;
        private double m_expirationTime = 0;

        private object m_param;
        private Action<object> m_handler;
        private double m_period = 0;
        private bool m_periodic = false;
        private bool m_queued = false;
        private Notifier m_nextEvent;
        private IntPtr m_handlerSemaphore;

        /// <summary>
        /// Create a notifier for the timer event notification.
        /// </summary>
        /// <param name="handler">The callback that is called at the notification time
        /// which is set using <see cref="StartSingle"/> or <see cref="StartPeriodic"/></param>
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

        /// <summary>
        /// Create a notifier for the timer event notification.
        /// </summary>
        /// <param name="handler">The callback that is called at the notification time
        /// which is set using <see cref="StartSingle"/> or <see cref="StartPeriodic"/></param>
        /// <param name="param">The object to pass to the callback.</param>
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

        /// <summary>
        /// Disposes of the Notifier
        /// </summary>
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

        /// <summary>
        /// Update the alarm hardware to reflect the current first element in the queue
        /// </summary>
        /// <remarks>Compute the time the next alarm should occur based on the current time and
        /// the period for the first element in the timer queue.
        /// WARNING: this method does not do synchronization! It must be called from
        /// somewhere that is taking care of synchronizing access to the queue.</remarks>
        protected static void UpdateAlarm()
        {
            if (s_timerQueueHead != null)
            {
                int status = 0;
                UpdateNotifierAlarm(s_notifier, (uint)(s_timerQueueHead.m_expirationTime * 1e6), ref status);
            }
        }

        /// <summary>
        /// Insert this Notifier into the timer queue in the right place.
        /// </summary>
        /// <remarks>WARNING: this method does not do synchronization! It must be called from
        /// somewhere that is taking care of synchronizing access to the queue.</remarks>
        /// <param name="reschedule">If false, the scheduled alarm is based on the current
        /// time and UpdateAlarm method is called which will enable the alarm if
        /// necessary.If true, update the time by adding the period(no drift) when
        /// rescheduled periodic from ProcessQueue.This ensures that the public
        /// methods only update the queue after finishing inserting.</param>
        protected void InsertInQueue(bool reschedule)
        {
            if (reschedule)
            {
                m_expirationTime += m_period;
            }
            else
            {
                m_expirationTime = GetFPGATimestamp() + m_period;
            }
            if (m_expirationTime > ((1L << 32) / 1e6))
            {
                m_expirationTime -= ((1L << 32) / 1e6);
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
                for (Notifier n = s_timerQueueHead; ; n = n.m_nextEvent)
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

        /// <summary>
        /// Delete this Notifier from the timer queue.
        /// </summary>
        /// <remarks>WARNING: this method does not do synchronization! It must be called from
        /// somewhere that is taking care of synchronizing access to the queue.
        /// Remove this Notifier from the timer queue and adjust the next interrupt
        /// time to reflect the current top of the queue.</remarks>
        private void DeleteFromQueue()
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

        /// <summary>
        /// Register for a single event notification
        /// </summary>
        /// <remarks>A timer event is queued for a single event after the specified delay.</remarks>
        /// <param name="delay">Seconds to wait before the handler is called.</param>
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

        /// <summary>
        /// Register for periodic event notification..
        /// </summary>
        /// <remarks> timer event is queued for periodic event notification. Each time the
        /// interrupt occurs, the event will be immediately requeued for the same time
        /// interval.</remarks>
        /// <param name="period">Period in seconds to call the handler starting one
        /// period after  the call to this method.</param>
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

        /// <summary>
        /// Stop timer events from occurring.
        /// </summary>
        /// <remarks>Stop any repeating timer events from occurring. This will also remove any
        /// single notification events from the queue.
        /// If a timer-based call to the registered handler is in progress, this
        /// function will block until the handler call is complete.</remarks>
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

        /// <summary>
        /// Called by the timer to process the queue and see
        /// if there are any handlers to call.
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="param"></param>
        static private void ProcessQueue(uint mask, IntPtr param)
        {
            while (true)
            {
                Notifier current;
                lock (s_queueSemaphore)
                {
                    double currentTime = GetFPGATimestamp();
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
