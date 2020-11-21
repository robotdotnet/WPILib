
using Hal.Natives;
using WPIUtil.ILGeneration;

namespace Hal
{

    public static unsafe class CANLowLevel
    {
        internal static CANLowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void CloseStreamSession(uint sessionHandle)
        {
            lowLevel.HAL_CAN_CloseStreamSession(sessionHandle);
        }

        public static void GetStatus(float* percentBusUtilization, uint* busOffCount, uint* txFullCount, uint* receiveErrorCount, uint* transmitErrorCount)
        {
            lowLevel.HAL_CAN_GetCANStatus(percentBusUtilization, busOffCount, txFullCount, receiveErrorCount, transmitErrorCount);
        }

        public static void OpenStreamSession(uint* sessionHandle, uint messageID, uint messageIDMask, uint maxMessages)
        {
            lowLevel.HAL_CAN_OpenStreamSession(sessionHandle, messageID, messageIDMask, maxMessages);
        }

        public static void ReadStreamSession(uint sessionHandle, CANStreamMessage* messages, uint messagesToRead, uint* messagesRead)
        {
            lowLevel.HAL_CAN_ReadStreamSession(sessionHandle, messages, messagesToRead, messagesRead);
        }

        public static void ReceiveMessage(uint* messageID, uint messageIDMask, byte* data, byte* dataSize, uint* timeStamp)
        {
            lowLevel.HAL_CAN_ReceiveMessage(messageID, messageIDMask, data, dataSize, timeStamp);
        }

        public static void SendMessage(uint messageID, byte* data, byte dataSize, int periodMs)
        {
            lowLevel.HAL_CAN_SendMessage(messageID, data, dataSize, periodMs);
        }

    }
}
