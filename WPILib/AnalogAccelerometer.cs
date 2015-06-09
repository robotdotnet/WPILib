using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Interfaces;
using HAL_Base;

namespace WPILib
{
    public class AnalogAccelerometer : SensorBase, IPIDSource
    {
        private AnalogInput m_analogChannel;
        private double m_voltsPerG = 1.0;
        private double m_zeroGVoltage = 2.5;
        private bool m_allocatedChannel;

        private void InitAccelerometer()
        {
            HAL.Report(ResourceType.kResourceType_Accelerometer, (byte) m_analogChannel.Channel);
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

        public override void Dispose()
        {
            if (m_analogChannel != null && m_allocatedChannel)
            {
                m_analogChannel.Dispose();
            }
            m_analogChannel = null;
            //base.Dispose();
        }

        public double Acceleration => (m_analogChannel.AverageVoltage - m_zeroGVoltage)/m_voltsPerG;

        public double Sensitivity
        {
            set { m_voltsPerG = value; }
        }

        public double Zero
        {
            set { m_zeroGVoltage = value; }
        }

        public double PidGet => Acceleration;
    }
}
