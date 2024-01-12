using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalI2C
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CloseI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseI2C(I2CPort port);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitializeI2C(I2CPort port, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReadI2C(I2CPort port, int deviceAddress, byte* buffer, int count);

    [LibraryImport("wpiHal", EntryPoint = "HAL_TransactionI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int TransactionI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize, byte* dataReceived, int receiveSize);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteI2C")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int WriteI2C(I2CPort port, int deviceAddress, byte* dataToSend, int sendSize);


}
