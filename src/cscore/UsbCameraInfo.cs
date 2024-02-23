using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

[NativeMarshalling(typeof(UsbCameraInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly struct UsbCameraInfo : INativeArrayFree<UsbCameraInfoMarshaller.NativeUsbCameraInfo>
{
    public required int Dev { get; init; }
    public required string Path { get; init; }
    public required string Name { get; init; }
    public required string[] OtherPaths { get; init; }
    public required int VendorId { get; init; }
    public required int ProductId { get; init; }

    public static unsafe void FreeArray(UsbCameraInfoMarshaller.NativeUsbCameraInfo* array, int len)
    {
        CsNative.FreeEnumeratedUsbCameras(array, len);
    }
}

[CustomMarshaller(typeof(UsbCameraInfo), MarshalMode.ElementOut, typeof(UsbCameraInfoMarshaller))]
[CustomMarshaller(typeof(UsbCameraInfo), MarshalMode.ManagedToUnmanagedOut, typeof(UsbCameraInfoMarshaller))]
public static unsafe class UsbCameraInfoMarshaller
{
    public static NativeUsbCameraInfo ConvertToUnmanaged(in UsbCameraInfo managed)
    {
        throw new System.NotSupportedException();
    }

    public static UsbCameraInfo ConvertToManaged(in NativeUsbCameraInfo unmanaged)
    {
        string[] otherPaths;
        if (unmanaged.otherPathsCount == 0)
        {
            otherPaths = [];
        }
        else
        {
            otherPaths = new string[unmanaged.otherPathsCount];
            for (int i = 0; i < otherPaths.Length; i++)
            {
                otherPaths[i] = Marshal.PtrToStringUTF8((nint)unmanaged.otherPaths[i]) ?? "";
            }
        }

        return new UsbCameraInfo()
        {
            Dev = unmanaged.dev,
            Name = Marshal.PtrToStringUTF8((nint)unmanaged.name) ?? "",
            Path = Marshal.PtrToStringUTF8((nint)unmanaged.path) ?? "",
            VendorId = unmanaged.vendorId,
            ProductId = unmanaged.productId,
            OtherPaths = otherPaths
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeUsbCameraInfo
    {
        public int dev;
        public byte* path;
        public byte* name;
        public int otherPathsCount;
        public byte** otherPaths;
        public int vendorId;
        public int productId;
    }
}
