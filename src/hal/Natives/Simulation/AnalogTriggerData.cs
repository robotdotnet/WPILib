using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
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
    public static partial int FindAnalogTriggerForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAnalogTriggerData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAnalogTriggerData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogTriggerInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogTriggerInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAnalogTriggerInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogTriggerInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerLowerBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogTriggerTriggerLowerBoundCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerLowerBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogTriggerTriggerLowerBoundCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerLowerBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogTriggerTriggerLowerBound(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerLowerBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogTriggerTriggerLowerBound(int index, double triggerLowerBound);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerUpperBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogTriggerTriggerUpperBoundCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerUpperBoundCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogTriggerTriggerUpperBoundCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerUpperBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogTriggerTriggerUpperBound(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerUpperBound")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogTriggerTriggerUpperBound(int index, double triggerUpperBound);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerTriggerModeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAnalogTriggerTriggerModeCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAnalogTriggerTriggerModeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAnalogTriggerTriggerModeCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAnalogTriggerTriggerMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimAnalogTriggerMode GetAnalogTriggerTriggerMode(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAnalogTriggerTriggerMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogTriggerTriggerMode(int index, HalSimAnalogTriggerMode triggerMode);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAnalogTriggerAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAnalogTriggerAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
