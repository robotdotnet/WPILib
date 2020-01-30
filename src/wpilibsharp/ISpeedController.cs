using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;

namespace WPILib
{
    public static class SpeedControllerExtensions
    {
        public static void SetVoltage(this ISpeedController @this, ElectricPotential potential)
        {
            @this.Set(potential / RobotController.BatteryVoltage);
        }
    }

    public interface ISpeedController
    {
        void Set(double speed);

        double Get();

        bool Inverted { get; set; }

        void Disable();

        void StopMotor();

    }
}
