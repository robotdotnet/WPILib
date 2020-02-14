namespace WPILib.Shuffleboard
{
    public sealed class BuiltInWidgets : IWidgetType
    {
        public static BuiltInWidgets TextView { get; } = new BuiltInWidgets("Text View");

        public string WidgetName { get; }

        internal BuiltInWidgets(string widgetName)
        {
            WidgetName = widgetName;
        }
    }
}
