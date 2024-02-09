using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIHal.Handles;
using WPIHal.Marshal;
using WPIUtil.Marshal;

namespace WPIHal.Natives;

public static partial class HalBase
{
    [LibraryImport("wpiHal", EntryPoint = "HAL_Initialize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool Initialize(int timeout, int mode);

    [LibraryImport("wpiHal", EntryPoint = "HAL_ExpandFPGATime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial ulong ExpandFPGATimeRefShim(uint unexpandedLower, ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetBrownedOut")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetBrownedOutRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetErrorMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<NoFreeNullTerminatedStringFree>))]
    public static partial string GetErrorMessage(int code);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetFPGAButtonRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGARevision")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial long GetFPGARevisionRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGAVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetFPGAVersionRefShim(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalPortHandle GetPort(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPortWithModule")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalPortHandle GetPortWithModule(int module, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRuntimeType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RuntimeType GetRuntimeType();

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSystemActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial int GetSystemActiveRefShim(ref HalStatus status);


}
