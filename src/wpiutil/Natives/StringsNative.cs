using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace WPIUtil.Natives;

// Cannot be a ref struct due to being used as a parameter to ReadOnlySpanMarshaller in the ElementIn case
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct WpiConstString(byte* str, nuint len)
{
    public readonly byte* Str = str;
    public readonly nuint Len = len;
}

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedIn, typeof(PassStringTo))]
[CustomMarshaller(typeof(string), MarshalMode.ElementIn, typeof(PassStringToArray))]
[CustomMarshaller(typeof(ReadOnlySpan<byte>), MarshalMode.ManagedToUnmanagedIn, typeof(PassSpanIn))]
public static unsafe class WpiConstStringMarshaller
{
    public ref struct PassSpanIn
    {
        private ReadOnlySpan<byte> data;
        private WpiConstString nativeString;

        public void FromManaged(ReadOnlySpan<byte> managed)
        {
            data = managed;
        }

        public readonly ref readonly byte GetPinnableReference()
        {
            return ref data.GetPinnableReference();
        }

        public WpiConstString* ToUnmanaged()
        {
            if (data.Length == 0)
            {
                nativeString = new WpiConstString(null, 0);
            }
            else
            {
                // Getting a pointer to the span is safe here because we've pinned it due to GetPinnableReference()
                nativeString = new((byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in data[0])), (nuint)data.Length);
            }
            // AsPointer is safe here due to being inside of a ref struct
            return (WpiConstString*)Unsafe.AsPointer(ref nativeString);
        }

        public void Free()
        {
            // Throw is here to satisfy the marshaller. I would LOVE to remove if, as that would
            // remove the try finally from the marshaller
        }
    }

    public static class PassStringToArray
    {
        public static WpiConstString ConvertToUnmanaged(string? managed)
        {
            if (managed is null || managed.Length == 0)
            {
                return new WpiConstString(null, 0);
            }

            int exactByteCount = Encoding.UTF8.GetByteCount(managed);
            byte* mem = (byte*)NativeMemory.Alloc((nuint)exactByteCount);
            Span<byte> buffer = new(mem, exactByteCount);

            int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
            Debug.Assert(byteCount == exactByteCount);
            return new WpiConstString(mem, (nuint)exactByteCount);
        }

        public static void Free(WpiConstString unmanaged)
        {
            byte* ptr = unmanaged.Str;
            if (ptr != null)
            {
                NativeMemory.Free(ptr);
            }
        }

        public static string ConvertToManaged(WpiConstString unmanaged)
        {
            throw new NotSupportedException();
        }
    }

    public ref struct PassStringTo
    {
        public static int BufferSize => 256;
        private bool wasAllocated;
        private WpiConstString nativeString;
        public void FromManaged(string? managed, Span<byte> callerAllocatedBuffer)
        {
            wasAllocated = false;

            if (managed is null || managed.Length == 0)
            {
                nativeString = new WpiConstString(null, 0);
                return;
            }

            int exactByteCount = Encoding.UTF8.GetByteCount(managed);
            if (exactByteCount <= callerAllocatedBuffer.Length)
            {
                nativeString = new((byte*)Unsafe.AsPointer(ref callerAllocatedBuffer[0]), (nuint)exactByteCount);
            }
            else
            {
                nativeString = new((byte*)NativeMemory.Alloc((nuint)exactByteCount), (nuint)exactByteCount);
                wasAllocated = true;
            }

            Span<byte> buffer = new(nativeString.Str, exactByteCount);

            int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
            Debug.Assert(byteCount == exactByteCount);

        }

        public WpiConstString* ToUnmanaged()
        {
            // AsPointer is safe here due to being inside of a ref struct
            return (WpiConstString*)Unsafe.AsPointer(ref nativeString);
        }

        public readonly void Free()
        {
            if (!wasAllocated)
            {
                return;
            }
            byte* ptr = nativeString.Str;
            if (ptr != null)
            {
                NativeMemory.Free(ptr);
            }
        }
    }
}


public static partial class StringsNative
{
    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void TakesString([MarshalUsing(typeof(WpiConstStringMarshaller))] string value);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void TakesSpan([MarshalUsing(typeof(WpiConstStringMarshaller))] ReadOnlySpan<byte> value);

    [LibraryImport("wpiutil", EntryPoint = "WPI_DestroyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void TakesStringArray([MarshalUsing(typeof(WpiConstStringMarshaller), ElementIndirectionDepth = 1)] ReadOnlySpan<string> value);
}
