using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_ConstBufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAddressableLEDData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindAddressableLEDForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindAddressableLEDForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAddressableLEDData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAddressableLEDInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAddressableLEDInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAddressableLEDInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDOutputPortCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAddressableLEDOutputPortCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDOutputPortCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAddressableLEDOutputPortCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAddressableLEDOutputPort(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDOutputPort(int index, int outputPort);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAddressableLEDLengthCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAddressableLEDLengthCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAddressableLEDLength(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDLength(int index, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDRunningCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAddressableLEDRunningCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDRunningCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAddressableLEDRunningCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDRunning")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetAddressableLEDRunning(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDRunning")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDRunning(int index, [MarshalAs(UnmanagedType.I4)] bool running);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterAddressableLEDDataCallback(int index, HAL_ConstBufferCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelAddressableLEDDataCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetAddressableLEDData(int index, HalAddressableLED.LedData* data);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDData(int index, HalAddressableLED.LedData* data, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAddressableLEDAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
