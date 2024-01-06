using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(TopicArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public static unsafe class TopicArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged {
    public static T[] AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        throw new System.NotImplementedException();
    }

    public static System.ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanaged, int numElements)
    {
        throw new System.NotImplementedException();
    }

    public static System.Span<TUnmanagedElement> GetManagedValuesDestination(T[] managed)
    {
        throw new System.NotImplementedException();
    }

    public static void Free(TUnmanagedElement* unmanaged) {

    }
}