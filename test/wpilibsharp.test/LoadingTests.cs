using Hal;
using NetworkTables.Natives;
using Xunit;

namespace wpilibsharp.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestHalLoads()
        {
            Assert.True(HALLowLevel.Initialize());
        }

        [Fact]
        public void TestNtCoreLoads()
        {
            NtCore.Initialize();
        }
    }
}
