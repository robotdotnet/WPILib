using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAnalogInData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAnalogInData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAnalogInData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAnalogInInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetAnalogInSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAverageBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAverageBitsCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAverageBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAverageBitsCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAnalogInAverageBits(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAverageBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAverageBits(int index, int averageBits);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInOversampleBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInOversampleBitsCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInOversampleBitsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInOversampleBitsCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAnalogInOversampleBits(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInOversampleBits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInOversampleBits(int index, int oversampleBits);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInVoltageCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInVoltageCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInVoltageCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogInVoltage(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInVoltage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInVoltage(int index, double voltage);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAccumulatorInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAccumulatorInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAnalogInAccumulatorInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAccumulatorInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool accumulatorInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAccumulatorValueCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAccumulatorValueCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAnalogInAccumulatorValue(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAccumulatorValue(int index, long accumulatorValue);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAccumulatorCountCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorCountCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAccumulatorCountCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetAnalogInAccumulatorCount(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAccumulatorCount(int index, long accumulatorCount);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorCenterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAccumulatorCenterCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorCenterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAccumulatorCenterCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAnalogInAccumulatorCenter(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorCenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAccumulatorCenter(int index, int accumulatorCenter);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAccumulatorDeadbandCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogInAccumulatorDeadbandCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogInAccumulatorDeadbandCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogInAccumulatorDeadbandCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogInAccumulatorDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAnalogInAccumulatorDeadband(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogInAccumulatorDeadband")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogInAccumulatorDeadband(int index, int accumulatorDeadband);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogInAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAnalogInAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
