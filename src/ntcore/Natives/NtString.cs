using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(string), MarshalMode.ElementIn, typeof(NtStringMarshaller))]
public static unsafe class NtStringMarshaller
{
    public static NtString ConvertToUnmanaged(string? managed)
    {
        if (managed is null || managed.Length == 0)
        {
            return new NtString()
            {
                str = null,
                len = 0
            };
        }

        int exactByteCount = checked(Encoding.UTF8.GetByteCount(managed));
        byte* mem = (byte*)Marshal.AllocCoTaskMem(exactByteCount);
        Span<byte> buffer = new(mem, exactByteCount);

        int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
        return new NtString()
        {
            str = mem,
            len = (nuint)byteCount,
        };
    }

    public static void Free(NtString unmanaged)
    {
        if (unmanaged.str != null)
        {
            Marshal.FreeCoTaskMem((nint)unmanaged.str);
        }
    }

    public static string ConvertToManaged(NtString unmanaged)
    {
        if (unmanaged.str == null || unmanaged.len == 0)
        {
            return string.Empty;
        }

        return Marshal.PtrToStringUTF8((nint)unmanaged.str, checked((int)unmanaged.len));
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtString: INativeFree<NtString>
{
    public byte* str;
    public nuint len;

    public static void Free(NtString* ptr)
    {
        NtCore.DisposeString(ptr);
    }
}
