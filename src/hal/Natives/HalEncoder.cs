using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalEncoder
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeEncoderRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDecodingScaleFactor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetEncoderDecodingScaleFactorRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderDirectionRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDistance")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetEncoderDistanceRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetEncoderDistancePerPulseRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderEncodingScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderEncodingScaleRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderEncodingType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial EncoderEncodingType GetEncoderEncodingTypeRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderFPGAIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderFPGAIndexRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetEncoderPeriodRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetEncoderRateRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderRaw")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderRawRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderSamplesToAverageRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetEncoderStopped")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetEncoderStoppedRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalEncoderHandle InitializeEncoderRefShim(HalAnalogTriggerHandle digitalSourceHandleA, AnalogTriggerType analogTriggerTypeA, HalAnalogTriggerHandle digitalSourceHandleB, AnalogTriggerType analogTriggerTypeB, int reverseDirection, EncoderEncodingType encodingType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetEncoder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ResetEncoderRefShim(HalEncoderHandle encoderHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderDistancePerPulse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderDistancePerPulseRefShim(HalEncoderHandle encoderHandle, double distancePerPulse, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderIndexSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderIndexSourceRefShim(HalEncoderHandle encoderHandle, HalDigitalHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type, ref HalStatus status);

    public static void SetEncoderIndexSource(HalEncoderHandle encoderHandle, HalDigitalHandle digitalSourceHandle, EncoderIndexingType type, out HalStatus status)
    {
        SetEncoderIndexSource(encoderHandle, digitalSourceHandle, AnalogTriggerType.State, type, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderIndexSource")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderIndexSourceRefShim(HalEncoderHandle encoderHandle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, EncoderIndexingType type, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderMaxPeriod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderMaxPeriodRefShim(HalEncoderHandle encoderHandle, double maxPeriod, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderMinRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderMinRateRefShim(HalEncoderHandle encoderHandle, double minRate, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderReverseDirection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderReverseDirectionRefShim(HalEncoderHandle encoderHandle, int reverseDirection, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderSamplesToAverage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetEncoderSamplesToAverageRefShim(HalEncoderHandle encoderHandle, int samplesToAverage, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetEncoderSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetEncoderSimDevice(HalEncoderHandle handle, HalSimDeviceHandle device);


}
