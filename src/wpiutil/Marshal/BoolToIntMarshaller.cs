using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Marshal;

[CustomMarshaller(typeof(bool), MarshalMode.Default, typeof(BoolToIntMarshaller))]
public static class BoolToIntMarshaller
{
    public static int ConvertToUnmanaged(bool managed)
    {
        return managed ? 1 : 0;
    }

    public static bool ConvertToManaged(int unmanaged)
    {
        return unmanaged != 0;
    }
}
