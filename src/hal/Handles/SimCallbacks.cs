using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WPIHal.Handles;

public delegate void NotifyCallback(string name, HalValue value);
public delegate void NotifyCallbackUtf8(ReadOnlySpan<byte> name, HalValue value);

public delegate void BufferCallback(string name, Span<byte> buffer);
public delegate void BufferCallbackUtf8(ReadOnlySpan<byte> name, Span<byte> buffer);

public delegate void ConstBufferCallback(string name, ReadOnlySpan<byte> buffer);
public delegate void ConstBufferCallbackUtf8(ReadOnlySpan<byte> name, ReadOnlySpan<byte> buffer);

public class SimCallbacks
{
    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalNotifyCallback(byte* name, void* param, HalValue* value)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is NotifyCallback stringCallback)
        {
            string n = System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, *value);
        }
        else if (handle.Target is NotifyCallbackUtf8 utf8Callback)
        {
            ReadOnlySpan<byte> n = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(name);
            utf8Callback(n, *value);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalBufferCallback(byte* name, void* param, byte* buffer, uint count)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is BufferCallback stringCallback)
        {
            string n = System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, new(buffer, (int)count));
        }
        else if (handle.Target is BufferCallbackUtf8 utf8Callback)
        {
            ReadOnlySpan<byte> n = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(name);
            utf8Callback(n, new(buffer, (int)count));
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe void HalConstBufferCallback(byte* name, void* param, byte* buffer, uint count)
    {
        GCHandle handle = GCHandle.FromIntPtr((nint)param);
        if (handle.Target is ConstBufferCallback stringCallback)
        {
            string n = System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)name) ?? "";
            stringCallback(n, new(buffer, (int)count));
        }
        else if (handle.Target is ConstBufferCallbackUtf8 utf8Callback)
        {
            ReadOnlySpan<byte> n = MemoryMarshal.CreateReadOnlySpanFromNullTerminated(name);
            utf8Callback(n, new(buffer, (int)count));
        }
    }
}
