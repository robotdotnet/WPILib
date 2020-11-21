using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class CANLowLevelNative : ICAN
{
[NativeFunctionPointer("HAL_CAN_CloseStreamSession")]
private readonly delegate* unmanaged[Cdecl]<uint, void> HAL_CAN_CloseStreamSessionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_CloseStreamSession(uint sessionHandle)
{
HAL_CAN_CloseStreamSessionFunc(sessionHandle);
}


[NativeFunctionPointer("HAL_CAN_GetCANStatus")]
private readonly delegate* unmanaged[Cdecl]<float*, uint*, uint*, uint*, uint*, int*, void> HAL_CAN_GetCANStatusFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_GetCANStatus(float* percentBusUtilization, uint* busOffCount, uint* txFullCount, uint* receiveErrorCount, uint* transmitErrorCount)
{
int status = 0;
HAL_CAN_GetCANStatusFunc(percentBusUtilization, busOffCount, txFullCount, receiveErrorCount, transmitErrorCount, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_CAN_OpenStreamSession")]
private readonly delegate* unmanaged[Cdecl]<uint*, uint, uint, uint, int*, void> HAL_CAN_OpenStreamSessionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_OpenStreamSession(uint* sessionHandle, uint messageID, uint messageIDMask, uint maxMessages)
{
int status = 0;
HAL_CAN_OpenStreamSessionFunc(sessionHandle, messageID, messageIDMask, maxMessages, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_CAN_ReadStreamSession")]
private readonly delegate* unmanaged[Cdecl]<uint, CANStreamMessage*, uint, uint*, int*, void> HAL_CAN_ReadStreamSessionFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_ReadStreamSession(uint sessionHandle, CANStreamMessage* messages, uint messagesToRead, uint* messagesRead)
{
int status = 0;
HAL_CAN_ReadStreamSessionFunc(sessionHandle, messages, messagesToRead, messagesRead, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_CAN_ReceiveMessage")]
private readonly delegate* unmanaged[Cdecl]<uint*, uint, byte*, byte*, uint*, int*, void> HAL_CAN_ReceiveMessageFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_ReceiveMessage(uint* messageID, uint messageIDMask, byte* data, byte* dataSize, uint* timeStamp)
{
int status = 0;
HAL_CAN_ReceiveMessageFunc(messageID, messageIDMask, data, dataSize, timeStamp, &status);
Hal.StatusHandling.StatusCheck(status);
}


[NativeFunctionPointer("HAL_CAN_SendMessage")]
private readonly delegate* unmanaged[Cdecl]<uint, byte*, byte, int, int*, void> HAL_CAN_SendMessageFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CAN_SendMessage(uint messageID, byte* data, byte dataSize, int periodMs)
{
int status = 0;
HAL_CAN_SendMessageFunc(messageID, data, dataSize, periodMs, &status);
Hal.StatusHandling.StatusCheck(status);
}



}
}
