using System;
using System.Runtime.InteropServices;
using HAL.Base;

// ReSharper disable CheckNamespace
namespace HAL.SimulatorHAL
{
    internal class HALSPI
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSPI.HAL_InitializeSPI = HAL_InitializeSPI;
            Base.HALSPI.HAL_TransactionSPI = HAL_TransactionSPI;
            Base.HALSPI.HAL_WriteSPI = HAL_WriteSPI;
            Base.HALSPI.HAL_ReadSPI = HAL_ReadSPI;
            Base.HALSPI.HAL_CloseSPI = HAL_CloseSPI;
            Base.HALSPI.HAL_SetSPISpeed = HAL_SetSPISpeed;
            Base.HALSPI.HAL_SetSPIOpts = HAL_SetSPIOpts;
            Base.HALSPI.HAL_SetSPIChipSelectActiveHigh = HAL_SetSPIChipSelectActiveHigh;
            Base.HALSPI.HAL_SetSPIChipSelectActiveLow = HAL_SetSPIChipSelectActiveLow;
            Base.HALSPI.HAL_GetSPIHandle = HAL_GetSPIHandle;
            Base.HALSPI.HAL_SetSPIHandle = HAL_SetSPIHandle;
            Base.HALSPI.HAL_InitSPIAccumulator = HAL_InitSPIAccumulator;
            Base.HALSPI.HAL_FreeSPIAccumulator = HAL_FreeSPIAccumulator;
            Base.HALSPI.HAL_ResetSPIAccumulator = HAL_ResetSPIAccumulator;
            Base.HALSPI.HAL_SetSPIAccumulatorCenter = HAL_SetSPIAccumulatorCenter;
            Base.HALSPI.HAL_SetSPIAccumulatorDeadband = HAL_SetSPIAccumulatorDeadband;
            Base.HALSPI.HAL_GetSPIAccumulatorLastValue = HAL_GetSPIAccumulatorLastValue;
            Base.HALSPI.HAL_GetSPIAccumulatorValue = HAL_GetSPIAccumulatorValue;
            Base.HALSPI.HAL_GetSPIAccumulatorCount = HAL_GetSPIAccumulatorCount;
            Base.HALSPI.HAL_GetSPIAccumulatorAverage = HAL_GetSPIAccumulatorAverage;
            Base.HALSPI.HAL_GetSPIAccumulatorOutput = HAL_GetSPIAccumulatorOutput;
        }

        public static void HAL_InitializeSPI(int port, ref int status)
        {
        }

        public static int HAL_TransactionSPI(int port, byte[] dataToSend, byte[] dataReceived, int size)
        {
            throw new NotImplementedException();
        }

        public static int HAL_WriteSPI(int port, byte[] dataToSend, int sendSize)
        {
            throw new NotImplementedException();
        }

        public static int HAL_ReadSPI(int port, byte[] buffer, int count)
        {
            throw new NotImplementedException();
        }

        public static void HAL_CloseSPI(int port)
        {
        }

        public static void HAL_SetSPISpeed(int port, int speed)
        {
        }

        public static void HAL_SetSPIOpts(int port, bool msb_first, bool sample_on_trailing, bool clk_idle_high)
        {
        }

        public static void HAL_SetSPIChipSelectActiveHigh(int port, ref int status)
        {
        }

        public static void HAL_SetSPIChipSelectActiveLow(int port, ref int status)
        {
        }

        public static int HAL_GetSPIHandle(int port)
        {
            throw new NotImplementedException();
        }

        public static void HAL_SetSPIHandle(int port, int handle)
        {
        }

        public static void HAL_InitSPIAccumulator(int port, int period, int cmd, int xfer_size, int valid_mask, int valid_value, int data_shift, int data_size, bool is_signed, bool big_endian, ref int status)
        {
        }

        public static void HAL_FreeSPIAccumulator(int port, ref int status)
        {
        }

        public static void HAL_ResetSPIAccumulator(int port, ref int status)
        {
        }

        public static void HAL_SetSPIAccumulatorCenter(int port, int center, ref int status)
        {
        }

        public static void HAL_SetSPIAccumulatorDeadband(int port, int deadband, ref int status)
        {
        }

        public static int HAL_GetSPIAccumulatorLastValue(int port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static long HAL_GetSPIAccumulatorValue(int port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static long HAL_GetSPIAccumulatorCount(int port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static double HAL_GetSPIAccumulatorAverage(int port, ref int status)
        {
            throw new NotImplementedException();
        }

        public static void HAL_GetSPIAccumulatorOutput(int port, ref long value, ref long count, ref int status)
        {
        }
    }
}

