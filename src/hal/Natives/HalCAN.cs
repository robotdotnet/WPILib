﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalCAN
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_CloseStreamSession")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseStreamSession(uint sessionHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_GetCANStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void GetCANStatusRefShim(out float percentBusUtilization, out uint busOffCount, out uint txFullCount, out uint receiveErrorCount, out uint transmitErrorCount, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_OpenStreamSession")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void OpenStreamSessionRefShim(out uint sessionHandle, uint messageID, uint messageIDMask, uint maxMessages, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_ReadStreamSession")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReadStreamSessionRefShim(uint sessionHandle, Span<CANStreamMessage> messages, uint messagesToRead, out uint messagesRead, ref HalStatus status);

    public static Span<CANStreamMessage> ReadStreamSession(uint sessionHandle, Span<CANStreamMessage> messages, out HalStatus status)
    {
        ReadStreamSession(sessionHandle, messages, (uint)messages.Length, out var messagesRead, out status);
        return messages[..(int)messagesRead];
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_ReceiveMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ReceiveMessageRefShim(ref uint messageID, uint messageIDMask, Span<byte> data, out byte dataSize, out uint timeStamp, ref HalStatus status);

    public static Span<byte> ReceiveMessage(ref uint messageID, uint messageIDMask, Span<byte> data, out uint timeStamp, out HalStatus status)
    {
        ReceiveMessage(ref messageID, messageIDMask, data, out var dataLen, out timeStamp, out status);
        return data[..dataLen];
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_CAN_SendMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SendMessageRefShim(uint messageID, ReadOnlySpan<byte> data, byte dataSize, int periodMs, ref HalStatus status);

    public static void SendMessage(uint messageID, ReadOnlySpan<byte> data, int periodMs, out HalStatus status)
    {
        SendMessage(messageID, data, checked((byte)data.Length), periodMs, out status);
    }




}