

using System;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class AnalogTrigger : IDisposable
    {
        internal IntPtr Port { get; private set; }

        internal int Index { get; private set; }

        protected void InitTrigger(int channel)
        {
            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            uint index = 0;

            Port = HALAnalog.InitializeAnalogTrigger(portPointer, ref index, ref status);
            Index = (int)index;

            HAL.Report(ResourceType.kResourceType_AnalogTrigger, (byte)channel);
        }

        public AnalogTrigger(int channel)
        {
            InitTrigger(channel);
        }

        public AnalogTrigger(AnalogInput channel)
        {
            if (channel == null)
                throw new NullReferenceException("The Analog Input given was null");
            InitTrigger(channel.Channel);
        }

        public void Dispose()
        {
            int status = 0;
            HALAnalog.CleanAnalogTrigger(Port, ref status);
            Port = IntPtr.Zero;
        }

        public void SetLimitsRaw(int lower, int upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.SetAnalogTriggerLimitsRaw(Port, lower, upper, ref status);
        }

        public void SetLimitsVoltage(double lower, double upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.SetAnalogTriggerLimitsVoltage(Port, lower, upper, ref status);
        }

        public bool Averaged
        {
            set
            {
                int status = 0;
                HALAnalog.SetAnalogTriggerFiltered(Port, value, ref status);
            }
        }

        public bool InWindow
        {
            get
            {
                int status = 0;
                bool value = HALAnalog.GetAnalogTriggerInWindow(Port, ref status);
                return value;
            }
        }

        public bool TriggerState
        {
            get
            {
                int status = 0;
                bool value = HALAnalog.GetAnalogTriggerTriggerState(Port, ref status);
                return value;
            }
        }

        public AnalogTriggerOutput CreateOutput(AnalogTriggerType type)
        {
            return new AnalogTriggerOutput(this, type);
        }
    }
}
