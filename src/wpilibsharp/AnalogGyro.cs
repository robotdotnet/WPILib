using Hal;
using UnitsNet;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class AnalogGyro : GyroBase
    {
        private static readonly double DefaultVoltsPerDegreePerSecond = 0.007;
        protected readonly AnalogInput m_analogInput;
        private readonly bool m_channelAllocated;
        private readonly int m_gyroHandle;

        public AnalogGyro(int channel, int center = 0, double offset = 0)
            : this(new AnalogInput(channel), center, offset)
        {
            m_channelAllocated = true;
            SendableRegistry.Instance.AddChild(this, m_analogInput);
        }

        public AnalogGyro(AnalogInput input, int center = 0, double offset = 0)
        {
            m_analogInput = input;

            m_gyroHandle = Hal.AnalogGyroLowLevel.Initialize(input.m_port);

            Hal.AnalogGyroLowLevel.Setup(m_gyroHandle);

            UsageReporting.Report(ResourceType.Gyro, m_analogInput.Channel + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogGyro", m_analogInput.Channel);

            if (center != 0 || offset != 0)
            {
                Hal.AnalogGyroLowLevel.SetParameters(m_gyroHandle, DefaultVoltsPerDegreePerSecond, offset, center);
                Reset();
            }
            else
            {
                Calibrate();
            }


        }

        public override Angle Angle => Angle.FromDegrees(Hal.AnalogGyroLowLevel.GetAngle(m_gyroHandle));

        public override RotationalSpeed Rate => RotationalSpeed.FromDegreesPerSecond(Hal.AnalogGyroLowLevel.GetRate(m_gyroHandle));

        public override void Calibrate()
        {
            Hal.AnalogGyroLowLevel.Calibrate(m_gyroHandle);
        }

        public override void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            if (m_channelAllocated)
            {
                m_analogInput.Dispose();
            }

            Hal.AnalogGyroLowLevel.Free(m_gyroHandle);
        }

        public override void Reset()
        {
            Hal.AnalogGyroLowLevel.Reset(m_gyroHandle);
        }

        public double Offset => Hal.AnalogGyroLowLevel.GetOffset(m_gyroHandle);

        public int Center => Hal.AnalogGyroLowLevel.GetCenter(m_gyroHandle);

        public double Sensitivity
        {
            set => Hal.AnalogGyroLowLevel.SetVoltsPerDegreePerSecond(m_gyroHandle, value);
        }

        public ElectricPotential Deadband
        {
            set => Hal.AnalogGyroLowLevel.SetDeadband(m_gyroHandle, value.Volts);
        }
    }
}
