

using System;
using System.Collections.Generic;
using System.Text;
using HAL_FRC;
using WPILib.Util;

namespace WPILib
{
    public class DigitalSource : InterruptableSensorBase
    {
        protected static Resource channels = new Resource(DigitalChannels);
        protected IntPtr _port;
        protected int _channel;

        protected void InitDigitalPort(int channel, bool input)
        {
            this._channel = channel;

            CheckDigitalChannel(channel);

            try
            {
                channels.Allocate(channel);
            }
            catch (CheckedAllocationException ex)
            {
                throw new AllocationException("Digital input " + channel + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            _port = HALDigital.initializeDigitalPort(portPointer, ref status);
            HALDigital.allocateDIO(_port, input, ref status);
        }

        public override void Free()
        {
            channels.Free(_channel);
            int status = 0;
            HALDigital.freeDIO(_port, ref status);
            _channel = 0;
        }

        public override int GetChannelForRouting()
        {
            return _channel;
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
