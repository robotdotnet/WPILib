using Xunit;

namespace NetworkTables.Test;

public class LoggerTest
{
    [Fact]
    public void AddMessageTest()
    {
        using var instance = NetworkTableInstance.Create();
        List<NetworkTableEvent> msgs = [];
        instance.AddLogger((int)NtLogLevel.Info, 100, msgs.Add);

        instance.StartClient4("client");
        instance.SetServer("127.0.0.1", 10000);

        // Wait for client to report it's started, then wait another 0.1 sec
        int count = 0;
        while (!instance.GetNetworkMode().HasFlag(NetworkMode.Client4))
        {
            Thread.Sleep(100);
            count++;
            if (count > 30)
            {
                Assert.Fail("Failed to converge");
            }
        }
        Thread.Sleep(100);

        Assert.NotEmpty(msgs);
    }
}
