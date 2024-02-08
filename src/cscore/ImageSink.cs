using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public abstract class ImageSink(CsSink handle) : VideoSink(handle)
{
    public new string Description
    {
        set
        {
            CsNative.SetSinkDescription(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }

    public string GetError()
    {
        CsNative.GetSinkError(Handle, out var error, out var status);
        VideoException.ThrowIfFailed(status);
        return error;
    }

    public bool Enabled
    {
        set
        {
            CsNative.SetSinkEnabled(Handle, value, out var status);
            VideoException.ThrowIfFailed(status);
        }
    }
}
