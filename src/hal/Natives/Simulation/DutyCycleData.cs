using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDutyCycleData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindDutyCycleForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindDutyCycleForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDutyCycleData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetDutyCycleData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleDigitalChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleDigitalChannel(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDutyCycleInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDutyCycleInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDutyCycleInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetDutyCycleSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDutyCycleOutputCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDutyCycleOutputCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDutyCycleOutput(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleOutput(int index, double output);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleFrequencyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDutyCycleFrequencyCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleFrequencyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDutyCycleFrequencyCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDutyCycleFrequency(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycleFrequency(int index, int frequency);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterDutyCycleAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
