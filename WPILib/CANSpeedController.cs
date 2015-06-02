namespace WPILib
{
    public enum ControlMode
    {
        PercentVbus = 0,
        Current = 1,
        Speed = 2,
        Position = 3,
        Voltage = 4,
        Follower = 5 // Not supported in Jaguar.

    };

    public enum Faults
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

    public enum Limits
    {
        ForwardLimit = 1,
        ReverseLimit = 2
    };

    public enum NeutralMode
    {
        /** Use the NeutralMode that is set by the jumper wire on the CAN device */
        Jumper = 0,
        /** Stop the motor's rotation by applying a force. */
        Brake = 1,
        /** Do not attempt to stop the motor. Instead allow it to coast to a stop without applying resistance. */
        Coast = 2
    };

    public enum LimitMode
    {
        /** Only use switches for limits */
        SwitchInputsOnly = 0,
        /** Use both switches and soft limits */
        SoftPositionLimits = 1,
        /* SRX extensions */
        /** Disable switches and disable soft limits */
        SrxDisableSwitchInputs = 2,
    };

    public interface CANSpeedController : SpeedController
    {
        new double Get();
        new void Set(double value, byte syncGroup = 0);
        new void Disable();
        void SetP(double p);
        void SetI(double i);
        void SetD(double d);
        void SetPID(double p, double i, double d);
        double GetP();
        double GetI();
        double GetD();
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
        ushort GetFaults();
        void SetVoltageRampRate(double rampRate);
        uint GetFirmwareVersion();
        void ConfigNeutralMode(NeutralMode mode);
        void ConfigEncoderCodesPerRev(int codesPerRev);
        void ConfigPotentiometerTurns(int turns);
        void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition);
        void DisableSoftPositionLimits();
        void ConfigLimitMode(LimitMode mode);
        void ConfigForwardLimit(double forwardLimitPosition);
        void ConfigReverseLimit(double reverseLimitPosition);
        void ConfigMaxOutputVoltage(double voltage);
        void ConfigFaultTime(float faultTime);
    }
}
