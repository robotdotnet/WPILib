using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

[NativeMarshalling(typeof(UsbCameraInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public struct UsbCameraInfo : INativeArrayFree<UsbCameraInfoMarshaller.NativeUsbCameraInfo>
{
    public int Dev { get; set; }
    public string Path { get; set; }
    public string Name { get; set; }
    public List<string>? OtherPaths { get; set; }
    public int VendorId { get; set; }
    public int ProductId { get; set; }

    public static unsafe void FreeArray(UsbCameraInfoMarshaller.NativeUsbCameraInfo* ptr, int len)
    {
        CsNative.FreeEnumeratedUsbCameras(ptr, len);
    }
}

[CustomMarshaller(typeof(UsbCameraInfo), MarshalMode.Default, typeof(UsbCameraInfoMarshaller))]
public static unsafe class UsbCameraInfoMarshaller
{
    public static NativeUsbCameraInfo ConvertToUnmanaged(in UsbCameraInfo managed)
    {
        throw new System.NotImplementedException();
    }

    public static UsbCameraInfo ConvertToManaged(in NativeUsbCameraInfo unmanaged)
    {
        throw new System.NotImplementedException();
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
