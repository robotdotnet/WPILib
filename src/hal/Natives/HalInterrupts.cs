using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalInterrupts
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CleanInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CleanInterrupts(HalInterruptHandle interruptHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalInterruptHandle InitializeInterrupts(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadInterruptFallingTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long ReadInterruptFallingTimestamp(HalInterruptHandle interruptHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ReadInterruptRisingTimestamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long ReadInterruptRisingTimestamp(HalInterruptHandle interruptHandle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_RequestInterrupts")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RequestInterrupts(HalInterruptHandle interruptHandle, HalAnalogTriggerHandle digitalSourceHandle, AnalogTriggerType analogTriggerType, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetInterruptUpSourceEdge")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInterruptUpSourceEdge(HalInterruptHandle interruptHandle, int risingEdge, int fallingEdge, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WaitForInterrupt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long WaitForInterrupt(HalInterruptHandle interruptHandle, double timeout, int ignorePrevious, out HalStatus status);


}
