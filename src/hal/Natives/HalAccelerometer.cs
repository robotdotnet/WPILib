using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalAccelerometer
{
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
    public static partial void SetAccelerometerRange(AccelerometerRange range);


}
