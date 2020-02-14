namespace WPILib.Shuffleboard
{
    public abstract class ShuffleboardWidget<W>
        : ShuffleboardComponent<W> where W : ShuffleboardWidget<W>
    {

        public ShuffleboardWidget(IShuffleboardContainer parent, string title)
            : base(parent, title)
        {

        }

        public W WithWidget(IWidgetType widgetType)
        {
            return WithWidget(widgetType.WidgetName);
        }

        public W WithWidget(string widgetType)
        {
            Type = widgetType;
            return (W)this;
        }
    }
}
