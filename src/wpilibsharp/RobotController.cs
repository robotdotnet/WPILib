using UnitsNet;
using UnitsNet.NumberExtensions.NumberToElectricCurrent;
using UnitsNet.NumberExtensions.NumberToElectricPotential;
using WPIHal.Natives;

namespace WPILib;

public static class RobotController
{
    public static int FPGAVersion => HalBase.GetFPGAVersion();
    public static long FPGARevision => HalBase.GetFPGARevision();
    public static string SerialNumber => HalBase.GetSerialNumber();
    public static string Comments => HalBase.GetComments();
    public static int TeamNumber => HalBase.GetTeamNumber();
    public static long FPGATime => (long)HalBase.GetFPGATime();
    public static bool UserButton => HalBase.GetFPGAButton();
    public static ElectricPotential BatteryVoltage => HalPower.GetVinVoltage().Volts();
    public static bool SysActive => HalBase.GetSystemActive();
    public static bool BrownedOut => HalBase.GetBrownedOut();
    public static bool RSLState => HalBase.GetRSLState();
    public static bool IsSystemTimeValid => HalBase.GetSystemTimeValid();
    public static ElectricPotential InputVoltage => BatteryVoltage;
    public static ElectricCurrent InputCurrent => HalPower.GetVinCurrent().Amperes();
}
