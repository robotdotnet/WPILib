using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalPWMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetPWMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetPWMData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetPWMInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPulseMicrosecondCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMPulseMicrosecondCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPulseMicrosecondCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMPulseMicrosecondCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPulseMicrosecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPWMPulseMicrosecond(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPulseMicrosecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMPulseMicrosecond(int index, int microsecondPulseTime);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMSpeedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMSpeedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMSpeedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMSpeedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPWMSpeed(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMSpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMSpeed(int index, double speed);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPositionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMPositionCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPositionCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMPositionCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPWMPosition(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMPosition(int index, double position);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMPeriodScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMPeriodScaleCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMPeriodScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMPeriodScaleCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPWMPeriodScale(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMPeriodScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMPeriodScale(int index, int periodScale);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMZeroLatchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPWMZeroLatchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelPWMZeroLatchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPWMZeroLatchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetPWMZeroLatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetPWMZeroLatch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetPWMZeroLatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPWMZeroLatch(int index, [MarshalAs(UnmanagedType.I4)] bool zeroLatch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterPWMAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterPWMAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
