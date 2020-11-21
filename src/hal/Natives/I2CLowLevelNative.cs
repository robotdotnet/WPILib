using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
namespace Hal.Natives
{
public unsafe class I2CLowLevelNative : II2C
{
[NativeFunctionPointer("HAL_CloseI2C")]
private readonly delegate* unmanaged[Cdecl]<I2CPort, void> HAL_CloseI2CFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_CloseI2C(I2CPort port)
{
HAL_CloseI2CFunc(port);
}


[NativeFunctionPointer("HAL_InitializeI2C")]
private readonly delegate* unmanaged[Cdecl]<I2CPort, int*, void> HAL_InitializeI2CFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public void HAL_InitializeI2C(I2CPort port)
{
int status = 0;
HAL_InitializeI2CFunc(port, &status);
            Hal.StatusHandling.I2CStatusCheck(status, port);
}


[NativeFunctionPointer("HAL_ReadI2C")]
private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, int> HAL_ReadI2CFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_ReadI2C(I2CPort port, int deviceAddress, byte* buffer, int count)
{
return HAL_ReadI2CFunc(port, deviceAddress, buffer, count);
}


[NativeFunctionPointer("HAL_TransactionI2C")]
private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, byte*, int, int> HAL_TransactionI2CFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_TransactionI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize, byte* dataReceived, int receiveSize)
{
return HAL_TransactionI2CFunc(port, deviceAddress, dataToSend, sendSize, dataReceived, receiveSize);
}


[NativeFunctionPointer("HAL_WriteI2C")]
private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, int> HAL_WriteI2CFunc;

[MethodImpl(MethodImplOptions.AggressiveInlining)]
public int HAL_WriteI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize)
{
return HAL_WriteI2CFunc(port, deviceAddress, dataToSend, sendSize);
}



}
}
