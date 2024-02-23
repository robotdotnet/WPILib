using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CsCore.Natives;

public static unsafe partial class CsNative
{
    [LibraryImport(LibraryName, EntryPoint = "CS_FreeEnumeratedUsbCameras")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEnumeratedUsbCameras(UsbCameraInfoMarshaller.NativeUsbCameraInfo* cameras, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_ReleaseEnumeratedSources")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseEnumeratedSources(int* sources, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_ReleaseEnumeratedSinks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ReleaseEnumeratedSinks(int* sinks, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeEnumPropertyChoices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEnumPropertyChoices(byte** choices, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeUsbCameraInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeUsbCameraInfo(UsbCameraInfoMarshaller.NativeUsbCameraInfo* info);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeHttpCameraUrls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeHttpCameraUrls(byte** choices, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeEnumeratedProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEnumeratedProperties(int* properties, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeEnumeratedVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeEnumeratedVideoModes(VideoModeMarshaller.NativeVideoMode* modes, int count);

    [LibraryImport(LibraryName, EntryPoint = "CS_FreeNetworkInterfaces")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeNetworkInterfaces(byte** interfaces, int count);
}
