using System.Runtime.CompilerServices;

namespace WPIHal;

public enum HalStatus : int
{
    Ok = 0,
}

public static class HalStatusExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfFailed(this HalStatus status)
    {
        if (status == HalStatus.Ok)
        {
            return;
        }
        // TODO Throw Exception
        throw new InvalidOperationException();
    }
}
