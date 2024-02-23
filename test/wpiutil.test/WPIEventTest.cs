using WPIUtil.Concurrent;
using Xunit;

namespace WPIUtil.Test;

public class WPIEventTest
{
    [Fact]
    public void TestEvent()
    {
        using WpiEvent evnt = new();
        using WpiEvent evnt2 = new();
    }
}
