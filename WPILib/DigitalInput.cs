using HAL_Base;

namespace WPILib
{
    public class DigitalInput : DigitalSource
    {
        public DigitalInput(int channel)
        {
            InitDigitalPort(channel, true);

            HAL.Report(ResourceType.kResourceType_DigitalInput, (byte)channel);
        }

        public bool Value
        {
            get
            {
                int status = 0;
                bool value = HALDigital.GetDIO(m_port, ref status);
                return value;
            }
        }

        public int Channel => m_channel;

        public override bool AnalogTriggerForRouting => false;
    }
}
