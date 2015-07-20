using System;

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
        ///Only use switches for limits
        SwitchInputsOnly = 0,
        ///Use both switches and soft limits
        SoftPositionLimits = 1,
        /* SRX extensions */
        ///Disable switches and disable soft limits
        SrxDisableSwitchInputs = 2,
    };

    /// <summary>
    /// Interface for CAN Speed Controllers like <see cref="CANTalon"/> and <see cref="CANJaguar"/>
    /// </summary>
    public interface ICANSpeedController : ISpeedController
    {
        double P { set; get; }
        double I { set; get; }
        double D { set; get; }
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
