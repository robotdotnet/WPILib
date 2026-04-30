using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalCANAPI
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetCANPacketBaseTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetCANPacketBaseTime(HalCANHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanCAN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CleanCAN(HalCANHandle handle);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeCAN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalCANHandle InitializeCAN(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketLatest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReadCANPacketLatest(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static CANMessage ReadCANPacketLatest(HalCANHandle handle, int apiId, out HalStatus status)
    {
        Span<byte> data = stackalloc byte[8];
        ReadCANPacketLatest(handle, apiId, data, out var dataLen, out var timestamp, out status);
        return new(data[..dataLen], timestamp);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketNew")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReadCANPacketNew(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static CANMessage ReadCANPacketNew(HalCANHandle handle, int apiId, out HalStatus status)
    {
        Span<byte> data = stackalloc byte[8];
        ReadCANPacketNew(handle, apiId, data, out var dataLen, out var timestamp, out status);
        return new(data[..dataLen], timestamp);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReadCANPacketTimeout(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, int timeoutMs, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static CANMessage ReadCANPacketTimeout(HalCANHandle handle, int apiId, int timeoutMs, out HalStatus status)
    {
        Span<byte> data = stackalloc byte[8];
        ReadCANPacketTimeout(handle, apiId, data, out var dataLen, out var timestamp, timeoutMs, out status);
        return new(data[..dataLen], timestamp);
    }

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_StopCANPacketRepeating")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopCANPacketRepeating(HalCANHandle handle, int apiId, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANPacket")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WriteCANPacket(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static void WriteCANPacket(HalCANHandle handle, ReadOnlySpan<byte> data, int apiId, out HalStatus status)
    {
        WriteCANPacket(handle, data, data.Length, apiId, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANPacketRepeating")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WriteCANPacketRepeating(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, int repeatMs, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    public static void WriteCANPacketRepeating(HalCANHandle handle, ReadOnlySpan<byte> data, int apiId, int repeatMs, out HalStatus status)
    {
        WriteCANPacketRepeating(handle, data, data.Length, apiId, repeatMs, out status);
    }

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANRTRFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WriteCANRTRFrame(HalCANHandle handle, int length, int apiId, out HalStatus status);
}
