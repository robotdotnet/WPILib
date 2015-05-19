

using System;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_Base;

namespace WPILib
{
    public class AnalogInput : SensorBase, PIDSource
    {
        //private static int AccumulatorSlot = 1;
        private static Resource s_channels = new Resource(AnalogInputChannels);
        private IntPtr _port;
        private int _channel;
        private static int[] s_accumulatorChannels = { 0, 1 };
        private long _accumulatorOffset;

        public AnalogInput(int channel)
        {
            _channel = channel;

            CheckAnalogInputChannel(channel);

            try
            {
                s_channels.Allocate(channel);
            }
            catch (CheckedAllocationException ex)
            {
                throw new AllocationException("Analog input channel " + _channel
                     + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte)channel);
            int status = 0;
            _port = HALAnalog.InitializeAnalogInputPort(portPointer, ref status);
            HAL.Report(ResourceType.kResourceType_AnalogChannel, (byte)channel);
        }

        public override void Free()
        {
            s_channels.Free(_channel);
            _channel = 0;
            _accumulatorOffset = 0;
        }

        public int GetValue()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogValue(_port, ref status);
            return value;
        }

        public int GetAverageValue()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogAverageValue(_port, ref status);
            return value;
        }

        public double GetVoltage()
        {
            int status = 0;
            double value = HALAnalog.GetAnalogVoltage(_port, ref status);
            return value;
        }

        public double GetAverageVoltage()
        {
            int status = 0;
            double value = HALAnalog.GetAnalogAverageVoltage(_port, ref status);
            return value;
        }

        public long GetLSBWeight()
        {
            int status = 0;
            long value = HALAnalog.GetAnalogLSBWeight(_port, ref status);
            return value;
        }

        public int GetOffset()
        {
            int status = 0;
            int value = HALAnalog.GetAnalogOffset(_port, ref status);
            return value;
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetAverageBits(int bits)
        {
            int status = 0;
            HALAnalog.SetAnalogAverageBits(_port, (uint)bits, ref status);
        }

        public int GetAverageBits()
        {
            int status = 0;
            uint value = HALAnalog.GetAnalogAverageBits(_port, ref status);
            return (int)value;
        }

        public void SetOversampleBits(int bits)
        {
            int status = 0;
            HALAnalog.SetAnalogOversampleBits(_port, (uint)bits, ref status);
        }

        public int GetOversampleBits()
        {
            int status = 0;
            uint value = HALAnalog.GetAnalogOversampleBits(_port, ref status);
            return (int)value;
        }

        public void InitAccumulator()
        {
            if (!IsAccumulatorChannel())
            {
                throw new AllocationException("This is not an accumulator");
            }
            _accumulatorOffset = 0;
            int status = 0;
            HALAnalog.InitAccumulator(_port, ref status);
        }

        public void SetAccumulatorInitialValue(long initialValue)
        {
            _accumulatorOffset = initialValue;
        }

        public void ResetAccumulator()
        {
            int status = 0;
            HALAnalog.ResetAccumulator(_port, ref status);

            double sampleTime = 1.0 / GetGlobalSampleRate();
            double overSamples = 1 << GetOversampleBits();
            double averageSamples = 1 << GetAverageBits();
            Timer.Delay(sampleTime * overSamples * averageSamples);
        }

        public void SetAccumulatorDeadband(int deadband)
        {
            int status = 0;
            HALAnalog.SetAccumulatorDeadband(_port, deadband, ref status);
        }

        public long GetAccumulatorValue()
        {
            int status = 0;
            long value = HALAnalog.GetAccumulatorValue(_port, ref status);
            return value + _accumulatorOffset;
        }

        public long GetAccumulatorCount()
        {
            int status = 0;
            long value = HALAnalog.GetAccumulatorCount(_port, ref status);
            return value;
        }

        public void GetAccumulatorOutput(AccumulatorResult result)
        {
            if (result == null)
                throw new ArgumentNullException();
            if (!IsAccumulatorChannel())
                throw new ArgumentException("Channel " + _channel
                    + " is not an accumulator channel.");

            uint count = 0;
            long value = 0;
            int status = 0;
            HALAnalog.GetAccumulatorOutput(_port, ref value, ref count, ref status);
            result.m_value = value + _accumulatorOffset;
            result.m_count = count;
        }


        public bool IsAccumulatorChannel()
        {
            for (int i = 0; i < s_accumulatorChannels.Length; i++)
            {
                if (_channel == s_accumulatorChannels[i])
                {
                    return true;
                }
            }
            return false;
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
