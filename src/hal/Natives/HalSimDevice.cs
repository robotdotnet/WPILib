using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal.Handles;
using WPIHal.Marshal;

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

    [LibraryImport("wpiHal", EntryPoint = "HAL_CreateSimValueEnumDouble", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalSimValueHandle CreateSimValueEnumDouble(HalSimDeviceHandle device, string name, int direction, int numOptions, ReadOnlySpan<string> options, ReadOnlySpan<double> optionValues, int initialValue);

    [LibraryImport("wpiHal", EntryPoint = "HAL_FreeSimDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeSimDevice(HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSimDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<NoFreeNullTerminatedStringFree>))]
    public static partial string GetSimDeviceName(HalSimDeviceHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetSimValue(HalSimValueHandle handle, out HalValue value);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ResetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetSimValue(HalSimValueHandle handle);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetSimValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetSimValue(HalSimValueHandle handle, in HalValue value);


}
