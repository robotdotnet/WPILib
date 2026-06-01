using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WPIUtil.Marshal;

namespace WPIUtil.Natives;

public static partial class StringsNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_InitString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void InitString(WpiStringMarshaller.WpiStringNative* wpiString);

    [LibraryImport("wpiutil", EntryPoint = "WPI_InitStringWithLength")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void InitStringWithLength(WpiStringMarshaller.WpiStringNative* wpiString, nuint len);

    [LibraryImport("wpiutil", EntryPoint = "WPI_AllocateString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void AllocateString(WpiStringMarshaller.WpiStringNative* wpiString, nuint len);

    [LibraryImport("wpiutil", EntryPoint = "WPI_AllocateStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial WpiStringMarshaller.WpiStringNative* AllocateStringArray(nuint count);

    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeString(WpiStringMarshaller.WpiStringNative* wpiString);

    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeStringArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeStringArray(WpiStringMarshaller.WpiStringNative* wpiString, nuint length);
}
