using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Handles;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

[NativeMarshalling(typeof(VideoEventMarshaller))]
[StructLayout(LayoutKind.Auto)]
public readonly struct VideoEvent(in VideoEventMarshaller.NativeCsEvent csEvent) : INativeArrayFree<VideoEventMarshaller.NativeCsEvent>
{
    public EventKind Kind { get; } = csEvent.kind;
    public CsListener Listener { get; } = new CsListener(csEvent.listener);

    public VideoEvent() : this(default)
    {

    }

    public static unsafe void FreeArray(VideoEventMarshaller.NativeCsEvent* array, int len)
    {
        CsNative.FreeEvents(array, len);
    }
}

[StructLayout(LayoutKind.Explicit)]
internal struct VideoEventUnion
{
    [FieldOffset(0)]
    public CsSource source;
    [FieldOffset(0)]
    public CsSink sink;
    [FieldOffset(0)]
    public VideoMode mode;
    [FieldOffset(0)]
    public PropertyTuple property;
}

[StructLayout(LayoutKind.Auto)]
internal struct PropertyTuple
{
    public CsProperty property;
    public PropertyKind kind;
    public int value;
}

[CustomMarshaller(typeof(VideoEvent), MarshalMode.ElementOut, typeof(VideoEventMarshaller))]
public static unsafe class VideoEventMarshaller
{

    public static NativeCsEvent ConvertToUnmanaged(in VideoEvent managed)
    {
        throw new NotSupportedException();
    }

    public static VideoEvent ConvertToManaged(in NativeCsEvent unmanaged)
    {
        return new(unmanaged);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeCsEvent
    {
        public EventKind kind;
        public int source;
        public int sink;
        public WpiStringMarshaller.WpiStringNative name;
        public VideoMode mode;
        public int property;
        public PropertyKind propertyKind;
        public int value;
        public WpiStringMarshaller.WpiStringNative valueStr;
        public int listener;
    }
}
