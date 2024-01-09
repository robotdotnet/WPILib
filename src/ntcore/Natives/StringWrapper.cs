using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(StringWrapper), MarshalMode.ManagedToUnmanagedIn, typeof(StringUtf8WrapperMarshaller))]
public static unsafe class StringUtf8WrapperMarshaller
{
    public static ref readonly byte GetPinnableReference(StringWrapper managed)
    {
        return ref managed.Str.AsSpan().GetPinnableReference();
    }

    public static byte* ConvertToUnmanaged(StringWrapper managed)
    {
        throw new NotSupportedException("Have to have to satify the marshaller");
    }
}

[NativeMarshalling(typeof(StringUtf8WrapperMarshaller))]
public readonly ref struct StringWrapper(string value)
{
    public readonly byte[] Str = Encoding.UTF8.GetBytes(value);

    public nuint Len => (nuint)Str.Length;

    public static implicit operator StringWrapper(string value)
    {
        return new StringWrapper(value);
    }
}
