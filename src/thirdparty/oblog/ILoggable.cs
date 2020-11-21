using System.Collections.Generic;
using WPILib.ShuffleboardNS;

namespace WPILib.Oblog
{
    public interface ILoggable
    {
        string ConfigureLogName();

        ILayoutType ConfigureLayoutType();

        int[] ConfigureLayoutSize();

        int[] ConfigureLayoutPosition();

        IDictionary<string, object> ConfigureLayoutProperties();

        void AddCustomLogging(IShuffleboardContainerWrapper container);

        bool SkipLayout();
    }
}
