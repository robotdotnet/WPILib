using System;
using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Natives;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder[]), MarshalMode.ManagedToUnmanagedOut, typeof(StaticArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public static unsafe class StaticArrayMarshaller<T, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    /// <summary>
    /// Allocates memory for the managed representation of the array.
    /// </summary>
    /// <param name="unmanaged">The unmanaged array.</param>
    /// <param name="numElements">The unmanaged element count.</param>
    /// <returns>The managed array.</returns>
    public static T[]? AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        if (unmanaged is null)
            return null;

        return new T[numElements];
    }

    /// <summary>
    /// Gets a destination for the managed elements in the array.
    /// </summary>
    /// <param name="managed">The managed array.</param>
    /// <returns>The <see cref="Span{T}"/> of managed elements.</returns>
    public static Span<T> GetManagedValuesDestination(T[]? managed)
        => managed;

    /// <summary>
    /// Gets a source for the unmanaged elements in the array.
    /// </summary>
    /// <param name="unmanagedValue">The unmanaged array.</param>
    /// <param name="numElements">The unmanaged element count.</param>
    /// <returns>The <see cref="ReadOnlySpan{TUnmanagedElement}"/> containing the unmanaged elements to marshal.</returns>
    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanagedValue, int numElements)
        => new(unmanagedValue, numElements);
}
