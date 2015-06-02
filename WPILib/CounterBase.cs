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
        int Get();
        void Reset();
        double GetPeriod();
        void SetMaxPeriod(double maxPeriod);
        bool GetStopped();
        bool GetDirection();
    }
}
