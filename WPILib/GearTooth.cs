using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;

namespace WPILib
{
    public class GearTooth : Counter
    {
        private const double GearToothThreshold = 55e-6;

        public void EnableDirectionSensing(bool directionSensitive)
        {
            if (directionSensitive)
            {
                SetPulseLengthMode(GearToothThreshold);
            }
        }

        public GearTooth(int channel) : this(channel, false)
        {
        }

        public GearTooth(int channel, bool directionSensitive) : base(channel)
        {
            EnableDirectionSensing(directionSensitive);
            if (directionSensitive)
            {
                HAL.Report(ResourceType.kResourceType_GearTooth, (byte) channel, 0, "D");
            }
            else
            {
                HAL.Report(ResourceType.kResourceType_GearTooth, (byte)channel, 0);
            }
        }

        public GearTooth(DigitalSource source, bool directionSensitive) : base(source)
        {
            EnableDirectionSensing(directionSensitive);
        }

        public GearTooth(DigitalSource source) : this(source, false)
        {
            
        }
    }
}
