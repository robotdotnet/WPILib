using WPIUtil.ILGeneration;

namespace Hal.Natives
{
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ICAN
    {
        void HAL_CAN_CloseStreamSession(uint sessionHandle);

        [StatusCheckLastParameter] void HAL_CAN_GetCANStatus(float* percentBusUtilization, uint* busOffCount, uint* txFullCount, uint* receiveErrorCount, uint* transmitErrorCount);

        [StatusCheckLastParameter] void HAL_CAN_OpenStreamSession(uint* sessionHandle, uint messageID, uint messageIDMask, uint maxMessages);

        [StatusCheckLastParameter] void HAL_CAN_ReadStreamSession(uint sessionHandle, CANStreamMessage* messages, uint messagesToRead, uint* messagesRead);

        [StatusCheckLastParameter] void HAL_CAN_ReceiveMessage(uint* messageID, uint messageIDMask, byte* data, byte* dataSize, uint* timeStamp);

        [StatusCheckLastParameter] void HAL_CAN_SendMessage(uint messageID, byte* data, byte dataSize, int periodMs);

    }
}
