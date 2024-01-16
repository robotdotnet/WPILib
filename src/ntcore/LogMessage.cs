using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables;

[NativeMarshalling(typeof(LogMessageMarshaller))]
[StructLayout(LayoutKind.Auto)]
public record struct LogMessage(int LogLevel, string Filename, int Line, string Message);

[CustomMarshaller(typeof(LogMessage), MarshalMode.ManagedToUnmanagedOut, typeof(LogMessageMarshaller))]
public static unsafe class LogMessageMarshaller
{
    public static LogMessage ConvertToManaged(in NativeLogMessage unmanaged)
    {
        return new LogMessage
        {
            LogLevel = (int)unmanaged.level,
            Message = Utf8StringMarshaller.ConvertToManaged(unmanaged.message) ?? "",
            Filename = Utf8StringMarshaller.ConvertToManaged(unmanaged.filename) ?? "",
            Line = (int)unmanaged.line
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct NativeLogMessage
    {
        public uint level;
        public byte* filename;
        public uint line;
        public byte* message;
    }

}
