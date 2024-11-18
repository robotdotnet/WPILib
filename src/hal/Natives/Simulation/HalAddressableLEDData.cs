using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_ConstBufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalAddressableLEDData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_FindAddressableLEDForChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int FindForChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetData(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterInitializedCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDInitializedCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelInitializedCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetInitialized(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInitialized(int index, [MarshalAs(UnmanagedType.I4)] bool initialized);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDOutputPortCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterOutputPortCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDOutputPortCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelOutputPortCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetOutputPort(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetOutputPort(int index, int outputPort);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterLengthCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDLengthCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelLengthCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetLength(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetLength(int index, int length);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDRunningCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterRunningCallback(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDRunningCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelRunningCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDRunning")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRunning(int index);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDRunning")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRunning(int index, [MarshalAs(UnmanagedType.I4)] bool running);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int RegisterDataCallback(int index, HAL_ConstBufferCallback callback, void* param);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_CancelAddressableLEDDataCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CancelDataCallback(int index, int uid);

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetDataInternal(int index, Span<HalAddressableLED.LedData> data);

    public static ReadOnlySpan<HalAddressableLED.LedData> GetData(int index, Span<HalAddressableLED.LedData> data)
    {
        int length = GetDataInternal(index, data);
        return data[..length];
    }

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_SetAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetData(int index, ReadOnlySpan<HalAddressableLED.LedData> data, int length);

    public static void SetData(int index, ReadOnlySpan<HalAddressableLED.LedData> data)
    {
        SetData(index, data, data.Length);
    }

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_RegisterAddressableLEDAllCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RegisterAllCallbacks(int index, HAL_NotifyCallback callback, void* param, [MarshalAs(UnmanagedType.I4)] bool initialNotify);

}
