using System.Runtime.CompilerServices;

namespace CsCore;

public enum StatusValue : int
{
    PropertyWriteFailed = 2000,
    Ok = 0,
    InvalidHandle = -2000,
    WrongHandleSubtype = -2001,
    InvalidProperty = -2002,
    WrongPropertyType = -2003,
    ReadFailed = -2004,
    SourceIsDisconnected = -2005,
    EmptyValue = -2006,
    BadUrl = -2007,
    TelemetryNotEnabled = -2008,
    UnsupportedMode = -2009,
}

public static class StatusValueExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void ThrowIfFailed(this StatusValue status)
    {
        if (status == StatusValue.Ok)
        {
            return;
        }
        VideoException.ThrowException(status);
    }
}
