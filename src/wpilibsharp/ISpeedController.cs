using System;
using UnitsNet;

namespace WPILib
{
    public static class SpeedControllerExtensions
    {
        public static void SetVoltage(this ISpeedController @this, ElectricPotential potential)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }

            @this.Set(potential / RobotController.BatteryVoltage);
        }
    }

    public interface ISpeedController
    {
#pragma warning disable CA1716 // Identifiers should not match keywords
        void Set(double speed);

        double Get();
#pragma warning restore CA1716 // Identifiers should not match keywords

        bool Inverted { get; set; }

        void Disable();

        void StopMotor();

    }
}
