

using System;
using WPILib.Util;
using HAL_FRC;

namespace WPILib
{
    public class DigitalSource : InterruptableSensorBase
    {
        protected static Resource s_channels = new Resource(DigitalChannels);
        protected IntPtr m_port;
        protected int m_channel;

        protected void InitDigitalPort(int channel, bool input)
        {
            this.m_channel = channel;

            CheckDigitalChannel(channel);

            try
            {
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException ex)
            {
                throw new AllocationException("Digital input " + channel + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            m_port = HALDigital.initializeDigitalPort(portPointer, ref status);
            HALDigital.allocateDIO(m_port, input, ref status);
        }

        public override void Free()
        {
            s_channels.Free(m_channel);
            int status = 0;
            HALDigital.freeDIO(m_port, ref status);
            m_channel = 0;
        }

        public override int GetChannelForRouting()
        {
            return m_channel;
        }

        public override byte GetModuleForRouting()
        {
            return 0;
        }

        public override bool GetAnalogTriggerForRouting()
        {
            return false;
        }
    }
}
