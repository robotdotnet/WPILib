using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using unsafe HAL_BufferCallback = delegate* unmanaged[Cdecl]<byte*, void*, byte*, uint, void>;
using unsafe HAL_NotifyCallback = delegate* unmanaged[Cdecl]<byte*, void*, WPIHal.HalValue*, void>;

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
