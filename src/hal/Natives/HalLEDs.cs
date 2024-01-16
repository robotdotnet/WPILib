using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalLEDs
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_SetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial void SetRadioLEDStateRefShim(RadioLEDState state, ref HalStatus status);


    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRadioLEDState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial RadioLEDState GetRadioLEDStateRefShim(ref HalStatus status);
}
