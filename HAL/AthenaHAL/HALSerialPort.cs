using System;
using System.Runtime.InteropServices;
using System.Security;
using HAL.Base;
namespace HAL.AthenaHAL
{
internal class HALSerialPort
{
internal static void Initialize(IntPtr library, ILibraryLoader loader)
{

Base.HALSerialPort.HAL_InitializeSerialPort = (Base.HALSerialPort.HAL_InitializeSerialPortDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_InitializeSerialPort"), typeof(Base.HALSerialPort.HAL_InitializeSerialPortDelegate));

Base.HALSerialPort.HAL_SetSerialBaudRate = (Base.HALSerialPort.HAL_SetSerialBaudRateDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialBaudRate"), typeof(Base.HALSerialPort.HAL_SetSerialBaudRateDelegate));

Base.HALSerialPort.HAL_SetSerialDataBits = (Base.HALSerialPort.HAL_SetSerialDataBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialDataBits"), typeof(Base.HALSerialPort.HAL_SetSerialDataBitsDelegate));

Base.HALSerialPort.HAL_SetSerialParity = (Base.HALSerialPort.HAL_SetSerialParityDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialParity"), typeof(Base.HALSerialPort.HAL_SetSerialParityDelegate));

Base.HALSerialPort.HAL_SetSerialStopBits = (Base.HALSerialPort.HAL_SetSerialStopBitsDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialStopBits"), typeof(Base.HALSerialPort.HAL_SetSerialStopBitsDelegate));

Base.HALSerialPort.HAL_SetSerialWriteMode = (Base.HALSerialPort.HAL_SetSerialWriteModeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialWriteMode"), typeof(Base.HALSerialPort.HAL_SetSerialWriteModeDelegate));

Base.HALSerialPort.HAL_SetSerialFlowControl = (Base.HALSerialPort.HAL_SetSerialFlowControlDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialFlowControl"), typeof(Base.HALSerialPort.HAL_SetSerialFlowControlDelegate));

Base.HALSerialPort.HAL_SetSerialTimeout = (Base.HALSerialPort.HAL_SetSerialTimeoutDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialTimeout"), typeof(Base.HALSerialPort.HAL_SetSerialTimeoutDelegate));

Base.HALSerialPort.HAL_EnableSerialTermination = (Base.HALSerialPort.HAL_EnableSerialTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_EnableSerialTermination"), typeof(Base.HALSerialPort.HAL_EnableSerialTerminationDelegate));

Base.HALSerialPort.HAL_DisableSerialTermination = (Base.HALSerialPort.HAL_DisableSerialTerminationDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_DisableSerialTermination"), typeof(Base.HALSerialPort.HAL_DisableSerialTerminationDelegate));

Base.HALSerialPort.HAL_SetSerialReadBufferSize = (Base.HALSerialPort.HAL_SetSerialReadBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialReadBufferSize"), typeof(Base.HALSerialPort.HAL_SetSerialReadBufferSizeDelegate));

Base.HALSerialPort.HAL_SetSerialWriteBufferSize = (Base.HALSerialPort.HAL_SetSerialWriteBufferSizeDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_SetSerialWriteBufferSize"), typeof(Base.HALSerialPort.HAL_SetSerialWriteBufferSizeDelegate));

Base.HALSerialPort.HAL_GetSerialBytesReceived = (Base.HALSerialPort.HAL_GetSerialBytesReceivedDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_GetSerialBytesReceived"), typeof(Base.HALSerialPort.HAL_GetSerialBytesReceivedDelegate));

Base.HALSerialPort.HAL_ReadSerial = (Base.HALSerialPort.HAL_ReadSerialDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ReadSerial"), typeof(Base.HALSerialPort.HAL_ReadSerialDelegate));

Base.HALSerialPort.HAL_WriteSerial = (Base.HALSerialPort.HAL_WriteSerialDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_WriteSerial"), typeof(Base.HALSerialPort.HAL_WriteSerialDelegate));

Base.HALSerialPort.HAL_FlushSerial = (Base.HALSerialPort.HAL_FlushSerialDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_FlushSerial"), typeof(Base.HALSerialPort.HAL_FlushSerialDelegate));

Base.HALSerialPort.HAL_ClearSerial = (Base.HALSerialPort.HAL_ClearSerialDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_ClearSerial"), typeof(Base.HALSerialPort.HAL_ClearSerialDelegate));

Base.HALSerialPort.HAL_CloseSerial = (Base.HALSerialPort.HAL_CloseSerialDelegate)Marshal.GetDelegateForFunctionPointer(loader.GetProcAddress(library, "HAL_CloseSerial"), typeof(Base.HALSerialPort.HAL_CloseSerialDelegate));
}
}
}

