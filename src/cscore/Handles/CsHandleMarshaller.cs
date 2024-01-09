using System.Runtime.InteropServices.Marshalling;

namespace CsCore.Handles;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(CsHandleMarshaller<>))]
public static class CsHandleMarshaller<T> where T : struct, ICsHandle
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
