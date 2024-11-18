using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalPowerDistributionData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetPowerDistributionData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionTemperatureCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterTemperatureCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionTemperatureCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelTemperatureCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionTemperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTemperature(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionTemperature")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTemperature(int index, double temperature);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterVoltageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelVoltageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVoltage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetVoltage(int index, double voltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCurrentCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPowerDistributionCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCurrentCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCurrent(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCurrent(int index, int channel, double current);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPowerDistributionAllCurrents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetAllCurrents(int index, double* currents, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPowerDistributionAllCurrents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAllCurrents(int index, double* currents, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPowerDistributionAllNonCurrentCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllNonCurrentCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
