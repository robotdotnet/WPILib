using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAccelerometerData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAccelerometerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterActiveCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelActiveCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetActive(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetActive(int index, [MarshalAs(UnmanagedType.I4)] bool active);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRangeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRangeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalAccelerometer.Range GetRange(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRange(int index, HalAccelerometer.Range range);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterXCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelXCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetX(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetX(int index, double x);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterYCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelYCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetY(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetY(int index, double y);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterZCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelZCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetZ(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetZ(int index, double z);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
