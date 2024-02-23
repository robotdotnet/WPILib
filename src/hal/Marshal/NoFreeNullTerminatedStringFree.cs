namespace WPIHal.Marshal;

public class NoFreeNullTerminatedStringFree : INullTerminatedStringFree<byte>
{
    public static unsafe void FreeString(byte* str)
    {
    }
}
