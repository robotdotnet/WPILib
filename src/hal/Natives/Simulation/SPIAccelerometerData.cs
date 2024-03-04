using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalSPIAccelerometerData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetSPIAccelerometerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSPIAccelerometerData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIAccelerometerActiveCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIAccelerometerActiveCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSPIAccelerometerActive(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAccelerometerActive(int index, [MarshalAs(UnmanagedType.I4)] bool active);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIAccelerometerRangeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIAccelerometerRangeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSPIAccelerometerRange(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAccelerometerRange(int index, int range);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIAccelerometerXCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIAccelerometerXCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetSPIAccelerometerX(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAccelerometerX(int index, double x);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIAccelerometerYCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIAccelerometerYCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetSPIAccelerometerY(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAccelerometerY(int index, double y);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIAccelerometerZCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIAccelerometerZCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetSPIAccelerometerZ(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAccelerometerZ(int index, double z);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerAllCallbcaks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterSPIAccelerometerAllCallbcaks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
