using System;
using System.Runtime.InteropServices.Marshalling;
using CsCore.Natives;
using WPIUtil.Marshal;

namespace CsCore;

public class HttpCameraUrlsStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* ptr)
    {
    }
}

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(HttpCameraUrlsArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe ref struct HttpCameraUrlsArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private T[]? managedStorage;

    public HttpCameraUrlsArrayMarshaller()
    {
        if (typeof(TUnmanagedElement) != typeof(byte*))
        {
            throw new InvalidOperationException("Target can only be byte*");
        }
    }

    public ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(int numElements)
    {
        length = numElements;
        return new ReadOnlySpan<TUnmanagedElement>(unmanagedStorage, numElements);
    }

    public Span<T> GetManagedValuesDestination(int numElements)
    {
        length = numElements;
        managedStorage = new T[numElements];
        return managedStorage;
    }

    public readonly void Free()
    {
        if (unmanagedStorage != null && length.HasValue)
        {
            CsNative.FreeHttpCameraUrls((byte**)unmanagedStorage, length.Value);
        }
    }

    public void FromUnmanaged(TUnmanagedElement* unmanaged)
    {
        unmanagedStorage = unmanaged;
    }

    public readonly T[] ToManaged()
    {
        return managedStorage!;
    }
}
