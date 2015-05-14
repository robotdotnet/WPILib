

using System;
using WPILib.Util;
using HAL_FRC;

namespace WPILib
{
    public class AnalogTrigger
    {
        protected IntPtr m_port;
        protected int m_index;

        protected void InitTrigger(int channel)
        {
            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            uint index = 0;

            m_port = HALAnalog.initializeAnalogTrigger(portPointer, ref index, ref status);
            m_index = (int)index;

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
            InitTrigger(channel.GetChannel());
        }

        public void Free()
        {
            int status = 0;
            HALAnalog.cleanAnalogTrigger(m_port, ref status);
            m_port = IntPtr.Zero;
        }

        public void SetLimitsRaw(int lower, int upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.setAnalogTriggerLimitsRaw(m_port, lower, upper, ref status);
        }

        public void SetLimitsVoltage(double lower, double upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.setAnalogTriggerLimitsVoltage(m_port, lower, upper, ref status);
        }

        public void SetAveraged(bool useAveragedValue)
        {
            int status = 0;
            HALAnalog.setAnalogTriggerFiltered(m_port, useAveragedValue, ref status);
        }

        public int GetIndex()
        {
            return m_index;
        }

        public bool GetInWindow()
        {
            int status = 0;
            bool value = HALAnalog.getAnalogTriggerInWindow(m_port, ref status);
            return value;
        }

        public bool GetTriggerState()
        {
            int status = 0;
            bool value = HALAnalog.getAnalogTriggerTriggerState(m_port, ref status);
            return value;
        }

        //Add analogTriggerOutput
    }
}
