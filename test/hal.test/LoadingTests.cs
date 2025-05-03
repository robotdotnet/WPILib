using WPIHal.Natives;
using Xunit;

namespace hal.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestHalLoads()
        {
            HalBase.Initialize();
        }
    }
}
