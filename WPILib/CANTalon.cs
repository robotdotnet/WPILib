using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPILib.Interfaces;

namespace WPILib
{
    using Impl = HAL_Base.HALCanTalonSRX;
    public class CANTalon : MotorSafety, PIDOutput, SpeedController
    {
        private MotorSafetyHelper safetyHelper;

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

        public enum FeedbackDevice
        {
            QuadEncoder = 0,
            AnalogPotentiometer = 2,
            AnalogEncoder = 3,
            EncoderRising = 4,
            EncoderFalling = 5
        }

        public enum StatusFrameRate
        {
            General = 0,
            Feedback = 1,
            QuadEncoder = 2,
            AnalogTempVbat = 3
        }

        private ControlMode controlMode;
        private IntPtr impl;
        private static const double DelayForSolicitedSignals = 0.004;
        private int deviceNumber;
        private Boolean controlEnabled;
        private int profile;
        private double setPoint;

        private CANTalon()
        {
            safetyHelper = new MotorSafetyHelper(this);
            controlEnabled = true;
            setPoint = 0;
        }

        public CANTalon(int deviceNumber)
            :this()
        {
            this.deviceNumber = deviceNumber;
            throw new NotImplementedException("Need to add P/Invoke for TalonSRX_Create(int)");
            Profile = 0;
            ApplyControlMode(ControlMode.PercentVbus);
        }

        public CANTalon(int deviceNumber, int controlPeriodMs)
            :this()
        {
            this.deviceNumber = deviceNumber;
            impl = Impl.C_TalonSRX_Create(deviceNumber, controlPeriodMs);
            Profile = 0;
            ApplyControlMode(ControlMode.PercentVbus);
        }

        public void Delete()
        {
            Impl.C_TalonSRX_Destroy(impl);
        }

        public void ReverseSensor(bool flip)
        {
            Impl.C_TalonSRX_SetRevFeedbackSensor(impl, flip ? 1 : 0);
        }

        public void ReverseOutput(bool flip)
        {
            Impl.C_TalonSRX_SetRevMotDuringCloseLoopEn(impl, flip ? 1 : 0);
        }

        public int GetEncoderPosition()
        {
            int pos = 0;
            Impl.C_TalonSRX_GetEncPosition(impl, ref pos);
            return pos;
        }

        public int GetEncoderVelocity()
        {
            int vel = 0;
            Impl.C_TalonSRX_GetEncVel(impl, ref vel);
            return vel;
        }

        public int GetNumberOfQuadIdxRises()
        {
            int state = 0;
            Impl.C_TalonSRX_GetEncIndexRiseEvents(impl, ref state);
            return state;
        }

        public int GetPinStateQuadA()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadApin(impl, ref state);
            return state;
        }

