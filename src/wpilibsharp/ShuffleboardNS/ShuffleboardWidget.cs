using System;

namespace WPILib.ShuffleboardNS
{
    public abstract class ShuffleboardWidget<T>
        : ShuffleboardComponent<T> where T : ShuffleboardWidget<T>
    {

        public ShuffleboardWidget(IShuffleboardContainer parent, string title)
            : base(parent, title)
        {

        }

        public T WithWidget(IWidgetType widgetType)
        {
            if (widgetType == null)
            {
                throw new ArgumentNullException(nameof(widgetType));
            }

            return WithWidget(widgetType.WidgetName);
        }

        public T WithWidget(string widgetType)
        {
            Type = widgetType;
            return (T)this;
        }
    }
}
