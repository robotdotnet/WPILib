namespace WPIUtil;

public enum PixelFormat : int
{
    Unknown = 0,
    Mjpeg,
    Yuyv,
    Rgb565,
    Bgr,
    Gray,
    Y16,
    Uyvy,
    Bgra
}

public enum TimestampSource : int
{
    Unknown = 0,
    FrameDequeue,
    V4lEof,
    V4lSoe
}
