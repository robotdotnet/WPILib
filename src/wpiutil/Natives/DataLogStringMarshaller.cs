using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace WPIUtil.Natives;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(DataLogStringMarshaller))]
public static unsafe class DataLogStringMarshaller
{
    public static DataLogString ConvertToUnmanaged(string? managed)
    {
        if (managed is null || managed.Length == 0)
        {
            return new DataLogString()
            {
                str = null,
                len = 0
            };
        }

        int exactByteCount = checked(Encoding.UTF8.GetByteCount(managed));
        byte* mem = (byte*)Marshal.AllocCoTaskMem(exactByteCount);
        Span<byte> buffer = new(mem, exactByteCount);

        int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
        return new DataLogString()
        {
            str = mem,
            len = (nuint)byteCount,
        };
    }

    /// <summary>
    /// Free the memory for a specified unmanaged string.
    /// </summary>
    /// <param name="unmanaged">The memory allocated for the unmanaged string.</param>
    public static void Free(DataLogString unmanaged)
    {
        if (unmanaged.str != null)
        {
            Marshal.FreeCoTaskMem((nint)unmanaged.str);
        }
    }

    public static string ConvertToManaged(DataLogString unmanaged)
    {
        if (unmanaged.str == null || unmanaged.len == 0)
        {
            return string.Empty;
        }

        return Marshal.PtrToStringUTF8((nint)unmanaged.str, checked((int)unmanaged.len));
    }
}
