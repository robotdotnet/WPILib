using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDIOData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDIOData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterValueCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelValueCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetValue(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetValue(int index, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOPulseLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPulseLengthCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOPulseLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPulseLengthCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOPulseLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetPulseLength(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOPulseLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPulseLength(int index, double pulseLength);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOIsInputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterIsInputCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOIsInputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelIsInputCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOIsInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetIsInput(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOIsInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetIsInput(int index, [MarshalAs(UnmanagedType.I4)] bool isInput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOFilterIndexCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterFilterIndexCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOFilterIndexCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelFilterIndexCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOFilterIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetFilterIndex(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOFilterIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetFilterIndex(int index, int filterIndex);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
