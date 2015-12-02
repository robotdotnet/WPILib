using System;
using WPILib.LiveWindow;

namespace WPILib.Interfaces
{
    /// <summary>
    /// Mode for determining how the <see cref="ICANSpeedController"/> is controlled, used internally.
    /// </summary>
    public enum ControlMode
    {
        /// <summary>
        /// Percent Vbus Mode (Similar to PWM).
        /// </summary>
        PercentVbus = 0,
        /// <summary>
        /// Follower Mode (sets the controller to follow another controller).
        /// </summary>
        Follower = 5,
        /// <summary>
        /// Runs the controller by directly setting the output voltage.
        /// </summary>
        Voltage = 4,
        /// <summary>
        /// Runs the controller in Closed Loop Position mode.
        /// </summary>
        Position = 1,
        /// <summary>
        /// Runs the controller in Closed Loop Speed mode.
        /// </summary>
        Speed = 2,
        /// <summary>
        /// Runs the controller in Closed Loop Current mode.
        /// </summary>
        Current = 3,
        /// <summary>
        /// If this mode is set, the controller is disabled.
        /// </summary>
        Disabled = 15
    }

    /// <summary>
    /// Extenstions to ease control mode checking.
    /// </summary>
    public static class ControlModeExtensions
    {
        /// <summary>
        /// Checks whether the <see cref="ControlMode"/> is a PID control mode.
        /// </summary>
        /// <param name="mode">The <see cref="ControlMode"/> to check.</param>
        /// <returns>True if the <see cref="ControlMode"/> is a PID control mode.</returns>
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
        /// <summary>
        /// Current Fault.
        /// </summary>
        CurrentFault = 1,
        /// <summary>
        /// Temperature Fault.
        /// </summary>
        TemperatureFault = 2,
        /// <summary>
        /// Bus Voltage Fault.
        /// </summary>
        BusVoltageFault = 4,
        /// <summary>
        /// Gate Driver Fault.
        /// </summary>
        GateDriverFault = 8,
        /* SRX extensions */
        /// <summary>
        /// Forward Limit Switch Fault.
        /// </summary>
        FwdLimitSwitch = 0x10,
        /// <summary>
        /// Reverse Limit Switch Fault.
        /// </summary>
        RevLimitSwitch = 0x20,
        /// <summary>
        /// Forward Soft Limit Fault.
        /// </summary>
        FwdSoftLimit = 0x40,
        /// <summary>
        /// Reverse Soft Limit Fault.
        /// </summary>
        RevSoftLimit = 0x80,
    };

    /// <summary>
    /// Limit switch enum for CAN devices
    /// </summary>
    public enum Limits
    {
        /// <summary>
        /// The forward limit
        /// </summary>
        ForwardLimit = 1,
        /// <summary>
        /// The reverse limit
        /// </summary>
        ReverseLimit = 2
    };

    /// <summary>
    /// Determines how the <see cref="ICANSpeedController"/> behaves when sending a
    /// zero signal.
    /// </summary>
    public enum NeutralMode
    {
        /// <summary>Use the <see cref="NeutralMode"/> that is set by the jumper wire on the CAN device.</summary>
        Jumper = 0,
        /// <summary>Stop the motor's rotation by applying a force.</summary>
        Brake = 1,
        /// <summary>Do not attempt to stop the motor. Instead allow it to coast to a stop without applying resistance.</summary>
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
    // ReSharper disable once PossibleInterfaceMemberAmbiguity
    public interface ICANSpeedController : ISpeedController, IPIDInterface, ILiveWindowSendable
    {
        /// <summary>
        /// Gets or Sets the <see cref="ControlMode"/> of the speed controller.
        /// </summary>
        ControlMode MotorControlMode { set; get; }
        /// <summary>
        /// Gets or sets the proportional PID constant.
        /// </summary>
        new double P { set; get; }
        /// <summary>
        /// Gets or sets the integral PID constant.
        /// </summary>
        new double I { set; get; }
        /// <summary>
        /// Gets or sets the derivative PID constant.
        /// </summary>
        new double D { set; get; }
        /// <summary>
        /// Gets or sets the feed-forward PID constant.
        /// </summary>
        double F { get; set; }
        /// <summary>
        /// Gets the current input (battery) voltage.
        /// </summary>
        /// <returns>The input voltage to the controller (in Volts).</returns>
        double GetBusVoltage();
        /// <summary>
        /// Gets the current output voltage.
        /// </summary>
        /// <returns>The output voltage of the controller (in Volts).</returns>
        double GetOutputVoltage();
        /// <summary>
        /// Gets the current being applied to the motor.
        /// </summary>
        /// <returns>The current motor current (in Amperes).</returns>
        double GetOutputCurrent();
        /// <summary>
        /// Gets the current temperature of the controller.
        /// </summary>
        /// <returns>The current temperature of the controller, in degrees Celsius.</returns>
        double GetTemperature();
        /// <summary>
        /// Return the current position of whatever the current selected sensor is.
        /// </summary>
        /// <remarks>
        /// See specific implementations for more information on selecting feedback sensors.
        /// </remarks>
        /// <returns>The current sensor position.</returns>
        double GetPosition();
        /// <summary>
        /// Return the current velocity of whatever the current selected sensor is.
        /// </summary>
        /// <remarks>
        /// See specific implementations for more information on selecting feedback sensors.
        /// </remarks>
        /// <returns>The current sensor velocity.</returns>
        double GetSpeed();
        /// <summary>
        /// Gets the status of the forward limit switch.
        /// </summary>
        /// <returns>The motor is allowed to turn in the forward direction when true.</returns>
        bool GetForwardLimitOk();
        /// <summary>
        /// Gets the status of the reverse limit switch.
        /// </summary>
        /// <returns>The motor is allowed to turn in the reverse direction when true.</returns>
        bool GetReverseLimitOk();
        /// <summary>
        /// Gets the status of any faults the speed controller has detected.
        /// </summary>
        /// <returns>Any faults returned by the controller.</returns>
        Faults GetFaults();

