using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALI2C
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALI2C.HAL_InitializeI2C = HAL_InitializeI2C;
            Base.HALI2C.HAL_TransactionI2C = HAL_TransactionI2C;
            Base.HALI2C.HAL_WriteI2C = HAL_WriteI2C;
            Base.HALI2C.HAL_ReadI2C = HAL_ReadI2C;
            Base.HALI2C.HAL_CloseI2C = HAL_CloseI2C;
        }

        public static void HAL_InitializeI2C(int port, ref int status)
        {
        }

        public static int HAL_TransactionI2C(int port, int deviceAddress, byte[] dataToSend, int sendSize, byte[] dataReceived, int receiveSize)
        {
            throw new NotImplementedException();
        }

        public static int HAL_WriteI2C(int port, int deviceAddress, byte[] dataToSend, int sendSize)
        {
            throw new NotImplementedException();
        }

        public static int HAL_ReadI2C(int port, int deviceAddress, byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }

        public static void HAL_CloseI2C(int port)
        {
        }
    }
}

