
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

public static int CheckModule(int module)
{
return lowLevel.HAL_CheckCompressorModule(module);
}

public static int Get(int compressorHandle)
{
return lowLevel.HAL_GetCompressor(compressorHandle);
}

public static int GetClosedLoopControl(int compressorHandle)
{
return lowLevel.HAL_GetCompressorClosedLoopControl(compressorHandle);
}

public static double GetCurrent(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrent(compressorHandle);
}

public static int GetCurrentTooHighFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrentTooHighFault(compressorHandle);
}

public static int GetCurrentTooHighStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorCurrentTooHighStickyFault(compressorHandle);
}

public static int GetNotConnectedFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorNotConnectedFault(compressorHandle);
}

public static int GetNotConnectedStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorNotConnectedStickyFault(compressorHandle);
}

public static int GetPressureSwitch(int compressorHandle)
{
return lowLevel.HAL_GetCompressorPressureSwitch(compressorHandle);
}

public static int GetShortedFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorShortedFault(compressorHandle);
}

public static int GetShortedStickyFault(int compressorHandle)
{
return lowLevel.HAL_GetCompressorShortedStickyFault(compressorHandle);
}

public static int Initialize(int module)
{
return lowLevel.HAL_InitializeCompressor(module);
}

public static void SetClosedLoopControl(int compressorHandle, int value)
{
lowLevel.HAL_SetCompressorClosedLoopControl(compressorHandle, value);
}

}
}
