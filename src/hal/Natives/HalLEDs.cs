using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalLEDs
{
    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRadioLEDState(RadioLEDState state, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = HalBase.StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RadioLEDState GetRadioLEDState(out HalStatus status);
}
