using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Util;
using WPILib.Interfaces;
using HAL_Base;

namespace WPILib
{
    public class AnalogPotentiometer : Potentiometer, IDisposable
    {
        private double m_fullRange, m_offset;
        private AnalogInput m_analogInput;
        private bool m_initAnalogInput;

        private void InitPot(AnalogInput input, double fullRange, double offset)
        {
            this.m_fullRange = fullRange;
            this.m_offset = offset;
            m_analogInput = input;
        }

        public AnalogPotentiometer(int channel, double fullRange, double offset)
        {
            AnalogInput input = new AnalogInput(channel);
            m_initAnalogInput = true;
            InitPot(input, fullRange, offset);
        }

        public AnalogPotentiometer(AnalogInput input, double fullRange, double offset)
        {
            m_initAnalogInput = false;
            InitPot(input, fullRange, offset);
        }

        public AnalogPotentiometer(int channel, double scale)
            : this(channel, scale, 0)
        {
        }

        public AnalogPotentiometer(AnalogInput input, double scale)
            : this(input, scale, 0)
        {
        }

        public AnalogPotentiometer(int channel)
            : this(channel, 1, 0)
        {
        }

        public AnalogPotentiometer(AnalogInput input)
            : this(input, 1, 0)
        {
        }

        public double Get()
        {
            return (m_analogInput.Voltage / ControllerPower.Voltage5V) * m_fullRange + m_offset;
        }

        public double PidGet => Get();

        public void Dispose()
        {
            if (m_initAnalogInput)
            {
                m_analogInput.Dispose();
                m_analogInput = null;
                m_initAnalogInput = false;
            }
        }
    }
}
