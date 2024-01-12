using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAnalogGyro
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CalibrateAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void CalibrateAnalogGyroRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnalogGyro(HalGyroHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogGyroAngleRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetAnalogGyroCenterRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogGyroOffsetRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogGyroRateRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogGyro", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalGyroHandle InitializeAnalogGyroRefShim(HalAnalogInputHandle handle, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ResetAnalogGyroRefShim(HalGyroHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogGyroDeadbandRefShim(HalGyroHandle handle, double volts, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroParameters")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogGyroParametersRefShim(HalGyroHandle handle, double voltsPerDegreePerSecond, double offset, int center, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroVoltsPerDegreePerSecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogGyroVoltsPerDegreePerSecondRefShim(HalGyroHandle handle, double voltsPerDegreePerSecond, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetupAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetupAnalogGyroRefShim(HalGyroHandle handle, ref HalStatus status);


}
