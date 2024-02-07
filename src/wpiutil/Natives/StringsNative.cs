using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Marshal;

namespace WPIUtil.Natives;

public static partial class StringsNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeString(WpiStringMarshaller.WpiStringNative* wpiString);

    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeStringArray(WpiStringMarshaller.WpiStringNative* wpiString, nuint length);
}
