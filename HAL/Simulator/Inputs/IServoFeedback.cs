namespace HAL.Simulator.Inputs
{
    public interface IServoFeedback
    {
        void SetPosition(double value);
        void SetRate(double rate);
    }
}
