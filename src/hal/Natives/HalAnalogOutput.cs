using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAnalogOutput
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckAnalogOutputChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckAnalogOutputChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAnalogOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnalogOutputPort(HalAnalogOutputHandle analogOutputHandle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial double GetAnalogOutputRefShim(HalAnalogOutputHandle analogOutputHandle, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogOutputPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial HalAnalogOutputHandle InitializeAnalogOutputPortRefShim(HalPortHandle portHandle, string allocationLocation, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetAnalogOutputRefShim(HalAnalogOutputHandle analogOutputHandle, double voltage, ref HalStatus status);


}
