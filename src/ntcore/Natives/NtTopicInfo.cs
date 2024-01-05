using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtTopicInfo
{
    public int topic;
    public NtString name;
    public NtType type;
    public NtString typeStr;
    public NtString properties;
}
