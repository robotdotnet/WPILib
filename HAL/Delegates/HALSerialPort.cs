using System.Runtime.InteropServices;
using NativeLibraryUtilities;

// ReSharper disable CheckNamespace
namespace HAL.Base
{
    public partial class HALSerialPort
    {
        static HALSerialPort()
        {
            NativeDelegateInitializer.SetupNativeDelegates<HALSerialPort>(LibraryLoaderHolder.NativeLoader);
        }

        /// <summary>
        /// Use this to force load the definitions in the file
        /// </summary>
        public static void Ping()
        {
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_InitializeSerialPortDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_InitializeSerialPortDelegate HAL_InitializeSerialPort;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialBaudRateDelegate(int port, int baud, ref int status);
        [NativeDelegate] public static HAL_SetSerialBaudRateDelegate HAL_SetSerialBaudRate;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialDataBitsDelegate(int port, int bits, ref int status);
        [NativeDelegate] public static HAL_SetSerialDataBitsDelegate HAL_SetSerialDataBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialParityDelegate(int port, int parity, ref int status);
        [NativeDelegate] public static HAL_SetSerialParityDelegate HAL_SetSerialParity;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialStopBitsDelegate(int port, int stopBits, ref int status);
        [NativeDelegate] public static HAL_SetSerialStopBitsDelegate HAL_SetSerialStopBits;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialWriteModeDelegate(int port, int mode, ref int status);
        [NativeDelegate] public static HAL_SetSerialWriteModeDelegate HAL_SetSerialWriteMode;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialFlowControlDelegate(int port, int flow, ref int status);
        [NativeDelegate] public static HAL_SetSerialFlowControlDelegate HAL_SetSerialFlowControl;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialTimeoutDelegate(int port, double timeout, ref int status);
        [NativeDelegate] public static HAL_SetSerialTimeoutDelegate HAL_SetSerialTimeout;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_EnableSerialTerminationDelegate(int port, byte terminator, ref int status);
        [NativeDelegate] public static HAL_EnableSerialTerminationDelegate HAL_EnableSerialTermination;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_DisableSerialTerminationDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_DisableSerialTerminationDelegate HAL_DisableSerialTermination;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialReadBufferSizeDelegate(int port, int size, ref int status);
        [NativeDelegate] public static HAL_SetSerialReadBufferSizeDelegate HAL_SetSerialReadBufferSize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_SetSerialWriteBufferSizeDelegate(int port, int size, ref int status);
        [NativeDelegate] public static HAL_SetSerialWriteBufferSizeDelegate HAL_SetSerialWriteBufferSize;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_GetSerialBytesReceivedDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_GetSerialBytesReceivedDelegate HAL_GetSerialBytesReceived;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_ReadSerialDelegate(int port, byte[] buffer, int count, ref int status);
        [NativeDelegate] public static HAL_ReadSerialDelegate HAL_ReadSerial;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate int HAL_WriteSerialDelegate(int port, byte[] buffer, int count, ref int status);
        [NativeDelegate] public static HAL_WriteSerialDelegate HAL_WriteSerial;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_FlushSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_FlushSerialDelegate HAL_FlushSerial;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_ClearSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_ClearSerialDelegate HAL_ClearSerial;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)] public delegate void HAL_CloseSerialDelegate(int port, ref int status);
        [NativeDelegate] public static HAL_CloseSerialDelegate HAL_CloseSerial;
    }
}

