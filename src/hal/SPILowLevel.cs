
using Hal.Natives;
using WPIUtil.ILGeneration;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class SPILowLevel
    {
        internal static SPILowLevelNative lowLevel = null!;

        internal static void InitializeNatives(IFunctionPointerLoader loader)
        {
            lowLevel = new(loader);
        }

        public static void Close(SPIPort port)
        {
            lowLevel.HAL_CloseSPI(port);
        }

        public static void ConfigureAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead)
        {
            lowLevel.HAL_ConfigureSPIAutoStall(port, csToSclkTicks, stallTicks, pow2BytesPerRead);
        }

        public static void ForceAutoRead(SPIPort port)
        {
            lowLevel.HAL_ForceSPIAutoRead(port);
        }

        public static void FreeAuto(SPIPort port)
        {
            lowLevel.HAL_FreeSPIAuto(port);
        }

        public static int GetAutoDroppedCount(SPIPort port)
        {
            return lowLevel.HAL_GetSPIAutoDroppedCount(port);
        }

        public static int GetHandle(SPIPort port)
        {
            return lowLevel.HAL_GetSPIHandle(port);
        }

        public static void InitAuto(SPIPort port, int bufferSize)
        {
            lowLevel.HAL_InitSPIAuto(port, bufferSize);
        }

        public static void Initialize(SPIPort port)
        {
            lowLevel.HAL_InitializeSPI(port);
        }

        public static int Read(SPIPort port, byte* buffer, int count)
        {
            return lowLevel.HAL_ReadSPI(port, buffer, count);
        }

        public static int ReadAutoReceivedData(SPIPort port, uint* buffer, int numToRead, double timeout)
        {
            return lowLevel.HAL_ReadSPIAutoReceivedData(port, buffer, numToRead, timeout);
        }

        public static void SetAutoTransmitData(SPIPort port, byte* dataToSend, int dataSize, int zeroSize)
        {
            lowLevel.HAL_SetSPIAutoTransmitData(port, dataToSend, dataSize, zeroSize);
        }

        public static void SetChipSelectActiveHigh(SPIPort port)
        {
            lowLevel.HAL_SetSPIChipSelectActiveHigh(port);
        }

        public static void SetChipSelectActiveLow(SPIPort port)
        {
            lowLevel.HAL_SetSPIChipSelectActiveLow(port);
        }

        public static void SetHandle(SPIPort port, int handle)
        {
            lowLevel.HAL_SetSPIHandle(port, handle);
        }

        public static void SetOpts(SPIPort port, int msbFirst, int sampleOnTrailing, int clkIdleHigh)
        {
            lowLevel.HAL_SetSPIOpts(port, msbFirst, sampleOnTrailing, clkIdleHigh);
        }

        public static void SetSpeed(SPIPort port, int speed)
        {
            lowLevel.HAL_SetSPISpeed(port, speed);
        }

        public static void StartAutoRate(SPIPort port, double period)
        {
            lowLevel.HAL_StartSPIAutoRate(port, period);
        }

        public static void StartAutoTrigger(SPIPort port, int digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling)
        {
            lowLevel.HAL_StartSPIAutoTrigger(port, digitalSourceHandle, analogTriggerType, triggerRising, triggerFalling);
        }

        public static void StopAuto(SPIPort port)
        {
            lowLevel.HAL_StopSPIAuto(port);
        }

        public static int Transaction(SPIPort port, byte* dataToSend, byte* dataReceived, int size)
        {
            return lowLevel.HAL_TransactionSPI(port, dataToSend, dataReceived, size);
        }

        public static int Write(SPIPort port, byte* dataToSend, int sendSize)
        {
            return lowLevel.HAL_WriteSPI(port, dataToSend, sendSize);
        }

    }
}
