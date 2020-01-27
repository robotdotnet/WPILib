
using Hal.Natives;
using System;
using WPIUtil.NativeUtilities;

namespace Hal
{
    [NativeInterface(typeof(ISPI))]
    public unsafe static class SPI
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
#pragma warning disable CS0649 // Field is never assigned to
#pragma warning disable IDE0044 // Add readonly modifier
        private static ISPI lowLevel;
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore CS0649 // Field is never assigned to
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

public static void CloseSPI(SPIPort port)
{
lowLevel.HAL_CloseSPI(port);
}

public static void ConfigureSPIAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead)
{
lowLevel.HAL_ConfigureSPIAutoStall(port, csToSclkTicks, stallTicks, pow2BytesPerRead);
}

public static void ForceSPIAutoRead(SPIPort port)
{
lowLevel.HAL_ForceSPIAutoRead(port);
}

public static void FreeSPIAuto(SPIPort port)
{
lowLevel.HAL_FreeSPIAuto(port);
}

public static int GetSPIAutoDroppedCount(SPIPort port)
{
return lowLevel.HAL_GetSPIAutoDroppedCount(port);
}

public static int GetSPIHandle(SPIPort port)
{
return lowLevel.HAL_GetSPIHandle(port);
}

public static void InitSPIAuto(SPIPort port, int bufferSize)
{
lowLevel.HAL_InitSPIAuto(port, bufferSize);
}

public static void InitializeSPI(SPIPort port)
{
lowLevel.HAL_InitializeSPI(port);
}

public static int ReadSPI(SPIPort port, byte* buffer, int count)
{
return lowLevel.HAL_ReadSPI(port, buffer, count);
}

public static int ReadSPIAutoReceivedData(SPIPort port, uint* buffer, int numToRead, double timeout)
{
return lowLevel.HAL_ReadSPIAutoReceivedData(port, buffer, numToRead, timeout);
}

public static void SetSPIAutoTransmitData(SPIPort port, byte* dataToSend, int dataSize, int zeroSize)
{
lowLevel.HAL_SetSPIAutoTransmitData(port, dataToSend, dataSize, zeroSize);
}

public static void SetSPIChipSelectActiveHigh(SPIPort port)
{
lowLevel.HAL_SetSPIChipSelectActiveHigh(port);
}

public static void SetSPIChipSelectActiveLow(SPIPort port)
{
lowLevel.HAL_SetSPIChipSelectActiveLow(port);
}

public static void SetSPIHandle(SPIPort port, int handle)
{
lowLevel.HAL_SetSPIHandle(port, handle);
}

public static void SetSPIOpts(SPIPort port, int msbFirst, int sampleOnTrailing, int clkIdleHigh)
{
lowLevel.HAL_SetSPIOpts(port, msbFirst, sampleOnTrailing, clkIdleHigh);
}

public static void SetSPISpeed(SPIPort port, int speed)
{
lowLevel.HAL_SetSPISpeed(port, speed);
}

public static void StartSPIAutoRate(SPIPort port, double period)
{
lowLevel.HAL_StartSPIAutoRate(port, period);
}

public static void StartSPIAutoTrigger(SPIPort port, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling)
{
lowLevel.HAL_StartSPIAutoTrigger(port, digitalSourceHandle, analogTriggerType, triggerRising, triggerFalling);
}

public static void StopSPIAuto(SPIPort port)
{
lowLevel.HAL_StopSPIAuto(port);
}

public static int TransactionSPI(SPIPort port, byte* dataToSend, byte* dataReceived, int size)
{
return lowLevel.HAL_TransactionSPI(port, dataToSend, dataReceived, size);
}

public static int WriteSPI(SPIPort port, byte* dataToSend, int sendSize)
{
return lowLevel.HAL_WriteSPI(port, dataToSend, sendSize);
}

}
}
