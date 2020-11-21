using WPILib.ShuffleboardNS;

namespace WPILib.Oblog
{
    public class WrappedShuffleboard : IShuffleboardWrapper
    {
        public IShuffleboardContainerWrapper GetTab(string title)
        {
            return new WrappedShuffleboardContainer(Shuffleboard.GetTab(title));
        }
    }
}
