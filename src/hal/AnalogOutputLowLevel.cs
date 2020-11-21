
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class AnalogOutputLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static AnalogOutputLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

        public static int CheckChannel(int channel)
        {
            return lowLevel.HAL_CheckAnalogOutputChannel(channel);
        }

        public static void FreePort(int analogOutputHandle)
        {
            lowLevel.HAL_FreeAnalogOutputPort(analogOutputHandle);
        }

        public static double Get(int analogOutputHandle)
        {
            return lowLevel.HAL_GetAnalogOutput(analogOutputHandle);
        }

        public static int InitializePort(int portHandle)
        {
            return lowLevel.HAL_InitializeAnalogOutputPort(portHandle);
        }

        public static void Set(int analogOutputHandle, double voltage)
        {
            lowLevel.HAL_SetAnalogOutput(analogOutputHandle, voltage);
        }

    }
}
