//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL_Base;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSerialPort
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALSerialPort.SerialInitializePort = (HAL_Base.HALSerialPort.SerialInitializePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialInitializePort"), typeof(HAL_Base.HALSerialPort.SerialInitializePortDelegate));

            HAL_Base.HALSerialPort.SerialSetBaudRate = (HAL_Base.HALSerialPort.SerialSetBaudRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetBaudRate"), typeof(HAL_Base.HALSerialPort.SerialSetBaudRateDelegate));

            HAL_Base.HALSerialPort.SerialSetDataBits = (HAL_Base.HALSerialPort.SerialSetDataBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetDataBits"), typeof(HAL_Base.HALSerialPort.SerialSetDataBitsDelegate));

            HAL_Base.HALSerialPort.SerialSetParity = (HAL_Base.HALSerialPort.SerialSetParityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetParity"), typeof(HAL_Base.HALSerialPort.SerialSetParityDelegate));

            HAL_Base.HALSerialPort.SerialSetStopBits = (HAL_Base.HALSerialPort.SerialSetStopBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetStopBits"), typeof(HAL_Base.HALSerialPort.SerialSetStopBitsDelegate));

            HAL_Base.HALSerialPort.SerialSetWriteMode = (HAL_Base.HALSerialPort.SerialSetWriteModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteMode"), typeof(HAL_Base.HALSerialPort.SerialSetWriteModeDelegate));

            HAL_Base.HALSerialPort.SerialSetFlowControl = (HAL_Base.HALSerialPort.SerialSetFlowControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetFlowControl"), typeof(HAL_Base.HALSerialPort.SerialSetFlowControlDelegate));

            HAL_Base.HALSerialPort.SerialSetTimeout = (HAL_Base.HALSerialPort.SerialSetTimeoutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetTimeout"), typeof(HAL_Base.HALSerialPort.SerialSetTimeoutDelegate));

            HAL_Base.HALSerialPort.SerialEnableTermination = (HAL_Base.HALSerialPort.SerialEnableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialEnableTermination"), typeof(HAL_Base.HALSerialPort.SerialEnableTerminationDelegate));

            HAL_Base.HALSerialPort.SerialDisableTermination = (HAL_Base.HALSerialPort.SerialDisableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialDisableTermination"), typeof(HAL_Base.HALSerialPort.SerialDisableTerminationDelegate));

            HAL_Base.HALSerialPort.SerialSetReadBufferSize = (HAL_Base.HALSerialPort.SerialSetReadBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetReadBufferSize"), typeof(HAL_Base.HALSerialPort.SerialSetReadBufferSizeDelegate));

            HAL_Base.HALSerialPort.SerialSetWriteBufferSize = (HAL_Base.HALSerialPort.SerialSetWriteBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteBufferSize"), typeof(HAL_Base.HALSerialPort.SerialSetWriteBufferSizeDelegate));

            HAL_Base.HALSerialPort.SerialGetBytesReceived = (HAL_Base.HALSerialPort.SerialGetBytesReceivedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialGetBytesReceived"), typeof(HAL_Base.HALSerialPort.SerialGetBytesReceivedDelegate));

            HAL_Base.HALSerialPort.SerialRead = (HAL_Base.HALSerialPort.SerialReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialRead"), typeof(HAL_Base.HALSerialPort.SerialReadDelegate));

            HAL_Base.HALSerialPort.SerialWrite = (HAL_Base.HALSerialPort.SerialWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialWrite"), typeof(HAL_Base.HALSerialPort.SerialWriteDelegate));

            HAL_Base.HALSerialPort.SerialFlush = (HAL_Base.HALSerialPort.SerialFlushDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialFlush"), typeof(HAL_Base.HALSerialPort.SerialFlushDelegate));

            HAL_Base.HALSerialPort.SerialClear = (HAL_Base.HALSerialPort.SerialClearDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClear"), typeof(HAL_Base.HALSerialPort.SerialClearDelegate));

            HAL_Base.HALSerialPort.SerialClose = (HAL_Base.HALSerialPort.SerialCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClose"), typeof(HAL_Base.HALSerialPort.SerialCloseDelegate));

        }
    }
}
