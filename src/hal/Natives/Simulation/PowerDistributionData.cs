using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalPowerDistributionData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetPowerDistributionData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetPowerDistributionData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPowerDistributionInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPowerDistributionInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetPowerDistributionInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPowerDistributionInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionTemperatureCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPowerDistributionTemperatureCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionTemperatureCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPowerDistributionTemperatureCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionTemperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPowerDistributionTemperature(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionTemperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPowerDistributionTemperature(int index, double temperature);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPowerDistributionVoltageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPowerDistributionVoltageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPowerDistributionVoltage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPowerDistributionVoltage(int index, double voltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPowerDistributionCurrentCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPowerDistributionCurrentCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPowerDistributionCurrent(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPowerDistributionCurrent(int index, int channel, double current);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionAllCurrents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetPowerDistributionAllCurrents(int index, double* currents, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionAllCurrents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPowerDistributionAllCurrents(int index, double* currents, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionAllNonCurrentCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterPowerDistributionAllNonCurrentCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
