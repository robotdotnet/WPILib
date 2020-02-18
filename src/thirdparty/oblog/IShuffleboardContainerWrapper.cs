using System;
using System.Collections.Generic;
using System.Text;
using WPILib.ShuffleboardNS;
using WPILib.SmartDashboardNS;

namespace WPILib.Oblog
{
    public interface IShuffleboardContainerWrapper
    {
        IShuffleboardLayoutWrapper GetLayout(string title, ILayoutType type);

        ISimpleWidgetWrapper Add<T>(string title, T defaultValue);

        IComplexWidgetWrapper Add(string title, ISendable defaultValue);
    }
}
