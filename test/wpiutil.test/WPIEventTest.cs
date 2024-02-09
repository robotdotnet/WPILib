using WPIUtil.Concurrent;
using Xunit;

namespace WPIUtil.Test;

public class WPIEventTest
{
    [Fact]
    public void TestEvent()
    {
        using Event evnt = new();
        using Event evnt2 = new();
    }
}
