using UnitsNet;
using UnitsNet.NumberExtensions.NumberToElectricCurrent;
using UnitsNet.NumberExtensions.NumberToElectricPotential;
using UnitsNet.NumberExtensions.NumberToTemperature;
using WPIHal;
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
    public static ElectricPotential BrownoutVoltage
    {
        get => HalPower.GetBrownoutVoltage().Volts();
        set => HalPower.SetBrownoutVoltage(value.Volts);
    }
    public static Temperature CPUTemp => HalPower.GetCPUTemp().DegreesCelsius();
    public static RadioLEDState RadioLED
    {
        get => HalLEDs.GetRadioLEDState();
        set => HalLEDs.SetRadioLEDState(value);
    }
    public static CANStatus CANStatus
    {
        get
        {
            HalCAN.GetCANStatus(out var percentBusUtilization, out var busOffCount, out var txFullCount, out var receiveErrorCount, out var transmitErrorCount);
            return new CANStatus(percentBusUtilization, (int)busOffCount, (int)txFullCount, (int)receiveErrorCount, (int)transmitErrorCount);
        }
    }
}
