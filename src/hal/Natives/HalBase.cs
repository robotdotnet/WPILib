using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CommunityToolkit.Diagnostics;
using WPIHal.Handles;
using WPIHal.Marshal;
using WPIUtil;

namespace WPIHal.Natives;

public static partial class HalBase
{
    public const string StatusCheckCall = "global::WPIHal.HalStatusExtensions.ThrowIfFailed({0});";
    public const string LibraryName = "wpiHal";

    [LibraryImport("wpiHal", EntryPoint = "HAL_Initialize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    internal static partial bool Initialize(int timeout, int mode);

    public static void Initialize()
    {
        if (!Initialize(500, 0))
        {
            ThrowHelper.ThrowInvalidOperationException("HAL failed to initialize");
        }
    }

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGATime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetFPGATime(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_ExpandFPGATime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong ExpandFPGATime(uint unexpandedLower, out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetBrownedOut")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetBrownedOut(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetErrorMessage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<NoFreeNullTerminatedStringFree>))]
    public static partial string GetErrorMessage(HalStatus code);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGAButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetFPGAButton(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGARevision")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial long GetFPGARevision(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetFPGAVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetFPGAVersion(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPort")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalPortHandle GetPort(int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetPortWithModule")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial HalPortHandle GetPortWithModule(int module, int channel);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRuntimeType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial RuntimeType GetRuntimeType();

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSystemActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSystemActive(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetLastError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(NullTerminatedStringMarshaller<NoFreeNullTerminatedStringFree>))]
    public static partial string GetLastError(ref HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSerialNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetSerialNumber(Span<byte> buffer, nuint bufferSize);

    public static string GetSerialNumber()
    {
        Span<byte> buffer = stackalloc byte[9];
        nuint actual = GetSerialNumber(buffer, (nuint)buffer.Length);
        if (actual == 0)
        {
            return "";
        }
        ReadOnlySpan<byte> read = buffer[0..checked((int)actual)];
        return Encoding.UTF8.GetString(read);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetComments")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetComments(Span<byte> buffer, nuint bufferSize);

    public static string GetComments()
    {
        Span<byte> buffer = stackalloc byte[65];
        nuint actual = GetComments(buffer, (nuint)buffer.Length);
        if (actual == 0)
        {
            return "";
        }
        ReadOnlySpan<byte> read = buffer[0..checked((int)actual)];
        return Encoding.UTF8.GetString(read);
    }

    [LibraryImport("wpiHal", EntryPoint = "HAL_GetTeamNumber")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetTeamNumber();

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetRSLState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetRSLState(out HalStatus status);

    [AutomateStatusCheck(StatusCheckMethod = StatusCheckCall)]
    [LibraryImport("wpiHal", EntryPoint = "HAL_GetSystemTimeValid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetSystemTimeValid(out HalStatus status);

    [LibraryImport("wpiHal", EntryPoint = "HAL_Shutdown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Shutdown();

    [LibraryImport("wpiHal", EntryPoint = "HAL_SimPeriodicBefore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SimPeriodicBefore();

    [LibraryImport("wpiHal", EntryPoint = "HAL_SimPeriodicAfter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SimPeriodicAfter();
}
