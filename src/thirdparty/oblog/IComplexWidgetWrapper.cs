using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Oblog
{
    public interface IComplexWidgetWrapper
    {
        ISimpleWidgetWrapper WithProperties(Dictionary<string, object> properties);

        ISimpleWidgetWrapper WithWidget(string widgetType);

        ISimpleWidgetWrapper WithSize(int width, int height);

        ISimpleWidgetWrapper WithPosition(int columnIndex, int rowIndex);
    }
}
