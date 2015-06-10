

using System;
using System.Linq;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class AnalogInput : SensorBase, IPIDSource
    {
        //private static int AccumulatorSlot = 1;
        private static Resource s_channels = new Resource(AnalogInputChannels);
        private IntPtr m_port;
        private static int[] s_accumulatorChannels = { 0, 1 };
        private long m_accumulatorOffset;

        public AnalogInput(int channel)
        {
            Channel = channel;

            CheckAnalogInputChannel(channel);

            try
            {
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException)
            {
                throw new AllocationException("Analog input channel " + Channel
                     + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            m_port = HALAnalog.InitializeAnalogInputPort(portPointer, ref status);
            HAL.Report(ResourceType.kResourceType_AnalogChannel, (byte)channel);
        }

        public override void Dispose()
        {
            s_channels.Dispose(Channel);
            Channel = 0;
            m_accumulatorOffset = 0;
        }

        public int Value
        {
            get
            {
                int status = 0;
                int value = HALAnalog.GetAnalogValue(m_port, ref status);
                return value;
            }
        }

        public int AverageValue
        {
            get
            {
                int status = 0;
                int value = HALAnalog.GetAnalogAverageValue(m_port, ref status);
                return value;
            }
        }

        public double Voltage
        {
            get
            {
                int status = 0;
                double value = HALAnalog.GetAnalogVoltage(m_port, ref status);
                return value;
            }
        }

        public double AverageVoltage
        {
            get
            {
                int status = 0;
                double value = HALAnalog.GetAnalogAverageVoltage(m_port, ref status);
                return value;
            }
        }

        public long LSBWeight
        {
            get
            {
                int status = 0;
                long value = HALAnalog.GetAnalogLSBWeight(m_port, ref status);
                return value;
            }
        }

        public int Offset
        {
            get
            {
                int status = 0;
                int value = HALAnalog.GetAnalogOffset(m_port, ref status);
                return value;
            }
        }

        public int Channel { get; private set; }

        public int AverageBits
        {
            set
            {
                int status = 0;
                HALAnalog.SetAnalogAverageBits(m_port, (uint) value, ref status);
            }
            get
            {
                int status = 0;
                uint value = HALAnalog.GetAnalogAverageBits(m_port, ref status);
                return (int) value;
            }
        }

        public int OversampleBits
        {
            set
            {
                int status = 0;
                HALAnalog.SetAnalogOversampleBits(m_port, (uint) value, ref status);
            }
            get
            {
                int status = 0;
                uint value = HALAnalog.GetAnalogOversampleBits(m_port, ref status);
                return (int) value;
            }
        }

        public void InitAccumulator()
        {
            if (!IsAccumulatorChannel)
            {
                throw new AllocationException("This is not an accumulator");
            }
            m_accumulatorOffset = 0;
            int status = 0;
            HALAnalog.InitAccumulator(m_port, ref status);
        }

        public long AccumulatorInitialValue
        {
            set { m_accumulatorOffset = value; }
        }

        public void ResetAccumulator()
        {
            int status = 0;
            HALAnalog.ResetAccumulator(m_port, ref status);

            double sampleTime = 1.0 / GlobalSampleRate;
            double overSamples = 1 << OversampleBits;
            double averageSamples = 1 << AverageBits;
            Timer.Delay(sampleTime * overSamples * averageSamples);
        }

        public int AccumulatorCenter
        {
            set
            {
                int status = 0;
                HALAnalog.SetAccumulatorCenter(m_port, value, ref status);
            }
        }

        public int AccumulatorDeadband
        {
            set
            {
                int status = 0;
                HALAnalog.SetAccumulatorDeadband(m_port, value, ref status);
            }
        }

        public long AccumulatorValue
        {
            get
            {
                int status = 0;
                long value = HALAnalog.GetAccumulatorValue(m_port, ref status);
                return value + m_accumulatorOffset;
            }
        }

        public long AccumulatorCount
        {
            get
            {
                int status = 0;
                long value = HALAnalog.GetAccumulatorCount(m_port, ref status);
                return value;
            }
        }

        public void GetAccumulatorOutput(AccumulatorResult result)
        {
            if (result == null)
                throw new ArgumentNullException();
            if (!IsAccumulatorChannel)
                throw new ArgumentException("Channel " + Channel
                    + " is not an accumulator channel.");

            uint count = 0;
            long value = 0;
            int status = 0;
            HALAnalog.GetAccumulatorOutput(m_port, ref value, ref count, ref status);
            result.Value = value + m_accumulatorOffset;
            result.Count = count;
        }


        public bool IsAccumulatorChannel
        {
            get { return s_accumulatorChannels.Any(t => Channel == t); }
        }

        public static double GlobalSampleRate
        {
            set
            {
                int status = 0;
                HALAnalog.SetAnalogSampleRate(value, ref status);
            }
            get
            {
                int status = 0;
                double value = HALAnalog.GetAnalogSampleRate(ref status);
                return value;
            }
        }

        public double PidGet() => AverageVoltage;
    }
}
