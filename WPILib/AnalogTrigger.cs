using System;
using HAL.Base;
using WPILib.Exceptions;
using static HAL.Base.HAL;
using static HAL.Base.HALAnalogTrigger;
using static WPILib.Utility;

namespace WPILib
{
    public enum AnalogTriggerType
    {
        InWindow = 0,
        State = 1,
        RisingPulse = 2,
        FallingPulse = 3,
    }

    /// <summary>
    /// Class for creating and configuring Analog Triggers.
    /// </summary>
    public class AnalogTrigger : IDisposable
    {
        internal int Port { get; private set; }

        /// <summary>
        /// Gets the index of the analog trigger
        /// </summary>
        public int Index { get; protected set; }

        private AnalogInput m_analogInput = null;
        private bool m_ownsAnalog = false;

        /// <summary>
        /// Constructor for an analog trigger given a channel number.
        /// </summary>
        /// <param name="channel">The port to use for the analog trigger 0-3 are on-board, 4-7 are on the MXP port</param>
        public AnalogTrigger(int channel) : this (new AnalogInput(channel))
        {
            m_ownsAnalog = true;
        }

        /// <summary>
        /// Construct an analog trigger given an analog channel.
        /// </summary>
        /// <remarks>Should be used in case of sharing an analog channel
        /// between a trigger and an input.</remarks>
        /// <param name="channel">The <see cref="AnalogInput"/> to use for the analog trigger</param>
        public AnalogTrigger(AnalogInput channel)
        {
            if (channel == null)
                throw new ArgumentNullException(nameof(channel), "The Analog Input given was null");
            m_analogInput = channel;

            int index = 0;
            int status = 0;

            Port = HAL_InitializeAnalogTrigger(m_analogInput.Port, ref index, ref status);
            CheckStatus(status);
            Index = index;

            Report(ResourceType.kResourceType_AnalogTrigger, m_analogInput.Channel);
        }

        /// <summary>
        /// Release the resources used by this object.
        /// </summary>
        public void Dispose()
        {
            int status = 0;
            HAL_CleanAnalogTrigger(Port, ref status);
            Port = 0;
            if (m_ownsAnalog)
            {
                m_analogInput?.Dispose();
            }
            CheckStatus(status);
            
        }

        /// <summary>
        /// Set the upper and lower limits of the analog trigger. The limits are
	    /// given in ADC codes.If oversampling is used, the units must be scaled
        /// appropriately.
        /// </summary>
        /// <param name="lower">The lower raw limit</param>
        /// <param name="upper">The upper raw limit</param>
        public void SetLimitsRaw(int lower, int upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HAL_SetAnalogTriggerLimitsRaw(Port, lower, upper, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Set the upper and lower limits of the analog trigger. The limits are
        /// given as floating point voltage values.
        /// </summary>
        /// <param name="lower">The lower voltage limit</param>
        /// <param name="upper">The upper voltage limit</param>
        public void SetLimitsVoltage(double lower, double upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HAL_SetAnalogTriggerLimitsVoltage(Port, lower, upper, ref status);
            CheckStatus(status);
        }

        /// <summary>
        /// Configure the analog trigger to use averaged vs. raw values.
        /// </summary>
        public bool Averaged
        {
            set
            {
                int status = 0;
                HAL_SetAnalogTriggerAveraged(Port, value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Configure the analog trigger to use a filtered value. True if filtered.
        /// </summary>
        public bool Filtered
        {
            set
            {
                int status = 0;
                HAL_SetAnalogTriggerFiltered(Port, value, ref status);
                CheckStatus(status);
            }
        }

        /// <summary>
        /// Return the InWindow output of the analog trigger. True if the analog input
        /// is between the upper and lower limits
        /// </summary>
        /// <returns>The InWindow output of the analog trigger</returns>
        public bool GetInWindow()
        {
            int status = 0;
            bool value = HAL_GetAnalogTriggerInWindow(Port, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Return the trigger state. True if above upper limit, False if below lower limit.
        /// Maintains previous value if in between limits.
        /// </summary>
        /// <returns>The TriggerState output of the analog trigger</returns>
        public bool GetTriggerState()
        {
            int status = 0;
            bool value = HAL_GetAnalogTriggerTriggerState(Port, ref status);
            CheckStatus(status);
            return value;
        }

        /// <summary>
        /// Creates an <see cref="AnalogTriggerOutput">analog trigger output</see>. 
        /// </summary>
        /// <param name="type">The type of object to create.</param>
        /// <returns>A pointer to a new <see cref="AnalogTriggerOutput"/></returns>
        public AnalogTriggerOutput CreateOutput(AnalogTriggerType type)
        {
            return new AnalogTriggerOutput(this, type);
        }
    }
}
