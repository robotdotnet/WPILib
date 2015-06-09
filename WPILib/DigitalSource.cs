﻿

using System;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class DigitalSource : InterruptableSensorBase
    {
        protected static Resource s_channels = new Resource(DigitalChannels);
        protected IntPtr m_port;
        protected int m_channel;

        protected void InitDigitalPort(int channel, bool input)
        {
            m_channel = channel;

            CheckDigitalChannel(channel);

            try
            {
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException)
            {
                throw new AllocationException("Digital input " + channel + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            m_port = HALDigital.InitializeDigitalPort(portPointer, ref status);
            HALDigital.AllocateDIO(m_port, input, ref status);
            Utility.CheckStatus(status);
        }

        public override void Dispose()
        {
            s_channels.Dispose(m_channel);
            int status = 0;
            HALDigital.FreeDIO(m_port, ref status);
            m_channel = 0;
        }

        public override int ChannelForRouting => m_channel;

        public override byte ModuleForRouting => 0;

        public override bool AnalogTriggerForRouting => false;
    }
}
