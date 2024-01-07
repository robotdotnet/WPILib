using System;
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

[CustomMarshaller(typeof(ServiceData[]), MarshalMode.ManagedToUnmanagedOut, typeof(ServiceDataArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe struct ServiceDataArrayMarshaller<GenericPlaceholder, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private ServiceData[]? managedStorage;

    public ServiceDataArrayMarshaller()
    {
        if (typeof(TUnmanagedElement) != typeof(ServiceDataRaw))
        {
            throw new InvalidOperationException("Unmanaged type must be ServiceDataRaw");
        }
    }

    public ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(int numElements)
    {
        length = numElements;
        return new ReadOnlySpan<TUnmanagedElement>(unmanagedStorage, numElements);
    }

    public Span<ServiceData> GetManagedValuesDestination(int numElements)
    {
        length = numElements;
        managedStorage = new ServiceData[numElements];
        return managedStorage;
    }

    public readonly void Free()
    {
        if (unmanagedStorage != null && length.HasValue)
        {
            MulticastServiceResolver.FreeMulticastServiceResolverData((ServiceDataRaw*)unmanagedStorage, length.Value);
        }
    }

    public void FromUnmanaged(TUnmanagedElement* unmanaged)
    {
        unmanagedStorage = unmanaged;
    }

    public readonly ServiceData[] ToManaged()
    {
        return managedStorage!;
    }
}
