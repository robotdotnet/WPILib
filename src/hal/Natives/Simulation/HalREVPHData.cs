using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public enum RevPHCompressorConfigType : int
{
    Disabled = 0,
    Digital = 1,
    Analog = 2,
    Hybrid = 3
}

public static unsafe partial class HalREVPHData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetREVPHData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool solenoidInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSolenoidOutputCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSolenoidOutputCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSolenoidOutput(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSolenoidOutput(int index, int channel, [MarshalAs(UnmanagedType.I4)] bool solenoidOutput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCompressorOnCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCompressorOnCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetCompressorOn(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCompressorOn(int index, [MarshalAs(UnmanagedType.I4)] bool compressorOn);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorConfigTypeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCompressorConfigTypeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorConfigTypeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCompressorConfigTypeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorConfigType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RevPHCompressorConfigType GetCompressorConfigType(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorConfigType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCompressorConfigType(int index, RevPHCompressorConfigType configType);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterPressureSwitchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelPressureSwitchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetPressureSwitch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetPressureSwitch(int index, [MarshalAs(UnmanagedType.I4)] bool pressureSwitch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterCompressorCurrentCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelCompressorCurrentCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetCompressorCurrent(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCompressorCurrent(int index, double compressorCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetAllSolenoids(int index, Span<byte> values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAllSolenoids(int index, byte values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHAllNonSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllNonSolenoidCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHAllSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllSolenoidCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
