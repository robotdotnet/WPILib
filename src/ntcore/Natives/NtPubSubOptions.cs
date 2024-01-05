using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct NtPubSubOptions
{
    public uint structSize;
    public uint pollSize;
    public double periodic;
    public int excludePublisher;
    public int sendAll;
    public int topicsOnly;
    public int prefixMatch;
    public int keepDuplicates;
    public int disableRemote;
    public int disableLocal;
    public int excludeSelf;

}
