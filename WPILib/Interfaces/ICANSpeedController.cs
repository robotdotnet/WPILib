using System;
using WPILib.LiveWindows;

namespace WPILib.Interfaces
{
    /// <summary>
    /// Mode for determining how the <see cref="ICANSpeedController"/> is controlled, used internally.
    /// </summary>
    public enum ControlMode
    {
        PercentVbus = 0,
        Follower = 5,
        Voltage = 4,
        Position = 1,
        Speed = 2,
        Current = 3,
        Disabled = 15
    }

    public static class ControlModeExtensions
    {
        public static bool IsPID(this ControlMode mode)
        {
            return mode == ControlMode.Current || mode == ControlMode.Speed || mode == ControlMode.Position;
        }
    }

    /// <summary>
    /// Fault enum for CAN devices.
    /// </summary>
    [Flags]
    public enum Faults : ushort
    {
        CurrentFault = 1,
        TemperatureFault = 2,
        BusVoltageFault = 4,
        GateDriverFault = 8,
        /* SRX extensions */
        FwdLimitSwitch = 0x10,
        RevLimitSwitch = 0x20,
        FwdSoftLimit = 0x40,
        RevSoftLimit = 0x80,
    };

    /// <summary>
    /// Limit switch enum for CAN devices
    /// </summary>
    public enum Limits
    {
        ForwardLimit = 1,
        ReverseLimit = 2
    };

    /// <summary>
    /// Determines how the <see cref="ICANSpeedController"/> behaves when sending a
    /// zero signal.
    /// </summary>
    public enum NeutralMode
    {
        ///Use the <see cref="NeutralMode"/> that is set by the jumper wire on the CAN device
        Jumper = 0,
        ///Stop the motor's rotation by applying a force.
        Brake = 1,
        ///Do not attempt to stop the motor. Instead allow it to coast to a stop without applying resistance.
        Coast = 2
    };

    /// <summary>
    /// Determines which sensor to use for position reference.
    /// </summary>
    public enum LimitMode
    {
        /// <summary>
        /// Only use switches for limits
        /// </summary>
        SwitchInputsOnly = 0,
        /// <summary>
        /// Use both switches and soft limits
        /// </summary>
        SoftPositionLimits = 1,
        /* SRX extensions */
        /// <summary>
        /// Disable switches and disable soft limits.  Only valid for methods on <see cref="CANTalon"/> objects.
        /// </summary>
        SrxDisableSwitchInputs = 2,
    };

    /// <summary>
    /// Interface for CAN Speed Controllers like <see cref="CANTalon"/> and <see cref="CANJaguar"/>
    /// </summary>
    public interface ICANSpeedController : ISpeedController, IPIDInterface, ILiveWindowSendable
    {
        ControlMode MotorControlMode { set; get; }
        double P { set; get; }
        double I { set; get; }
        double D { set; get; }
        double F { get; set; }
        void SetPID(double p, double i, double d);
        double GetBusVoltage();
        double GetOutputVoltage();
        double GetOutputCurrent();
        double GetTemperature();
        double GetPosition();
        double GetSpeed();
// ReSharper disable InconsistentNaming
        bool GetForwardLimitOK();
        bool GetReverseLimitOK();
// ReSharper restore InconsistentNaming
        Faults GetFaults();
        double VoltageRampRate { set; }
        uint FirmwareVersion { get; }
        NeutralMode ConfigNeutralMode { set; }
        int EncoderCodesPerRev { set; }
        int PotentiometerTurns { set; }
        void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition);
        void DisableSoftPositionLimits();
        LimitMode LimitMode { set; }
        double ForwardLimit { set; }
        double ReverseLimit { set; }
        double MaxOutputVoltage { set; }
        float FaultTime { set; }
    }
}
