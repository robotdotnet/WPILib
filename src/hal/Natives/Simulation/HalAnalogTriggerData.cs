using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public enum HalSimAnalogTriggerMode : int
{
    Unassigned,
    Filtered,
    DutyCycle,
    Averaged
}

public static unsafe partial class HalAnalogTriggerData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindAnalogTriggerForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAnalogTriggerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterTriggerLowerBoundCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerLowerBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelTriggerLowerBoundCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerLowerBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTriggerLowerBound(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerLowerBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTriggerLowerBound(int index, double triggerLowerBound);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterTriggerUpperBoundCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerUpperBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelTriggerUpperBoundCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerUpperBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTriggerUpperBound(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerUpperBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTriggerUpperBound(int index, double triggerUpperBound);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerModeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterTriggerModeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerModeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelTriggerModeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimAnalogTriggerMode GetTriggerMode(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTriggerMode(int index, HalSimAnalogTriggerMode triggerMode);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
