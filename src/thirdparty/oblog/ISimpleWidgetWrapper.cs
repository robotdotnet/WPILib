using System.Collections.Generic;
using NetworkTables;

namespace WPILib.Oblog
{
    public interface ISimpleWidgetWrapper
    {
        NetworkTableEntry Entry { get; }

        ISimpleWidgetWrapper WithProperties(Dictionary<string, object> properties);

        ISimpleWidgetWrapper WithWidget(string widgetType);

        ISimpleWidgetWrapper WithSize(int width, int height);

        ISimpleWidgetWrapper WithPosition(int columnIndex, int rowIndex);
    }
}
