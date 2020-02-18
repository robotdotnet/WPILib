using System;
using Hal;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public class DigitalInput : IDigitalSource, IDisposable, ISendable
    {
        private readonly int m_handle;

        public DigitalInput(int channel)
        {
            m_handle = Hal.DIOLowLevel.InitializePort(Hal.HALLowLevel.GetPort(channel), true);

            UsageReporting.Report(ResourceType.DigitalInput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "DigitalInput", channel);
        }

        public bool IsAnalogTrigger => false;

        public int Channel { get; }

        public AnalogTriggerType AnalogTriggerTypeForRouting => 0;

        public int PortHandleForRouting => m_handle;

        public bool Output => Hal.DIOLowLevel.Get(m_handle);

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.DIOLowLevel.FreePort(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
