﻿using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalEncoder
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEncoder(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoder(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDecodingScaleFactor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderDecodingScaleFactor(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderDirection(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderDistance(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderDistancePerPulse(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderEncodingScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderEncodingScale(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderEncodingType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial EncoderEncodingType GetEncoderEncodingType(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderFPGAIndex(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderPeriod(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetEncoderRate(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderRaw(HalEncoderHandle encoderHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetEncoderSamplesToAverage(HalEncoderHandle encoderHandle, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderStopped method. * * @param[in] encoderHandle the encoder handle * @param[in] maxPeriod     the maximum period where the counted device is *                          considered moving in seconds * @param[out] status       Error status variable. 0 on success. */ void HAL_SetEncoderMaxPeriod")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial* GetEncoderStopped method. ** @param[in] encoderHandle the encoder handle * @param[in] maxPeriod     the maximum period where the counted device is *                          considered moving in seconds* @param[out] status Error status variable. 0 on success. */ void SetEncoderMaxPeriod(HalEncoderHandle encoderHandle, double maxPeriod, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeEncoder")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial HalEncoderHandle InitializeEncoder(HALHANDLETODO digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, HALHANDLETODO digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "HAL_ResetEncoder")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial void ResetEncoder(HalEncoderHandle encoderHandle, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "                     passed to HAL_SetEncoderDistancePerPulse) */ double HAL_GetEncoderDistance")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial* passed to SetEncoderDistancePerPulse) */ double GetEncoderDistance(HalEncoderHandle encoderHandle, out HalStatus status);

    // [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderIndexSource")]
    // [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    // public static partial void SetEncoderIndexSource(HalEncoderHandle encoderHandle, HALHANDLETODO digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderMaxPeriod(HalEncoderHandle encoderHandle, double maxPeriod, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderMinRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderMinRate(HalEncoderHandle encoderHandle, double minRate, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderReverseDirection(HalEncoderHandle encoderHandle, int reverseDirection, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderSamplesToAverage(HalEncoderHandle encoderHandle, int samplesToAverage, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderSimDevice(HalEncoderHandle handle, HalSimDeviceHandle device);


}
