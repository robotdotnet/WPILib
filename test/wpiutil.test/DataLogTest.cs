using WPIUtil.Logging;
using Xunit;

namespace WPIUtil.Test;

internal class CallbackData
{
    public List<byte[]> ReceivedData { get; } = [];

    public void Callback(ReadOnlySpan<byte> data)
    {
        ReceivedData.Add(data.ToArray());
    }
}

public class DataLogTest
{
    [Fact]
    public void TestDoesntCrash()
    {
        CallbackData cb = new();
        {
            using var dl = new DataLogBackgroundWriter(cb.Callback);
        }
    }
}
