
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ICAN))]
    public unsafe static class CAN
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ICAN lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void CAN_CloseStreamSession(uint sessionHandle)
{
lowLevel.HAL_CAN_CloseStreamSession(sessionHandle);
}

public static void CAN_GetCANStatus(float* percentBusUtilization, uint* busOffCount, uint* txFullCount, uint* receiveErrorCount, uint* transmitErrorCount)
{
lowLevel.HAL_CAN_GetCANStatus(percentBusUtilization, busOffCount, txFullCount, receiveErrorCount, transmitErrorCount);
}

public static void CAN_OpenStreamSession(uint* sessionHandle, uint messageID, uint messageIDMask, uint maxMessages)
{
lowLevel.HAL_CAN_OpenStreamSession(sessionHandle, messageID, messageIDMask, maxMessages);
}

public static void CAN_ReadStreamSession(uint sessionHandle, CANStreamMessage* messages, uint messagesToRead, uint* messagesRead)
{
lowLevel.HAL_CAN_ReadStreamSession(sessionHandle, messages, messagesToRead, messagesRead);
}

public static void CAN_ReceiveMessage(uint* messageID, uint messageIDMask, byte* data, byte* dataSize, uint* timeStamp)
{
lowLevel.HAL_CAN_ReceiveMessage(messageID, messageIDMask, data, dataSize, timeStamp);
}

public static void CAN_SendMessage(uint messageID, byte* data, byte dataSize, int periodMs)
{
lowLevel.HAL_CAN_SendMessage(messageID, data, dataSize, periodMs);
}

}
}
