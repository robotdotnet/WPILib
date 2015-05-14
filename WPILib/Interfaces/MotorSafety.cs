namespace WPILib.Interfaces
{
    public interface MotorSafety
    {
        void SetExpiration(double timeout);
        double GetExpiration();
        bool IsAlive();
        void StopMotor();
        void SetSafetyEnabled(bool enabled);
        bool IsSafetyEnabled();
        string GetDescription();
    }
}
