using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalAccelerometer
{
    public enum Range : int
    {
#pragma warning disable CA1712 // Do not prefix enum values with type name
        Range2G = 0,
        Range4G = 1,
        Range8G = 2,
#pragma warning restore CA1712 // Do not prefix enum values with type name
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccelerometerX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerX();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccelerometerY")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerY();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetAccelerometerZ")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetAccelerometerZ();

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAccelerometerActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerActive(int active);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetAccelerometerRange")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetAccelerometerRange(Range range);
}
