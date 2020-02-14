namespace WPILib.Shuffleboard
{
    public sealed class BuiltInLayouts : ILayoutType
    {
        public static BuiltInLayouts List { get; } = new BuiltInLayouts("List Layout");

        public static BuiltInLayouts Grid { get; } = new BuiltInLayouts("Grid Layout");

        public string LayoutName { get; }

        internal BuiltInLayouts(string layoutName)
        {
            LayoutName = layoutName;
        }
    }
}
