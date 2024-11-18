using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives.Simulation;

public struct HalSimNotifierInfo
{
    // TODO fill this in
}

public static unsafe partial class HalNotifierData
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetNextNotifierTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetNextTimeout();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetNumNotifiers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNums();

    [LibraryImport("wpiHal", EntryPoint = "HALSIM_GetNotifierInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetInfo(Span<HalSimNotifierInfo> arr, int size);

}
