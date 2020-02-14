using System;
using System.Collections.Generic;
using System.Text;

namespace WPIUtil
{
    public struct UTF8String : IEquatable<UTF8String>
    {
        private readonly byte[] buffer;

        /// <summary>
        /// The buffer to the string. Null terminated
        /// </summary>
        public ReadOnlySpan<byte> Buffer => buffer;
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
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }
            var encoding = Encoding.UTF8;
            var bytes = encoding.GetByteCount(str);
            buffer = new byte[bytes + 1];
            encoding.GetBytes(str, 0, str.Length, buffer, 0);
            buffer[bytes] = 0;
        }

        /// <summary>
        /// Constructs a managed UTF8 string from a C# string
        /// </summary>
        /// <param name="str"></param>
        public unsafe UTF8String(ReadOnlySpan<char> str)
        {
            if (str.IsEmpty)
            {
                buffer = new byte[1] { 0 };
                return;
            }

            var encoding = Encoding.UTF8;
            fixed (char* strP = str)
            {
                var bytes = encoding.GetByteCount(strP, str.Length);
                buffer = new byte[bytes + 1];
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
        /// Reads a UTF8 string from a native pointer.
        /// </summary>
        /// <param name="str">The pointer to read from</param>
        /// <param name="size">The length of the string</param>
        /// <returns>The managed string</returns>
        public static unsafe string ReadUTF8String(byte* str, UIntPtr size)
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

        public override bool Equals(object? obj)
        {
            return obj is UTF8String @string && Equals(@string);
        }

        public bool Equals(UTF8String other)
        {
            return EqualityComparer<byte[]>.Default.Equals(buffer, other.buffer);
        }

        public override int GetHashCode()
        {
            return 143091379 + EqualityComparer<byte[]>.Default.GetHashCode(buffer);
        }

        public static bool operator ==(UTF8String left, UTF8String right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UTF8String left, UTF8String right)
        {
            return !(left == right);
        }
    }
}
