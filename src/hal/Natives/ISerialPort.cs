using WPIUtil.ILGeneration;

namespace Hal.Natives
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
    [StatusCheckedBy(typeof(StatusHandling))]
    public unsafe interface ISerialPort
    {
        [StatusCheckLastParameter] void HAL_ClearSerial(int handle);

        [StatusCheckLastParameter] void HAL_CloseSerial(int handle);

        [StatusCheckLastParameter] void HAL_DisableSerialTermination(int handle);

        [StatusCheckLastParameter] void HAL_EnableSerialTermination(int handle, byte terminator);

        [StatusCheckLastParameter] void HAL_FlushSerial(int handle);

        [StatusCheckLastParameter] int HAL_GetSerialBytesReceived(int handle);

        [StatusCheckLastParameter] int HAL_GetSerialFD(int handle);

        [StatusCheckRange(0, typeof(StatusHandling), "SerialPortStatusCheck")] int HAL_InitializeSerialPort(SerialPortLocation port);

        [StatusCheckLastParameter] int HAL_InitializeSerialPortDirect(SerialPortLocation port, byte* portName);

        [StatusCheckLastParameter] int HAL_ReadSerial(int handle, byte* buffer, int count);

        [StatusCheckLastParameter] void HAL_SetSerialBaudRate(int handle, int baud);

        [StatusCheckLastParameter] void HAL_SetSerialDataBits(int handle, int bits);

        [StatusCheckLastParameter] void HAL_SetSerialFlowControl(int handle, int flow);

        [StatusCheckLastParameter] void HAL_SetSerialParity(int handle, int parity);

        [StatusCheckLastParameter] void HAL_SetSerialReadBufferSize(int handle, int size);

        [StatusCheckLastParameter] void HAL_SetSerialStopBits(int handle, int stopBits);

        [StatusCheckLastParameter] void HAL_SetSerialTimeout(int handle, double timeout);

        [StatusCheckLastParameter] void HAL_SetSerialWriteBufferSize(int handle, int size);

        [StatusCheckLastParameter] void HAL_SetSerialWriteMode(int handle, int mode);

        [StatusCheckLastParameter] int HAL_WriteSerial(int handle, byte* buffer, int count);

    }
}
