using System;
using System.Runtime.CompilerServices;

namespace CsCore;

public class VideoException(string msg) : Exception(msg)
{
    public override string ToString()
    {
        return $"VideoException [{base.ToString()}]";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfFailed(StatusValue status)
    {

    }
}
