using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace WPIUtil.Marshal;

public unsafe interface IStringLengthPair
{
    public byte* Ptr { get; set; }
    public nuint Len { get; set; }
}

[CustomMarshaller(typeof(string), MarshalMode.ElementIn, typeof(StringLengthPairMarshaller<>.PassToArray))]
public static unsafe class StringLengthPairMarshaller<T> where T : unmanaged, IStringLengthPair
{
    public static string? ManagedConvert(T unmanaged)
    {
        byte* ptr = unmanaged.Ptr;
        if (ptr is null)
        {
            return null;
        }

        string? ret = System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)ptr, checked((int)unmanaged.Len));
        return ret;
    }

    public static class PassToArray
    {
        public static T ConvertToUnmanaged(string? managed)
        {
            if (managed is null || managed.Length == 0)
            {
                return new T
                {
                    Ptr = (byte*)NativeMemory.Alloc(0),
                    Len = 0
                };
            }

            int exactByteCount = Encoding.UTF8.GetByteCount(managed);
            byte* mem = (byte*)NativeMemory.Alloc((nuint)exactByteCount);
            Span<byte> buffer = new(mem, exactByteCount);

            int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
            return new T
            {
                Ptr = mem,
                Len = (nuint)byteCount,
            };
        }

        public static void Free(T unmanaged)
        {
            byte* ptr = unmanaged.Ptr;
            if (ptr != null)
            {
                NativeMemory.Free(ptr);
            }
        }

        public static string ConvertToManaged(T unmanaged)
        {
            throw new NotSupportedException();
        }
    }
}
