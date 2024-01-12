using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIHal;
using WPIHal.Handles;

namespace Hal.Natives;

public static partial class HalSimDevice
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimDeviceHandle CreateSimDevice(string name);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimValueHandle CreateSimValue(HalSimDeviceHandle device, string name, int direction, Value* initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimValueEnum")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimValueHandle CreateSimValueEnum(HalSimDeviceHandle device, string name, int direction, int numOptions, string* options, int initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeSimDevice(HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSimValue(HalSimValueHandle handle, Value* value);

    [LibraryImport("wpiHal", EntryPoint = "v = HAL_MakeBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Value v = MakeBoolean(initialValue);

    [LibraryImport("wpiHal", EntryPoint = "v = HAL_MakeDouble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Value v = MakeDouble(initialValue);

    [LibraryImport("wpiHal", EntryPoint = "v = HAL_MakeInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Value v = MakeInt(initialValue);

    [LibraryImport("wpiHal", EntryPoint = "v = HAL_MakeLong")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Value v = MakeLong(initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSimValue(HalSimValueHandle handle, Value* value);


}
