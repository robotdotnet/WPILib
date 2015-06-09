﻿using System;
using HAL_Base;
using NetworkTablesDotNet.Tables;
using WPILib.livewindow;

namespace WPILib
{
    using Impl = HALCanTalonSRX;
    public class CANTalon : IMotorSafety, CANSpeedController, LiveWindowSendable, ITableListener, IDisposable
    {
        private MotorSafetyHelper m_safetyHelper;

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


        private ControlMode m_controlMode;
        private IntPtr m_impl;
        private const double DelayForSolicitedSignals = 0.004;
        private int m_deviceNumber;
        private bool m_controlEnabled;
        private int m_profile;
        private double m_setPoint;


        public CANTalon(int deviceNumber, int controlPeriodMs = 10)
        {
            m_deviceNumber = deviceNumber;
            m_impl = Impl.C_TalonSRX_Create(deviceNumber, controlPeriodMs);
            m_safetyHelper = new MotorSafetyHelper(this);
            m_controlEnabled = true;
            m_setPoint = 0;
            Profile = 0;
            ApplyControlMode(ControlMode.PercentVbus);
            LiveWindow.AddActuator("CANTalonSRX", deviceNumber, this);
            HAL.Report(ResourceType.kResourceType_CANTalonSRX, (byte)(deviceNumber + 1), (byte)m_controlMode);
        }

        public void Dispose()
        {
            Impl.C_TalonSRX_Destroy(m_impl);
        }

        private double GetParam(HALCanTalonSRX.ParamID id)
        {
            Impl.C_TalonSRX_RequestParam(m_impl, (int)id);
            Timer.Delay(DelayForSolicitedSignals);
            var value = 0.0;
            var status = Impl.C_TalonSRX_GetParamResponse(m_impl, (int)id, ref value);
            if (status != CTR_Code.CTR_OKAY)
                Utility.CheckStatus((int)status);
            return value;
        }

        private int GetParamInt32(HALCanTalonSRX.ParamID id)
        {
            Impl.C_TalonSRX_RequestParam(m_impl, (int)id);
            Timer.Delay(DelayForSolicitedSignals);
            var value = 0;
            var status = Impl.C_TalonSRX_GetParamResponseInt32(m_impl, (int)id, ref value);
            if (status != CTR_Code.CTR_OKAY)
                Utility.CheckStatus((int)status);
            return value;
        }

        private void SetParam(HALCanTalonSRX.ParamID id, double value)
        {
            var errorCode = Impl.C_TalonSRX_SetParam(m_impl, (int)id, value);
            if (errorCode != CTR_Code.CTR_OKAY)
                Utility.CheckStatus((int)errorCode);
        }

        [Obsolete("Use the Dispose method or a using block instead of Delete")]
        public void Delete()
        {
            Dispose();
        }

        public void ReverseSensor(bool flip)
        {
            Impl.C_TalonSRX_SetRevFeedbackSensor(m_impl, flip ? 1 : 0);
        }

        public void ReverseOutput(bool flip)
        {
            Impl.C_TalonSRX_SetRevMotDuringCloseLoopEn(m_impl, flip ? 1 : 0);
        }

        public int GetEncoderPosition()
        {
            int pos = 0;
            Impl.C_TalonSRX_GetEncPosition(m_impl, ref pos);
            return pos;
        }

        public int GetEncoderVelocity()
        {
            int vel = 0;
            Impl.C_TalonSRX_GetEncVel(m_impl, ref vel);
            return vel;
        }

        public int GetNumberOfQuadIdxRises()
        {
            int state = 0;
            Impl.C_TalonSRX_GetEncIndexRiseEvents(m_impl, ref state);
            return state;
        }

        public int GetPinStateQuadA()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadApin(m_impl, ref state);
            return state;
        }

