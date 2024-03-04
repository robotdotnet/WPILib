using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAnalogGyroData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAnalogGyroData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAnalogGyroData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogGyroAngleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogGyroAngleCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogGyroAngleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogGyroAngleCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogGyroAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogGyroAngle(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogGyroAngle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroAngle(int index, double angle);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogGyroRateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogGyroRateCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogGyroRateCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogGyroRateCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogGyroRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogGyroRate(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogGyroRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroRate(int index, double rate);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogGyroInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogGyroInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogGyroInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogGyroInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogGyroInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAnalogGyroInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogGyroInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogGyroInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogGyroAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAnalogGyroAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
