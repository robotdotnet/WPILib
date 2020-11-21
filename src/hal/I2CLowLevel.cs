
using Hal.Natives;
using WPIUtil.NativeUtilities;

namespace Hal
{

    public static unsafe class I2CLowLevel
    {
#pragma warning disable CS0649 // Field is never assigned to
        internal static I2CLowLevelNative lowLevel = null!;
#pragma warning restore CS0649 // Field is never assigned to

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
