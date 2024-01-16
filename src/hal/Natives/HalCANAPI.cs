using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalCANAPI
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanCAN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CleanCAN(HalCANHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeCAN")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalCANHandle InitializeCANRefShim(CANManufacturer manufacturer, int deviceId, CANDeviceType deviceType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketLatest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReadCANPacketLatestRefShim(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, ref HalStatus status);

    public static Span<byte> ReadCANPacketLatest(HalCANHandle handle, int apiId, Span<byte> data, out ulong receivedTimestamp, out HalStatus status)
    {
        ReadCANPacketLatest(handle, apiId, data, out var dataLen, out receivedTimestamp, out status);
        return data[..dataLen];
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketNew")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReadCANPacketNewRefShim(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, ref HalStatus status);

    public static Span<byte> ReadCANPacketNew(HalCANHandle handle, int apiId, Span<byte> data, out ulong receivedTimestamp, out HalStatus status)
    {
        ReadCANPacketNew(handle, apiId, data, out var dataLen, out receivedTimestamp, out status);
        return data[..dataLen];
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadCANPacketTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReadCANPacketTimeoutRefShim(HalCANHandle handle, int apiId, Span<byte> data, out int length, out ulong receivedTimestamp, int timeoutMs, ref HalStatus status);

    public static Span<byte> ReadCANPacketTimeout(HalCANHandle handle, int apiId, Span<byte> data, out ulong receivedTimestamp, int timeoutMs, out HalStatus status)
    {
        ReadCANPacketTimeout(handle, apiId, data, out var dataLen, out receivedTimestamp, timeoutMs, out status);
        return data[..dataLen];
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopCANPacketRepeating")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StopCANPacketRepeatingRefShim(HalCANHandle handle, int apiId, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANPacket")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WriteCANPacketRefShim(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, ref HalStatus status);

    public static void WriteCANPacket(HalCANHandle handle, ReadOnlySpan<byte> data, int apiId, out HalStatus status)
    {
        WriteCANPacket(handle, data, data.Length, apiId, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANPacketRepeating")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WriteCANPacketRepeatingRefShim(HalCANHandle handle, ReadOnlySpan<byte> data, int length, int apiId, int repeatMs, ref HalStatus status);

    public static void WriteCANPacketRepeating(HalCANHandle handle, ReadOnlySpan<byte> data, int apiId, int repeatMs, out HalStatus status)
    {
        WriteCANPacketRepeating(handle, data, data.Length, apiId, repeatMs, out status);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteCANRTRFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WriteCANRTRFrameRefShim(HalCANHandle handle, int length, int apiId, ref HalStatus status);


}
