using System;
using System.Collections.Generic;
using System.Text;
using Hal;
using UnitsNet;
using WPILib.Counters;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class DutyCycleEncoder : ISendable, IDisposable
    {
        private readonly DutyCycle m_dutyCycle;
        private bool m_ownsDutyCycle;
        private AnalogTrigger m_analogTrigger;
        private UpDownCounter m_counter;

        private Frequency m_frequencyThreshold = Frequency.FromHertz(100);
        private double m_positionOffset;
        private double m_distancePerRotation = 1.0;
        private Angle m_lastPosition;

        private SimDevice m_simDevice;

        public DutyCycleEncoder(int channel)
#pragma warning disable CA2000 // Dispose objects before losing scope
            : this(new DigitalInput(channel))
#pragma warning restore CA2000 // Dispose objects before losing scope
        {

        }

        public DutyCycleEncoder(IDigitalSource source)
            : this(new DutyCycle(source))
        {
            m_ownsDutyCycle = true;
        }

        public DutyCycleEncoder(DutyCycle dutyCycle)
        {
            m_dutyCycle = dutyCycle;
            m_analogTrigger = new AnalogTrigger(m_dutyCycle);
            m_counter = new UpDownCounter();

            m_simDevice = SimDevice.Create("DutyCycleEncoder", m_dutyCycle.FPGAIndex);

            if (m_simDevice)
            {

            }

            m_analogTrigger.SetLimitsDutyCycle(0.25, 0.75);
            m_counter.UpSource = m_analogTrigger.CreateOutput(AnalogTriggerType.kRisingPulse);
            m_counter.DownSource = m_analogTrigger.CreateOutput(AnalogTriggerType.kFallingPulse);

            SendableRegistry.Instance.AddLW(this, "DutyCycle Encoder", m_dutyCycle.SourceChannel);
        }

        public void Reset()
        {
            m_counter.Reset();
            m_positionOffset = m_dutyCycle.Output;
        }

        public Angle Output
        {
            get
            {
                for (int i = 0; i < 10; i++)
                {
                    double counter = m_counter.Count;
                    double pos = m_dutyCycle.Output;
                    double counter2 = m_counter.Count;
                    double pos2 = m_dutyCycle.Output;
                    if (counter == counter2 && pos == pos2)
                    {
                        double position = counter + pos - m_positionOffset;
                        var anglePos = Angle.FromRevolutions(position);
                        m_lastPosition = anglePos;
                        return anglePos;
                    }
                }
                return m_lastPosition;
            }
        }

        public double PositionOffset => m_positionOffset;

        public double Distance
        {
            get
            {
                return Output.Revolutions * DistancePerRotation;
            }
        }

        public double DistancePerRotation
        {
            get
            {
                return m_distancePerRotation;
            }
        }

        public Frequency Frequency => m_dutyCycle.Frequency;

        public Frequency ConnectedFrequencyThreshold
        {
            get => m_frequencyThreshold;
            set
            {
                if (value < Frequency.Zero)
                {
                    value = Frequency.Zero;
                }
                m_frequencyThreshold = value;
            }
        }

        public bool IsConnected
        {
            get
            {
                return Frequency > m_frequencyThreshold;
            }
        }

        public void Dispose()
        {
            m_counter.Dispose();
            m_analogTrigger.Dispose();
            if (m_ownsDutyCycle)
            {
                m_dutyCycle.Dispose();
            }
            if (m_simDevice)
            {
                m_simDevice.Dispose();
            }
        }

        void ISendable.InitSendable(ISendableBuilder builder)
        {
            builder.SmartDashboardType = "AbsoluteEncoder";
            builder.AddDoubleProperty("Distance", () => Distance, null);
            builder.AddDoubleProperty("Distance Per Rotation", () => DistancePerRotation, null);
            builder.AddBooleanProperty("Is Connected", () => IsConnected, null);
        }
    }
}
