using Epilogue.Logging;
using Xunit;

namespace Epilogue;

public class LazyLoggerTest
{
    [Fact]
    public void LazyOfLazyReturnsSelf()
    {
        var lazy = new LazyLogger(new NullLogger());
        Assert.Same(lazy, lazy.Lazy);
    }

    [Fact]
    public void LazyInt()
    {
        var logger = new TestLogger();
        var lazy = logger.Lazy;

        {
            // First time logging to "int" should go through.
            lazy.Log("int", 0);
            Assert.Equal(new TestLogger.LogEntry[] { new("int", 0) }, logger.Entries);
        }

        {
            // Logging the current value shouldn't go through
            lazy.Log("int", 0);
            Assert.Equal(new TestLogger.LogEntry[] { new("int", 0) }, logger.Entries);
        }

        {
            // Logging a new value should go through.
            lazy.Log("int", 1);
            Assert.Equal(new TestLogger.LogEntry[] { new("int", 0), new("int", 1) }, logger.Entries);
        }

        {
            // Logging a previous value should go through.
            lazy.Log("int", 0);
            Assert.Equal(new TestLogger.LogEntry[] { new("int", 0), new("int", 1), new("int", 0) }, logger.Entries);
        }
    }
}
