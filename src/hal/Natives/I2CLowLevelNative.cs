﻿using WPIUtil.ILGeneration;
using System.Runtime.CompilerServices;
using System;

namespace Hal.Natives
{
    public unsafe class I2CLowLevelNative
    {
        public I2CLowLevelNative(IFunctionPointerLoader loader)
        {
            if (loader == null)
            {
                throw new ArgumentNullException(nameof(loader));
            }

            HAL_CloseI2CFunc = (delegate* unmanaged[Cdecl] < Hal.I2CPort, void >)loader.GetProcAddress("HAL_CloseI2C");
            HAL_InitializeI2CFunc = (delegate* unmanaged[Cdecl] < Hal.I2CPort, int *, void >)loader.GetProcAddress("HAL_InitializeI2C");
            HAL_ReadI2CFunc = (delegate* unmanaged[Cdecl] < Hal.I2CPort, System.Int32, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_ReadI2C");
            HAL_TransactionI2CFunc = (delegate* unmanaged[Cdecl] < Hal.I2CPort, System.Int32, System.Byte *, System.Int32, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_TransactionI2C");
            HAL_WriteI2CFunc = (delegate* unmanaged[Cdecl] < Hal.I2CPort, System.Int32, System.Byte *, System.Int32, System.Int32 >)loader.GetProcAddress("HAL_WriteI2C");
        }

        private readonly delegate* unmanaged[Cdecl]<I2CPort, void> HAL_CloseI2CFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_CloseI2C(I2CPort port)
        {
            HAL_CloseI2CFunc(port);
        }



        private readonly delegate* unmanaged[Cdecl]<I2CPort, int*, void> HAL_InitializeI2CFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void HAL_InitializeI2C(I2CPort port)
        {
            int status = 0;
            HAL_InitializeI2CFunc(port, &status);
            Hal.StatusHandling.I2CStatusCheck(status, port);
        }



        private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, int> HAL_ReadI2CFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_ReadI2C(I2CPort port, int deviceAddress, byte* buffer, int count)
        {
            return HAL_ReadI2CFunc(port, deviceAddress, buffer, count);
        }



        private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, byte*, int, int> HAL_TransactionI2CFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_TransactionI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize, byte* dataReceived, int receiveSize)
        {
            return HAL_TransactionI2CFunc(port, deviceAddress, dataToSend, sendSize, dataReceived, receiveSize);
        }



        private readonly delegate* unmanaged[Cdecl]<I2CPort, int, byte*, int, int> HAL_WriteI2CFunc;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int HAL_WriteI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize)
        {
            return HAL_WriteI2CFunc(port, deviceAddress, dataToSend, sendSize);
        }



    }
}
