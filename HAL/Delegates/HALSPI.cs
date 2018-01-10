using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSPI
    {
        static HALSPI()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSPI>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_InitializeSPIDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_InitializeSPIDelegate HAL_InitializeSPI;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_TransactionSPIDelegate(int port, byte[] dataToSend, byte[] dataReceived, int size);
        [NativeDelegate] public static HAL_TransactionSPIDelegate HAL_TransactionSPI;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_WriteSPIDelegate(int port, byte[] dataToSend, int sendSize);
        [NativeDelegate] public static HAL_WriteSPIDelegate HAL_WriteSPI;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_ReadSPIDelegate(int port, byte[] buffer, int count);
        [NativeDelegate] public static HAL_ReadSPIDelegate HAL_ReadSPI;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CloseSPIDelegate(int port);
        [NativeDelegate] public static HAL_CloseSPIDelegate HAL_CloseSPI;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPISpeedDelegate(int port, int speed);
        [NativeDelegate] public static HAL_SetSPISpeedDelegate HAL_SetSPISpeed;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIOptsDelegate(int port, [MarshalAs(UnmanagedType.Bool)]bool msb_first, [MarshalAs(UnmanagedType.Bool)]bool sample_on_trailing, [MarshalAs(UnmanagedType.Bool)]bool clk_idle_high);
        [NativeDelegate] public static HAL_SetSPIOptsDelegate HAL_SetSPIOpts;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIChipSelectActiveHighDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_SetSPIChipSelectActiveHighDelegate HAL_SetSPIChipSelectActiveHigh;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIChipSelectActiveLowDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_SetSPIChipSelectActiveLowDelegate HAL_SetSPIChipSelectActiveLow;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetSPIHandleDelegate(int port);
        [NativeDelegate] public static HAL_GetSPIHandleDelegate HAL_GetSPIHandle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIHandleDelegate(int port, int handle);
        [NativeDelegate] public static HAL_SetSPIHandleDelegate HAL_SetSPIHandle;
        /*
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_InitSPIAccumulatorDelegate(int port, int period, int cmd, int xfer_size, int valid_mask, int valid_value, int data_shift, int data_size, [MarshalAs(UnmanagedType.Bool)]bool is_signed, [MarshalAs(UnmanagedType.Bool)]bool big_endian, ref int status);
        [NativeDelegate] public static HAL_InitSPIAccumulatorDelegate HAL_InitSPIAccumulator;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FreeSPIAccumulatorDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_FreeSPIAccumulatorDelegate HAL_FreeSPIAccumulator;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ResetSPIAccumulatorDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_ResetSPIAccumulatorDelegate HAL_ResetSPIAccumulator;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIAccumulatorCenterDelegate(int port, int center, ref int status);
        [NativeDelegate] public static HAL_SetSPIAccumulatorCenterDelegate HAL_SetSPIAccumulatorCenter;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSPIAccumulatorDeadbandDelegate(int port, int deadband, ref int status);
        [NativeDelegate] public static HAL_SetSPIAccumulatorDeadbandDelegate HAL_SetSPIAccumulatorDeadband;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetSPIAccumulatorLastValueDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSPIAccumulatorLastValueDelegate HAL_GetSPIAccumulatorLastValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetSPIAccumulatorValueDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSPIAccumulatorValueDelegate HAL_GetSPIAccumulatorValue;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate long HAL_GetSPIAccumulatorCountDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSPIAccumulatorCountDelegate HAL_GetSPIAccumulatorCount;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate double HAL_GetSPIAccumulatorAverageDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSPIAccumulatorAverageDelegate HAL_GetSPIAccumulatorAverage;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_GetSPIAccumulatorOutputDelegate(int port, ref long value, ref long count, ref int status);
        [NativeDelegate] public static HAL_GetSPIAccumulatorOutputDelegate HAL_GetSPIAccumulatorOutput;
        */
    }
}

