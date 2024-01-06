using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

public static unsafe partial class TopicArrayFree
{
    [LibraryImport("ntcore", EntryPoint = "NT_FreeCharArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Free(int* arr);
}

[CustomMarshaller(typeof(int[]), MarshalMode.ManagedToUnmanagedOut, typeof(TopicArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public static unsafe class TopicArrayMarshaller<GenericPlaceholder, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    /// <summary>
    /// Allocates memory for the managed representation of the array.
    /// </summary>
    /// <param name="unmanaged">The unmanaged array.</param>
    /// <param name="numElements">The unmanaged element count.</param>
    /// <returns>The managed array.</returns>
    public static int[]? AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        if (typeof(TUnmanagedElement) != typeof(int))
        {
            throw new InvalidOperationException("Unmanaged type must be int");
        }

        if (unmanaged is null)
            return null;

        return new int[numElements];
    }

    /// <summary>
    /// Gets a destination for the managed elements in the array.
    /// </summary>
    /// <param name="managed">The managed array.</param>
    /// <returns>The <see cref="Span{T}"/> of managed elements.</returns>
    public static Span<int> GetManagedValuesDestination(int[]? managed)
        => managed;

    /// <summary>
    /// Gets a source for the unmanaged elements in the array.
    /// </summary>
    /// <param name="unmanagedValue">The unmanaged array.</param>
    /// <param name="numElements">The unmanaged element count.</param>
    /// <returns>The <see cref="ReadOnlySpan{TUnmanagedElement}"/> containing the unmanaged elements to marshal.</returns>
    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanagedValue, int numElements)
        => new(unmanagedValue, numElements);

    public static void Free(TUnmanagedElement* unmanagedValue)
    {
        TopicArrayFree.Free((int*)unmanagedValue);
    }
}
