//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL;

namespace HAL_RoboRIO
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSerialPort
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALSerialPort.SerialInitializePort = (global::HAL.HALSerialPort.SerialInitializePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialInitializePort"), typeof(global::HAL.HALSerialPort.SerialInitializePortDelegate));

            global::HAL.HALSerialPort.SerialSetBaudRate = (global::HAL.HALSerialPort.SerialSetBaudRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetBaudRate"), typeof(global::HAL.HALSerialPort.SerialSetBaudRateDelegate));

            global::HAL.HALSerialPort.SerialSetDataBits = (global::HAL.HALSerialPort.SerialSetDataBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetDataBits"), typeof(global::HAL.HALSerialPort.SerialSetDataBitsDelegate));

            global::HAL.HALSerialPort.SerialSetParity = (global::HAL.HALSerialPort.SerialSetParityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetParity"), typeof(global::HAL.HALSerialPort.SerialSetParityDelegate));

            global::HAL.HALSerialPort.SerialSetStopBits = (global::HAL.HALSerialPort.SerialSetStopBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetStopBits"), typeof(global::HAL.HALSerialPort.SerialSetStopBitsDelegate));

            global::HAL.HALSerialPort.SerialSetWriteMode = (global::HAL.HALSerialPort.SerialSetWriteModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteMode"), typeof(global::HAL.HALSerialPort.SerialSetWriteModeDelegate));

            global::HAL.HALSerialPort.SerialSetFlowControl = (global::HAL.HALSerialPort.SerialSetFlowControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetFlowControl"), typeof(global::HAL.HALSerialPort.SerialSetFlowControlDelegate));

            global::HAL.HALSerialPort.SerialSetTimeout = (global::HAL.HALSerialPort.SerialSetTimeoutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetTimeout"), typeof(global::HAL.HALSerialPort.SerialSetTimeoutDelegate));

            global::HAL.HALSerialPort.SerialEnableTermination = (global::HAL.HALSerialPort.SerialEnableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialEnableTermination"), typeof(global::HAL.HALSerialPort.SerialEnableTerminationDelegate));

            global::HAL.HALSerialPort.SerialDisableTermination = (global::HAL.HALSerialPort.SerialDisableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialDisableTermination"), typeof(global::HAL.HALSerialPort.SerialDisableTerminationDelegate));

            global::HAL.HALSerialPort.SerialSetReadBufferSize = (global::HAL.HALSerialPort.SerialSetReadBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetReadBufferSize"), typeof(global::HAL.HALSerialPort.SerialSetReadBufferSizeDelegate));

            global::HAL.HALSerialPort.SerialSetWriteBufferSize = (global::HAL.HALSerialPort.SerialSetWriteBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteBufferSize"), typeof(global::HAL.HALSerialPort.SerialSetWriteBufferSizeDelegate));

            global::HAL.HALSerialPort.SerialGetBytesReceived = (global::HAL.HALSerialPort.SerialGetBytesReceivedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialGetBytesReceived"), typeof(global::HAL.HALSerialPort.SerialGetBytesReceivedDelegate));

            global::HAL.HALSerialPort.SerialRead = (global::HAL.HALSerialPort.SerialReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialRead"), typeof(global::HAL.HALSerialPort.SerialReadDelegate));

            global::HAL.HALSerialPort.SerialWrite = (global::HAL.HALSerialPort.SerialWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialWrite"), typeof(global::HAL.HALSerialPort.SerialWriteDelegate));

            global::HAL.HALSerialPort.SerialFlush = (global::HAL.HALSerialPort.SerialFlushDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialFlush"), typeof(global::HAL.HALSerialPort.SerialFlushDelegate));

            global::HAL.HALSerialPort.SerialClear = (global::HAL.HALSerialPort.SerialClearDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClear"), typeof(global::HAL.HALSerialPort.SerialClearDelegate));

            global::HAL.HALSerialPort.SerialClose = (global::HAL.HALSerialPort.SerialCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClose"), typeof(global::HAL.HALSerialPort.SerialCloseDelegate));

        }
    }
}
