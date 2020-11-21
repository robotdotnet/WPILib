
using Hal.Natives;
using System;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class AddressableLEDLowLevel
    {
        internal static AddressableLEDLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void Free(int handle)
        {
            lowLevel.HAL_FreeAddressableLED(handle);
        }

        public static int Initialize(int outputPort)
        {
            return lowLevel.HAL_InitializeAddressableLED(outputPort);
        }

        public static void SetBitTiming(int handle, int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds)
        {
            lowLevel.HAL_SetAddressableLEDBitTiming(handle, lowTime0NanoSeconds, highTime0NanoSeconds, lowTime1NanoSeconds, highTime1NanoSeconds);
        }

        public static void SetLength(int handle, int length)
        {
            lowLevel.HAL_SetAddressableLEDLength(handle, length);
        }

        public static void SetOutputPort(int handle, int outputPort)
        {
            lowLevel.HAL_SetAddressableLEDOutputPort(handle, outputPort);
        }

        public static void SetSyncTime(int handle, int syncTimeMicroSeconds)
        {
            lowLevel.HAL_SetAddressableLEDSyncTime(handle, syncTimeMicroSeconds);
        }

        public static void StartOutput(int handle)
        {
            lowLevel.HAL_StartAddressableLEDOutput(handle);
        }

        public static void StopOutput(int handle)
        {
            lowLevel.HAL_StopAddressableLEDOutput(handle);
        }

        public static void WriteData(int handle, AddressableLEDData* data, int length)
        {
            lowLevel.HAL_WriteAddressableLEDData(handle, data, length);
        }

        public static void WriteData(int handle, ReadOnlySpan<AddressableLEDData> data)
        {
            fixed (AddressableLEDData* d = data)
            {
                lowLevel.HAL_WriteAddressableLEDData(handle, d, data.Length);
            }
        }

    }
}