        public int GetPinStateQuadB()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadBpin(impl, ref state);
            return state;
        }

        public int GetPinStateQuadIdx()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadIdxpin(impl, ref state);
            return state;
        }

        public int GetAnalogInPosition()
        {
            int position = 0;
            Impl.C_TalonSRX_GetAnalogInWithOv(impl, ref position);
            return position;
        }

        public int GetAnalogInRaw()
        {
            return GetAnalogInPosition() & 0x3FF;
        }

        public int GetAnalogInVelocity()
        {
            int velocity = 0;
            Impl.C_TalonSRX_GetAnalogInVel(impl, ref velocity);
            return velocity;
        }

        public int getClosedLoopError()
        {
            int error = 0;
            Impl.C_TalonSRX_GetCloseLoopErr(impl, ref error);
            return error;
        }

        public bool IsForwardLimitSwitchClosed()
        {
            int state = 0;
            Impl.C_TalonSRX_GetLimitSwitchClosedFor(impl, ref state);
            return state != 0;
        }

        public bool IsReverseLimitSwitchClosed()
        {
            int state = 0;
            Impl.C_TalonSRX_GetLimitSwitchClosedFor(impl, ref state);
            return state != 0;
        }

        public bool IsBreakEnabledduringNeutral()
        {
            int state = 0;
            Impl.C_TalonSRX_GetBrakeIsEnabled(impl, ref state);
            return state != 0;
        }

        public double GetTemp()
        {
            double temp = 0.0;
            Impl.C_TalonSRX_GetTemp(impl, ref temp);
            return temp;
        }

        public double GetOutputCurrent()
        {
            double current = 0.0;
            Impl.C_TalonSRX_GetCurrent(impl, ref current);
            return current;
        }

        public double GetOutputVoltage()
        {
            int throttle = 0;
            Impl.C_TalonSRX_GetAppliedThrottle(impl, ref throttle);
            return GetBusVoltage() * (throttle / 1023.0);
        }

        public double GetBusVoltage()
        {
            double voltage = 0.0;
            Impl.C_TalonSRX_GetBatteryV(impl, ref voltage);
            return voltage;
        }

        public double GetPosition()
        {
            int pos = 0;
            Impl.C_TalonSRX_GetSensorPosition(impl, ref pos);
            return pos;
        }

        public void SetPosition()
        {
            throw new NotImplementedException("Add P/Invoke for TalonSRX_SetSensorPosition(IntPtr, ref int)");
        }

        public double GetSpeed()
        {
            int vel = 0;
            Impl.C_TalonSRX_GetSensorVelocity(impl, ref vel);
            return vel;
        }

        private void ApplyControlMode(ControlMode value)
        {
            controlMode = value;
            if (value == ControlMode.Disabled)
                controlEnabled = false;
            Impl.C_TalonSRX_SetModeSelect(impl, (int)ControlMode.Disabled);
        }

        public ControlMode MotorControlMode
        {
            get { return controlMode; }
            set
            {
                if (controlMode == value) return;
                ApplyControlMode(value);
            }
        }

        public FeedbackDevice FeedBackDevice
        {
            get
            {
                int device = 0;
                Impl.C_TalonSRX_GetFeedbackDeviceSelect(impl, ref device);
                return (FeedbackDevice)device;
            }
            set
            {
                Impl.C_TalonSRX_SetFeedbackDeviceSelect(impl, (int)value);
            }
        }

        public bool ControlEnabled
        {
            get
            {
                return controlEnabled;
            }
            set
            {
                if (controlEnabled == value) return;
                if (controlEnabled && !value)
                {
                    Impl.C_TalonSRX_SetModeSelect(impl, (int)ControlMode.Disabled);
                    controlEnabled = false;
                }
                else
                {
                    controlEnabled = true;
                }
            }
        }

        private void EnsureInPIDMode()
        {
            if (!(MotorControlMode == ControlMode.Position || MotorControlMode == ControlMode.Speed))
            {
                throw new InvalidOperationException("PID mode only applies to Position and Speed modes.");
            }
        }

        public double P
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }

        public double I
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }

        public double D
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }

        public double F
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }

        public double IZone
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }
        public double GetIaccum()
        {
            EnsureInPIDMode();
            throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
        }

        public void ClearIAccum()
        {
            EnsureInPIDMode();
            throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
        }

        public double CloseLoopRampRate
        {
            get
            {
                EnsureInPIDMode();
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
            set
            {
                int rate = (int)(value * 1023.0 / 12.0 / 1000.0);
                throw new NotImplementedException("Need P/Invoke methods from HAL for PID");
            }
        }

        public void SetPID(double p, double i, double d, double f, int izone, double closeLoopRampRate, int profile)
        {
            if (profile != 0 && profile != 1)
                throw new ArgumentOutOfRangeException("Talon PID profile must be 0 or 1.");
            this.profile = profile;
            P = p;
            I = i;
            D = d;
            F = f;
            IZone = izone;
            CloseLoopRampRate = closeLoopRampRate;
        }

        public void SetPID(double p, double i, double d)
        {
            SetPID(p, i, d, 0, 0, 0, profile);
        }

        public double Setpoint
        {
            get
            {
                return setPoint;
            }
            set
            {
                Set(value);
            }
        }

        public int Profile
        {
            get { return profile; }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException("Talon PID profile must be 0 or 1.");
                profile = value;
                Impl.C_TalonSRX_SetProfileSlotSelect(impl, profile);
            }
        }

        public double VoltageRampRate
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {
                int rate = (int)(value * 1023.0 / 12.0 / 100.0);
            }
        }

        public long FirmwareVersion
        {
            get
            {
                int version = 0;
                Impl.C_TalonSRX_GetFirmVers(impl, ref version);
                return version;
            }
        }

        public int DeviceID
        {
            get { return deviceNumber; }
        }

        public int ForwardSoftLimit
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {

                throw new NotImplementedException("Need Soft Limit P/Invoke functions to support setting value.");
            }
        }

        public int ForwardSoftLimitEnabled
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {

                throw new NotImplementedException("Need Soft Limit P/Invoke functions to support setting value.");
            }
        }
        public int ReverseSoftLimit
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {

                throw new NotImplementedException("Need Soft Limit P/Invoke functions to support setting value.");
            }
        }

        public int ReverseSoftLimitEnabled
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {

                throw new NotImplementedException("Need Soft Limit P/Invoke functions to support setting value.");
            }
        }

        public void ClearStickyFaults()
        {
            Impl.C_TalonSRX_ClearStickyFaults(impl);
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            int mask = 1 << 2 | (forward ? 1 : 0) << 1 | (reverse ? 1 : 0);
            Impl.C_TalonSRX_SetOverrideLimitSwitchEn(impl, mask);
        }

        public bool ForwardLimitSwitchNormallyOpen
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {
                throw new NotImplementedException("Need params_t enumeration to support setting value.");
            }
        }

        public bool ReverseLimitSwitchNormallyOpen
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {
                throw new NotImplementedException("Need params_t enumeration to support setting value.");
            }
        }

        public bool BrakeModeEnabled
        {
            get
            {
                throw new NotImplementedException("Need params_t enumeration to support retrieving value.");
            }
            set
            {
                Impl.C_TalonSRX_SetOverrideBrakeType(impl, value ? 2 : 1);
            }
        }

        public int FaultOverTemp
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_OverTemp(impl, ref val);
                return val;
            }
        }

        public int FaultUnderVoltage
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_UnderVoltage(impl, ref val);
                return val;
            }
        }

        public int FaultForwardLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_ForLim(impl, ref val);
                return val;
            }
        }

        public int FaultReverseLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_RevLim(impl, ref val);
                return val;
            }
        }

        public int FaultHardwareFailure
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_HardwareFailure(impl, ref val);
                return val;
            }
        }

        public int FaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_ForSoftLim(impl, ref val);
                return val;
            }
        }

        public int FaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_RevSoftLim(impl, ref val);
                return val;
            }
        }

        public int StickyFaultOverTemp
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_OverTemp(impl, ref val);
                return val;
            }
        }

        public int StickyFaultUnderVoltage
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_UnderVoltage(impl, ref val);
                return val;
            }
        }

        public int StickyFaultForwardLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_ForLim(impl, ref val);
                return val;
            }
        }

        public int StickyFaultReverseLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_RevLim(impl, ref val);
                return val;
            }
        }

        public int StickyFaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_ForSoftLim(impl, ref val);
                return val;
            }
        }

        public int StickyFaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_RevSoftLim(impl, ref val);
                return val;
            }
        }

        public void SetExpiration(double timeout)
        {
            safetyHelper.SetExpiration(timeout);
        }

        public double GetExpiration()
        {
            return safetyHelper.GetExpiration();
        }

        public bool IsAlive()
        {
            return safetyHelper.IsAlive();
        }

        [Obsolete("Use Disable() instead, or set ControlEnabled to false.")]
        public void StopMotor()
        {
            ControlEnabled = false;
        }

        public void SetSafetyEnabled(bool enabled)
        {
            safetyHelper.SetSafetyEnabled(enabled);
        }

        public bool IsSafetyEnabled()
        {
            return safetyHelper.IsSafetyEnabled();
        }

        public string GetDescription()
        {
            return "CAN TalonSRX ID " + deviceNumber;
        }

        public void PidWrite(double output)
        {
            if (controlMode == ControlMode.PercentVbus)
            {
                Set(output);
            }
            else
            {
                throw new InvalidOperationException("PID on RoboRIO only supported in Voltage Bus (PWM-like) mode");
            }
        }

        public double Get()
        {
            int value = 0;
            switch (controlMode)
            {
                case ControlMode.Voltage:
                    return GetOutputVoltage();
                case ControlMode.Position:
                    Impl.C_TalonSRX_GetSensorPosition(impl, ref value);
                    return value;
                case ControlMode.Speed:
                    Impl.C_TalonSRX_GetSensorVelocity(impl, ref value);
                    return value;
                case ControlMode.Current:
                    return GetOutputCurrent();
                case ControlMode.PercentVbus:
                default:
                    Impl.C_TalonSRX_GetAppliedThrottle(impl, ref value);
                    return value / 1023.0;
            }
        }

        public void Set(double output, byte unused)
        {
            Set(output);
        }

        public void Set(double output)
        {
            safetyHelper.Feed();
            if (controlEnabled)
            {
                setPoint = output;
                switch (controlMode)
                {
                    case ControlMode.PercentVbus:
                        throw new NotImplementedException("Need Set method on HAL P/Invoke.");
                        break;
                    case ControlMode.Voltage:
                        int volts = (int)(output * 256);
                        Impl.C_TalonSRX_SetDemand(impl, volts);
                        break;
                    case ControlMode.Position:
                    case ControlMode.Speed:
                    case ControlMode.Follower:
                        Impl.C_TalonSRX_SetDemand(impl, (int)output);
                        break;
                    default:
                        break;
                }
                Impl.C_TalonSRX_SetModeSelect(impl, (int)MotorControlMode);
            }
        }

        public void Disable()
        {
            ControlEnabled = false;
        }
    }
}
