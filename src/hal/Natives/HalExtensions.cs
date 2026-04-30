using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Natives;

public static partial class HalExtensions
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_LoadOneExtension", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LoadOneExtension(string library);

    [LibraryImport("wpiHal", EntryPoint = "HAL_LoadExtensions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int LoadExtensions();

    [LibraryImport("wpiHal", EntryPoint = "HAL_RegisterExtension", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void RegisterExtension(string name, void* data);

    [LibraryImport("wpiHal", EntryPoint = "HAL_RegisterExtensionListener")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void RegisterExtensionListener(void* param, delegate* unmanaged[Cdecl]<void*, byte*, void*, void> func);

    [LibraryImport("wpiHal", EntryPoint = "HAL_SetShowExtensionsNotFoundMessages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetShowExtensionsNotFoundMessages([MarshalAs(UnmanagedType.I4)] bool showMessage);

    [LibraryImport("wpiHal", EntryPoint = "HAL_OnShutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void OnShutdown(void* param, delegate* unmanaged[Cdecl]<void*, void> func);
}
