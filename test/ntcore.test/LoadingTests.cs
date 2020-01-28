using NetworkTables.Natives;
using System;
using Xunit;

namespace ntcore.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestNtCoreLoads()
        {
            Assert.True(NtCore.Initialize());
        }
    }
}
