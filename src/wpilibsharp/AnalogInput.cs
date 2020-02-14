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

            m_port = Hal.AnalogInputLowLevel.InitializePort(Hal.HALLowLevel.GetPort(channel));
            UsageReporting.Report(ResourceType.AnalogOutput, channel + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogInput", channel);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.AnalogInputLowLevel.FreePort(m_port);
        }

        public int RawValue => Hal.AnalogInputLowLevel.GetAnalogValue(m_port);

        public int RawAverageValue => Hal.AnalogInputLowLevel.GetAnalogAverageValue(m_port);

        public ElectricPotential Voltage => ElectricPotential.FromVolts(Hal.AnalogInputLowLevel.GetAnalogVoltage(m_port));

        public ElectricPotential AverageVoltage => ElectricPotential.FromVolts(Hal.AnalogInputLowLevel.GetAnalogAverageVoltage(m_port));

        public long LSBWeight => Hal.AnalogInputLowLevel.GetAnalogLSBWeight(m_port);

        public int Offset => Hal.AnalogInputLowLevel.GetAnalogOffset(m_port);

        public int AverageBits
        {
            get => Hal.AnalogInputLowLevel.GetAnalogAverageBits(m_port);
            set => Hal.AnalogInputLowLevel.SetAnalogAverageBits(m_port, value);
        }

        public int OversampleBits
        {
            get => Hal.AnalogInputLowLevel.GetAnalogOversampleBits(m_port);
            set => Hal.AnalogInputLowLevel.SetAnalogOversampleBits(m_port, value);
        }

        public bool IsAccumulatorChannel => Hal.AnalogAccumulatorLowLevel.IsAccumulatorChannel(m_port);

        public static double GlobalSampleRate
        {
            get => Hal.AnalogInputLowLevel.GetAnalogSampleRate();
            set => Hal.AnalogInputLowLevel.SetAnalogSampleRate(value);
        }

        public SimDevice SimDevice
        {
            set => Hal.AnalogInputLowLevel.SetSimDevice(m_port, value.NativeHandle);
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Analog Input";
            builder.AddDoubleProperty("Value", () => AverageVoltage.Volts, null);
        }
    }
}
