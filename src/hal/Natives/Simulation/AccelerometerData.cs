using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAccelerometerData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAccelerometerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAccelerometerData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccelerometerActiveCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccelerometerActiveCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAccelerometerActive(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerActive(int index, [MarshalAs(UnmanagedType.I4)] bool active);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccelerometerRangeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccelerometerRangeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalAccelerometer.Range GetAccelerometerRange(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerRange(int index, HalAccelerometer.Range range);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccelerometerXCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccelerometerXCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerX(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerX(int index, double x);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccelerometerYCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccelerometerYCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerY(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerY(int index, double y);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAccelerometerZCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAccelerometerZCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerZ(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerZ(int index, double z);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAccelerometerAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAccelerometerAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
