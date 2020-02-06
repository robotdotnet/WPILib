using System;
using Hal;
using UnitsNet;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class AnalogOutput : ISendable, IDisposable
    {
        private readonly int m_port;
        private int Channel { get; }

        public AnalogOutput(int channel)
        {
            Channel = channel;
            m_port = Hal.AnalogOutput.InitializePort(Hal.HalBase.GetPort(channel));
            UsageReporting.Report(ResourceType.AnalogOutput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogOutput", channel);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.AnalogOutput.FreePort(m_port);
        }

        public ElectricPotential Voltage
        {
            get => ElectricPotential.FromVolts(Hal.AnalogOutput.Get(m_port));
            set => Hal.AnalogOutput.Set(m_port, value.Volts);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Analog Output";
            builder.AddDoubleProperty("Value", () => Voltage.Volts, (v) => Voltage = ElectricPotential.FromVolts(v));
        }
    }
}
