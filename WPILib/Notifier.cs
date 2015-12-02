using System;
using System.Threading;
using System.Collections.Generic;
using static WPILib.Timer;
using static HAL.HALSemaphore;
using static HAL.HALNotifier;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// The Notifier class is used to create alarms from the FPGA.
    /// </summary>
    public class Notifier : IDisposable
    {
        private static Action<uint, IntPtr> ProcessCallback;

        private static LinkedList<Notifier> s_timerQueue = new LinkedList<Notifier>();
        private static readonly object s_queueMutex = new object();
        private static IntPtr s_notifier = IntPtr.Zero;
        private static int s_refCount = 0;

        private Action<object> m_handler;
        private object m_param;
        private double m_period = 0;
        private double m_expirationTime = 0;
        private bool m_periodic = false;
        private bool m_queued = false;
        private readonly object m_handlerMutex;






        /// <summary>
        /// Create a notifier for the timer event notification.
        /// </summary>
        /// <param name="handler">The callback that is called at the notification time
        /// which is set using <see cref="StartSingle"/> or <see cref="StartPeriodic"/></param>
        public Notifier(Action handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "Handler must not be null");
            }

            m_handler = o => handler();
            m_param = null;
            m_handlerMutex = new object();

            lock (s_queueMutex)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    ProcessCallback = ProcessQueue;
                    s_notifier = InitializeNotifier(ProcessQueue, IntPtr.Zero, ref status);
				    CheckStatus(status);
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
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "Handler must not be null");
            }

            m_handler = handler;
            m_param = param;
            m_handlerMutex = new object();

            lock (s_queueMutex)
            {
                if (s_refCount == 0)
                {
                    int status = 0;
                    ProcessCallback = ProcessQueue;
                    s_notifier = InitializeNotifier(ProcessCallback, IntPtr.Zero, ref status);
				    CheckStatus(status);
                }
                s_refCount++;
            }
        }

        /// <summary>
        /// Disposes of the Notifier
        /// </summary>
        public void Dispose()
        {
            lock (s_queueMutex)
            {
                DeleteFromQueue();
                if ((--s_refCount) == 0)
                {
                    int status = 0;
                    CleanNotifier(s_notifier, ref status);
                    CheckStatus(status);
                }
            }

            lock (m_handlerMutex)
            {

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
            if (s_timerQueue.Count != 0)
            {
                int status = 0;
                Notifier head = s_timerQueue.First.Value;
                UpdateNotifierAlarm(s_notifier, (uint)(head.m_expirationTime * 1e6), ref status);
                CheckStatus(status);
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

            //Attempt to insert new entry into queue
            for (var i = s_timerQueue.First; i != s_timerQueue.Last; i = i.Next)
            {
                if (i.Value.m_expirationTime > m_expirationTime)
                {
                    s_timerQueue.AddBefore(i, this);
                    m_queued = true;
                }
            }

            if (!m_queued)
            {
                s_timerQueue.AddFirst(this);
                m_queued = true;

                if (!reschedule)
                {
                    UpdateAlarm();
                }
            }
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
                if (s_timerQueue.Count == 0)
                {
                    throw new InvalidOperationException("Cannot delete from an empty queue");
                }
                if (s_timerQueue.First.Value == this)
                {
                    s_timerQueue.RemoveFirst();
                    UpdateAlarm();
                }
                else
                {
                    s_timerQueue.Remove(this);
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
            lock (s_queueMutex)
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
            lock (s_queueMutex)
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
            lock (s_queueMutex)
            {
                DeleteFromQueue();
            }
            lock (m_handlerMutex)
            {

            }

        }

        /// <summary>
        /// Called by the timer to process the queue and see
        /// if there are any handlers to call.
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="param"></param>
        private static void ProcessQueue(uint currentTimeInt, IntPtr param)
        {
            Notifier current;
            while (true)
            {
                lock (s_queueMutex)
                {
                    double currentTime = currentTimeInt * 1.0e-6;
                    if (s_timerQueue.Count == 0)
                    {
                        break;
                    }
                    current = s_timerQueue.First.Value;
                    if (current.m_expirationTime > currentTime)
                    {
                        break; //No more events to process
                    }

                    s_timerQueue.RemoveFirst();

                    current.m_queued = false;

                    if (current.m_periodic)
                    {
                        current.InsertInQueue(true);
                    }

                    Monitor.Enter(current.m_handlerMutex);

                }
                current.m_handler(current.m_param);
                Monitor.Exit(current.m_handlerMutex);
            }

            lock (s_queueMutex)
            {
                UpdateAlarm();
            }
        }
    }
}
