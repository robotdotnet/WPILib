using System;
using System.Collections.Generic;
using System.Text;
using Hal;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class DigitalInput : IDigitalSource, IDisposable, ISendable
    {
        private readonly int m_handle;

        public DigitalInput(int channel)
        {
            m_handle = Hal.DIO.InitializePort(Hal.HalBase.GetPort(channel), true);

            UsageReporting.Report(ResourceType.DigitalInput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "DigitalInput", channel);
        }

        public bool IsAnalogTrigger => false;

        public int Channel { get; }

        public AnalogTriggerType AnalogTriggerTypeForRouting => 0;

        public int PortHandleForRouting => m_handle;

        public bool Output => Hal.DIO.Get(m_handle);

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.DIO.FreePort(m_handle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
        }
    }
}
