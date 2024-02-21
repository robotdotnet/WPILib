using System.Runtime.InteropServices.Marshalling;

namespace WPINet.Natives;

[CustomMarshaller(typeof(ServiceData), MarshalMode.ElementOut, typeof(ServiceDataMarshaller))]
public static unsafe class ServiceDataMarshaller
{
    public static ServiceDataRaw ConvertToUnmanaged(ServiceData managed)
    {
        throw new NotSupportedException();
    }

    public static ServiceData ConvertToManaged(in ServiceDataRaw unmanaged)
    {
        (string, string)[] txt = new (string, string)[unmanaged.txtCount];
        for (int i = 0; i < unmanaged.txtCount; i++)
        {
            txt[i] = (Utf8StringMarshaller.ConvertToManaged(unmanaged.txtKeys[i]) ?? "", Utf8StringMarshaller.ConvertToManaged(unmanaged.txtValues[i]) ?? "");
        }
        return new ServiceData(unmanaged.ipv4Address, unmanaged.port, Utf8StringMarshaller.ConvertToManaged(unmanaged.serviceName) ?? "", Utf8StringMarshaller.ConvertToManaged(unmanaged.hostName) ?? "", txt);
    }
}
