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
    }
}
