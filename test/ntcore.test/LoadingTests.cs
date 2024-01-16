using NetworkTables.Natives;
using Xunit;

namespace ntcore.test
{
    public class LoadingTests
    {
        [Fact]
        public void TestNtCoreLoads()
        {
            NtCore.Now();
        }
    }
}
