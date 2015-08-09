namespace WPILib.Interfaces
{
    /// <summary>
    /// Encoding Types for Counters and Encoders.
    /// </summary>
    public enum EncodingType
    {
        K1X = 0,
        K2X = 1,
        K4X = 2,
    }

    /// <summary>
    /// Interface for counting input ticks.
    /// </summary>
    public interface ICounterBase
    {
        int Get();
        void Reset();
        double GetPeriod();
        double MaxPeriod { set; }
        bool GetStopped();
        bool GetDirection();
    }
}
