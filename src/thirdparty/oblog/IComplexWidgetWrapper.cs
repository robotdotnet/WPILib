using System;
using System.Collections.Generic;
using System.Text;

namespace WPILib.Oblog
{
    public interface IComplexWidgetWrapper
    {
        IComplexWidgetWrapper WithProperties(Dictionary<string, object> properties);

        IComplexWidgetWrapper WithWidget(string widgetType);

        IComplexWidgetWrapper WithSize(int width, int height);

        IComplexWidgetWrapper WithPosition(int columnIndex, int rowIndex);
    }
}
