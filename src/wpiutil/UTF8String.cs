using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil
{
    public class UTF8String
    {
        /// <summary>
        /// The buffer to the string. Do not modify this array. Null terminated
        /// </summary>
        public byte[] Buffer { get; }
        /// <summary>
        /// The length of this string, not including the null terminator
        /// </summary>
        public UIntPtr Length
        {
            get
            {
                return (UIntPtr)(Buffer.Length - 1);
            }
        }

        /// <summary>
        /// Constructs a managed UTF8 string from a C# string
        /// </summary>
        /// <param name="str"></param>
        public UTF8String(string str)
        {
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetByteCount(str);
            Buffer = new byte[bytes + 1];
            encoding.GetBytes(str, 0, str.Length, Buffer, 0);
            Buffer[bytes] = 0;
        }

        /// <summary>
        /// Constructs a managed UTF8 string from a C# string
        /// </summary>
        /// <param name="str"></param>
        public unsafe UTF8String(ReadOnlySpan<char> str)
        {
            if (str.IsEmpty)
            {
                Buffer = new byte[1] { 0 };
                return;
            }

            var encoding = Encoding.UTF8;
            fixed (char* strP = str)
            {
                var bytes = encoding.GetByteCount(strP, str.Length);
                Buffer = new byte[bytes + 1];
                fixed (byte* b = Buffer)
                {
                    encoding.GetBytes(strP, str.Length, b, bytes);
                    b[bytes] = 0;
                }
            }
            
        }

        /// <summary>
        /// Reads a UTF8 string from a native pointer.
        /// </summary>
        /// <param name="str">The pointer to read from</param>
        /// <param name="size">The length of the string</param>
        /// <returns>The managed string</returns>
        public static unsafe string ReadUTF8String(byte* str, int size)
        {
            return Encoding.UTF8.GetString(str, (int)size);
        }

        /// <summary>
        /// Reads a UTF8 string from a null termincated native pointer
        /// </summary>
        /// <param name="str">The pointer to read from (must be null terminated)</param>
        /// <returns>The managed string</returns>
        public static unsafe string ReadUTF8String(byte* str)
        {
            byte* data = str;
            int count = 0;
            while (true)
            {
                if (data[count] == 0)
                {
                    break;
                }
                count++;
            }

            return ReadUTF8String(str, count);
        }
    }
}
