using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(string), MarshalMode.ElementIn, typeof(PassToArray))]
public static unsafe class NtStringMarshaller
{
    public static string ManagedConvert(NtString unmanaged)
    {
        if (unmanaged.str is null || unmanaged.len is 0)
        {
            return "";
        }

        return Marshal.PtrToStringUTF8((nint)unmanaged.str, checked((int)unmanaged.len));
    }

    public static class PassToArray
    {
        public static NtString ConvertToUnmanaged(string? managed)
        {
            if (managed is null || managed.Length == 0)
            {
                return new NtString()
                {
                    str = (byte*)Marshal.AllocCoTaskMem(0),
                    len = 0
                };
            }

            int exactByteCount = Encoding.UTF8.GetByteCount(managed);
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
            throw new NotSupportedException();
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtString : INativeFree<NtString>
{
    public byte* str;
    public nuint len;

    public static void Free(NtString* ptr)
    {
        NtCore.DisposeString(ptr);
    }
}
