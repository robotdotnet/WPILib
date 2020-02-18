using UnitsNet;
using System;
using WPILib.SmartDashboardNS;
using Hal;

namespace WPILib
{
    public class AnalogAccelerometer : ISendable, IDisposable
    {
        private readonly AnalogInput m_analogInput;
        private double m_voltsPerG = 1.0;
        private ElectricPotential m_zeroGVoltage = ElectricPotential.FromVolts(2.5);

        private readonly bool m_allocatedChannel;

        public AnalogAccelerometer(int channel)
            : this(new AnalogInput(channel))
        {
            m_allocatedChannel = true;
        }

        public AnalogAccelerometer(AnalogInput analogInput)
        {
            m_analogInput = analogInput;
            UsageReporting.Report(ResourceType.Accelerometer, analogInput.Channel + 1);
            SendableRegistry.Instance.AddLW(this, "Accelerometer", analogInput.Channel);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            if (m_allocatedChannel)
            {
                m_analogInput.Dispose();
            }
        }

        public Acceleration Acceleration => Acceleration.FromStandardGravity((m_analogInput.AverageVoltage - m_zeroGVoltage).Volts / m_voltsPerG);

        public double Sensitivity
        {
            set => m_voltsPerG = value;
        }

        public ElectricPotential Zero
        {
            set => m_zeroGVoltage = value;
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "Accelerometer";
            builder.AddDoubleProperty("Value", () => Acceleration.StandardGravity, null);
        }
    }
}
