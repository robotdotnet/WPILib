using System;
using HAL;
using HAL.Base;
using WPILib.Interfaces;
using WPILib.LiveWindow;

namespace WPILib
{
    /// <summary>
    /// Class for interfacing with an analog gyro to get robot heading.
    /// </summary>
    public class AnalogGyro : GyroBase, IPIDSource, ILiveWindowSendable
    {
        private static int kOversampleBits = 10;
        private static int kAverageBits = 0;
        private static double kSamplesPerSecond = 50.0;
        private static double kCalibrationSampleTime = 5.0;
        private static double kDefaultVoltsPerDegreePerSecond = 0.007;

        /// <summary>
        /// The <see cref="AnalogInput"/> that this gyro uses.
        /// </summary>
        protected AnalogInput m_analog;
        private double m_offset;
        private int m_center;
        readonly bool m_channelAllocated = false;

        /// <inheritdoc/>
        public override void Calibrate()
        {
            m_analog.InitAccumulator();
            m_analog.ResetAccumulator();

            if (RobotBase.IsSimulation)
            {
                //In simulation, we do not have to do anything here.
                return;
            }

            Timer.Delay(kCalibrationSampleTime);

            long value = 0;
            uint count = 0;
            m_analog.GetAccumulatorOutput(ref value, ref count);

            m_center = (int)((double)value / (double)count + .5);

            m_offset = ((double)value / (double)count)
                    - m_center;


            m_analog.AccumulatorCenter = m_center;
            m_analog.ResetAccumulator();
        }

        /// <summary>
        /// Creates a new Analog Gyro on the specified channel.
        /// </summary>
        /// <param name="channel">The channel the gyro is on.</param>
        public AnalogGyro(int channel)
        {
            AnalogInput aIn = new AnalogInput(channel);
            try
            {
                CreateGyro(aIn);
            }
            catch 
            {
                aIn.Dispose();
                throw;
            }
            m_channelAllocated = true;
        }

        /// <summary>
        /// Creates a new Analog Gyro with an existing <see cref="AnalogInput"/>.
        /// </summary>
        /// <param name="channel">The analog input this gyro is attached to.</param>
        public AnalogGyro(AnalogInput channel)
        {
            CreateGyro(channel);
        }

        private void CreateGyro(AnalogInput channel)
        {
            m_analog = channel;
            if (m_analog == null)
            {
                throw new ArgumentNullException(nameof(channel), "AnalogInput supplied to Gyro constructor is null");
            }
            if (!m_analog.IsAccumulatorChannel)
            {
                throw new ArgumentOutOfRangeException(nameof(channel), "Channel must be an accumulator channel");
            }
            Sensitivity = kDefaultVoltsPerDegreePerSecond;
            m_analog.AverageBits = kAverageBits;
            m_analog.OversampleBits = kOversampleBits;
            double sampleRate = kSamplesPerSecond
                    * (1 << (kAverageBits + kOversampleBits));
            AnalogInput.GlobalSampleRate = sampleRate;
            Timer.Delay(0.1);

            Deadband = 0.0;

            PIDSourceType = PIDSourceType.Displacement;


            HAL.Base.HAL.Report(ResourceType.kResourceType_Gyro, (byte)m_analog.Channel);
            LiveWindow.LiveWindow.AddSensor("AnalogGyro", m_analog.Channel, this);

            Calibrate();
        }

        ///<inheritdoc/>
        public override void Reset() => m_analog?.ResetAccumulator();

        ///<inheritdoc/>
        public override void Dispose()
        {
            if (m_analog != null && m_channelAllocated)
            {
                m_analog.Dispose();
            }
            m_analog = null;
            base.Dispose();
        }

        ///<inheritdoc/>
        public override double GetAngle()
        {
            if (m_analog == null)
            {
                return 0.0;
            }
            else
            {
                if (RobotBase.IsSimulation)
                {
                    //Use our simulator hack.
                    return BitConverter.Int64BitsToDouble(m_analog.GetAccumulatorValue());
                }
                long rawValue = 0;
                uint count = 0;
                m_analog.GetAccumulatorOutput(ref rawValue, ref count);

                long value = rawValue - (long)(count * m_offset);

                double scaledValue = value
                                     * 1e-9
                                     * m_analog.LSBWeight
                                     * (1 << m_analog.AverageBits)
                                     / (AnalogInput.GlobalSampleRate * Sensitivity);

                return scaledValue;
            }
        }

        ///<inheritdoc/>
        public override double GetRate()
        {
            if (m_analog == null)
            {
                return 0.0;
            }
            else
            {
                if (RobotBase.IsSimulation)
                {
                    //Use our simulator hack
                    return BitConverter.ToSingle(BitConverter.GetBytes(m_analog.GetAccumulatorCount()), 0);
                }
                return (m_analog.GetAverageValue() - (m_center + m_offset))
                       * 1e-9
                       * m_analog.LSBWeight
                       / ((1 << m_analog.OversampleBits) * Sensitivity);
            }
        }

        /// <summary>
        /// Gets or sets the sensitivity of the gyroscope.
        /// </summary>
        public double Sensitivity { get; set; }

        private double Deadband
        {
            set
            {
                int deadband = (int)(value * 1e9 / m_analog.LSBWeight * (1 << m_analog.OversampleBits));
                m_analog.AccumulatorDeadband = deadband;
            }
        }

        ///<inheritdoc />
        public override string SmartDashboardType => "AnalogGyro";
    }
}
