using System.Collections.Generic;

namespace WPILib.Shuffleboard
{
    public interface IShuffleboardContainer : IShuffleboardValue
    {
        List<ShuffleboardComponent> Components { get; }

        ShuffleboardLayout GetLayout(string title, string type);

        ShuffleboardLayout GetLayout(string title, ILayoutType layoutType);

        ShuffleboardLayout GetLayout(string title);


    }
}
