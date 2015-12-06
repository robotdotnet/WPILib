//File automatically generated using robotdotnet-tools. Please do not modify.

using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;

namespace HAL.AthenaHAL
{
    [SuppressUnmanagedCodeSecurity]
    internal class HALSerialPort
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            Base.HALSerialPort.SerialInitializePort = (Base.HALSerialPort.SerialInitializePortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialInitializePort"), typeof(Base.HALSerialPort.SerialInitializePortDelegate));

            Base.HALSerialPort.SerialSetBaudRate = (Base.HALSerialPort.SerialSetBaudRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetBaudRate"), typeof(Base.HALSerialPort.SerialSetBaudRateDelegate));

            Base.HALSerialPort.SerialSetDataBits = (Base.HALSerialPort.SerialSetDataBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetDataBits"), typeof(Base.HALSerialPort.SerialSetDataBitsDelegate));

            Base.HALSerialPort.SerialSetParity = (Base.HALSerialPort.SerialSetParityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetParity"), typeof(Base.HALSerialPort.SerialSetParityDelegate));

            Base.HALSerialPort.SerialSetStopBits = (Base.HALSerialPort.SerialSetStopBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetStopBits"), typeof(Base.HALSerialPort.SerialSetStopBitsDelegate));

            Base.HALSerialPort.SerialSetWriteMode = (Base.HALSerialPort.SerialSetWriteModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteMode"), typeof(Base.HALSerialPort.SerialSetWriteModeDelegate));

            Base.HALSerialPort.SerialSetFlowControl = (Base.HALSerialPort.SerialSetFlowControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetFlowControl"), typeof(Base.HALSerialPort.SerialSetFlowControlDelegate));

            Base.HALSerialPort.SerialSetTimeout = (Base.HALSerialPort.SerialSetTimeoutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetTimeout"), typeof(Base.HALSerialPort.SerialSetTimeoutDelegate));

            Base.HALSerialPort.SerialEnableTermination = (Base.HALSerialPort.SerialEnableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialEnableTermination"), typeof(Base.HALSerialPort.SerialEnableTerminationDelegate));

            Base.HALSerialPort.SerialDisableTermination = (Base.HALSerialPort.SerialDisableTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialDisableTermination"), typeof(Base.HALSerialPort.SerialDisableTerminationDelegate));

            Base.HALSerialPort.SerialSetReadBufferSize = (Base.HALSerialPort.SerialSetReadBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetReadBufferSize"), typeof(Base.HALSerialPort.SerialSetReadBufferSizeDelegate));

            Base.HALSerialPort.SerialSetWriteBufferSize = (Base.HALSerialPort.SerialSetWriteBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialSetWriteBufferSize"), typeof(Base.HALSerialPort.SerialSetWriteBufferSizeDelegate));

            Base.HALSerialPort.SerialGetBytesReceived = (Base.HALSerialPort.SerialGetBytesReceivedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialGetBytesReceived"), typeof(Base.HALSerialPort.SerialGetBytesReceivedDelegate));

            Base.HALSerialPort.SerialRead = (Base.HALSerialPort.SerialReadDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialRead"), typeof(Base.HALSerialPort.SerialReadDelegate));

            Base.HALSerialPort.SerialWrite = (Base.HALSerialPort.SerialWriteDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialWrite"), typeof(Base.HALSerialPort.SerialWriteDelegate));

            Base.HALSerialPort.SerialFlush = (Base.HALSerialPort.SerialFlushDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialFlush"), typeof(Base.HALSerialPort.SerialFlushDelegate));

            Base.HALSerialPort.SerialClear = (Base.HALSerialPort.SerialClearDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClear"), typeof(Base.HALSerialPort.SerialClearDelegate));

            Base.HALSerialPort.SerialClose = (Base.HALSerialPort.SerialCloseDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "serialClose"), typeof(Base.HALSerialPort.SerialCloseDelegate));

        }
    }
}
