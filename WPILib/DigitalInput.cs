

using System;
using System.Collections.Generic;
using System.Text;
using HAL_FRC;

namespace WPILib
{
    public class DigitalInput : DigitalSource
    {
        public DigitalInput(int channel)
        {
            InitDigitalPort(channel, true);

            HAL.Report(ResourceType.kResourceType_DigitalInput, (byte)channel);
        }

        public bool Get()
        {
            int status = 0;
            bool value = HALDigital.getDIO(_port, ref status);
            return value;
        }

        public int GetChannel()
        {
            return _channel;
        }

        public override bool GetAnalogTriggerForRouting()
        {
            return false;
        }
    }
}
