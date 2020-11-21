using System.Collections.Generic;

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
