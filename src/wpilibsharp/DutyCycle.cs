using System;
using Hal;
using UnitsNet;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class DutyCycle : ISendable, IDisposable
    {
        internal readonly int m_handle;

        private readonly IDigitalSource m_source;

        public DutyCycle(IDigitalSource digitalSource)
        {
            m_handle = Hal.DutyCycleLowLevel.Initialize(digitalSource.PortHandleForRouting, digitalSource.AnalogTriggerTypeForRouting);

            m_source = digitalSource;
            var index = FPGAIndex;
            UsageReporting.Report(ResourceType.DutyCycle, index + 1);
            SendableRegistry.Instance.AddLW(this, "Duty Cycle", index);
        }

        public void Dispose()
        {
            Hal.DutyCycleLowLevel.Free(m_handle);
        }

        public Frequency Frequency => Frequency.FromHertz(Hal.DutyCycleLowLevel.GetFrequency(m_handle));

        public double Output => Hal.DutyCycleLowLevel.GetOutput(m_handle);

        public int OutputRaw => Hal.DutyCycleLowLevel.GetOutputRaw(m_handle);

        public int OutputScaleFactor => Hal.DutyCycleLowLevel.GetOutputScaleFactor(m_handle);

        public int FPGAIndex => Hal.DutyCycleLowLevel.GetFPGAIndex(m_handle);

        public int SourceChannel => m_source.Channel;

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Duty Cycle";
            builder.AddDoubleProperty("Frequency", () => Frequency.Hertz, null);
            builder.AddDoubleProperty("Output", () => Output, null);
        }
    }
}
