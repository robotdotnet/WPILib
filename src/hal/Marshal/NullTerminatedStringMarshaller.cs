using System.Runtime.InteropServices.Marshalling;

namespace WPIHal.Marshal;

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(NullTerminatedStringMarshaller<>))]
[CustomMarshaller(typeof(string), MarshalMode.ElementOut, typeof(NullTerminatedStringMarshaller<>))]
public static unsafe class NullTerminatedStringMarshaller<TFree> where TFree : INullTerminatedStringFree<byte>
{

    public static string? ConvertToManaged(byte* unmanaged)
    {
        string? ret = System.Runtime.InteropServices.Marshal.PtrToStringUTF8((nint)unmanaged);
        TFree.FreeString(unmanaged);
        return ret;
    }

    public static byte* ConvertToUnmanaged(string? managed)
    {
        throw new NotSupportedException();
    }
}
