using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal.Handles;

namespace WPIHal.Natives;

public static partial class HalSimDevice
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimDevice", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimDeviceHandle CreateSimDevice(string name);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimValue", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimValueHandle CreateSimValue(HalSimDeviceHandle device, string name, int direction, in HalValue initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimValueEnum", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimValueHandle CreateSimValueEnum(HalSimDeviceHandle device, string name, int direction, int numOptions, ReadOnlySpan<string> options, int initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeSimDevice(HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSimValue(HalSimValueHandle handle, out HalValue value);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSimValue(HalSimValueHandle handle, in HalValue value);


}
