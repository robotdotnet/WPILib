namespace CsCore;

public class AxisCamera : HttpCamera
{
    private static string HostToUrl(string host)
    {
        return $"http://{host}/mjpg/video.mjpg";
    }

    private static string[] HostToUrl(ReadOnlySpan<string> hosts)
    {
        string[] urls = new string[hosts.Length];
        for (int i = 0; i < urls.Length; i++)
        {
            urls[i] = HostToUrl(hosts[i]);
        }
        return urls;
    }

    public AxisCamera(string name, string host) : base(name, HostToUrl(host), HttpCameraKind.Axis) { }

    public AxisCamera(string name, ReadOnlySpan<string> hosts) : base(name, HostToUrl(hosts), HttpCameraKind.Axis) { }
}
