using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Handles;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(WPIIntHandleMarshaller<>))]
public static class WPIIntHandleMarshaller<T> where T : struct, IWPIIntHandle
{
    public static int ConvertToUnmanaged(T managed)
    {
        return managed.Handle;
    }

    public static T ConvertToManaged(int unmanaged)
    {
        T ret = new()
        {
            Handle = unmanaged
        };
        return ret;
    }
}
