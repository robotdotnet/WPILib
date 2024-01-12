using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalAnalogGyro
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CalibrateAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CalibrateAnalogGyro(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnalogGyro(HalGyroHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogGyroAngle(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAnalogGyroCenter(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroOffset")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogGyroOffset(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogGyroRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogGyroRate(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogGyro", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalGyroHandle InitializeAnalogGyro(HalAnalogInputHandle handle, string allocationLocation, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAnalogGyro(HalGyroHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroDeadband(HalGyroHandle handle, double volts, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroParameters")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroParameters(HalGyroHandle handle, double voltsPerDegreePerSecond, double offset, int center, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogGyroVoltsPerDegreePerSecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroVoltsPerDegreePerSecond(HalGyroHandle handle, double voltsPerDegreePerSecond, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetupAnalogGyro")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetupAnalogGyro(HalGyroHandle handle, out HalStatus status);


}
