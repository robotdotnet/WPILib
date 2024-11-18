using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalRelayData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetRelayData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayInitializedForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedForwardCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayInitializedForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedForwardCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayInitializedForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitializedForward(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayInitializedForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitializedForward(int index, [MarshalAs(UnmanagedType.I4)] bool initializedForward);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayInitializedReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedReverseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayInitializedReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedReverseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayInitializedReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitializedReverse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayInitializedReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitializedReverse(int index, [MarshalAs(UnmanagedType.I4)] bool initializedReverse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterForwardCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelForwardCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetForward(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetForward(int index, [MarshalAs(UnmanagedType.I4)] bool forward);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterReverseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelReverseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetReverse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetReverse(int index, [MarshalAs(UnmanagedType.I4)] bool reverse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
