using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalCTREPCMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetCTREPCMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetCTREPCMData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCTREPCMInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool solenoidInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMSolenoidOutputCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMSolenoidOutputCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCTREPCMSolenoidOutput(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMSolenoidOutput(int index, int channel, [MarshalAs(UnmanagedType.I4)] bool solenoidOutput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMCompressorOnCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMCompressorOnCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCTREPCMCompressorOn(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMCompressorOn(int index, [MarshalAs(UnmanagedType.I4)] bool compressorOn);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMClosedLoopEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMClosedLoopEnabledCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMClosedLoopEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMClosedLoopEnabledCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMClosedLoopEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCTREPCMClosedLoopEnabled(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMClosedLoopEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMClosedLoopEnabled(int index, [MarshalAs(UnmanagedType.I4)] bool closedLoopEnabled);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMPressureSwitchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMPressureSwitchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCTREPCMPressureSwitch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMPressureSwitch(int index, [MarshalAs(UnmanagedType.I4)] bool pressureSwitch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCTREPCMCompressorCurrentCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCTREPCMCompressorCurrentCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCTREPCMCompressorCurrent(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMCompressorCurrent(int index, double compressorCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetCTREPCMAllSolenoids(int index, Span<byte> values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCTREPCMAllSolenoids(int index, byte values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMAllNonSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterCTREPCMAllNonSolenoidCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMAllSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterCTREPCMAllSolenoidCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
