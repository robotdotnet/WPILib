using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(NtLengthStringMarshaller<>))]
[ContiguousCollectionMarshaller]
public static unsafe class NtLengthStringMarshaller<TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    public static string AllocateContainerForManagedElements(TUnmanagedElement* unmanaged, int numElements)
    {
        if (unmanaged is null)
            return "";

        if (typeof(TUnmanagedElement) != typeof(byte))
        {
            throw new InvalidOperationException("TUnmanagedElement must be byte");
        }

        string ret = Encoding.UTF8.GetString((byte*)unmanaged, numElements);
        NtCore.FreeCharArray((byte*)unmanaged);
        return ret;
    }

    public static Span<byte> GetManagedValuesDestination(string managed)
        => new();

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanagedValue, int numElements)
        => new();
}
