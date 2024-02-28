using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalI2C
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CloseI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseI2C(I2CPort port);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitializeI2C(I2CPort port, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReadI2C(I2CPort port, int deviceAddress, Span<byte> buffer, int count);

    public static Span<byte> ReadI2C(I2CPort port, int deviceAddress, Span<byte> buffer)
    {
        int read = ReadI2C(port, deviceAddress, buffer, buffer.Length);
        if (read > 0)
        {
            return buffer[..read];
        }
        else
        {
            return default;
        }
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_TransactionI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int TransactionI2C(I2CPort port, int deviceAddress, ReadOnlySpan<byte> dataToSend, int sendSize, Span<byte> dataReceived, int receiveSize);

    public static Span<byte> TransactionI2C(I2CPort port, int deviceAddress, ReadOnlySpan<byte> dataToSend, Span<byte> dataReceived)
    {
        int read = TransactionI2C(port, deviceAddress, dataToSend, dataToSend.Length, dataReceived, dataReceived.Length);
        if (read > 0)
        {
            return dataReceived[..read];
        }
        else
        {
            return default;
        }
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int WriteI2C(I2CPort port, int deviceAddress, ReadOnlySpan<byte> dataToSend, int sendSize);

    public static int WriteI2C(I2CPort port, int deviceAddress, ReadOnlySpan<byte> dataToSend)
    {
        return WriteI2C(port, deviceAddress, dataToSend, dataToSend.Length);
    }


}
