using System;

namespace WPIHal.Natives;

public static unsafe partial class HalCAN
{
    public static void GetCANStatus(out float percentBusUtilization, out uint busOffCount, out uint txFullCount, out uint receiveErrorCount, out uint transmitErrorCount, out HalStatus status)
    {
        status = HalStatus.Ok;
        GetCANStatusRefShim(out percentBusUtilization, out busOffCount, out txFullCount, out receiveErrorCount, out transmitErrorCount, ref status);
    }
    public static void OpenStreamSession(out uint sessionHandle, uint messageID, uint messageIDMask, uint maxMessages, out HalStatus status)
    {
        status = HalStatus.Ok;
        OpenStreamSessionRefShim(out sessionHandle, messageID, messageIDMask, maxMessages, ref status);
    }
    internal static void ReadStreamSession(uint sessionHandle, Span<CANStreamMessage> messages, uint messagesToRead, out uint messagesRead, out HalStatus status)
    {
        status = HalStatus.Ok;
        ReadStreamSessionRefShim(sessionHandle, messages, messagesToRead, out messagesRead, ref status);
    }
    internal static void ReceiveMessage(ref uint messageID, uint messageIDMask, Span<byte> data, out byte dataSize, out uint timeStamp, out HalStatus status)
    {
        status = HalStatus.Ok;
        ReceiveMessageRefShim(ref messageID, messageIDMask, data, out dataSize, out timeStamp, ref status);
    }
    internal static void SendMessage(uint messageID, ReadOnlySpan<byte> data, byte dataSize, int periodMs, out HalStatus status)
    {
        status = HalStatus.Ok;
        SendMessageRefShim(messageID, data, dataSize, periodMs, ref status);
    }
}
