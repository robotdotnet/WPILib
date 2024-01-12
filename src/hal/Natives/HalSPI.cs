using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalSPI
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CloseSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseSPI(SPIPort port);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ConfigureSPIAutoStall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ConfigureSPIAutoStallRefShim(SPIPort port, int csToSclkTicks, int stallTicks, int pow2BytesPerRead, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ForceSPIAutoRead")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void ForceSPIAutoReadRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void FreeSPIAutoRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSPIAutoDroppedCount")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetSPIAutoDroppedCountRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSPIHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSPIHandle(SPIPort port);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void InitSPIAutoRefShim(SPIPort port, int bufferSize, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void InitializeSPIRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int ReadSPI(SPIPort port, Span<byte> buffer, int count);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadSPIAutoReceivedData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int ReadSPIAutoReceivedDataRefShim(SPIPort port, Span<uint> buffer, int numToRead, double timeout, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIAutoTransmitData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSPIAutoTransmitDataRefShim(SPIPort port, ReadOnlySpan<byte> dataToSend, int dataSize, int zeroSize, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIChipSelectActiveHigh")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSPIChipSelectActiveHighRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIChipSelectActiveLow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetSPIChipSelectActiveLowRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPIHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPIHandle(SPIPort port, int handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSPISpeed")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSPISpeed(SPIPort port, int speed);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartSPIAutoRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StartSPIAutoRateRefShim(SPIPort port, double period, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartSPIAutoTrigger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StartSPIAutoTriggerRefShim(SPIPort port, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, int triggerRising, int triggerFalling, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopSPIAuto")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StopSPIAutoRefShim(SPIPort port, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_TransactionSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int TransactionSPI(SPIPort port, ReadOnlySpan<byte> dataToSend, Span<byte> dataReceived, int size);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteSPI")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int WriteSPI(SPIPort port, ReadOnlySpan<byte> dataToSend, int sendSize);


}
