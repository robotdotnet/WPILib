using System;
using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(string), MarshalMode.ElementOut, typeof(EmptyStringMarshaller))]
public static unsafe class EmptyStringMarshaller
{
    public static string ConvertToManaged(WpiStringMarshaller.WpiStringNative unmanaged)
    {
        return default!;
    }

    public static WpiStringMarshaller.WpiStringNative ConvertToUnmanaged(string managed)
    {
        return default;
    }
}
