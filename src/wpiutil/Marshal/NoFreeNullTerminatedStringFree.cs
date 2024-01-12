namespace WPIUtil.Marshal;

public class NoFreeNullTerminatedStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* ptr)
    {
    }
}
