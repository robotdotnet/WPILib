using System;
using System.Reflection;
using WPIUtil.NativeUtilities;
using Xunit;

namespace wpiutil.test
{
    public class LoadingTest
    {
        public interface IInterfaceToLoad
        {
            ulong WPI_Now();
        }

        [NativeInterface(typeof(IInterfaceToLoad))]
        private class LoadTest
        {
            public static IInterfaceToLoad LoadedInterface = null;
        }

        internal static void StatusCheck(int status)
        {

        }

        [Fact]
        public void TestThatNativeLoadingWorks()
        {
            Assert.True(NativeInterfaceInitializer.LoadAndInitializeNativeTypes(typeof(LoadingTest).Assembly,
                "wpiutil", typeof(LoadingTest).GetMethod(nameof(StatusCheck), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!,
                out var generator));

            Assert.NotNull(LoadTest.LoadedInterface);
            Assert.NotEqual(0ul, LoadTest.LoadedInterface!.WPI_Now());
        }
    }
}
