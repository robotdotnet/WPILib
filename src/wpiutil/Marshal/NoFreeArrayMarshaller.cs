using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(NoFreeArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public static unsafe class NoFreeArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged
{

    public static T[]? AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        if (unmanaged is null)
            return null;

        return new T[numElements];
    }

    public static Span<T> GetManagedValuesDestination(T[]? managed)
        => managed;

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanagedValue, int numElements)
        => new(unmanagedValue, numElements);
}
