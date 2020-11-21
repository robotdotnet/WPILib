
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class AnalogInputLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static AnalogInputLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckAnalogInputChannel(channel);
        }

        public static int CheckAnalogModule(int module)
        {
            return lowLevel.HAL_CheckAnalogModule(module);
        }

        public static void FreePort(int analogPortHandle)
        {
            lowLevel.HAL_FreeAnalogInputPort(analogPortHandle);
        }

        public static int GetAnalogAverageBits(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogAverageBits(analogPortHandle);
        }

        public static int GetAnalogAverageValue(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogAverageValue(analogPortHandle);
        }

        public static double GetAnalogAverageVoltage(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogAverageVoltage(analogPortHandle);
        }

        public static int GetAnalogLSBWeight(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogLSBWeight(analogPortHandle);
        }

        public static int GetAnalogOffset(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogOffset(analogPortHandle);
        }

        public static int GetAnalogOversampleBits(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogOversampleBits(analogPortHandle);
        }

        public static double GetAnalogSampleRate()
        {
            return lowLevel.HAL_GetAnalogSampleRate();
        }

        public static int GetAnalogValue(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogValue(analogPortHandle);
        }

        public static double GetAnalogValueToVolts(int analogPortHandle, int rawValue)
        {
            return lowLevel.HAL_GetAnalogValueToVolts(analogPortHandle, rawValue);
        }

        public static double GetAnalogVoltage(int analogPortHandle)
        {
            return lowLevel.HAL_GetAnalogVoltage(analogPortHandle);
        }

        public static int GetAnalogVoltsToValue(int analogPortHandle, double voltage)
        {
            return lowLevel.HAL_GetAnalogVoltsToValue(analogPortHandle, voltage);
        }

        public static int InitializePort(int portHandle)
        {
            return lowLevel.HAL_InitializeAnalogInputPort(portHandle);
        }

        public static void SetAnalogAverageBits(int analogPortHandle, int bits)
        {
            lowLevel.HAL_SetAnalogAverageBits(analogPortHandle, bits);
        }

        public static void SetSimDevice(int handle, int device)
        {
            lowLevel.HAL_SetAnalogInputSimDevice(handle, device);
        }

        public static void SetAnalogOversampleBits(int analogPortHandle, int bits)
        {
            lowLevel.HAL_SetAnalogOversampleBits(analogPortHandle, bits);
        }

        public static void SetAnalogSampleRate(double samplesPerSecond)
        {
            lowLevel.HAL_SetAnalogSampleRate(samplesPerSecond);
        }

    }
}
