using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Natives;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(WpiString), MarshalMode.ManagedToUnmanagedIn, typeof(PassTo))]
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(ReceiveFromString))]
[CustomMarshaller(typeof(string), MarshalMode.ElementOut, typeof(ReceiveFromArray))]
[CustomMarshaller(typeof(byte[]), MarshalMode.ManagedToUnmanagedOut, typeof(ReceiveFromByteArray))]
[CustomMarshaller(typeof(string), MarshalMode.ElementIn, typeof(PassStringToArray))]
public static unsafe class WpiStringMarshaller
{
    public static class ReceiveFromArray
    {
        public static string ConvertToManaged(WpiStringNative unmanaged)
        {
            if (unmanaged.Len == 0)
            {
                return "";
            }
            string buf = Encoding.UTF8.GetString(unmanaged.Str, checked((int)unmanaged.Len));
            return buf;
        }

        public static WpiStringNative ConvertToUnmanaged(string managed)
        {
            throw new NotSupportedException();
        }
    }

    public static class ReceiveFromString
    {
        public static string ConvertToManaged(WpiStringNative unmanaged)
        {
            if (unmanaged.Len == 0)
            {
                StringsNative.FreeString(&unmanaged);
                return "";
            }
            string buf = Encoding.UTF8.GetString(unmanaged.Str, checked((int)unmanaged.Len));
            StringsNative.FreeString(&unmanaged);
            return buf;
        }
    }

    public static class ReceiveFromByteArray
    {
        public static byte[] ConvertToManaged(WpiStringNative unmanaged)
        {
            if (unmanaged.Len == 0)
            {
                StringsNative.FreeString(&unmanaged);
                return [];
            }
            byte[] buf = new byte[unmanaged.Len];
            new ReadOnlySpan<byte>(unmanaged.Str, checked((int)unmanaged.Len)).CopyTo(buf);
            StringsNative.FreeString(&unmanaged);
            return buf;
        }
    }

    public static class PassStringToArray
    {
        public static WpiStringNative ConvertToUnmanaged(string? managed)
        {
            if (managed is null || managed.Length == 0)
            {
                return new WpiStringNative(null, 0);
            }

            int exactByteCount = Encoding.UTF8.GetByteCount(managed);
            byte* mem = (byte*)NativeMemory.Alloc((nuint)exactByteCount);
            Span<byte> buffer = new(mem, exactByteCount);

            int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
            Debug.Assert(byteCount == exactByteCount);
            return new WpiStringNative(mem, (nuint)exactByteCount);
        }

        public static void Free(WpiStringNative unmanaged)
        {
            byte* ptr = unmanaged.Str;
            if (ptr is not null)
            {
                NativeMemory.Free(ptr);
            }
        }

        public static string ConvertToManaged(WpiStringNative unmanaged)
        {
            throw new NotSupportedException();
        }
    }

    public ref struct PassTo
    {
        public static int BufferSize => 256;
        private ReadOnlySpan<byte> data;
        private WpiStringNative nativeString;
        public void FromManaged(WpiString managed, Span<byte> callerAllocatedBuffer)
        {
            if (managed.isString)
            {
                ReadOnlySpan<char> strBuf = MemoryMarshal.Cast<byte, char>(managed.buffer);
                int exactByteCount = Encoding.UTF8.GetByteCount(strBuf);
                if (exactByteCount > callerAllocatedBuffer.Length)
                {
                    callerAllocatedBuffer = new byte[exactByteCount];
                }
                int byteCount = Encoding.UTF8.GetBytes(strBuf, callerAllocatedBuffer);
                Debug.Assert(byteCount == exactByteCount);
                data = callerAllocatedBuffer[..byteCount];
            }
            else
            {
                data = managed.buffer;
            }
        }

        public readonly ref readonly byte GetPinnableReference()
        {
            return ref data.GetPinnableReference();
        }

        public WpiStringNative* ToUnmanaged()
        {
            if (data.Length == 0)
            {
                nativeString = new WpiStringNative(null, 0);
            }
            else
            {
                // Getting a pointer to the span is safe here because we've pinned it due to GetPinnableReference()
                nativeString = new((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in data[0])), (nuint)data.Length);
            }
            // AsPointer is safe here due to being inside of a ref struct
            return (WpiStringNative*)Unsafe.AsPointer(ref nativeString);
        }

        public readonly void Free()
        {
            // Purposely empty
        }
    }

    // Cannot be a ref struct due to being used as a parameter to ReadOnlySpanMarshaller in the ElementIn case
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct WpiStringNative(byte* str, nuint len) : INativeArrayFree<WpiStringNative>
    {
        public Ptr<byte> Str = str;
        public nuint Len = len;

        public WpiStringNative() : this(null, 0)
        {

        }

        public static void FreeArray(WpiStringNative* array, int len)
        {
            StringsNative.FreeStringArray(array, (nuint)len);
        }

        public readonly string ConvertToString()
        {
            if (Len == 0)
            {
                return "";
            }

            return Encoding.UTF8.GetString(Str, checked((int)Len));
        }
    }
}
