using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class AnalogOutput : SensorBase
    {
        private static Resource channels = new Resource(AnalogOutputChannels);
        private IntPtr m_port;
        private int m_channel;

        public AnalogOutput(int channel)
        {
            m_channel = channel;

            if (!HALAnalog.CheckAnalogOutputChannel((uint)channel))
            {
                throw new AllocationException("Analog output channel " + m_channel
                        + " cannot be allocated. Channel is not present.");
            }
            try
            {
                channels.Allocate(channel);
            }
            catch (CheckedAllocationException e)
            {
                throw new AllocationException("Analog output channel " + m_channel
                        + " is already allocated");
            }

            IntPtr port_pointer = HAL.GetPort((byte) channel);

            int status = 0;
            m_port = HALAnalog.InitializeAnalogOutputPort(port_pointer, ref status);

            HAL.Report(ResourceType.kResourceType_AnalogChannel, (byte) channel, 1);
        }

        public override void Free()
        {
            channels.Free(m_channel);
            m_channel = 0;
            //base.Free();
        }

        public void SetVoltage(double voltage)
        {
            int status = 0;
            HALAnalog.SetAnalogOutput(m_port, voltage, ref status);
        }

        public double GetVoltage()
        {
            int status = 0;

            double voltage = HALAnalog.GetAnalogOutput(m_port, ref status);

            return voltage;
        }


    }
}
