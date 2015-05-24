namespace WPILib.Interfaces
{
    public enum PIDSourceParameter
    {
        Distance = 0,
        Rate = 1,
        Angle = 2,
    }
    public interface PIDSource
    {
        double PidGet();
    }
}
