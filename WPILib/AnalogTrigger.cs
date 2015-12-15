using System;
using HAL;
using HAL.Base;
using WPILib.Exceptions;
using static HAL.Base.HALAnalog;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// Class for creating and configuring Analog Triggers.
    /// </summary>
    public class AnalogTrigger : IDisposable
    {
        internal IntPtr Port { get; private set; }

        /// <summary>
        /// Gets the index of the analog trigger
        /// </summary>
        public int Index { get; protected set; }

        /// <summary>
        /// Initialize an analog trigger from a channel
        /// </summary>
        /// <param name="channel">The port to use for the analog trigger. [0..3] on RIO, [4..7] on MXP.</param>
        protected void InitTrigger(int channel)
        {
            IntPtr portPointer = HAL.Base.HAL.GetPort((byte)channel);
            int status = 0;
            uint index = 0;

            Port = InitializeAnalogTrigger(portPointer, ref index, ref status);
            Index = (int)index;
            CheckStatus(status);
            HAL.Base.HAL.Report(ResourceType.kResourceType_AnalogTrigger, (byte)channel);
        }

        /// <summary>
        /// Constructor for an analog trigger given a channel number.
        /// </summary>
        /// <param name="channel">The port to use for the analog trigger 0-3 are on-board, 4-7 are on the MXP port</param>
        public AnalogTrigger(int channel)
        {
            InitTrigger(channel);
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
                throw new NullReferenceException("The Analog Input given was null");
            InitTrigger(channel.Channel);
        }

        /// <summary>
        /// Release the resources used by this object.
        /// </summary>
        public void Dispose()
        {
            int status = 0;
            CleanAnalogTrigger(Port, ref status);
            CheckStatus(status);
            Port = IntPtr.Zero;
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
            SetAnalogTriggerLimitsRaw(Port, lower, upper, ref status);
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
            SetAnalogTriggerLimitsVoltage(Port, lower, upper, ref status);
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
                SetAnalogTriggerFiltered(Port, value, ref status);
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
                SetAnalogTriggerFiltered(Port, value, ref status);
                CheckStatus(status);
            }
        }
        
        /// <summary>
        /// Return the InWindow output of the analog trigger. True if between the limits.
        /// </summary>
        public bool InWindow
        {
            get
            {
                int status = 0;
                bool value = GetAnalogTriggerInWindow(Port, ref status);
                CheckStatus(status);
                return value;
            }
        }

        /// <summary>
        /// Return the trigger state. True if above upper limit, False if below lower limit.
        /// Maintains previous value if in between limits.
        /// </summary>
        public bool TriggerState
        {
            get
            {
                int status = 0;
                bool value = GetAnalogTriggerTriggerState(Port, ref status);
                CheckStatus(status);
                return value;
            }
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
