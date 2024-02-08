using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Handles;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

[NativeMarshalling(typeof(VideoEventMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly struct VideoEvent : INativeArrayFree<VideoEventMarshaller.NativeCsEvent>
{
    public required EventKind Kind { get; init; }

    public required CsListener Listener { get; init; }

    public static unsafe void FreeArray(VideoEventMarshaller.NativeCsEvent* ptr, int len)
    {
        CsNative.FreeEvents(ptr, len);
    }
}

[CustomMarshaller(typeof(VideoEvent), MarshalMode.ElementOut, typeof(VideoEventMarshaller))]
public static unsafe class VideoEventMarshaller
{

    public static NativeCsEvent ConvertToUnmanaged(in VideoEvent managed)
    {
        throw new System.NotSupportedException();
    }

    public static VideoEvent ConvertToManaged(in NativeCsEvent unmanaged)
    {
        throw new NotImplementedException();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeCsEvent
    {
        public EventKind kind;
        public int source;
        public int sink;
        public byte* name;
        public VideoMode mode;
        public int property;
        public PropertyKind propertyKind;
        public int value;
        public byte* valueStr;
        public int listener;
    }
}
