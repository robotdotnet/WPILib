using Hal;
using System;
using System.Globalization;
using WPILib.SmartDashboardNS;

namespace WPILib
{
#pragma warning disable CA1063 // Implement IDisposable Correctly
    public class PWM : MotorSafety, ISendable, IDisposable
#pragma warning restore CA1063 // Implement IDisposable Correctly
    {
        public enum PeriodMultiplierType
        {
            k1X,
            k2X,
            k4X
        }

        private readonly int m_channel;
        internal int m_handle;

        public PWM(int channel)
        {
            m_channel = channel;
            m_handle = Hal.PWMLowLevel.InitializePort(Hal.HALLowLevel.GetPort(channel));

            SetDisabled();

            Hal.PWMLowLevel.SetEliminateDeadband(m_handle, false);

            UsageReporting.Report(ResourceType.PWM, channel + 1);

            SendableRegistry.Instance.AddLW(this, "PWM", channel);

            SafetyEnabled = false;
        }

#pragma warning disable CA1063 // Implement IDisposable Correctly
        public virtual void Dispose()
#pragma warning restore CA1063 // Implement IDisposable Correctly
        {
            SendableRegistry.Instance.Remove(this);
            if (m_handle == 0) return;
            SetDisabled();
            Hal.PWMLowLevel.FreePort(m_handle);
            m_handle = 0;
            GC.SuppressFinalize(this);
        }

        public override void StopMotor()
        {
            SetDisabled();
        }

        public void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            Hal.PWMLowLevel.SetConfig(m_handle, max, deadbandMax, center, deadbandMin, min);
        }

        public override string Description => $"PWM {Channel.ToString(CultureInfo.InvariantCulture)}";

        public int Channel => m_channel;

        public double Position
        {
            get
            {
                return Hal.PWMLowLevel.GetPosition(m_handle);
            }
            set
            {
                Hal.PWMLowLevel.SetPosition(m_handle, value);
            }
        }

        public double Speed
        {
            get
            {
                return Hal.PWMLowLevel.GetSpeed(m_handle);
            }
            set
            {
                Hal.PWMLowLevel.SetSpeed(m_handle, value);
            }
        }

        public int Raw
        {
            get
            {
                return Hal.PWMLowLevel.GetRaw(m_handle);
            }
            set
            {
                Hal.PWMLowLevel.SetRaw(m_handle, value);
            }
        }

        public void SetDisabled()
        {
            Hal.PWMLowLevel.SetDisabled(m_handle);
        }

        public PeriodMultiplierType PeriodMultiplier
        {
            set
            {
                switch (value)
                {
                    case PeriodMultiplierType.k1X:
                        Hal.PWMLowLevel.SetPeriodScale(m_handle, 0);
                        break;
                    case PeriodMultiplierType.k2X:
                        Hal.PWMLowLevel.SetPeriodScale(m_handle, 1);
                        break;
                    case PeriodMultiplierType.k4X:
                        Hal.PWMLowLevel.SetPeriodScale(m_handle, 3);
                        break;
                }
            }
        }

        public void SetZeroLatch()
        {
            Hal.PWMLowLevel.LatchZero(m_handle);
        }

        public virtual void InitSendable(ISendableBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.SmartDashboardType = "PWM";
            builder.IsActuator = true;
            builder.SafeState = SetDisabled;
            builder.AddDoubleProperty("Value", () => Raw, v => Raw = (int)v);
        }
    }
}
