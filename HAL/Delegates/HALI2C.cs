using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALI2C
    {
        static HALI2C()
        {
            HAL.Initialize();
        }

        public delegate void HAL_InitializeI2CDelegate(int port, ref int status);
        public static HAL_InitializeI2CDelegate HAL_InitializeI2C;

        public delegate int HAL_TransactionI2CDelegate(int port, int deviceAddress, byte[] dataToSend, int sendSize, byte[] dataReceived, int receiveSize);
        public static HAL_TransactionI2CDelegate HAL_TransactionI2C;

        public delegate int HAL_WriteI2CDelegate(int port, int deviceAddress, byte[] dataToSend, int sendSize);
        public static HAL_WriteI2CDelegate HAL_WriteI2C;

        public delegate int HAL_ReadI2CDelegate(int port, int deviceAddress, byte[] buffer, int count);
        public static HAL_ReadI2CDelegate HAL_ReadI2C;

        public delegate void HAL_CloseI2CDelegate(int port);
        public static HAL_CloseI2CDelegate HAL_CloseI2C;
    }
}

