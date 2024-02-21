using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Natives;

[CustomMarshaller(typeof(RawFrameWriter), MarshalMode.ManagedToUnmanagedIn, typeof(RawFrameWriterMarshaller))]
public unsafe ref struct RawFrameWriterMarshaller
{
    private NativeRawFrame rawFrame;
    private ReadOnlySpan<byte> data;

    public RawFrameWriterMarshaller()
    {
    }

    public void FromManaged(in RawFrameWriter managed)
    {
        data = managed.Data;
        rawFrame.freeCbData = null;
        rawFrame.capacity = (nuint)managed.Data.Length;
        rawFrame.size = (nuint)managed.Data.Length;
        rawFrame.width = managed.Width;
        rawFrame.height = managed.Height;
        rawFrame.stride = managed.Stride;
        rawFrame.pixelFormat = managed.PixelFormat;
    }
    public readonly ref readonly byte GetPinnableReference()
    {
        return ref data.GetPinnableReference();
    }

    public NativeRawFrame* ToUnmanaged()
    {
        if (data.Length != 0)
        {
            rawFrame.data = (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in data[0]));
        }
        return (NativeRawFrame*)Unsafe.AsPointer(ref rawFrame);
    }

    public readonly void Free()
    {
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct NativeRawFrame
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_AllocateRawFrameData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool AllocateRawFrameData(ref NativeRawFrame frame, nuint requestedSize);

    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeRawFrameData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeRawFrameData(ref NativeRawFrame frame);

    public byte* data;
    public delegate* unmanaged[Cdecl]<void*, void*, nuint, void> freeFunc;
    public void* freeCbData;
    public nuint capacity;
    public nuint size;
    public PixelFormat pixelFormat;
    public int width;
    public int height;
    public int stride;
}
