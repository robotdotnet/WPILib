using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDutyCycleData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindDutyCycleForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDutyCycleData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleDigitalChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDigitalChannel(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WPIHal.Handles.HalSimDeviceHandle GetSimDevice(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterOutputCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelOutputCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetOutput(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetOutput(int index, double output);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleFrequencyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterFrequencyCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDutyCycleFrequencyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelFrequencyCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetFrequency(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDutyCycleFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetFrequency(int index, int frequency);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDutyCycleAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
