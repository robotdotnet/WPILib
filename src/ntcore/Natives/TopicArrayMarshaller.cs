using System;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(TopicArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public static unsafe class TopicArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    public static T[] AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        if (typeof(TUnmanagedElement) != typeof(int))
        {
            throw new InvalidOperationException("This marshaller only supports int targets");
        }

        return new T[numElements];
    }

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanaged, int numElements) => new(unmanaged, numElements);

    public static Span<T> GetManagedValuesDestination(T[] managed) => new(managed);

    public static void Free(TUnmanagedElement* unmanaged)
    {
        NtCore.FreeCharArray((byte*)unmanaged);
    }
}
