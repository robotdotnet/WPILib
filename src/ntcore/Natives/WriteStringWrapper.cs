using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(WriteStringWrapper), MarshalMode.ManagedToUnmanagedIn, typeof(WriteStringWrapperMarshaller))]
public static unsafe class WriteStringWrapperMarshaller
{
    public static ref readonly byte GetPinnableReference(WriteStringWrapper managed)
    {
        return ref managed.Str.AsSpan().GetPinnableReference();
    }

    public static byte* ConvertToUnmanaged(WriteStringWrapper managed)
    {
        throw new NotSupportedException("Have to have to satify the marshaller");
    }
}

[NativeMarshalling(typeof(WriteStringWrapperMarshaller))]
public readonly ref struct WriteStringWrapper(string value)
{
    public readonly byte[] Str = Encoding.UTF8.GetBytes(value);

    public nuint Len => (nuint)Str.Length;

    public static implicit operator WriteStringWrapper(string value)
    {
        return new WriteStringWrapper(value);
    }
}