        public int GetPinStateQuadB()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadBpin(m_impl, ref state);
            return state;
        }

        public int GetPinStateQuadIdx()
        {
            int state = 0;
            Impl.C_TalonSRX_GetQuadIdxpin(m_impl, ref state);
            return state;
        }

        public int GetAnalogInPosition()
        {
            int position = 0;
            Impl.C_TalonSRX_GetAnalogInWithOv(m_impl, ref position);
            return position;
        }

        public int GetAnalogInRaw()
        {
            return GetAnalogInPosition() & 0x3FF;
        }

        public int GetAnalogInVelocity()
        {
            int velocity = 0;
            Impl.C_TalonSRX_GetAnalogInVel(m_impl, ref velocity);
            return velocity;
        }

        public int GetClosedLoopError()
        {
            int error = 0;
            Impl.C_TalonSRX_GetCloseLoopErr(m_impl, ref error);
            return error;
        }

        public bool IsForwardLimitSwitchClosed()
        {
            int state = 0;
            Impl.C_TalonSRX_GetLimitSwitchClosedFor(m_impl, ref state);
            return state != 0;
        }

        public bool IsReverseLimitSwitchClosed()
        {
            int state = 0;
            Impl.C_TalonSRX_GetLimitSwitchClosedFor(m_impl, ref state);
            return state != 0;
        }

        public bool IsBreakEnabledduringNeutral()
        {
            int state = 0;
            Impl.C_TalonSRX_GetBrakeIsEnabled(m_impl, ref state);
            return state != 0;
        }

        public double Temperature
        {
            get
            {
                double temp = 0.0;
                Impl.C_TalonSRX_GetTemp(m_impl, ref temp);
                return temp;
            }
        }

        public double OutputCurrent
        {
            get
            {
                double current = 0.0;
                Impl.C_TalonSRX_GetCurrent(m_impl, ref current);
                return current;
            }
        }

        public double OutputVoltage
        {
            get
            {
                int throttle = 0;
                Impl.C_TalonSRX_GetAppliedThrottle(m_impl, ref throttle);
                return BusVoltage * (throttle / 1023.0);
            }
        }

        public double BusVoltage
        {
            get
            {
                double voltage = 0.0;
                Impl.C_TalonSRX_GetBatteryV(m_impl, ref voltage);
                return voltage;
            }
        }

        public double Position
        {
            get
            {
                int pos = 0;
                Impl.C_TalonSRX_GetSensorPosition(m_impl, ref pos);
                return pos;
            }
        }

        public void SetPosition(double pos)
        {
            SetParam(HALCanTalonSRX.ParamID.eSensorPosition, pos);
        }

        public double Speed
        {
            get
            {
                int vel = 0;
                Impl.C_TalonSRX_GetSensorVelocity(m_impl, ref vel);
                return vel;
            }
        }

        public bool ForwardLimitOK
        {
            get
            {
                int limSwitch = FaultForwardLimit;
                int softLim = FaultForwardSoftLimit;
                return (softLim == 0 && limSwitch == 0);
            }
        }

        public bool ReverseLimitOK
        {
            get
            {
                int limSwitch = FaultReverseLimit;
                int softLim = FaultReverseSoftLimit;
                return (softLim == 0 && limSwitch == 0);
            }
        }

        public Faults Faults
        {
            get
            {
                Faults retVal = 0;

                int val;

                CTR_Code status;

                //Temp
                val = 0;
                status = Impl.C_TalonSRX_GetFault_OverTemp(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.TemperatureFault : 0;

                //Voltage
                val = 0;
                status = Impl.C_TalonSRX_GetFault_UnderVoltage(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.BusVoltageFault : 0;

                //Fwd Limit Switch
                val = 0;
                status = Impl.C_TalonSRX_GetFault_ForLim(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.FwdLimitSwitch : 0;

                //Rev Limit Switch
                val = 0;
                status = Impl.C_TalonSRX_GetFault_RevLim(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.RevLimitSwitch : 0;

                //Fwd Soft Limit
                val = 0;
                status = Impl.C_TalonSRX_GetFault_ForSoftLim(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.FwdSoftLimit : 0;

                //Rev Soft Limit
                val = 0;
                status = Impl.C_TalonSRX_GetFault_RevSoftLim(m_impl, ref val);

                if (status != CTR_Code.CTR_OKAY)
                {
                    Utility.CheckStatus((int)status);
                }

                retVal |= (val != 0) ? Faults.RevSoftLimit : 0;

                return retVal;
            }
        }

        private void ApplyControlMode(ControlMode value)
        {
            m_controlMode = value;
            if (value == ControlMode.Disabled)
                m_controlEnabled = false;
            Impl.C_TalonSRX_SetModeSelect(m_impl, (int)ControlMode.Disabled);
        }

        [Obsolete("Use MotorControlMode property.")]
        public ControlMode GetControlMode() { return MotorControlMode; }

        [Obsolete("Use MotorControlMode property.")]
        public void SetControlMode(ControlMode mode) { MotorControlMode = mode; }

        public ControlMode MotorControlMode
        {
            get { return m_controlMode; }
            set
            {
                if (m_controlMode == value) return;
                ApplyControlMode(value);
            }
        }

        [Obsolete("Use FeedBackDevice property instead.")]
        public FeedbackDevice GetFeedbackDevice()
        {
            return FeedBackDevice;
        }

        [Obsolete("Use FeedBackDevice property instead.")]
        public void SetFeedbackDevice(FeedbackDevice device)
        {
            FeedBackDevice = device;
        }

        public FeedbackDevice FeedBackDevice
        {
            get
            {
                int device = 0;
                Impl.C_TalonSRX_GetFeedbackDeviceSelect(m_impl, ref device);
                return (FeedbackDevice)device;
            }
            set
            {
                Impl.C_TalonSRX_SetFeedbackDeviceSelect(m_impl, (int)value);
            }
        }

        [Obsolete("Use ControlEnabled property instead.")]
        public bool IsControlEnabled()
        {
            return ControlEnabled;
        }

        [Obsolete("Set ControlEnabled property to true instead.")]
        public void EnableControl()
        {
            ControlEnabled = true;
        }

        [Obsolete("Set ControlEnabled property to false instead.")]
        public void DisableControl()
        {
            ControlEnabled = false;
        }

        public bool ControlEnabled
        {
            get
            {
                return m_controlEnabled;
            }
            set
            {
                if (m_controlEnabled == value) return;
                if (m_controlEnabled && !value)
                {
                    Impl.C_TalonSRX_SetModeSelect(m_impl, (int)ControlMode.Disabled);
                    m_controlEnabled = false;
                }
                else
                {
                    m_controlEnabled = true;
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
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_P);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_P);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_P, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_P, value);
            }
        }

        public double I
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_I);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_I);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_I, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_I, value);
            }
        }

        public double D
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_D);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_D);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_D, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_D, value);
            }
        }

        [Obsolete("Use F property instead.")]
        public double GetF() { return F; }
        [Obsolete("Use F property instead.")]
        public void SetF(double f) { F = f; }

        public double F
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_F);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_F);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_F, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_F, value);
            }
        }

        [Obsolete("Use IZone property instead.")]
        public double GetIZone() { return IZone; }
        [Obsolete("Use IZone property instead.")]
        public void SetIZone(double iZone) { IZone = iZone; }

        public double IZone
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone, value);
            }
        }

        public double GetIaccum()
        {
            EnsureInPIDMode();
            return GetParamInt32(HALCanTalonSRX.ParamID.ePidIaccum);
        }

        public void ClearIAccum()
        {
            EnsureInPIDMode();
            SetParam(HALCanTalonSRX.ParamID.ePidIaccum, 0.0);
        }

        [Obsolete("Use CloseLoopRampRate property instead.")]
        public double GetCloseLoopRampRate() { return CloseLoopRampRate; }
        [Obsolete("Use CloseLoopRampRate property instead.")]
        public void SetCloseLoopRampRate(double rate) { CloseLoopRampRate = rate; }

        public double CloseLoopRampRate
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate);
                else
                    return GetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate);
            }
            set
            {
                if (m_profile == 0)
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate, value);
                else
                    SetParam(HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate, value);
            }
        }

        public void SetPID(double p, double i, double value, double f, int izone, double closeLoopRampRate, int profile)
        {
            if (profile != 0 && profile != 1)
                throw new ArgumentOutOfRangeException("Talon PID profile must be 0 or 1.");
            m_profile = profile;
            P = p;
            I = i;
            D = value;
            F = f;
            IZone = izone;
            CloseLoopRampRate = closeLoopRampRate;
        }

        public void SetPID(double p, double i, double d)
        {
            SetPID(p, i, d, 0, 0, 0, m_profile);
        }

        [Obsolete("Use Setpoint property instead.")]
        public double GetSetpoint() { return Setpoint; }

        public double Setpoint
        {
            get
            {
                return m_setPoint;
            }
            set
            {
                Value = value;
            }
        }

        [Obsolete("Use Profile property instead.")]
        public int GetProfile() { return Profile; }
        [Obsolete("Use Profile property instead.")]
        public void SetProfile(int profile) { Profile = profile; }

        public int Profile
        {
            get { return m_profile; }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException("Talon PID profile must be 0 or 1.");
                m_profile = value;
                Impl.C_TalonSRX_SetProfileSlotSelect(m_impl, m_profile);
            }
        }



        [Obsolete("Use the VoltageRampRate property instead.")]
        public double GetVoltageRampRate() { return VoltageRampRate; }
        [Obsolete("Use VoltageRampRate property instead.")]
        public void SetVoltageRampRate(double rate) { VoltageRampRate = rate; }

        public NeutralMode ConfigNeutralMode
        {
            set
            {
                CTR_Code status;
                switch (value)
                {
                    default:
                    case NeutralMode.Jumper:
                        status = Impl.C_TalonSRX_SetOverrideBrakeType(m_impl,
                            HALCanTalonSRX.kBrakeOverride_UseDefaultsFromFlash);
                        break;
                    case NeutralMode.Brake:
                        status = Impl.C_TalonSRX_SetOverrideBrakeType(m_impl,
                            HALCanTalonSRX.kBrakeOverride_OverrideBrake);
                        break;
                    case NeutralMode.Coast:
                        status = Impl.C_TalonSRX_SetOverrideBrakeType(m_impl,
                            HALCanTalonSRX.kBrakeOverride_OverrideCoast);
                        break;
                }

                if (status != CTR_Code.CTR_OKAY)
                    Utility.CheckStatus((int)status);
            }
        }

        public int EncoderCodesPerRev
        {
            set { }
        }

        public int PotentiometerTurns
        {
            set { }
        }

        public void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition)
        {
            LimitMode = LimitMode.SoftPositionLimits;
            ForwardLimit = forwardLimitPosition;
            ReverseLimit = reverseLimitPosition;
        }

        public void DisableSoftPositionLimits()
        {
            LimitMode = LimitMode.SwitchInputsOnly;
        }

        public LimitMode LimitMode
        {
            set
            {
                CTR_Code status;
                switch (value)
                {
                    case LimitMode.SwitchInputsOnly:
#pragma warning disable 618
                        status = SetForwardSoftLimitEnabled(false);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = SetReverseSoftLimitEnabled(false);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = Impl.C_TalonSRX_SetOverrideLimitSwitchEn(m_impl,
                            HALCanTalonSRX.kLimitSwitchOverride_EnableFwd_EnableRev);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        break;
                    case LimitMode.SoftPositionLimits:
                        status = SetForwardSoftLimitEnabled(true);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = SetReverseSoftLimitEnabled(true);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = Impl.C_TalonSRX_SetOverrideLimitSwitchEn(m_impl,
                            HALCanTalonSRX.kLimitSwitchOverride_EnableFwd_EnableRev);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        break;
                    case LimitMode.SrxDisableSwitchInputs:
                        status = SetForwardSoftLimitEnabled(false);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = SetReverseSoftLimitEnabled(false);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        status = Impl.C_TalonSRX_SetOverrideLimitSwitchEn(m_impl,
                            HALCanTalonSRX.kLimitSwitchOverride_DisableFwd_DisableRev);
                        if (status != CTR_Code.CTR_OKAY)
                            Utility.CheckStatus((int)status);
                        break;
#pragma warning restore 618
                }
            }
        }

        public double ForwardLimit
        {
            set { ForwardSoftLimit = (value); }
        }

        public double ReverseLimit
        {
            set { ReverseSoftLimit = (value); }
        }

        public double MaxOutputVoltage
        {
            set { Utility.CheckStatus(-9); }
        }

        public float FaultTime
        {
            set { Utility.CheckStatus(-9); }
        }

        public double VoltageRampRate
        {
            get
            {
                return GetParamInt32(HALCanTalonSRX.ParamID.eRampThrottle);
            }
            set
            {
                int rate = (int)(value * 1023.0 / 12.0 / 100.0);
                Impl.C_TalonSRX_SetParam(m_impl, (int)HALCanTalonSRX.ParamID.eRampThrottle, rate);
            }
        }

        public uint FirmwareVersion
        {
            get
            {
                int version = 0;
                Impl.C_TalonSRX_GetFirmVers(m_impl, ref version);
                return (uint)version;
            }
        }

        [Obsolete("Use DeviceID property instead.")]
        public int GetDeviceID() { return DeviceID; }

        public int DeviceID => m_deviceNumber;

        [Obsolete("Use ForwardSoftLimit property instead.")]
        public double GetForwardSoftLimit() { return ForwardSoftLimit; }
        [Obsolete("Use ForwardSoftLimit poperty instead.")]
        public void SetForwardSoftLimit(double value) { ForwardSoftLimit = value; }

        public double ForwardSoftLimit
        {
            get
            {
                return GetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold);
            }
            set
            {

                SetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold, value);
            }
        }

        [Obsolete("Use ForwardSoftLimitEnabled property instead.")]
        public bool GetForwardSoftLimitEnabled() { return ForwardSoftLimitEnabled; }
        [Obsolete("Use ForwardSoftLimitEnabled poperty instead.")]
        public CTR_Code SetForwardSoftLimitEnabled(bool value)
        {
            return (CTR_Code)GetParamInt32(HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable);
        }
        public bool ForwardSoftLimitEnabled
        {
            get
            {
                return GetParamInt32(HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable) != 0;
            }
            set
            {

                SetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable, value ? 1 : 0);
            }
        }

        [Obsolete("Use ReverseSoftLimit property instead.")]
        public double GetReverseSoftLimit() { return ReverseSoftLimit; }
        [Obsolete("Use ReverseSoftLimit poperty instead.")]
        public void SetReverseSoftLimit(double value) { ReverseSoftLimit = value; }

        public double ReverseSoftLimit
        {
            get
            {
                return GetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold);
            }
            set
            {

                SetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold, value);
            }
        }

        [Obsolete("Use ReverseSoftLimitEnabled property instead.")]
        public bool GetReverseSoftLimitEnabled() { return ReverseSoftLimitEnabled; }
        [Obsolete("Use ReverseSoftLimitEnabled poperty instead.")]
        public CTR_Code SetReverseSoftLimitEnabled(bool value)
        {
            return (CTR_Code)GetParamInt32(HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable);
        }
        public bool ReverseSoftLimitEnabled
        {
            get
            {
                return GetParamInt32(HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable) != 0;
            }
            set
            {

                SetParam(HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable, value ? 1 : 0);
            }
        }
        public void ClearStickyFaults()
        {
            Impl.C_TalonSRX_ClearStickyFaults(m_impl);
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            int mask = 1 << 2 | (forward ? 1 : 0) << 1 | (reverse ? 1 : 0);
            Impl.C_TalonSRX_SetOverrideLimitSwitchEn(m_impl, mask);
        }

        [Obsolete("Use ForwardLimitSwitchNormallyOpen property instead.")]
        public void ConfigFwdLimitSwitchNormallyOpen(bool value) { ForwardLimitSwitchNormallyOpen = value; }

        public bool ForwardLimitSwitchNormallyOpen
        {
            get
            {
                return GetParamInt32(HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed) != 0;
            }
            set
            {
                SetParam(HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed, value ? 0 : 1);
            }
        }

        [Obsolete("Use ReverseLimitSwitchNormallyOpen property instead.")]
        public void ConfigRevLimitSwitchNormallyOpen(bool value) { ReverseLimitSwitchNormallyOpen = value; }

        public bool ReverseLimitSwitchNormallyOpen
        {
            get
            {
                return GetParamInt32(HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed) != 0;
            }
            set
            {
                SetParam(HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed, value ? 0 : 1);
            }
        }

        public void EnableBrakeMode(bool brake)
        {
            Impl.C_TalonSRX_SetOverrideBrakeType(m_impl, brake ? 2 : 1);
        }

        public int FaultOverTemp
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_OverTemp(m_impl, ref val);
                return val;
            }
        }

        public int FaultUnderVoltage
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_UnderVoltage(m_impl, ref val);
                return val;
            }
        }

        public int FaultForwardLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_ForLim(m_impl, ref val);
                return val;
            }
        }

        public int FaultReverseLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_RevLim(m_impl, ref val);
                return val;
            }
        }

        public int FaultHardwareFailure
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_HardwareFailure(m_impl, ref val);
                return val;
            }
        }

        public int FaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_ForSoftLim(m_impl, ref val);
                return val;
            }
        }

        public int FaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetFault_RevSoftLim(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultOverTemp
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_OverTemp(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultUnderVoltage
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_UnderVoltage(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultForwardLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_ForLim(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultReverseLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_RevLim(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_ForSoftLim(m_impl, ref val);
                return val;
            }
        }

        public int StickyFaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                Impl.C_TalonSRX_GetStckyFault_RevSoftLim(m_impl, ref val);
                return val;
            }
        }

        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }

        public bool Alive => m_safetyHelper.Alive;

        [Obsolete("Set the ControlEnabled to false.")]
        public void StopMotor()
        {
            ControlEnabled = false;
        }

        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        public string Description => $"CAN TalonSRX ID {m_deviceNumber}";

        public double PidWrite
        {
            set
            {
                if (m_controlMode == ControlMode.PercentVbus)
                {
                    Value = value;
                }
                else
                {
                    throw new InvalidOperationException("PID on RoboRIO only supported in Voltage Bus (PWM-like) mode");
                }
            }
        }

        public double Value
        {
            get
            {
                int value = 0;
                switch (m_controlMode)
                {
                    case ControlMode.Voltage:
                        return OutputVoltage;
                    case ControlMode.Position:
                        Impl.C_TalonSRX_GetSensorPosition(m_impl, ref value);
                        return value;
                    case ControlMode.Speed:
                        Impl.C_TalonSRX_GetSensorVelocity(m_impl, ref value);
                        return value;
                    case ControlMode.Current:
                        return OutputCurrent;
                    case ControlMode.PercentVbus:
                    default:
                        Impl.C_TalonSRX_GetAppliedThrottle(m_impl, ref value);
                        return value / 1023.0;
                }
            }
            set
            {
                m_safetyHelper.Feed();
                if (m_controlEnabled)
                {
                    m_setPoint = value;
                    switch (m_controlMode)
                    {
                        case ControlMode.PercentVbus:
                            Impl.C_TalonSRX_SetDemand(m_impl, (int)(value * 1023));
                            break;
                        case ControlMode.Voltage:
                            int volts = (int)(value * 256);
                            Impl.C_TalonSRX_SetDemand(m_impl, volts);
                            break;
                        case ControlMode.Position:
                        case ControlMode.Speed:
                        case ControlMode.Follower:
                            Impl.C_TalonSRX_SetDemand(m_impl, (int)value);
                            break;
                        default:
                            break;
                    }
                    Impl.C_TalonSRX_SetModeSelect(m_impl, (int)MotorControlMode);
                }
            }
        }

        [Obsolete("This is only here to make CAN Jaguars happy")]
        public void Set(double value, byte syncGroup)
        {
            Value = value;
        }

        public void SelectProfileSlot(int slotIdx)
        {
            m_profile = (slotIdx == 0) ? 0 : 1;
            CTR_Code status = Impl.C_TalonSRX_SetProfileSlotSelect(m_impl, m_profile);
            if (status != CTR_Code.CTR_OKAY)
                Utility.CheckStatus((int)status);
        }

        public void Disable()
        {
            ControlEnabled = false;
        }

        public void UpdateTable()
        {
            Table?.PutNumber("Value", Value);
        }

        public void StartLiveWindowMode()
        {
            Value = 0.0;
            Table.AddTableListener("Value", this, true);
        }

        public void StopLiveWindowMode()
        {
            Value = 0.0;
            Table.RemoveTableListener(this);
        }

        public string SmartDashboardType => "Speed Controller";

        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            Value = (double)value;
        }

        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        public ITable Table { get; private set; }
    }
}
