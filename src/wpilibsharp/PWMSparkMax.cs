using Hal;
using WPILib.SmartDashboardNS;

namespace WPILib
{
    public sealed class PWMSparkMax : PWMSpeedController
    {
        public PWMSparkMax(int channel) : base(channel)
        {
            SetBounds(2.003, 1.55, 1.50, 1.46, 0.999);
            PeriodMultiplier = PeriodMultiplierType.k1X;
            Speed = 0.0;
            SetZeroLatch();

            UsageReporting.Report(ResourceType.RevSparkMaxPWM, Channel + 1);
            SendableRegistry.Instance.SetName(this, "PWMSparkMax", Channel);
        }
    }
}