        /// <summary>
        /// Sets the maximum voltage change rate in Volts/s.
        /// </summary>
        /// <remarks>
        /// When in <see cref="ControlMode.PercentVbus"/> or <see cref="ControlMode.Voltage"/> output mode,
        /// the rate at which the voltage changes can be limited to reduce current spike. Set this to 0.0
        /// to disable rate limiting.
        /// </remarks>
        double VoltageRampRate { set; }
        /// <summary>
        /// Gets the firmware version of the speed controller.
        /// </summary>
        uint FirmwareVersion { get; }
        /// <summary>
        /// Sets what the controller does to the H-Bridge when neutral (not driving the output).
        /// </summary>
        /// <remarks>
        /// This allows you to override the jumper configuration for brake or coast.
        /// </remarks>
        NeutralMode NeutralMode { set; }
        /// <summary>
        /// Sets how many codes per revolution are generated by your encoder.
        /// </summary>
        int EncoderCodesPerRev { set; }
        /// <summary>
        /// Sets the number of turns on the potentiometer.
        /// </summary>
        /// <remarks>
        /// There is no special support for continuous turn potentiometers.
        /// Only integer numbers of turns are supported.
        /// </remarks>
        int PotentiometerTurns { set; }
        /// <summary>
        /// Configure soft position limits when in position controller mode.
        /// </summary>
        /// <remarks>
        /// When controlling position, you can add additional limits on top of the limit
        /// switch inputs that are based on the position feedback. If the position limit is reached or the switch
        /// is opened, that direction will be disabled.
        /// </remarks>
        /// <param name="forwardLimitPosition">The position that if exceeded will disabled the forward direction.</param>
        /// <param name="reverseLimitPosition">The position that if exceeded will disabled the reverse direction.</param>
        void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition);
        /// <summary>
        /// Disables soft position limits if previously enabled (disabled by default).
        /// </summary>
        void DisableSoftPositionLimits();
        /// <summary>
        /// Sets the limit mode for position control mode.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ConfigSoftPositionLimits"/> or <see cref="DisableSoftPositionLimits"/>
        /// to set this automatically.
        /// </remarks>
        LimitMode LimitMode { set; }
        /// <summary>
        /// Sets the position that if exceeded will disable the forward direction.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ConfigSoftPositionLimits"/> to set this and the limit mode automatically.
        /// </remarks>
        double ForwardLimit { set; }
        /// <summary>
        /// Sets the position that if exceeded will disable the reverese direction.
        /// </summary>
        /// <remarks>
        /// Use <see cref="ConfigSoftPositionLimits"/> to set this and the limit mode automatically.
        /// </remarks>
        double ReverseLimit { set; }
        /// <summary>
        /// Sets the maximum voltage that the speed controller will ever output.
        /// </summary>
        /// <remarks>
        /// This can be used to limit the maximum output voltage in all modes so that
        /// motors which cannot withstand full bus voltage can be used safely.
        /// </remarks>
        double MaxOutputVoltage { set; }
        /// <summary>
        /// Set how long the speed controller waits in the case of a fault before
        /// resuming operation.
        /// </summary>
        /// <remarks>
        /// Faults include over temperature, over current, and bus under voltage.
        /// The default is 3.0 seconds, but can be reduced to as low as 0.5 seconds.
        /// </remarks>
        float FaultTime { set; }
    }
}
