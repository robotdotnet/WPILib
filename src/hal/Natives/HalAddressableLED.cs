using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalAddressableLED
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAddressableLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAddressableLED(HalAddressableLEDHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAddressableLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalAddressableLEDHandle InitializeAddressableLED(HalDigitalHandle outputPort, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDBitTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDBitTiming(HalAddressableLEDHandle handle, int highTime0NanoSeconds, int lowTime0NanoSeconds, int highTime1NanoSeconds, int lowTime1NanoSeconds, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDLength(HalAddressableLEDHandle handle, int length, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDOutputPort(HalAddressableLEDHandle handle, HalDigitalHandle outputPort, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDSyncTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAddressableLEDSyncTime(HalAddressableLEDHandle handle, int syncTimeMicroSeconds, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartAddressableLEDOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StartAddressableLEDOutput(HalAddressableLEDHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopAddressableLEDOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void StopAddressableLEDOutput(HalAddressableLEDHandle handle, out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WriteAddressableLEDData(HalAddressableLEDHandle handle, ReadOnlySpan<AddressableLEDData> data, int length, out HalStatus status);

    public static void WriteAddressableLEDData(HalAddressableLEDHandle handle, ReadOnlySpan<AddressableLEDData> data, out HalStatus status)
    {
        WriteAddressableLEDData(handle, data, data.Length, out status);
    }


}
