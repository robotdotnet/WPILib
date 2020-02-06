using Hal;
using System;
using System.Collections.Generic;
using System.Text;
using WPILib.SmartDashboard;

namespace WPILib
{
    public class PWM : MotorSafety, ISendable, IDisposable
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
            m_handle = Hal.PWM.InitializePort(Hal.HalBase.GetPort(channel));

            SetDisabled();

            Hal.PWM.SetEliminateDeadband(m_handle, false);

            UsageReporting.Report(ResourceType.PWM, channel + 1);

            SendableRegistry.Instance.AddLW(this, "PWM", channel);

            SafetyEnabled = false;
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            if (m_handle == 0) return;
            SetDisabled();
            Hal.PWM.FreePort(m_handle);
            m_handle = 0;
        }

        public override void StopMotor()
        {
            SetDisabled();
        }

        public void SetBounds(double max, double deadbandMax, double center, double deadbandMin, double min)
        {
            Hal.PWM.SetConfig(m_handle, max, deadbandMax, center, deadbandMin, min);
        }

        public override string Description => $"PWM {Channel.ToString()}";

        public int Channel => m_channel;

        public double Position
        {
            get
            {
                return Hal.PWM.GetPosition(m_handle);
            }
            set
            {
                Hal.PWM.SetPosition(m_handle, value);
            }
        }

        public double Speed
        {
            get
            {
                return Hal.PWM.GetSpeed(m_handle);
            }
            set
            {
                Hal.PWM.SetSpeed(m_handle, value);
            }
        }

        public int Raw
        {
            get
            {
                return Hal.PWM.GetRaw(m_handle);
            }
            set
            {
                Hal.PWM.SetRaw(m_handle, value);
            }
        }

        public void SetDisabled()
        {
            Hal.PWM.SetDisabled(m_handle);
        }

        public PeriodMultiplierType PeriodMultiplier
        {
            set
            {
                switch (value)
                {
                    case PeriodMultiplierType.k1X:
                        Hal.PWM.SetPeriodScale(m_handle, 0);
                        break;
                    case PeriodMultiplierType.k2X:
                        Hal.PWM.SetPeriodScale(m_handle, 1);
                        break;
                    case PeriodMultiplierType.k4X:
                        Hal.PWM.SetPeriodScale(m_handle, 3);
                        break;
                }
            }
        }

        public void SetZeroLatch()
        {
            Hal.PWM.LatchZero(m_handle);
        }

        public virtual void InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "PWM";
            builder.Actuator = true;
            builder.SafeState = SetDisabled;
            builder.AddDoubleProperty("Value", () => Raw, v => Raw = (int)v);
        }
    }
}
