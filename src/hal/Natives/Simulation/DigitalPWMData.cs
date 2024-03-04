using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalDigitalPWMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindDigitalPWMForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindDigitalPWMForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetDigitalPWMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetDigitalPWMData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDigitalPWMInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDigitalPWMInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetDigitalPWMInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMDutyCycleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDigitalPWMDutyCycleCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMDutyCycleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDigitalPWMDutyCycleCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetDigitalPWMDutyCycle(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMDutyCycle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMDutyCycle(int index, double dutyCycle);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMPinCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDigitalPWMPinCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelDigitalPWMPinCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDigitalPWMPinCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetDigitalPWMPin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDigitalPWMPin(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetDigitalPWMPin")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetDigitalPWMPin(int index, int pin);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterDigitalPWMAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterDigitalPWMAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
