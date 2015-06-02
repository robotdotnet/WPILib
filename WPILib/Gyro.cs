using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HAL_Base;
using WPILib.Util;
using WPILib.Interfaces;
using System.Threading;

namespace WPILib
{
    public class Gyro : SensorBase, PIDSource
    {
        private static int kOversampleBits = 10;
        private static int kAverageBits = 0;
        private static double kSamplesPerSecond = 50.0;
        private static double kCalibrationSampleTime = 5.0;
        private static double kDefaultVoltsPerDegreePerSecond = 0.007;

        protected AnalogInput m_analog;
        private double m_voltsPerDegreePerSecond;
        private double m_offset;
        private int m_center;
        bool m_channelAllocated = false;
        private AccumulatorResult m_result;
        private PIDSourceParameter m_pidSource;

        public void InitGyro()
        {
            m_result = new AccumulatorResult();
            if (m_analog == null)
            {
                Console.WriteLine("Null m_analog");
                throw new ArgumentNullException("m_analog");
            }
            m_voltsPerDegreePerSecond = kDefaultVoltsPerDegreePerSecond;
            m_analog.SetAverageBits(kAverageBits);
            m_analog.SetOversampleBits(kOversampleBits);
            double sampleRate = kSamplesPerSecond
                    * (1 << (kAverageBits + kOversampleBits));
            AnalogInput.SetGlobalSampleRate(sampleRate);
            Timer.Delay(1.0);

            m_analog.InitAccumulator();
            m_analog.ResetAccumulator();

            Timer.Delay(kCalibrationSampleTime);

            m_analog.GetAccumulatorOutput(m_result);

            m_center = (int)((double)m_result.value / (double)m_result.count + .5);

            m_offset = ((double)m_result.value / (double)m_result.count)
                    - m_center;

            
            m_analog.SetAccumulatorCenter(m_center);
            m_analog.ResetAccumulator();

            SetDeadband(0.0);

            SetPIDSourceParameter(PIDSourceParameter.Angle);

            HAL.Report(ResourceType.kResourceType_Gyro, (byte)m_analog.GetChannel());

        }

        public Gyro(int channel) : this(new AnalogInput(channel))
        {
            m_channelAllocated = true;
        }

        public Gyro(AnalogInput channel)
        {
            m_analog = channel;
            if (m_analog == null)
            {
                throw new NullReferenceException("AnalogInput supplied to Gyro constructor is null");
            }
            InitGyro();
        }

        public void Reset()
        {
            if (m_analog != null)
            {
                m_analog.ResetAccumulator();
            }
        }

        public override void Free()
        {
            if (m_analog != null && m_channelAllocated)
            {
                m_analog.Free();
            }
            m_analog = null;
            //base.Free();
        }

        public double GetAngle()
        {
            if (m_analog == null)
            {
                return 0.0;
            }
            else
            {
                m_analog.GetAccumulatorOutput(m_result);

                long value = m_result.value - (long)(m_result.count * m_offset);

                double scaledValue = value
                        * 1e-9
                        * m_analog.GetLSBWeight()
                        * (1 << m_analog.GetAverageBits())
                        / (AnalogInput.GetGlobalSampleRate() * m_voltsPerDegreePerSecond);

                return scaledValue;
            }
        }

        public double GetRate()
        {
            if (m_analog == null)
            {
                return 0.0;
            }
            else
            {
                return (m_analog.GetAverageValue() - (m_center + m_offset))
                        * 1e-9
                        * m_analog.GetLSBWeight()
                        / ((1 << m_analog.GetOversampleBits()) * m_voltsPerDegreePerSecond);
            }
        }

        public void SetSensitivity(double voltsPerDegreePerSecond)
        {
            m_voltsPerDegreePerSecond = voltsPerDegreePerSecond;
        }

        void SetDeadband(double volts)
        {
            int deadband = (int)(volts * 1e9 / m_analog.GetLSBWeight() * (1 << m_analog.GetOversampleBits()));
            m_analog.SetAccumulatorDeadband(deadband);
        }

        public void SetPIDSourceParameter(PIDSourceParameter pidSource)
        {
            BoundaryException.AssertWithinBounds((int)pidSource, 1, 2);
            m_pidSource = pidSource;
        }

        public double PidGet()
        {
            switch (m_pidSource)
            {
                case PIDSourceParameter.Rate:
                    return GetRate();
                case PIDSourceParameter.Angle:
                    return GetAngle();
                default:
                    return 0.0;
            }
        }

    }
}
