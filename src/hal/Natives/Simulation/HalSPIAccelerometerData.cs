using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalSPIAccelerometerData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetSPIAccelerometerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterActiveCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerActiveCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelActiveCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetActive(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetActive(int index, [MarshalAs(UnmanagedType.I4)] bool active);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRangeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerRangeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRangeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetRange(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRange(int index, int range);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterXCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerXCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelXCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetX(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetX(int index, double x);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterYCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerYCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelYCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetY(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetY(int index, double y);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterZCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIAccelerometerZCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelZCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetZ(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetZ(int index, double z);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIAccelerometerAllCallbcaks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbcaks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
