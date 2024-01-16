using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalConstants
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSystemClockTicksPerMicrosecond")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSystemClockTicksPerMicrosecond();


}
