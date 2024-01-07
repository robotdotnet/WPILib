using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

public static unsafe partial class NtCore {
    [LibraryImport("ntcore", EntryPoint = "NT_ReadListenerQueue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(CustomFreeArrayMarshaller<,>), CountElementName = nameof(len))]
    public static partial NetworkTableEvent[] ReadListenerQueue(int poller, out nuint len);
}
