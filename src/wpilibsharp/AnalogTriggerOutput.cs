using System;
using Hal;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class AnalogTriggerOutput : DigitalSource, ISendable
    {
        public class AnalogTriggerOutputException : Exception
        {

        }

        private readonly AnalogTrigger m_trigger;
        private readonly AnalogTriggerType m_outputType;

        public AnalogTriggerOutput(AnalogTrigger trigger, AnalogTriggerType type)
        {
            m_trigger = trigger;
            m_outputType = type;

            UsageReporting.Report(ResourceType.AnalogTriggerOutput, trigger.Index + 1, (int)type + 1);
        }

        public bool Output => Hal.AnalogTrigger.GetOutput(m_trigger.m_port, m_outputType);

        public override bool IsAnalogTrigger => true;

        public override int Channel => m_trigger.Index;

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
