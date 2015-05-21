using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Interfaces;
using HAL_Base;

namespace WPILib
{
    public class AnalogAccelerometer : SensorBase, PIDSource
    {
        private AnalogInput m_analogChannel;
        private double m_voltsPerG = 1.0;
        private double m_zeroGVoltage = 2.5;
        private bool m_allocatedChannel;

        private void InitAccelerometer()
        {
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte) m_analogChannel.GetChannel());
        }

        public AnalogAccelerometer(int channel)
        {
            m_allocatedChannel = true;
            m_analogChannel = new AnalogInput(channel);
            InitAccelerometer();
        }

        public AnalogAccelerometer(AnalogInput channel)
        {
            m_allocatedChannel = false;
            if (channel == null)
                throw new NullReferenceException("Analog Channel given was null");
            m_analogChannel = channel;
            InitAccelerometer();
        }

        public override void Free()
        {
            if (m_analogChannel != null && m_allocatedChannel)
            {
                m_analogChannel.Free();
            }
            m_analogChannel = null;
            //base.Free();
        }

        public double GetAcceleration()
        {
            return (m_analogChannel.GetAverageVoltage() - m_zeroGVoltage)/m_voltsPerG;
        }

        public void SetSensitivity(double sensitivity)
        {
            m_voltsPerG = sensitivity;
        }

        public void SetZero(double zero)
        {
            m_zeroGVoltage = zero;
        }

        public double PidGet()
        {
            return GetAcceleration();
        }
    }
}
