
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(IEncoder))]
    public unsafe static class Encoder
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static IEncoder lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void FreeEncoder(int encoderHandle)
{
lowLevel.HAL_FreeEncoder(encoderHandle);
}

public static int GetEncoder(int encoderHandle)
{
return lowLevel.HAL_GetEncoder(encoderHandle);
}

public static double GetEncoderDecodingScaleFactor(int encoderHandle)
{
return lowLevel.HAL_GetEncoderDecodingScaleFactor(encoderHandle);
}

public static int GetEncoderDirection(int encoderHandle)
{
return lowLevel.HAL_GetEncoderDirection(encoderHandle);
}

public static double GetEncoderDistance(int encoderHandle)
{
return lowLevel.HAL_GetEncoderDistance(encoderHandle);
}

public static double GetEncoderDistancePerPulse(int encoderHandle)
{
return lowLevel.HAL_GetEncoderDistancePerPulse(encoderHandle);
}

public static int GetEncoderEncodingScale(int encoderHandle)
{
return lowLevel.HAL_GetEncoderEncodingScale(encoderHandle);
}

public static EncoderEncodingType GetEncoderEncodingType(int encoderHandle)
{
return lowLevel.HAL_GetEncoderEncodingType(encoderHandle);
}

public static int GetEncoderFPGAIndex(int encoderHandle)
{
return lowLevel.HAL_GetEncoderFPGAIndex(encoderHandle);
}

public static double GetEncoderPeriod(int encoderHandle)
{
return lowLevel.HAL_GetEncoderPeriod(encoderHandle);
}

public static double GetEncoderRate(int encoderHandle)
{
return lowLevel.HAL_GetEncoderRate(encoderHandle);
}

public static int GetEncoderRaw(int encoderHandle)
{
return lowLevel.HAL_GetEncoderRaw(encoderHandle);
}

public static int GetEncoderSamplesToAverage(int encoderHandle)
{
return lowLevel.HAL_GetEncoderSamplesToAverage(encoderHandle);
}

public static int InitializeEncoder(int digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, int digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType)
{
return lowLevel.HAL_InitializeEncoder(digitalSourceHandleA, analogTriggerTypeA, digitalSourceHandleB, analogTriggerTypeB, reverseDirection, encodingType);
}

public static void ResetEncoder(int encoderHandle)
{
lowLevel.HAL_ResetEncoder(encoderHandle);
}

public static void SetEncoderIndexSource(int encoderHandle, int digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type)
{
lowLevel.HAL_SetEncoderIndexSource(encoderHandle, digitalSourceHandle, analogTriggerType, type);
}

public static void SetEncoderMaxPeriod(int encoderHandle, double maxPeriod)
{
lowLevel.HAL_SetEncoderMaxPeriod(encoderHandle, maxPeriod);
}

public static void SetEncoderMinRate(int encoderHandle, double minRate)
{
lowLevel.HAL_SetEncoderMinRate(encoderHandle, minRate);
}

public static void SetEncoderReverseDirection(int encoderHandle, int reverseDirection)
{
lowLevel.HAL_SetEncoderReverseDirection(encoderHandle, reverseDirection);
}

public static void SetEncoderSamplesToAverage(int encoderHandle, int samplesToAverage)
{
lowLevel.HAL_SetEncoderSamplesToAverage(encoderHandle, samplesToAverage);
}

public static void SetEncoderSimDevice(int handle, int device)
{
lowLevel.HAL_SetEncoderSimDevice(handle, device);
}

}
}
