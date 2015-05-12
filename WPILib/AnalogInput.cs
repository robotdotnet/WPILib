using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Interfaces;
using WPILib.Util;
using HAL_FRC;

namespace WPILib
{
    public class AnalogInput : SensorBase, PIDSource
    {
        //private static int AccumulatorSlot = 1;
        private static Resource channels = new Resource(AnalogInputChannels);
        private IntPtr _port;
        private int _channel;
        private static int[] AccumulatorChannels = { 0, 1 };
        private long _accumulatorOffset;

        public AnalogInput(int channel)
        {
            _channel = channel;

            CheckAnalogInputChannel(channel);

            try
            {
                channels.Allocate(channel);
            }
            catch( CheckedAllocationException ex)
            {
                throw new AllocationException("Analog input channel " + _channel
                     + " is already allocated");
            }

            IntPtr portPointer = HAL.GetPort((byte) channel);
            int status = 0;
            _port = HALAnalog.initializeAnalogInputPort(portPointer, ref status);
            HAL.Report(ResourceType.kResourceType_AnalogChannel, (byte)channel);
        }

        public override void Free()
        {
            channels.Free(_channel);
            _channel = 0;
            _accumulatorOffset = 0;
        }

        public int GetValue()
        {
            int status = 0;
            int value = HALAnalog.getAnalogValue(_port, ref status);
            return value;
        }

        public int GetAverageValue()
        {
            int status = 0;
            int value = HALAnalog.getAnalogAverageValue(_port, ref status);
            return value;
        }

        public double GetVoltage()
        {
            int status = 0;
            double value = HALAnalog.getAnalogVoltage(_port, ref status);
            return value;
        }

        public double GetAverageVoltage()
        {
            int status = 0;
            double value = HALAnalog.getAnalogAverageVoltage(_port, ref status);
            return value;
        }

        public long GetLSBWeight()
        {
            int status = 0;
            long value = HALAnalog.getAnalogLSBWeight(_port, ref status);
            return value;
        }

        public int GetOffset()
        {
            int status = 0;
            int value = HALAnalog.getAnalogOffset(_port, ref status);
            return value;
        }

        public int GetChannel()
        {
            return _channel;
        }

        public void SetAverageBits(int bits)
        {
            int status = 0;
            HALAnalog.setAnalogAverageBits(_port, (uint)bits, ref status);
        }

        public int GetAverageBits()
        {
            int status = 0;
            uint value = HALAnalog.getAnalogAverageBits(_port, ref status);
            return (int)value;
        }

        public void SetOversampleBits(int bits)
        {
            int status = 0;
            HALAnalog.setAnalogOversampleBits(_port, (uint)bits, ref status);
        }

        public int GetOversampleBits()
        {
            int status = 0;
            uint value = HALAnalog.getAnalogOversampleBits(_port, ref status);
            return (int) value;
        }

        public void InitAccumulator()
        {
            if (!IsAccumulatorChannel())
            {
                throw new AllocationException("This is not an accumulator");
            }
            _accumulatorOffset = 0;
            int status = 0;
            HALAnalog.initAccumulator(_port, ref status);
        }

        public void SetAccumulatorInitialValue(long initialValue)
        {
            _accumulatorOffset = initialValue;
        }

        public void ResetAccumulator()
        {
            int status = 0;
            HALAnalog.resetAccumulator(_port, ref status);

            double sampleTime = 1.0 / GetGlobalSampleRate();
            double overSamples = 1 << GetOversampleBits();
            double averageSamples = 1 << GetAverageBits();
            Timer.Delay(sampleTime * overSamples * averageSamples);
        }

        public void SetAccumulatorDeadband (int deadband)
        {
            int status = 0;
            HALAnalog.setAccumulatorDeadband(_port, deadband, ref status);
        }

        public long GetAccumulatorValue()
        {
            int status = 0;
            long value = HALAnalog.getAccumulatorValue(_port, ref status);
            return value + _accumulatorOffset;
        }

        public long GetAccumulatorCount()
        {
            int status = 0;
            long value = HALAnalog.getAccumulatorCount(_port, ref status);
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
            int value = 0;
            int status = 0;
            HALAnalog.getAccumulatorOutput(_port, ref value, ref count, ref status);
            result.value = value + _accumulatorOffset;
            result.count = count;
        }

        
        public bool IsAccumulatorChannel()
        {
            for (int i = 0; i < AccumulatorChannels.Length; i++)
            {
                if (_channel == AccumulatorChannels[i])
                {
                    return true;
                }
            }
            return false;
        }

        public static void SetGlobalSampleRate(double samplesPerSecond)
        {
            int status = 0;
            HALAnalog.setAnalogSampleRate(samplesPerSecond, ref status);
        }

        public static double GetGlobalSampleRate()
        {
            int status = 0;
            double value = HALAnalog.getAnalogSampleRate(ref status);
            return value;
        }

        public double PidGet()
        {
            return GetAverageVoltage();
        }
    }
}
