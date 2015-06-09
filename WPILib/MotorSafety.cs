namespace WPILib
{
    public interface IMotorSafety
    {
        double Expiration { set; get; }
        bool Alive { get; }
        void StopMotor();
        bool SafetyEnabled { set; get; }
        string Description { get; }
    }
}
