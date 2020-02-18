using System;
using Hal;
using UnitsNet;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class AnalogOutput : ISendable, IDisposable
    {
        private readonly int m_port;
        public int Channel { get; }

        public AnalogOutput(int channel)
        {
            Channel = channel;
            m_port = Hal.AnalogOutputLowLevel.InitializePort(Hal.HALLowLevel.GetPort(channel));
            UsageReporting.Report(ResourceType.AnalogOutput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogOutput", channel);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.AnalogOutputLowLevel.FreePort(m_port);
        }

        public ElectricPotential Voltage
        {
            get => ElectricPotential.FromVolts(Hal.AnalogOutputLowLevel.Get(m_port));
            set => Hal.AnalogOutputLowLevel.Set(m_port, value.Volts);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Analog Output";
            builder.AddDoubleProperty("Value", () => Voltage.Volts, (v) => Voltage = ElectricPotential.FromVolts(v));
        }
    }
}
