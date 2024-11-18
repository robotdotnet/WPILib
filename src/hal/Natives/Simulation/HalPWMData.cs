using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalPWMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetPWMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPulseMicrosecondCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPulseMicrosecondCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPulseMicrosecondCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPulseMicrosecondCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPulseMicrosecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPulseMicrosecond(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPulseMicrosecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPulseMicrosecond(int index, int microsecondPulseTime);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMSpeedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSpeedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMSpeedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSpeedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetSpeed(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSpeed(int index, double speed);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPositionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPositionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPositionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPositionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPosition(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPosition(int index, double position);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPeriodScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPeriodScaleCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPeriodScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPeriodScaleCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPeriodScale(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPeriodScale(int index, int periodScale);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMZeroLatchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterZeroLatchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMZeroLatchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelZeroLatchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMZeroLatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetZeroLatch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMZeroLatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetZeroLatch(int index, [MarshalAs(UnmanagedType.I4)] bool zeroLatch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
