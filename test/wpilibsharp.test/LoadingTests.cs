using Hal;
using NetworkTables.Natives;
using System;
using Xunit;

namespace wpilibsharp.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestHalLoads()
        {
            Assert.True(HalBase.Initialize());
        }

        [Fact]
        public void TestNtCoreLoads()
        {
            Assert.True(NtCore.Initialize());
        }
    }
}
