using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalAnalogOutput
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CheckAnalogOutputChannel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int CheckAnalogOutputChannel(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeAnalogOutputPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeAnalogOutputPort(HalAnalogOutputHandle analogOutputHandle);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAnalogOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAnalogOutput(HalAnalogOutputHandle analogOutputHandle, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_InitializeAnalogOutputPort", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalAnalogOutputHandle InitializeAnalogOutputPort(HalPortHandle portHandle, string allocationLocation, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAnalogOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAnalogOutput(HalAnalogOutputHandle analogOutputHandle, double voltage, out HalStatus status);
}
