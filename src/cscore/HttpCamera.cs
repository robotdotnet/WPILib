using System;
using CsCore.Handles;
using CsCore.Natives;

namespace CsCore;

public class HttpCamera : VideoCamera
{
    private static CsSource CreateHttpCamera(string name, string url, HttpCameraKind kind)
    {
        var source = CsNative.CreateHttpCamera(name, url, kind, out var status);
        VideoException.ThrowIfFailed(status);
        return source;
    }

    private static CsSource CreateHttpCamera(string name, ReadOnlySpan<string> urls, HttpCameraKind kind)
    {
        var source = CsNative.CreateHttpCamera(name, urls, kind, out var status);
        VideoException.ThrowIfFailed(status);
        return source;
    }


    public HttpCamera(string name, string url, HttpCameraKind kind = HttpCameraKind.Unknown) : base(CreateHttpCamera(name, url, kind))
    {
    }

    public HttpCamera(string name, ReadOnlySpan<string> urls, HttpCameraKind kind) : base(CreateHttpCamera(name, urls, kind))
    {
    }

    public HttpCameraKind HttpCameraKind
    {
        get
        {
            var ret = CsNative.GetHttpCameraKind(Handle, out var status);
            VideoException.ThrowIfFailed(status);
            return ret;
        }
    }

    public void SetUrls(ReadOnlySpan<string> urls)
    {
        CsNative.SetHttpCameraUrls(Handle, urls, out var status);
        VideoException.ThrowIfFailed(status);
    }

    public string[] GetUrls()
    {
        var ret = CsNative.GetHttpCameraUrls(Handle, out var status);
        VideoException.ThrowIfFailed(status);
        return ret;
    }
}
