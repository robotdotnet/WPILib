using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives.Simulation;

public static unsafe partial class HalReset
{
    [LibraryImport("wpiHal", EntryPoint = "HALSIM_ResetAllSimData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetAllSimData();

}
