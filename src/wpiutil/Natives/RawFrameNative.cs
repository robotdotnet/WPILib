using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// cbdata, data, capacity
using unsafe FreeFunc = delegate* unmanaged[Cdecl]<void*, void*, nuint, void>;

namespace WPIUtil.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct RawFrameRaw
{
    public byte* data;
    public FreeFunc freeFunc;
    public void* freeCbData;
    public nuint capacity;
    public nuint size;
    public int pixelFormat;
    public int width;
    public int height;
    public int stride;
}

public static partial class RawFrameNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_AllocateRawFrameData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.U4)]
    public static unsafe partial bool AllocateRawFrameData(ref RawFrameRaw frame, nuint requestedSize);

    [LibraryImport("wpiutil", EntryPoint = "WPI_FreeRawFrameData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void FreeRawFrameData(ref RawFrameRaw frame);

    [LibraryImport("wpiutil", EntryPoint = "WPI_SetRawFrameData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void SetRawFrameData(ref RawFrameRaw frame, void* data, nuint size, nuint capacity, void* cbData, FreeFunc freeFunc);
}
