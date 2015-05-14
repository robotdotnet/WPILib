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
            bool value = HALDigital.getDIO(m_port, ref status);
            return value;
        }

        public int GetChannel()
        {
            return m_channel;
        }

        public override bool GetAnalogTriggerForRouting()
        {
            return false;
        }
    }
}
