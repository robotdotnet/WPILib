using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(UnmanagedFreeArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe ref struct UnmanagedFreeArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged, INativeArrayFree<TUnmanagedElement>
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
        if (unmanagedStorage is not null && length.HasValue)
        {
            TUnmanagedElement.FreeArray(unmanagedStorage, length.Value);
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
