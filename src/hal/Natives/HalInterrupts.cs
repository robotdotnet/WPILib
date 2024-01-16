using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalInterrupts
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CleanInterrupts(HalInterruptHandle interruptHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalInterruptHandle InitializeInterruptsRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadInterruptFallingTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long ReadInterruptFallingTimestampRefShim(HalInterruptHandle interruptHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadInterruptRisingTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long ReadInterruptRisingTimestampRefShim(HalInterruptHandle interruptHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_RequestInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void RequestInterruptsRefShim(HalInterruptHandle interruptHandle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetInterruptUpSourceEdge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetInterruptUpSourceEdgeRefShim(HalInterruptHandle interruptHandle, int risingEdge, int fallingEdge, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WaitForInterrupt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long WaitForInterruptRefShim(HalInterruptHandle interruptHandle, double timeout, int ignorePrevious, ref HalStatus status);


}
