using Hal;
using System;
using Xunit;

namespace hal.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestHalLoads()
        {
            Assert.True(HalBase.Initialize());
        }
    }
}
