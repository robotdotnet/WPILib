using System;
using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(CustomFreeArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe ref struct CustomFreeArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged, INativeArrayFree
{
    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private T[]? managedStorage;

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
            TUnmanagedElement.Free(unmanagedStorage, length.Value);
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