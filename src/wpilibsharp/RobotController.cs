using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;

namespace WPILib
{
    public static class RobotController
    {
        public static ElectricPotential BatteryVoltage => ElectricPotential.FromVolts(Hal.Power.GetVinVoltage());

        public static ElectricPotential Voltage5V => ElectricPotential.FromVolts(Hal.Power.GetUserVoltage5V());
    }
}
