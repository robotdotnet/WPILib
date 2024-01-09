using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CsCore.Handles;

namespace CsCore.Natives;

public static unsafe partial class CsNatives
{
    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertyKind GetPropertyKind(CsProperty property, out StatusValue status);

    [LibraryImport("cscore", EntryPoint = "CS_GetPropertyKind")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    internal static partial byte* GetPropertyNameRaw(CsProperty property, out StatusValue status);

    public static string? GetPropertyName(CsProperty property, out StatusValue status)
    {
        byte* str = GetPropertyNameRaw(property, out status);
        string? ret = Marshal.PtrToStringUTF8((nint)str);
        if (str != null)
        {
            FreeString(str);
        }
        return ret;
    }


    [LibraryImport("cscore", EntryPoint = "CS_FreeString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FreeString(byte* str);
}
