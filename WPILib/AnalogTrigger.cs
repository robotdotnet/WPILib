using System;
using System.Collections.Generic;
using System.Text;
using HAL_RoboRIO;
using WPILib.Util;

namespace WPILib
{
    public class AnalogTrigger
    {
        protected IntPtr _port;
        protected int _index;

        protected void InitTrigger(int channel)
        {
            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            uint index = 0;

            _port = HALAnalog.initializeAnalogTrigger(portPointer, ref index, ref status);
            _index = (int)index;

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
            HALAnalog.cleanAnalogTrigger(_port, ref status);
            _port = IntPtr.Zero;
        }

        public void SetLimitsRaw(int lower, int upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.setAnalogTriggerLimitsRaw(_port, lower, upper, ref status);
        }

        public void SetLimitsVoltage(double lower, double upper)
        {
            if (lower > upper)
                throw new BoundaryException("Lower bound is greater than upper");
            int status = 0;
            HALAnalog.setAnalogTriggerLimitsVoltage(_port, lower, upper, ref status);
        }

        public void SetAveraged(bool useAveragedValue)
        {
            int status = 0;
            HALAnalog.setAnalogTriggerFiltered(_port, useAveragedValue, ref status);
        }

        public int GetIndex()
        {
            return _index;
        }

        public bool GetInWindow()
        {
            int status = 0;
            bool value = HALAnalog.getAnalogTriggerInWindow(_port, ref status);
            return value;
        }

        public bool GetTriggerState()
        {
            int status = 0;
            bool value = HALAnalog.getAnalogTriggerTriggerState(_port, ref status);
            return value;
        }

        //Add analogTriggerOutput
    }
}
