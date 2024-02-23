using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Handles;

#pragma warning disable CA1000 // Do not declare static members on generic types
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
#pragma warning restore CA1000 // Do not declare static members on generic types