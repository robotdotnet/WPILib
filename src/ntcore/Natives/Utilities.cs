using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace NetworkTables.Natives
{
    internal static class Utilities
    {
        internal static unsafe void CreateNtString(ReadOnlySpan<char> vStr, NtString* nStr)
        {
            fixed (char* str = vStr)
            {
                var encoding = Encoding.UTF8;
                int bytes = encoding.GetByteCount(str, vStr.Length);
                nStr->str = (byte*)Marshal.AllocHGlobal((bytes) * sizeof(byte));
                nStr->len = (UIntPtr)bytes;
                encoding.GetBytes(str, vStr.Length, nStr->str, bytes);
            }
        }

        internal static unsafe void CreateNtString(ReadOnlyMemory<char> vStr, NtString* nStr)
        {
            fixed (char* str = vStr.Span)
            {
                var encoding = Encoding.UTF8;
                int bytes = encoding.GetByteCount(str, vStr.Length);
                nStr->str = (byte*)Marshal.AllocHGlobal((bytes) * sizeof(byte));
                nStr->len = (UIntPtr)bytes;
                encoding.GetBytes(str, vStr.Length, nStr->str, bytes);
            }
        }

        internal static unsafe void CreateNtString(string vStr, NtString* nStr)
        {
            fixed (char* str = vStr)
            {
                var encoding = Encoding.UTF8;
                int bytes = encoding.GetByteCount(str, vStr.Length);
                nStr->str = (byte*)Marshal.AllocHGlobal((bytes) * sizeof(byte));
                nStr->len = (UIntPtr)bytes;
                encoding.GetBytes(str, vStr.Length, nStr->str, bytes);
            }
        }


        internal static unsafe void DisposeNtString(NtString* str)
        {
            Marshal.FreeHGlobal((IntPtr)str->str);
        }
    }
}
