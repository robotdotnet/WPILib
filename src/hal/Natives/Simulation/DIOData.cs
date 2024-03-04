using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDIOData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDIOData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetDIOData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDIOInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDIOInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDIOInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetDIOSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDIOValueCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOValueCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDIOValueCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDIOValue(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOValue(int index, [MarshalAs(UnmanagedType.I4)] bool value);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOPulseLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDIOPulseLengthCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOPulseLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDIOPulseLengthCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOPulseLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDIOPulseLength(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOPulseLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOPulseLength(int index, double pulseLength);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOIsInputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDIOIsInputCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOIsInputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDIOIsInputCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOIsInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDIOIsInput(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOIsInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOIsInput(int index, [MarshalAs(UnmanagedType.I4)] bool isInput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOFilterIndexCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDIOFilterIndexCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDIOFilterIndexCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDIOFilterIndexCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDIOFilterIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDIOFilterIndex(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDIOFilterIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDIOFilterIndex(int index, int filterIndex);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDIOAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterDIOAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
