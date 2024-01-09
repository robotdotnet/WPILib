using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Handles;

[CustomMarshaller(typeof(CustomMarshallerAttribute.GenericPlaceholder), MarshalMode.Default, typeof(NtHandleMarshaller<>))]
public static class NtHandleMarshaller<T> where T : struct, INtHandle
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
