using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(LogMessage), MarshalMode.ManagedToUnmanagedOut, typeof(NtLogMessageMarshaller))]
public static unsafe class NtLogMessageMarshaller
{
    public static LogMessage ConvertToManaged(in NtLogMessage unmanaged)
    {
        return new LogMessage {
            LogLevel = (int)unmanaged.level,
            Message = Utf8StringMarshaller.ConvertToManaged(unmanaged.message) ?? "",
            Filename = Utf8StringMarshaller.ConvertToManaged(unmanaged.filename) ?? "",
            Line = (int)unmanaged.line
        };
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NtLogMessage
{
    public uint level;
    public byte* filename;
    public uint line;
    public byte* message;
}
