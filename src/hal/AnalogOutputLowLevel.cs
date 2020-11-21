
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class AnalogOutputLowLevel
    {
        internal static AnalogOutputLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

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
