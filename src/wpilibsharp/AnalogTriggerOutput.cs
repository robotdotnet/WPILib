using System;
using Hal;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public class AnalogTriggerOutput : IDigitalSource, ISendable
    {

        private readonly AnalogTrigger m_trigger;

        public AnalogTriggerOutput(AnalogTrigger trigger, AnalogTriggerType type)
        {
            m_trigger = trigger ?? throw new ArgumentNullException(nameof(trigger));
            AnalogTriggerTypeForRouting = type;

            UsageReporting.Report(ResourceType.AnalogTriggerOutput, trigger.Index + 1, (int)type + 1);
        }

        public bool Output => Hal.AnalogTriggerLowLevel.GetOutput(m_trigger.m_port, AnalogTriggerTypeForRouting);

        public bool IsAnalogTrigger => true;

        public int Channel => m_trigger.Index;

        public AnalogTriggerType AnalogTriggerTypeForRouting { get; }

        public int PortHandleForRouting => m_trigger.m_port;

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
