using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDigitalPWMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindDigitalPWMForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDigitalPWMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMDutyCycleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDutyCycleCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMDutyCycleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDutyCycleCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDutyCycle(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDutyCycle(int index, double dutyCycle);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMPinCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPinCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMPinCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPinCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMPin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPin(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMPin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPin(int index, int pin);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
