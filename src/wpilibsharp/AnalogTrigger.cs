using System;
using Hal;
using UnitsNet;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class AnalogTrigger : ISendable, IDisposable
    {
        internal readonly int m_port;
        private readonly AnalogInput? m_analogInput;
        private readonly DutyCycle? m_dutyCycle;
        private readonly bool m_ownsAnalog;

        public AnalogTrigger(int channel)
            : this(new AnalogInput(channel))
        {
            m_ownsAnalog = true;
            SendableRegistry.Instance.AddChild(this, m_analogInput!);
        }

        public AnalogTrigger(AnalogInput analogInput)
        {
            m_analogInput = analogInput ?? throw new ArgumentNullException(nameof(analogInput));
            m_port = Hal.AnalogTriggerLowLevel.Initialize(analogInput.m_port);

            var index = Index;

            UsageReporting.Report(ResourceType.AnalogTrigger, index + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogTrigger", index);
        }

        public AnalogTrigger(DutyCycle dutyCycle)
        {
            m_dutyCycle = dutyCycle ?? throw new ArgumentNullException(nameof(dutyCycle));

            m_port = Hal.AnalogTriggerLowLevel.InitializeDutyCycle(dutyCycle.m_handle);

            var index = Index;

            UsageReporting.Report(ResourceType.AnalogTrigger, index + 1);
            SendableRegistry.Instance.AddLW(this, "AnalogTrigger", index);
        }

        public void SetLimitsRaw(int lower, int upper)
        {
            if (lower > upper)
            {
                throw new BoundaryException("Lower bound is greater then upper");
            }

            Hal.AnalogTriggerLowLevel.SetLimitsRaw(m_port, lower, upper);
        }

        public void SetLimitsDutyCycle(double lower, double upper)
        {
            if (lower > upper)
            {
                throw new BoundaryException("Lower bound is greater then upper");
            }

            Hal.AnalogTriggerLowLevel.SetLimitsDutyCycle(m_port, lower, upper);
        }

        public void SetLimitsVoltage(ElectricPotential lower, ElectricPotential upper)
        {
            if (lower > upper)
            {
                throw new BoundaryException("Lower bound is greater then upper");
            }

            Hal.AnalogTriggerLowLevel.SetLimitsVoltage(m_port, lower.Volts, upper.Volts);
        }

        public bool Averaged
        {
            set => Hal.AnalogTriggerLowLevel.SetAveraged(m_port, value);
        }

        public bool Filtered
        {
            set => Hal.AnalogTriggerLowLevel.SetFiltered(m_port, value);
        }

        public int Index => Hal.AnalogTriggerLowLevel.GetFPGAIndex(m_port);

        public bool InWindow => Hal.AnalogTriggerLowLevel.GetInWindow(m_port);

        public bool TriggerState => Hal.AnalogTriggerLowLevel.GetTriggerState(m_port);

        public AnalogTriggerOutput CreateOutput(AnalogTriggerType type)
        {
            return new AnalogTriggerOutput(this, type);
        }

        public void Dispose()
        {
            SendableRegistry.Instance.Remove(this);
            Hal.AnalogTriggerLowLevel.Clean(m_port);
            if (m_ownsAnalog)
            {
                m_analogInput?.Dispose();
            }
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            if (m_ownsAnalog)
            {
                (m_analogInput as ISendable)?.InitSendable(builder);
            }
        }
    }
}
