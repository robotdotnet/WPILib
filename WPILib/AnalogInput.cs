

using System;
using System.Linq;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class AnalogInput : SensorBase, PIDSource
    {
        //private static int AccumulatorSlot = 1;
        private static Resource s_channels = new Resource(AnalogInputChannels);
        private IntPtr m_port;
        private int m_channel;
        private static int[] s_accumulatorChannels = { 0, 1 };
        private long m_accumulatorOffset;

        public AnalogInput(int channel)
        {
            m_channel = channel;

            CheckAnalogInputChannel(channel);

            try
            {
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException ex)
            {
                throw new AllocationException("Analog input channel " + m_channel
                     + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            m_port = HALAnalog.InitializeAnalogInputPort(portPointer, ref status);
            HAL.Report(ResourceType.kResourceType_AnalogChannel, (byte)channel);
        }

        public override void Free()
        {
            s_channels.Free(m_channel);
            m_channel = 0;
            m_accumulatorOffset = 0;
        }

        public int GetValue()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogValue(m_port, ref status);
            return value;
        }

        public int GetAverageValue()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogAverageValue(m_port, ref status);
            return value;
        }

        public double GetVoltage()
        {
            int status = 0;
            double value = HALAnalog.GetAnalogVoltage(m_port, ref status);
            return value;
        }

        public double GetAverageVoltage()
        {
            int status = 0;
            double value = HALAnalog.GetAnalogAverageVoltage(m_port, ref status);
            return value;
        }

        public long GetLSBWeight()
        {
            int status = 0;
            long value = HALAnalog.GetAnalogLSBWeight(m_port, ref status);
            return value;
        }

        public int GetOffset()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogOffset(m_port, ref status);
            return value;
        }

        public int GetChannel()
        {
            return m_channel;
        }

        public void SetAverageBits(int bits)
        {
            int status = 0;
            HALAnalog.SetAnalogAverageBits(m_port, (uint)bits, ref status);
        }

        public int GetAverageBits()
        {
            int status = 0;
            uint value = HALAnalog.GetAnalogAverageBits(m_port, ref status);
            return (int)value;
        }

        public void SetOversampleBits(int bits)
        {
            int status = 0;
            HALAnalog.SetAnalogOversampleBits(m_port, (uint)bits, ref status);
        }

        public int GetOversampleBits()
        {
            int status = 0;
            uint value = HALAnalog.GetAnalogOversampleBits(m_port, ref status);
            return (int)value;
        }

        public void InitAccumulator()
        {
            if (!IsAccumulatorChannel())
            {
                throw new AllocationException("This is not an accumulator");
            }
            m_accumulatorOffset = 0;
            int status = 0;
            HALAnalog.InitAccumulator(m_port, ref status);
        }

        public void SetAccumulatorInitialValue(long initialValue)
        {
            m_accumulatorOffset = initialValue;
        }

        public void ResetAccumulator()
        {
            int status = 0;
            HALAnalog.ResetAccumulator(m_port, ref status);

            double sampleTime = 1.0 / GetGlobalSampleRate();
            double overSamples = 1 << GetOversampleBits();
            double averageSamples = 1 << GetAverageBits();
            Timer.Delay(sampleTime * overSamples * averageSamples);
        }

        public void SetAccumulatorCenter(int center)
        {
            int status = 0;
            HALAnalog.SetAccumulatorCenter(m_port, center, ref status);
        }

        public void SetAccumulatorDeadband(int deadband)
        {
            int status = 0;
            HALAnalog.SetAccumulatorDeadband(m_port, deadband, ref status);
        }

        public long GetAccumulatorValue()
        {
            int status = 0;
            long value = HALAnalog.GetAccumulatorValue(m_port, ref status);
            return value + m_accumulatorOffset;
        }

        public long GetAccumulatorCount()
        {
            int status = 0;
            long value = HALAnalog.GetAccumulatorCount(m_port, ref status);
            return value;
        }

        public void GetAccumulatorOutput(AccumulatorResult result)
        {
            if (result == null)
                throw new ArgumentNullException();
            if (!IsAccumulatorChannel())
                throw new ArgumentException("Channel " + m_channel
                    + " is not an accumulator channel.");

            uint count = 0;
            long value = 0;
            int status = 0;
            HALAnalog.GetAccumulatorOutput(m_port, ref value, ref count, ref status);
            result.value = value + m_accumulatorOffset;
            result.count = count;
        }


        public bool IsAccumulatorChannel()
        {
            return s_accumulatorChannels.Any(t => m_channel == t);
        }

        public static void SetGlobalSampleRate(double samplesPerSecond)
        {
            int status = 0;
            HALAnalog.SetAnalogSampleRate(samplesPerSecond, ref status);
        }

        public static double GetGlobalSampleRate()
        {
            int status = 0;
            double value = HALAnalog.GetAnalogSampleRate(ref status);
            return value;
        }

        public double PidGet()
        {
            return GetAverageVoltage();
        }
    }
}
