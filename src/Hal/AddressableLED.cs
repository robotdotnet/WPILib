
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IAddressableLED))]
    public unsafe static class AddressableLED
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IAddressableLED lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void FreeAddressableLED(int handle)
{
lowLevel.HAL_FreeAddressableLED(handle);
}

public static int InitializeAddressableLED(int outputPort)
{
return lowLevel.HAL_InitializeAddressableLED(outputPort);
}

public static void SetAddressableLEDBitTiming(int handle, int lowTime0NanoSeconds, int highTime0NanoSeconds, int lowTime1NanoSeconds, int highTime1NanoSeconds)
{
lowLevel.HAL_SetAddressableLEDBitTiming(handle, lowTime0NanoSeconds, highTime0NanoSeconds, lowTime1NanoSeconds, highTime1NanoSeconds);
}

public static void SetAddressableLEDLength(int handle, int length)
{
lowLevel.HAL_SetAddressableLEDLength(handle, length);
}

public static void SetAddressableLEDOutputPort(int handle, int outputPort)
{
lowLevel.HAL_SetAddressableLEDOutputPort(handle, outputPort);
}

public static void SetAddressableLEDSyncTime(int handle, int syncTimeMicroSeconds)
{
lowLevel.HAL_SetAddressableLEDSyncTime(handle, syncTimeMicroSeconds);
}

public static void StartAddressableLEDOutput(int handle)
{
lowLevel.HAL_StartAddressableLEDOutput(handle);
}

public static void StopAddressableLEDOutput(int handle)
{
lowLevel.HAL_StopAddressableLEDOutput(handle);
}

public static void WriteAddressableLEDData(int handle, AddressableLEDData* data, int length)
{
lowLevel.HAL_WriteAddressableLEDData(handle, data, length);
}

}
}
