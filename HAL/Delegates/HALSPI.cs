using System;
using System.Runtime.InteropServices;
// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSPI
    {
        static HALSPI()
        {
            HAL.Initialize();
        }

        public delegate void HAL_InitializeSPIDelegate(int port, ref int status);
        public static HAL_InitializeSPIDelegate HAL_InitializeSPI;

        public delegate int HAL_TransactionSPIDelegate(int port, byte[] dataToSend, byte[] dataReceived, int size);
        public static HAL_TransactionSPIDelegate HAL_TransactionSPI;

        public delegate int HAL_WriteSPIDelegate(int port, byte[] dataToSend, int sendSize);
        public static HAL_WriteSPIDelegate HAL_WriteSPI;

        public delegate int HAL_ReadSPIDelegate(int port, byte[] buffer, int count);
        public static HAL_ReadSPIDelegate HAL_ReadSPI;

        public delegate void HAL_CloseSPIDelegate(int port);
        public static HAL_CloseSPIDelegate HAL_CloseSPI;

        public delegate void HAL_SetSPISpeedDelegate(int port, int speed);
        public static HAL_SetSPISpeedDelegate HAL_SetSPISpeed;

        public delegate void HAL_SetSPIOptsDelegate(int port, [MarshalAs(UnmanagedType.I4)]bool msb_first, [MarshalAs(UnmanagedType.I4)]bool sample_on_trailing, [MarshalAs(UnmanagedType.I4)]bool clk_idle_high);
        public static HAL_SetSPIOptsDelegate HAL_SetSPIOpts;

        public delegate void HAL_SetSPIChipSelectActiveHighDelegate(int port, ref int status);
        public static HAL_SetSPIChipSelectActiveHighDelegate HAL_SetSPIChipSelectActiveHigh;

        public delegate void HAL_SetSPIChipSelectActiveLowDelegate(int port, ref int status);
        public static HAL_SetSPIChipSelectActiveLowDelegate HAL_SetSPIChipSelectActiveLow;

        public delegate int HAL_GetSPIHandleDelegate(int port);
        public static HAL_GetSPIHandleDelegate HAL_GetSPIHandle;

        public delegate void HAL_SetSPIHandleDelegate(int port, int handle);
        public static HAL_SetSPIHandleDelegate HAL_SetSPIHandle;

        public delegate void HAL_InitSPIAccumulatorDelegate(int port, int period, int cmd, int xfer_size, int valid_mask, int valid_value, int data_shift, int data_size, [MarshalAs(UnmanagedType.I4)]bool is_signed, [MarshalAs(UnmanagedType.I4)]bool big_endian, ref int status);
        public static HAL_InitSPIAccumulatorDelegate HAL_InitSPIAccumulator;

        public delegate void HAL_FreeSPIAccumulatorDelegate(int port, ref int status);
        public static HAL_FreeSPIAccumulatorDelegate HAL_FreeSPIAccumulator;

        public delegate void HAL_ResetSPIAccumulatorDelegate(int port, ref int status);
        public static HAL_ResetSPIAccumulatorDelegate HAL_ResetSPIAccumulator;

        public delegate void HAL_SetSPIAccumulatorCenterDelegate(int port, int center, ref int status);
        public static HAL_SetSPIAccumulatorCenterDelegate HAL_SetSPIAccumulatorCenter;

        public delegate void HAL_SetSPIAccumulatorDeadbandDelegate(int port, int deadband, ref int status);
        public static HAL_SetSPIAccumulatorDeadbandDelegate HAL_SetSPIAccumulatorDeadband;

        public delegate int HAL_GetSPIAccumulatorLastValueDelegate(int port, ref int status);
        public static HAL_GetSPIAccumulatorLastValueDelegate HAL_GetSPIAccumulatorLastValue;

        public delegate long HAL_GetSPIAccumulatorValueDelegate(int port, ref int status);
        public static HAL_GetSPIAccumulatorValueDelegate HAL_GetSPIAccumulatorValue;

        public delegate long HAL_GetSPIAccumulatorCountDelegate(int port, ref int status);
        public static HAL_GetSPIAccumulatorCountDelegate HAL_GetSPIAccumulatorCount;

        public delegate double HAL_GetSPIAccumulatorAverageDelegate(int port, ref int status);
        public static HAL_GetSPIAccumulatorAverageDelegate HAL_GetSPIAccumulatorAverage;

        public delegate void HAL_GetSPIAccumulatorOutputDelegate(int port, ref long value, ref long count, ref int status);
        public static HAL_GetSPIAccumulatorOutputDelegate HAL_GetSPIAccumulatorOutput;
    }
}

