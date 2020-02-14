using Hal;
using Xunit;

namespace hal.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestHalLoads()
        {
            Assert.True(HALLowLevel.Initialize());
        }
    }
}
