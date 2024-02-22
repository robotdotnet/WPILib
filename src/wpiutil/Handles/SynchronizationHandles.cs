using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Handles;

public interface IWpiSynchronizationHandle : IWPIIntHandle;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<WpiSemaphoreHandle>))]
public record struct WpiSemaphoreHandle(int Handle) : IWPIIntHandle, IWpiSynchronizationHandle
{
}

[NativeMarshalling(typeof(WPIIntHandleMarshaller<WpiEventHandle>))]
public record struct WpiEventHandle(int Handle) : IWPIIntHandle, IWpiSynchronizationHandle
{
}
