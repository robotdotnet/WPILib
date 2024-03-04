using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_ConstBufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;
using unsafe HAL_SpiReadAutoReceiveBufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, uint*, int, uint*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalSPIData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetSPIData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSPIData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetSPIInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSPIInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetSPIInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIReadCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIReadCallback(int index, HAL_BufferCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIReadCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIReadCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIWriteCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIWriteCallback(int index, HAL_ConstBufferCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIWriteCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIWriteCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterSPIReadAutoReceivedDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterSPIReadAutoReceivedDataCallback(int index, HAL_SpiReadAutoReceiveBufferCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelSPIReadAutoReceivedDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelSPIReadAutoReceivedDataCallback(int index, int uid);

}
