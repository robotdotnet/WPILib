using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalCTREPCMData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetCTREPCMData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool solenoidInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSolenoidOutputCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSolenoidOutputCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSolenoidOutput(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSolenoidOutput(int index, int channel, [MarshalAs(UnmanagedType.I4)] bool solenoidOutput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCompressorOnCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCompressorOnCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCompressorOn(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCompressorOn(int index, [MarshalAs(UnmanagedType.I4)] bool compressorOn);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMClosedLoopEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterClosedLoopEnabledCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMClosedLoopEnabledCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelClosedLoopEnabledCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMClosedLoopEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetClosedLoopEnabled(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMClosedLoopEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetClosedLoopEnabled(int index, [MarshalAs(UnmanagedType.I4)] bool closedLoopEnabled);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPressureSwitchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPressureSwitchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetPressureSwitch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPressureSwitch(int index, [MarshalAs(UnmanagedType.I4)] bool pressureSwitch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCompressorCurrentCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelCTREPCMCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCompressorCurrentCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCompressorCurrent(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCompressorCurrent(int index, double compressorCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetCTREPCMAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetAllSolenoids(int index, Span<byte> values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetCTREPCMAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAllSolenoids(int index, byte values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMAllNonSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllNonSolenoidCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterCTREPCMAllSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllSolenoidCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
