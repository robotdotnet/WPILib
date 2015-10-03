namespace HAL_Simulator.Inputs
{
    public interface IServoFeedback
    {
        void SetPosition(double value);
        void SetPeriod(double period);
    }
}
