using System;
using HAL_Base;
using WPILib.Exceptions;

namespace WPILib
{
    public class AnalogOutput : SensorBase
    {
        private static Resource s_channels = new Resource(AnalogOutputChannels);
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
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException)
            {
                throw new AllocationException("Analog output channel " + m_channel
                        + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte) channel);

            int status = 0;
            m_port = HALAnalog.InitializeAnalogOutputPort(portPointer, ref status);

            HAL.Report(ResourceType.kResourceType_AnalogOutput, (byte) channel, 1);
        }

        public override void Dispose()
        {
            s_channels.Dispose(m_channel);
            m_channel = 0;
            //base.Dispose();
        }

        public double Voltage
        {
            set
            {
                int status = 0;
                HALAnalog.SetAnalogOutput(m_port, value, ref status);
            }
            get
            {
                int status = 0;

                double voltage = HALAnalog.GetAnalogOutput(m_port, ref status);

                return voltage;
            }
        }
    }
}
