using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAddressableLED
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAddressableLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAddressableLED(HalAddressableLEDHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAddressableLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalAddressableLEDHandle InitializeAddressableLEDRefShim(HalDigitalHandle outputPort, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDBitTiming")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAddressableLEDBitTimingRefShim(HalAddressableLEDHandle handle, int highTime0NanoSeconds, int lowTime0NanoSeconds, int highTime1NanoSeconds, int lowTime1NanoSeconds, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAddressableLEDLengthRefShim(HalAddressableLEDHandle handle, int length, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAddressableLEDOutputPortRefShim(HalAddressableLEDHandle handle, HalDigitalHandle outputPort, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAddressableLEDSyncTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAddressableLEDSyncTimeRefShim(HalAddressableLEDHandle handle, int syncTimeMicroSeconds, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StartAddressableLEDOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StartAddressableLEDOutputRefShim(HalAddressableLEDHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_StopAddressableLEDOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void StopAddressableLEDOutputRefShim(HalAddressableLEDHandle handle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_WriteAddressableLEDData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void WriteAddressableLEDDataRefShim(HalAddressableLEDHandle handle, ReadOnlySpan<AddressableLEDData> data, int length, ref HalStatus status);

    public static void WriteAddressableLEDData(HalAddressableLEDHandle handle, ReadOnlySpan<AddressableLEDData> data, out HalStatus status)
    {
        WriteAddressableLEDData(handle, data, data.Length, out status);
    }


}
