using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class AnalogTriggerOutput : DigitalSource
    {
        private AnalogTrigger m_trigger;
        private AnalogTriggerType m_outputType;

        public AnalogTriggerOutput(AnalogTrigger trigger, AnalogTriggerType outputType)
        {
            if (trigger == null)
                throw new NullReferenceException("Analog Trigger give was null");
            m_trigger = trigger;
            m_outputType = outputType;

            HAL.Report(ResourceType.kResourceType_AnalogTriggerOutput, (byte) trigger.Index, (byte) outputType);
        }

        public override void Dispose()
        {
            //base.Dispose();
        }

        public bool Get()
        {
            int status = 0;
            bool value = HALAnalog.GetAnalogTriggerOutput(m_trigger.Port, m_outputType, ref status);
            return value;
        }

        public override int ChannelForRouting => (m_trigger.Index << 2) + (int) m_outputType;

        public override byte ModuleForRouting => (byte) (m_trigger.Index >> 2);

        public override bool AnalogTriggerForRouting => true;
    }
}
