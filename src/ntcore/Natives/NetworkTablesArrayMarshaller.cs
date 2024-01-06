using System;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(NetworkTablesArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe ref struct NetworkTablesArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    private static readonly Type[] supportedTypes = [typeof(NtTopicInfo)];

    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private T[]? managedStorage;

    public NetworkTablesArrayMarshaller()
    {
        if (!supportedTypes.Contains(typeof(TUnmanagedElement))) {
            throw new InvalidOperationException($"{typeof(TUnmanagedElement)} is not supported by SizedFreeArrayMarshaller");
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
            // TODO
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