using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalExtensions
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_LoadOneExtension", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LoadOneExtension(string library);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetShowExtensionsNotFoundMessages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetShowExtensionsNotFoundMessages([MarshalAs(UnmanagedType.I4)] bool showMessage);
}
