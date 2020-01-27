
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ICompressor))]
    public unsafe static class Compressor
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ICompressor lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static int CheckCompressorModule(int module)
{
return lowLevel.HAL_CheckCompressorModule(module);
}

public static int GetCompressor(int compressorHandle)
{
return lowLevel.HAL_GetCompressor(compressorHandle);
}

public static int GetCompressorClosedLoopControl(int compressorHandle)
{
return lowLevel.HAL_GetCompressorClosedLoopControl(compressorHandle);
}

public static double GetCompressorCurrent(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrent(compressorHandle);
}

public static int GetCompressorCurrentTooHighFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrentTooHighFault(compressorHandle);
}

public static int GetCompressorCurrentTooHighStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrentTooHighStickyFault(compressorHandle);
}

public static int GetCompressorNotConnectedFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorNotConnectedFault(compressorHandle);
}

public static int GetCompressorNotConnectedStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorNotConnectedStickyFault(compressorHandle);
}

public static int GetCompressorPressureSwitch(int compressorHandle)
{
return lowLevel.HAL_GetCompressorPressureSwitch(compressorHandle);
}

public static int GetCompressorShortedFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorShortedFault(compressorHandle);
}

public static int GetCompressorShortedStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorShortedStickyFault(compressorHandle);
}

public static int InitializeCompressor(int module)
{
return lowLevel.HAL_InitializeCompressor(module);
}

public static void SetCompressorClosedLoopControl(int compressorHandle, int value)
{
lowLevel.HAL_SetCompressorClosedLoopControl(compressorHandle, value);
}

}
}
