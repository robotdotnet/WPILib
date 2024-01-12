using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalLEDs
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetRadioLEDState(RadioLEDState state, out HalStatus status);


    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RadioLEDState GetRadioLEDState(out HalStatus status);
}
