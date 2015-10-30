namespace WPILib.Interfaces
{
    public interface IPIDInterface
    {
        void SetPID(double p, double i, double d);
        double P { get; set; }
        double I { get; set; }
        double D { get; set; }
        double Setpoint { get; set; }
        double GetError();
        void Enable();
        void Disable();
        bool Enabled { get; }
        void Reset();
    }
}
