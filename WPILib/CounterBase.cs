namespace WPILib
{
    public enum EncodingType
    {
        K1X = 0,
        K2X = 1,
        K4X = 2,
    }
    public interface CounterBase
    {
        int Value { get; }
        void Reset();
        double Period { get; }
        double MaxPeriod { set; }
        bool Stopped { get; }
        bool Direction { get; }
    }
}
