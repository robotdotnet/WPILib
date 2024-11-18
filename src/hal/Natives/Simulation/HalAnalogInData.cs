using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAnalogInData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAnalogInData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAverageBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAverageBitsCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAverageBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAverageBitsCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAverageBits(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAverageBits(int index, int averageBits);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInOversampleBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterOversampleBitsCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInOversampleBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelOversampleBitsCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetOversampleBits(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetOversampleBits(int index, int oversampleBits);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterVoltageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelVoltageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetVoltage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetVoltage(int index, double voltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccumulatorInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccumulatorInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAccumulatorInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccumulatorInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool accumulatorInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccumulatorValueCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccumulatorValueCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAccumulatorValue(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccumulatorValue(int index, long accumulatorValue);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccumulatorCountCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccumulatorCountCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAccumulatorCount(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccumulatorCount(int index, long accumulatorCount);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorCenterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccumulatorCenterCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorCenterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccumulatorCenterCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAccumulatorCenter(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccumulatorCenter(int index, int accumulatorCenter);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorDeadbandCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccumulatorDeadbandCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorDeadbandCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccumulatorDeadbandCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAccumulatorDeadband(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccumulatorDeadband(int index, int accumulatorDeadband);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
