using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalSPI
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CloseSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseSPI(SPIPort port);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_ConfigureSPIAutoStall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ConfigureSPIAutoStall(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_ForceSPIAutoRead")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ForceSPIAutoRead(SPIPort port, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeSPIAuto(SPIPort port, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSPIAutoDroppedCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSPIAutoDroppedCount(SPIPort port, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSPIHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSPIHandle(SPIPort port);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitSPIAuto(SPIPort port, int bufferSize, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitializeSPI(SPIPort port, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReadSPI(SPIPort port, Span<byte> buffer, int count);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadSPIAutoReceivedData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReadSPIAutoReceivedData(SPIPort port, Span<uint> buffer, int numToRead, double timeout, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIAutoTransmitData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIAutoTransmitData(SPIPort port, ReadOnlySpan<byte> dataToSend, int dataSize, int zeroSize, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIChipSelectActiveHigh")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIChipSelectActiveHigh(SPIPort port, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIChipSelectActiveLow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIChipSelectActiveLow(SPIPort port, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIHandle(SPIPort port, int handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPISpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPISpeed(SPIPort port, int speed);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_StartSPIAutoRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartSPIAutoRate(SPIPort port, double period, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_StartSPIAutoTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartSPIAutoTrigger(SPIPort port, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_StopSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopSPIAuto(SPIPort port, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_TransactionSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int TransactionSPI(SPIPort port, ReadOnlySpan<byte> dataToSend, Span<byte> dataReceived, int size);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int WriteSPI(SPIPort port, ReadOnlySpan<byte> dataToSend, int sendSize);
}
