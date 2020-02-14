using UnitsNet;

namespace WPILib
{
    public static class RobotController
    {
        public static ElectricPotential BatteryVoltage => ElectricPotential.FromVolts(Hal.PowerLowLevel.GetVinVoltage());

        public static ElectricPotential Voltage5V => ElectricPotential.FromVolts(Hal.PowerLowLevel.GetUserVoltage5V());
    }
}
