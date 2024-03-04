using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalRelayData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetRelayData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetRelayData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayInitializedForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRelayInitializedForwardCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayInitializedForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRelayInitializedForwardCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayInitializedForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRelayInitializedForward(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayInitializedForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRelayInitializedForward(int index, [MarshalAs(UnmanagedType.I4)] bool initializedForward);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayInitializedReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRelayInitializedReverseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayInitializedReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRelayInitializedReverseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayInitializedReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRelayInitializedReverse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayInitializedReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRelayInitializedReverse(int index, [MarshalAs(UnmanagedType.I4)] bool initializedReverse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRelayForwardCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayForwardCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRelayForwardCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRelayForward(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayForward")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRelayForward(int index, [MarshalAs(UnmanagedType.I4)] bool forward);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRelayReverseCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelRelayReverseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRelayReverseCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetRelayReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRelayReverse(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetRelayReverse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRelayReverse(int index, [MarshalAs(UnmanagedType.I4)] bool reverse);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterRelayAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterRelayAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
