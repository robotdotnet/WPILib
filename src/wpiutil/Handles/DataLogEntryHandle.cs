using System.Runtime.InteropServices.Marshalling;

namespace WPIUtil.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<DataLogEntryHandle>))]
public record struct DataLogEntryHandle(int Handle) : IWPIIntHandle;
