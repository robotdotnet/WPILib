using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Handles;

namespace WPIUtil.Handles;

[NativeMarshalling(typeof(WPIIntHandleMarshaller<DataLogEntryHandle>))]
public record struct DataLogEntryHandle(int Handle) : IWPIIntHandle;
