using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;

namespace WPILib
{
    public static class SpeedControllerExtensions
    {
        public static void SetVoltage(this SpeedController @this, ElectricPotential potential)
        {
            @this.Set(potential / RobotController.BatteryVoltage);
        }
    }

    public interface SpeedController
    {
        void Set(double speed);

        double Get();

        bool Inverted { get; set; }

        void Disable();

        void StopMotor();

    }
}
