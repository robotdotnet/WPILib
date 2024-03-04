using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
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
    public static partial void ResetREVPHData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetREVPHInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool solenoidInitialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHSolenoidOutputCallback(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHSolenoidOutputCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHSolenoidOutputCallback(int index, int channel, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetREVPHSolenoidOutput(int index, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHSolenoidOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHSolenoidOutput(int index, int channel, [MarshalAs(UnmanagedType.I4)] bool solenoidOutput);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHCompressorOnCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorOnCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHCompressorOnCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetREVPHCompressorOn(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorOn")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHCompressorOn(int index, [MarshalAs(UnmanagedType.I4)] bool compressorOn);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorConfigTypeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHCompressorConfigTypeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorConfigTypeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHCompressorConfigTypeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorConfigType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RevPHCompressorConfigType GetREVPHCompressorConfigType(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorConfigType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHCompressorConfigType(int index, RevPHCompressorConfigType configType);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHPressureSwitchCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHPressureSwitchCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHPressureSwitchCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetREVPHPressureSwitch(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHPressureSwitch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHPressureSwitch(int index, [MarshalAs(UnmanagedType.I4)] bool pressureSwitch);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterREVPHCompressorCurrentCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelREVPHCompressorCurrentCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelREVPHCompressorCurrentCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetREVPHCompressorCurrent(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHCompressorCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHCompressorCurrent(int index, double compressorCurrent);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetREVPHAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetREVPHAllSolenoids(int index, Span<byte> values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetREVPHAllSolenoids")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetREVPHAllSolenoids(int index, byte values);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHAllNonSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterREVPHAllNonSolenoidCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterREVPHAllSolenoidCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterREVPHAllSolenoidCallbacks(int index, int channel, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
