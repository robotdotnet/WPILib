using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace WPIHal;

[NativeMarshalling(typeof(MatchInfoMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct MatchInfo(string EventName, MatchType MatchType, int MatchNumber, int ReplayNumber, string GameSpecificMessage);

[CustomMarshaller(typeof(MatchInfo), MarshalMode.ManagedToUnmanagedOut, typeof(MatchInfoMarshaller))]
[CustomMarshaller(typeof(MatchInfo), MarshalMode.ManagedToUnmanagedIn, typeof(MatchInfoMarshaller))]
public static class MatchInfoMarshaller
{
    public static unsafe MatchInfo ConvertToManaged(NativeMatchInfo unmanaged)
    {
        return new MatchInfo
        {
            EventName = unmanaged.eventName.FromNullTerminatedString(),
            MatchType = unmanaged.matchType,
            MatchNumber = unmanaged.matchNumber,
            ReplayNumber = unmanaged.replayNumber,
            GameSpecificMessage = unmanaged.gameSpecificMessage.FromByteString(unmanaged.gameSpecificMessageSize)
        };
    }

    public static NativeMatchInfo ConvertToUnmanaged(in MatchInfo managed)
    {
        throw new NotImplementedException();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeMatchInfo
    {
        [System.Runtime.CompilerServices.InlineArray(64)]
        public struct Utf8StringBuffer
        {
            private byte _element0;

            public readonly unsafe string FromNullTerminatedString()
            {
                ReadOnlySpan<byte> thisSpan = this;
                fixed (byte* b = thisSpan)
                {
                    return System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)b)!;
                }
            }

            public readonly unsafe byte[] FromRawBytes(int length)
            {
                byte[] ret = new byte[int.Min(length, 64)];
                ReadOnlySpan<byte> thisSpan = this;
                thisSpan[..ret.Length].CopyTo(ret.AsSpan());
                return ret;
            }

            public readonly unsafe string FromByteString(int length)
            {
                ReadOnlySpan<byte> thisSpan = this;
                return Encoding.UTF8.GetString(thisSpan[..length]);
            }
        }

        public Utf8StringBuffer eventName;
        public MatchType matchType;
        public ushort matchNumber;
        public byte replayNumber;
        public Utf8StringBuffer gameSpecificMessage;
        public ushort gameSpecificMessageSize;
    }
}
