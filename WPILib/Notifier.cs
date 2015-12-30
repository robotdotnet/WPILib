using System;
using static WPILib.Timer;
using static HAL.Base.HALNotifier;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// The Notifier class is used to create alarms from the FPGA.
    /// </summary>
    public class Notifier : IDisposable
    {
        private readonly object m_processMutex = new object();
        private IntPtr m_notifier;
        private Action<object> m_handler;
        private object m_param;
        private double m_expirationTime = 0;
        private double m_period = 0;
        private bool m_periodic = false;

        private readonly object m_handlerMutex = new object();

        private Action<uint, IntPtr> process;

        /// <summary>
        /// Notify is called by the HAL Layer. We simply need to pass it through to
        /// the user handler.
        /// </summary>
        /// <param name="currentTimeInt">Current FPGA Time</param>
        /// <param name="param">Param passed to the notifier</param>
        private void Notify(uint currentTimeInt, IntPtr param)
        {
            lock(m_processMutex)
            {
                if (m_periodic)
                {
                    m_expirationTime += m_period;
                    UpdateAlarm();
                }
            }
            lock (m_handlerMutex)
            {
                m_handler?.Invoke(m_param);
            }
        }


        /// <summary>
        /// Create a notifier for the timer event notification.
        /// </summary>
        /// <param name="handler">The callback that is called at the notification time
        /// which is set using <see cref="StartSingle"/> or <see cref="StartPeriodic"/></param>
        public Notifier(Action handler)
        {
            if (handler == null)
            {
                throw new ArgumentNullException(nameof(handler), "Handler must not be null.");
            }

            m_handler = o => handler();
            m_param = null;

            process = Notify;

            int status = 0;
            m_notifier = InitializeNotifier(process, IntPtr.Zero, ref status);
            CheckStatus(status);
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
                throw new ArgumentNullException(nameof(handler), "Handler must not be null.");
            }

            m_handler = handler;
            m_param = param;

            process = Notify;

            int status = 0;
            m_notifier = InitializeNotifier(process, IntPtr.Zero, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Disposes of the Notifier
        /// </summary>
        public void Dispose()
        {
            int status = 0;
            CleanNotifier(m_notifier, ref status);
            CheckStatus(status);

            lock (m_handlerMutex) { }
        }

        /// <summary>
        /// Update the HAL alarm time.
        /// </summary>
        private void UpdateAlarm()
        {
            int status = 0;
            UpdateNotifierAlarm(m_notifier, (uint)(m_expirationTime * 1e6), ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Register for a single event notification
        /// </summary>
        /// <remarks>A timer event is queued for a single event after the specified delay.</remarks>
        /// <param name="delay">Seconds to wait before the handler is called.</param>
        public void StartSingle(double delay)
        {
            lock (m_processMutex)
            {
                m_periodic = false;
                m_period = delay;
                m_expirationTime = GetFPGATimestamp() + m_period;
                UpdateAlarm();
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
            lock (m_processMutex)
            {
                m_periodic = true;
                m_period = period;
                m_expirationTime = GetFPGATimestamp() + m_period;
                UpdateAlarm();
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
            int status = 0;
            StopNotifierAlarm(m_notifier, ref status);
            CheckStatus(status);
            
            lock(m_handlerMutex) { }
        }

    }
}
