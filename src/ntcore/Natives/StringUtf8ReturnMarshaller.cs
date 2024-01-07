using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace NetworkTables.Natives;

public static unsafe partial class StringArrayFree
{
    [LibraryImport("ntcore", EntryPoint = "NT_FreeCharArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void Free(byte* arr);
}

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(StringUtf8ReturnMarshaller<>))]
[ContiguousCollectionMarshaller]
public static unsafe class StringUtf8ReturnMarshaller<TUnmanagedElement> where TUnmanagedElement : unmanaged
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
        StringArrayFree.Free((byte*)unmanaged);
        return ret;
    }

    public static Span<byte> GetManagedValuesDestination(string managed)
        => new Span<byte>();

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(TUnmanagedElement* unmanagedValue, int numElements)
        => new ReadOnlySpan<TUnmanagedElement>();
}
