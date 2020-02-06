using System;
using Hal;
using UnitsNet;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class AnalogInput : ISendable, IDisposable
    {
        internal readonly int m_port;
        public int Channel { get; }

        public AnalogInput(int channel)
        {
            Channel = channel;

            m_port = Hal.AnalogInput.InitializePort(Hal.HalBase.GetPort(channel));
            UsageReporting.Report(ResourceType.AnalogOutput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogInput", channel);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.AnalogInput.FreePort(m_port);
        }

        public int RawValue => Hal.AnalogInput.GetAnalogValue(m_port);

        public int RawAverageValue => Hal.AnalogInput.GetAnalogAverageValue(m_port);

        public ElectricPotential Voltage => ElectricPotential.FromVolts(Hal.AnalogInput.GetAnalogVoltage(m_port));

        public ElectricPotential AverageVoltage => ElectricPotential.FromVolts(Hal.AnalogInput.GetAnalogAverageVoltage(m_port));

        public long LSBWeight => Hal.AnalogInput.GetAnalogLSBWeight(m_port);

        public int Offset => Hal.AnalogInput.GetAnalogOffset(m_port);

        public int AverageBits
        {
            get => Hal.AnalogInput.GetAnalogAverageBits(m_port);
            set => Hal.AnalogInput.SetAnalogAverageBits(m_port, value);
        }

        public int OversampleBits
        {
            get => Hal.AnalogInput.GetAnalogOversampleBits(m_port);
            set => Hal.AnalogInput.SetAnalogOversampleBits(m_port, value);
        }

        public bool IsAccumulatorChannel => Hal.AnalogAccumulator.IsAccumulatorChannel(m_port);

        public static double GlobalSampleRate
        {
            get => Hal.AnalogInput.GetAnalogSampleRate();
            set => Hal.AnalogInput.SetAnalogSampleRate(value);
        }

        public SimDevice SimDevice
        {
            set => Hal.AnalogInput.SetSimDevice(m_port, value.NativeHandle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Analog Input";
            builder.AddDoubleProperty("Value", () => AverageVoltage.Volts, null);
        }
    }
}
