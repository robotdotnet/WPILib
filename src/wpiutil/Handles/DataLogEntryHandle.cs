using System.Runtime.InteropServices.Marshalling;

#pragma warning disable CA1716
namespace WPIUtil.Handles;
#pragma warning restore CA1716

[NativeMarshalling(typeof(WPIIntHandleMarshaller<DataLogEntryHandle>))]
public record struct DataLogEntryHandle(int Handle) : IWPIIntHandle;
