
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(II2C))]
    public unsafe static class I2C
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static II2C lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void Close(I2CPort port)
{
lowLevel.HAL_CloseI2C(port);
}

public static void Initialize(I2CPort port)
{
lowLevel.HAL_InitializeI2C(port);
}

public static int Read(I2CPort port, int deviceAddress, byte* buffer, int count)
{
return lowLevel.HAL_ReadI2C(port, deviceAddress, buffer, count);
}

public static int Transaction(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize, byte* dataReceived, int receiveSize)
{
return lowLevel.HAL_TransactionI2C(port, deviceAddress, dataToSend, sendSize, dataReceived, receiveSize);
}

public static int Write(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize)
{
return lowLevel.HAL_WriteI2C(port, deviceAddress, dataToSend, sendSize);
}

}
}
