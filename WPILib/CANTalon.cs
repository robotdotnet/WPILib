using System;
using HAL_Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Interfaces;
using WPILib.LiveWindows;
using static HAL_Base.HALCanTalonSRX;
using static HAL_Base.HALCanTalonSRX.Constants;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// This Class represents a CAN Talon SRX Motor Controller.
    /// </summary>
    public class CANTalon : IMotorSafety, ICANSpeedController, ITableListener, IPIDOutput, IPIDSource
    {
        private readonly MotorSafetyHelper m_safetyHelper;

        private const int NativeAdcUnitsPerRotation = 1024;

        private const double NativePwdUnitsPerRotation = 4096.0;

        private const double MinutesPer100MsUnits = 1.0 / 600.0;

        /// <summary>
        /// Feedback type for CAN Talon
        /// </summary>
        public enum FeedbackDevice
        {
            /// <summary>
            /// A quadrature encoder.
            /// </summary>
            QuadEncoder = 0,
            /// <summary>
            /// An analog potentiometer.
            /// </summary>
            AnalogPotentiometer = 2,
            /// <summary>
            /// An analog encoder.
            /// </summary>
            AnalogEncoder = 3,
            /// <summary>
            /// An encoder that only reports when it hits a rising edge.
            /// </summary>
            EncoderRising = 4,
            /// <summary>
            /// An encoder that only reports when it hits a falling edge.
            /// </summary>
            EncoderFalling = 5,
            /// <summary>
            /// Relative magnetic encoder.
            /// </summary>
            CtreMagEncoderRelative = 6,
            /// <summary>
            /// Absolute magnetic encoder
            /// </summary>
            CtreMagEncoderAbsolute = 7,
            /// <summary>
            /// Encoder is a pulse width sensor.
            /// </summary>
            PulseWidth = 8
        }

        /// <summary>
        /// Status Rate for CAN Talon
        /// </summary>
        public enum StatusFrameRate
        {
            /// <summary>
            /// Requests a general status frame
            /// </summary>
            General = 0,
            /// <summary>
            /// Requests a feedback status frame
            /// </summary>
            Feedback = 1,
            /// <summary>
            /// Quad encoder status frame
            /// </summary>
            QuadEncoder = 2,
            /// <summary>
            /// Analog temp vbat status frame.
            /// </summary>
            AnalogTempVbat = 3,
            /// <summary>
            /// Pulse width status frame.
            /// </summary>
            PulseWidth = 4
        }

        /// <summary>
        /// The Feedback device status.
        /// </summary>
        public enum FeedbackDeviceStatus
        {
            /// <summary>
            /// Status unknown
            /// </summary>
            FeedbackStatusUnknown = 0,
            /// <summary>
            /// Status present
            /// </summary>
            FeedbackStatusPresent = 1,
            /// <summary>
            /// Status not present
            /// </summary>
            FeedbackStatusNotPresent = 2
        }


        private ControlMode m_controlMode;
        private readonly IntPtr m_talonPointer;
        private const double DelayForSolicitedSignals = 0.004;
        private bool m_controlEnabled;
        private int m_profile;
        private double m_setPoint;

        /// <inheritdoc/>
        public bool Inverted { get; set; }

        private int m_codesPerRev;

        private int m_numPotTurns;

        private FeedbackDevice m_feedbackDevice;


        /// <summary>
        /// The max Id allowed for a CAN Talon
        /// </summary>
        public const int TalonIds = 62;

        protected static Resource s_talonIds = new Resource(TalonIds);


        /// <summary>
        /// Constructs a CANTalon object.
        /// </summary>
        /// <param name="deviceNumber">The id of the Talon SRX this object will communicate with.</param>
        /// <param name="controlPeriodMs">The update period to the Talon SRX.  Defaults to 10ms.</param>
        public CANTalon(int deviceNumber, int controlPeriodMs = 10)
        {
            if (deviceNumber < 0 || deviceNumber >= TalonIds)
            {
                throw new ArgumentOutOfRangeException(nameof(deviceNumber), "Talon IDs must be between 0 and 62 inclusive.");
            }

            s_talonIds.Allocate(deviceNumber, $"CAN TalonSRX ID {deviceNumber} is already allocated.");

            DeviceID = deviceNumber;
            m_talonPointer = C_TalonSRX_Create(deviceNumber, controlPeriodMs);
            m_safetyHelper = new MotorSafetyHelper(this);
            m_controlEnabled = true;
            m_setPoint = 0;
            Profile = 0;
            m_codesPerRev = 0;
            m_numPotTurns = 0;
            m_feedbackDevice = FeedbackDevice.QuadEncoder;
            ApplyControlMode(ControlMode.PercentVbus);
            LiveWindow.AddActuator("CANTalonSRX", deviceNumber, this);
            HAL.Report(ResourceType.kResourceType_CANTalonSRX, (byte)(deviceNumber + 1), (byte)m_controlMode);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            s_talonIds.Deallocate(DeviceID);
            C_TalonSRX_Destroy(m_talonPointer);
        }

        private double GetParam(ParamID id)
        {
            C_TalonSRX_RequestParam(m_talonPointer, (int)id);
            Timer.Delay(DelayForSolicitedSignals);
            var value = 0.0;
            var status = C_TalonSRX_GetParamResponse(m_talonPointer, (int)id, ref value);
            if (status != CTR_Code.CTR_OKAY)
                CheckStatus((int)status);
            return value;
        }

        private int GetParamInt32(ParamID id)
        {
            int value;
            var status = GetParamInt32(id, out value);
            if (status != CTR_Code.CTR_OKAY)
                CheckStatus((int)status);
            return value;
        }

        private CTR_Code GetParamInt32(ParamID id, out int value)
        {
            C_TalonSRX_RequestParam(m_talonPointer, (int)id);
            Timer.Delay(DelayForSolicitedSignals);
            value = 0;
            return C_TalonSRX_GetParamResponseInt32(m_talonPointer, (int)id, ref value);
        }

        private void SetParam(ParamID id, double value)
        {
            var errorCode = C_TalonSRX_SetParam(m_talonPointer, (int)id, value);
            if (errorCode != CTR_Code.CTR_OKAY)
                CheckStatus((int)errorCode);
        }

        /// <summary>
        /// Sets whether to reverse the input sensor.
        /// </summary>
        /// <param name="flip">True to reverse, false to not</param>
        public void ReverseSensor(bool flip)
        {
            C_TalonSRX_SetRevFeedbackSensor(m_talonPointer, flip ? 1 : 0);
        }

        /// <summary>
        /// Sets whether to reverse the output.
        /// </summary>
        /// <param name="flip">True to reverse, false to not.</param>
        public void ReverseOutput(bool flip)
        {
            C_TalonSRX_SetRevMotDuringCloseLoopEn(m_talonPointer, flip ? 1 : 0);
        }

        /// <summary>
        /// Gets the current encoder position.
        /// </summary>
        /// <returns>The current encoder position.</returns>
        public int GetEncoderPosition()
        {
            int pos = 0;
            C_TalonSRX_GetEncPosition(m_talonPointer, ref pos);
            return pos;
        }

        /// <summary>
        /// Resets the encoder position to a specified point.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetEncoderPostition(int newPosition)
        {
            SetParam(ParamID.eEncPosition, newPosition);
        }

        /// <summary>
        /// Gets the current encoder velocity.
        /// </summary>
        /// <returns>The current encoder velocity</returns>
        public int GetEncoderVelocity()
        {
            int vel = 0;
            C_TalonSRX_GetEncVel(m_talonPointer, ref vel);
            return vel;
        }

        /// <summary>
        /// Gets the pulse width postion.
        /// </summary>
        /// <returns>The pulse width position</returns>
        public int GetPulseWidthPosition()
        {
            throw new NotImplementedException("Waiting on additions to the HAL");
        }

        /// <summary>
        /// Resets the pulse width position to a specified point.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetPulseWidthPosition(int newPosition)
        {
            SetParam(ParamID.ePwdPosition, newPosition);
        }

        /// <summary>
        /// Gets the pulse width velocity.
        /// </summary>
        /// <returns>The pulse width velocity.</returns>
        public int GetPulseWidthVelocity()
        {
            throw new NotImplementedException("Waiting on additions to the HAL");
        }
        
        /// <summary>
        /// Gets the pulse width rise to fall time.
        /// </summary>
        /// <returns>The pulse width time in microseconds.</returns>
        public int GetPulseWidthRiseToFallUs()
        {
            throw new NotImplementedException("Waiting on additions to the HAL");
        }

        /// <summary>
        /// Gets the pulse width rise to rise time.
        /// </summary>
        /// <returns>The pulse width time in microseconds.</returns>
        public int GetPulseWidthRiseToRiseUs()
        {
            throw new NotImplementedException("Waiting on additions to the HAL");
        }

        /// <summary>
        /// Gets whether the sensor is present.
        /// </summary>
        /// <param name="feedbackDevice">The sensor to check for.</param>
        /// <returns>The status of the feedback device.</returns>
        public FeedbackDeviceStatus IsSensorPresent(FeedbackDevice feedbackDevice)
        {
            FeedbackDeviceStatus retVal = FeedbackDeviceStatus.FeedbackStatusUnknown;
            switch (feedbackDevice)
            {
                case FeedbackDevice.QuadEncoder:
                case FeedbackDevice.AnalogPotentiometer:
                case FeedbackDevice.AnalogEncoder:
                case FeedbackDevice.EncoderRising:
                case FeedbackDevice.EncoderFalling:
                    break;
                case FeedbackDevice.CtreMagEncoderRelative:
                case FeedbackDevice.CtreMagEncoderAbsolute:
                case FeedbackDevice.PulseWidth:
                    long value = 0;
                    throw new NotImplementedException("Waiting for additions to the HAL");
            }
            return retVal;
        }

        /// <summary>
        /// Gets the number of quadrature index rises
        /// </summary>
        /// <returns>The number of rises on the index pin.</returns>
        public int GetNumberOfQuadIdxRises()
        {
            int state = 0;
            C_TalonSRX_GetEncIndexRiseEvents(m_talonPointer, ref state);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature A pin.
        /// </summary>
        /// <returns>The state of the A pin</returns>
        public int GetPinStateQuadA()
        {
            int state = 0;
            C_TalonSRX_GetQuadApin(m_talonPointer, ref state);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature B pin.
        /// </summary>
        /// <returns>The state of the B pin</returns>
        public int GetPinStateQuadB()
        {
            int state = 0;
            C_TalonSRX_GetQuadBpin(m_talonPointer, ref state);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature index pin.
        /// </summary>
        /// <returns>The state of the index pin</returns>
        public int GetPinStateQuadIdx()
        {
            int state = 0;
            C_TalonSRX_GetQuadIdxpin(m_talonPointer, ref state);
            return state;
        }

        /// <summary>
        /// Resets the analog position to a new position.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetAnalogPosition(int newPosition)
        {
            SetParam(ParamID.eAinPosition, (double)newPosition);
        }

        /// <summary>
        /// Gets the analog input position, regardless of whether it is in the current feedback device.
        /// </summary>
        /// <returns>The 24 bit analog position. The bottom 10 bits are the ADC value, 
        /// the upper 14 bits track the overflows and underflows.</returns>
        public int GetAnalogInPosition()
        {
            int position = 0;
            C_TalonSRX_GetAnalogInWithOv(m_talonPointer, ref position);
            return position;
        }

        /// <summary>
        /// Gets the analog input raw position, regardless of whether it is in the current feedback device.
        /// </summary>
        /// <returns>The ADC (0-1023) value on the analog pin.</returns>
        public int GetAnalogInRaw()
        {
            return GetAnalogInPosition() & 0x3FF;
        }

        /// <summary>
        /// Gets the analog input velocity, regardless of whether it is in the current feedback device.
        /// </summary>
        /// <returns>The analog input velocity.</returns>
        public int GetAnalogInVelocity()
        {
            int velocity = 0;
            C_TalonSRX_GetAnalogInVel(m_talonPointer, ref velocity);
            return velocity;
        }

        /// <summary>
        /// Gets the current difference between the setpoint and the sensor value.
        /// </summary>
        /// <returns>The error in the PID Controller.</returns>
        public int GetClosedLoopError()
        {
            int error = 0;
            C_TalonSRX_GetCloseLoopErr(m_talonPointer, ref error);
            return error;
        }

        /// <summary>
        /// Sets the max allowable closed loop error.
        /// </summary>
        /// <param name="allowableCloseLoopError">The max allowable close looped error for the selected profile.</param>
        public void SetAllowableClosedLoopErr(int allowableCloseLoopError)
        {
            if (m_profile == 0)
            {
                SetParam(ParamID.eProfileParamSlot0_AllowableClosedLoopErr, (double)allowableCloseLoopError);
            }
            else
            {
                SetParam(ParamID.eProfileParamSlot1_AllowableClosedLoopErr, (double)allowableCloseLoopError);
            }
        }

        /// <summary>
        /// Gets the value of the forward limit switch.
        /// </summary>
        /// <returns>True if the limit switch is closed, otherwise false.</returns>
        public bool IsForwardLimitSwitchClosed()
        {
            int state = 0;
            C_TalonSRX_GetLimitSwitchClosedFor(m_talonPointer, ref state);
            return state != 0;
        }

        /// <summary>
        /// Gets the value of the reverse limit switch.
        /// </summary>
        /// <returns>True if the limit switch is closed, otherwise false.</returns>
        public bool IsReverseLimitSwitchClosed()
        {
            int state = 0;
            C_TalonSRX_GetLimitSwitchClosedRev(m_talonPointer, ref state);
            return state != 0;
        }

        /// <summary>
        /// Returns whether the brake is enabled during neutral.
        /// </summary>
        /// <returns>True if brake mode, false if coast mode.</returns>
        public bool IsBrakeEnabledDuringNeutral()
        {
            int state = 0;
            C_TalonSRX_GetBrakeIsEnabled(m_talonPointer, ref state);
            return state != 0;
        }

        /// <inheritdoc/>
        public int EncoderCodesPerRev
        {
            set
            {
                m_codesPerRev = value;
                SetParam(ParamID.eNumberEncoderCPR, m_codesPerRev);
            }
        }

        /// <inheritdoc/>
        public int PotentiometerTurns
        {
            set
            {
                m_numPotTurns = value;
                SetParam(ParamID.eNumberPotTurns, m_numPotTurns);
            }
        }

        /// <inheritdoc/>
        public double GetTemperature()
        {
            double temp = 0.0;
            C_TalonSRX_GetTemp(m_talonPointer, ref temp);
            return temp;
        }

        /// <inheritdoc/>
        public double GetOutputCurrent()
        {
            double current = 0.0;
            C_TalonSRX_GetCurrent(m_talonPointer, ref current);
            return current;
        }

        /// <inheritdoc/>
        public double GetOutputVoltage()
        {
            int throttle = 0;
            C_TalonSRX_GetAppliedThrottle(m_talonPointer, ref throttle);
            return GetBusVoltage() * (throttle / 1023.0);
        }

        /// <inheritdoc/>
        public double GetBusVoltage()
        {
            double voltage = 0.0;
            C_TalonSRX_GetBatteryV(m_talonPointer, ref voltage);
            return voltage;
        }

        /// <inheritdoc/>
        public double GetPosition()
        {
            int pos = 0;
            C_TalonSRX_GetSensorPosition(m_talonPointer, ref pos);
            return ScaleNativeUnitsToRotations(m_feedbackDevice, pos);
        }

        /// <summary>
        /// Sets the position of the encoder or potentiometer
        /// </summary>
        /// <param name="pos">The new position of the sensor providing feedback.</param>
        public void SetPosition(double pos)
        {
            int nativePos = ScaleRotationsToNativeUnits(m_feedbackDevice, pos);
            SetParam(ParamID.eSensorPosition, nativePos);
        }

        /// <inheritdoc/>
        public double GetSpeed()
        {
            int vel = 0;
            C_TalonSRX_GetSensorVelocity(m_talonPointer, ref vel);
            return ScaleNativeUnitsToRpm(m_feedbackDevice, vel);
        }

        /// <inheritdoc/>
        public bool GetForwardLimitOk()
        {
            int limSwitch = FaultForwardLimit;
            int softLim = FaultForwardSoftLimit;
            return (softLim == 0 && limSwitch == 0);
        }

        /// <inheritdoc/>
        public bool GetReverseLimitOk()
        {
            int limSwitch = FaultReverseLimit;
            int softLim = FaultReverseSoftLimit;
            return (softLim == 0 && limSwitch == 0);
        }

        /// <inheritdoc/>
        public Faults GetFaults()
        {
            Faults retVal = 0;

            //Temp
            var val = 0;
            var status = C_TalonSRX_GetFault_OverTemp(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.TemperatureFault : 0;

            //Voltage
            val = 0;
            status = C_TalonSRX_GetFault_UnderVoltage(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.BusVoltageFault : 0;

            //Fwd Limit Switch
            val = 0;
            status = C_TalonSRX_GetFault_ForLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.FwdLimitSwitch : 0;

            //Rev Limit Switch
            val = 0;
            status = C_TalonSRX_GetFault_RevLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.RevLimitSwitch : 0;

            //Fwd Soft Limit
            val = 0;
            status = C_TalonSRX_GetFault_ForSoftLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.FwdSoftLimit : 0;

            //Rev Soft Limit
            val = 0;
            status = C_TalonSRX_GetFault_RevSoftLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckStatus((int)status);
            }

            retVal |= (val != 0) ? Faults.RevSoftLimit : 0;

            return retVal;
        }

        private void ApplyControlMode(ControlMode value)
        {
            m_controlMode = value;
            if (value == ControlMode.Disabled)
                m_controlEnabled = false;
            C_TalonSRX_SetModeSelect(m_talonPointer, (int)ControlMode.Disabled);
        }

        /// <summary>
        /// Gets the control mode.
        /// </summary>
        /// <returns>The current control mode</returns>
        [Obsolete("Use MotorControlMode property.")]
        public ControlMode GetControlMode() { return MotorControlMode; }

        /// <summary>
        /// Sets the control mode.
        /// </summary>
        /// <param name="mode">The control mode to set.</param>
        [Obsolete("Use MotorControlMode property.")]
        public void SetControlMode(ControlMode mode) { MotorControlMode = mode; }

        /// <inheritdoc/>
        public ControlMode MotorControlMode
        {
            get { return m_controlMode; }
            set
            {
                if (m_controlMode == value) return;
                ApplyControlMode(value);
            }
        }

        /// <summary>
        /// Gets the feedback device.
        /// </summary>
        /// <returns>The current feedback device.</returns>
        [Obsolete("Use FeedBackDevice property instead.")]
        public FeedbackDevice GetFeedbackDevice()
        {
            return FeedBackDevice;
        }

        /// <summary>
        /// Sets the feedback device.
        /// </summary>
        /// <param name="device">The feedback device to set.</param>
        [Obsolete("Use FeedBackDevice property instead.")]
        public void SetFeedbackDevice(FeedbackDevice device)
        {
            FeedBackDevice = device;
        }

        /// <summary>
        /// Gets or sets the feedback device to be used by the talon.
        /// </summary>
        /// <remarks>
        /// TODO: SOLVE THIS
        /// </remarks>
        public FeedbackDevice FeedBackDevice
        {
            get
            {
                int device = 0;
                C_TalonSRX_GetFeedbackDeviceSelect(m_talonPointer, ref device);
                return (FeedbackDevice)device;
            }
            set
            {
                m_feedbackDevice = value;
                C_TalonSRX_SetFeedbackDeviceSelect(m_talonPointer, (int)value);
            }
        }

        /// <summary>
        /// Returns if the closed loop mode of the controller is enabled.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use ControlEnabled property instead.")]
        public bool IsControlEnabled()
        {
            return ControlEnabled;
        }

        /// <summary>
        /// Enables the closed loop controller of the talon.
        /// </summary>
        /// <remarks>
        /// Starts controlling the output based on the feedback.
        /// </remarks>
        [Obsolete("Set ControlEnabled property to true instead.")]
        public void EnableControl()
        {
            ControlEnabled = true;
        }

        /// <inheritdoc/>
        public void Enable() => ControlEnabled = true;

        /// <summary>
        /// Disables the closed loop control of the talon.
        /// </summary>
        [Obsolete("Set ControlEnabled property to false instead.")]
        public void DisableControl()
        {
            ControlEnabled = false;
        }

        /// <summary>
        /// Gets or Sets whether closed loop control is enabled on the talon.
        /// </summary>
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
                    C_TalonSRX_SetModeSelect(m_talonPointer, (int)ControlMode.Disabled);
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

        /// <inheritdoc/>
        public double P
        {
            get
            {
                EnsureInPIDMode();
                if (m_profile == 0)
                    return GetParam(ParamID.eProfileParamSlot0_P);
                else
                    return GetParam(ParamID.eProfileParamSlot1_P);
            }
            set { SetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_P : ParamID.eProfileParamSlot1_P, value); }
        }

        /// <inheritdoc/>
        public double I
        {
            get
            {
                EnsureInPIDMode();
                return GetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_I : ParamID.eProfileParamSlot1_I);
            }
            set { SetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_I : ParamID.eProfileParamSlot1_I, value); }
        }

        /// <inheritdoc/>
        public double D
        {
            get
            {
                EnsureInPIDMode();
                return GetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_D : ParamID.eProfileParamSlot1_D);
            }
            set { SetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_D : ParamID.eProfileParamSlot1_D, value); }
        }

        /// <inheritdoc/>
        public double F
        {
            get
            {
                EnsureInPIDMode();
                return GetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_F : ParamID.eProfileParamSlot1_F);
            }
            set { SetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_F : ParamID.eProfileParamSlot1_F, value); }
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
                return GetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_IZone : ParamID.eProfileParamSlot1_IZone);
            }
            set
            {
                SetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_IZone : ParamID.eProfileParamSlot1_IZone, value);
            }
        }

        public double GetIaccum()
        {
            EnsureInPIDMode();
            return GetParamInt32(ParamID.ePidIaccum);
        }

        public void ClearIAccum()
        {
            EnsureInPIDMode();
            SetParam(ParamID.ePidIaccum, 0.0);
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
                return GetParam(m_profile == 0 ? ParamID.eProfileParamSlot0_CloseLoopRampRate : ParamID.eProfileParamSlot1_CloseLoopRampRate);
            }
            set
            {
                SetParam(m_profile == 0
                    ? ParamID.eProfileParamSlot0_CloseLoopRampRate
                    : ParamID.eProfileParamSlot1_CloseLoopRampRate, value);
            }
        }

        /// <summary>
        /// Sets the PID and extra constants of the controler.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="value"></param>
        /// <param name="f"></param>
        /// <param name="izone"></param>
        /// <param name="closeLoopRampRate"></param>
        /// <param name="profile"></param>
        public void SetPID(double p, double i, double value, double f, int izone, double closeLoopRampRate, int profile)
        {
            if (profile != 0 && profile != 1)
                throw new ArgumentOutOfRangeException(nameof(profile), "Talon PID profile must be 0 or 1.");
            m_profile = profile;
            P = p;
            I = i;
            D = value;
            F = f;
            IZone = izone;
            CloseLoopRampRate = closeLoopRampRate;
        }

        /// <inheritdoc/>
        public void SetPID(double p, double i, double d)
        {
            SetPID(p, i, d, 0, 0, 0, m_profile);
        }

        [Obsolete("Use Setpoint property instead.")]
        public double GetSetpoint() { return Setpoint; }

        /// <inheritdoc/>
        public double Setpoint
        {
            get
            {
                return m_setPoint;
            }
            set
            {
                Set(value);
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
                    throw new ArgumentOutOfRangeException(nameof(value), "Talon PID profile must be 0 or 1.");
                m_profile = value;
                C_TalonSRX_SetProfileSlotSelect(m_talonPointer, m_profile);
            }
        }



        [Obsolete("Use the VoltageRampRate property instead.")]
        public double GetVoltageRampRate() { return VoltageRampRate; }
        [Obsolete("Use VoltageRampRate property instead.")]
        public void SetVoltageRampRate(double rate) { VoltageRampRate = rate; }

        public void SetVoltageCompensationRampRate(double rampRate)
        {
            SetParam(ParamID.eProfileParamVcompRate, rampRate / 1000);
        }

        public double GetVoltageCompensationRampRate()
        {
            return GetParam(ParamID.eProfileParamVcompRate);
        }

        /// <inheritdoc/>
        public NeutralMode NeutralMode
        {
            set
            {
                CTR_Code status;
                switch (value)
                {
                    default:
                    case NeutralMode.Jumper:
                        status = C_TalonSRX_SetOverrideBrakeType(m_talonPointer,
                            kBrakeOverride_UseDefaultsFromFlash);
                        break;
                    case NeutralMode.Brake:
                        status = C_TalonSRX_SetOverrideBrakeType(m_talonPointer,
                            kBrakeOverride_OverrideBrake);
                        break;
                    case NeutralMode.Coast:
                        status = C_TalonSRX_SetOverrideBrakeType(m_talonPointer,
                            kBrakeOverride_OverrideCoast);
                        break;
                }

                if (status != CTR_Code.CTR_OKAY)
                    CheckStatus((int)status);
            }
        }

        /// <inheritdoc/>
        public void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition)
        {
            LimitMode = LimitMode.SoftPositionLimits;
            ForwardLimit = forwardLimitPosition;
            ReverseLimit = reverseLimitPosition;
        }

        /// <inheritdoc/>
        public void DisableSoftPositionLimits()
        {
            LimitMode = LimitMode.SwitchInputsOnly;
        }

        /// <inheritdoc/>
        public LimitMode LimitMode
        {
            set
            {
                switch (value)
                {
                    case LimitMode.SwitchInputsOnly:
                        ForwardSoftLimitEnabled = false;
                        ReverseSoftLimitEnabled = false;
                        EnableLimitSwitches(true, true);
                        break;
                    case LimitMode.SoftPositionLimits:
                        ForwardSoftLimitEnabled = true;
                        ReverseSoftLimitEnabled = true;
                        EnableLimitSwitches(true, true);
                        break;
                    case LimitMode.SrxDisableSwitchInputs:
                        ForwardSoftLimitEnabled = false;
                        ReverseSoftLimitEnabled = false;
                        EnableLimitSwitches(false, false);
                        break;
                }
            }
        }

        /// <inheritdoc/>
        public double ForwardLimit
        {
            set { ForwardSoftLimit = (value); }
        }

        /// <inheritdoc/>
        public double ReverseLimit
        {
            set { ReverseSoftLimit = (value); }
        }

        /// <inheritdoc/>
        public double MaxOutputVoltage
        {
            set
            {
                ConfigPeakOutputVoltage(value, -value);
            }
        }

        /// <inheritdoc/>
        public float FaultTime
        {
            set { CheckStatus(-9); }
        }

        /// <summary>
        /// Gets or sets the maximum voltage change rate in Volts/s.
        /// </summary>
        /// <remarks>
        /// When in <see cref="ControlMode.PercentVbus"/> or <see cref="ControlMode.Voltage"/> output mode,
        /// the rate at which the voltage changes can be limited to reduce current spike. Set this to 0.0
        /// to disable rate limiting.
        /// </remarks>
        public double VoltageRampRate
        {
            get
            {
                return GetParamInt32(ParamID.eRampThrottle);
            }
            set
            {
                int rate = (int)(value * 1023.0 / 12.0 / 100.0);
                C_TalonSRX_SetParam(m_talonPointer, (int)ParamID.eRampThrottle, rate);
            }
        }

        /// <inheritdoc/>
        public uint FirmwareVersion
        {
            get
            {
                int version = 0;
                C_TalonSRX_GetFirmVers(m_talonPointer, ref version);
                return (uint)version;
            }
        }

        [Obsolete("Use DeviceID property instead.")]
        public int GetDeviceID() { return DeviceID; }

        public int DeviceID { get; }

        [Obsolete("Use ForwardSoftLimit property instead.")]
        public double GetForwardSoftLimit() { return ForwardSoftLimit; }
        [Obsolete("Use ForwardSoftLimit poperty instead.")]
        public void SetForwardSoftLimit(double value) { ForwardSoftLimit = value; }

        public double ForwardSoftLimit
        {
            get
            {
                return GetParam(ParamID.eProfileParamSoftLimitForThreshold);
            }
            set
            {
                double nativeLimitPos = ScaleRotationsToNativeUnits(m_feedbackDevice, value);
                SetParam(ParamID.eProfileParamSoftLimitForThreshold, nativeLimitPos);
            }
        }

        [Obsolete("Use ForwardSoftLimitEnabled property instead.")]
        public bool GetForwardSoftLimitEnabled() { return ForwardSoftLimitEnabled; }
        [Obsolete("Use ForwardSoftLimitEnabled poperty instead.")]
        public void SetForwardSoftLimitEnabled(bool value)
        {
            ForwardSoftLimitEnabled = value;
        }
        public bool ForwardSoftLimitEnabled
        {
            get
            {
                return GetParamInt32(ParamID.eProfileParamSoftLimitForEnable) != 0;
            }
            set
            {
                SetParam(ParamID.eProfileParamSoftLimitForEnable, value ? 1 : 0);
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
                return GetParam(ParamID.eProfileParamSoftLimitRevThreshold);
            }
            set
            {
                double nativeLimitPos = ScaleRotationsToNativeUnits(m_feedbackDevice, value);
                SetParam(ParamID.eProfileParamSoftLimitRevThreshold, nativeLimitPos);
            }
        }

        [Obsolete("Use ReverseSoftLimitEnabled property instead.")]
        public bool GetReverseSoftLimitEnabled() { return ReverseSoftLimitEnabled; }
        [Obsolete("Use ReverseSoftLimitEnabled poperty instead.")]
        public void SetReverseSoftLimitEnabled(bool value)
        {
            ReverseSoftLimitEnabled = value;
        }
        public bool ReverseSoftLimitEnabled
        {
            get
            {
                return GetParamInt32(ParamID.eProfileParamSoftLimitRevEnable) != 0;
            }
            set
            {
                SetParam(ParamID.eProfileParamSoftLimitRevEnable, value ? 1 : 0);
            }
        }
        public void ClearStickyFaults()
        {
            C_TalonSRX_ClearStickyFaults(m_talonPointer);
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            int mask = 1 << 2 | (forward ? 1 : 0) << 1 | (reverse ? 1 : 0);
            CTR_Code status = C_TalonSRX_SetOverrideLimitSwitchEn(m_talonPointer, mask);
            if (status != CTR_Code.CTR_OKAY)
                CheckStatus((int)status);
        }

        [Obsolete("Use ForwardLimitSwitchNormallyOpen property instead.")]
        public void ConfigFwdLimitSwitchNormallyOpen(bool value) { ForwardLimitSwitchNormallyOpen = value; }

        public bool ForwardLimitSwitchNormallyOpen
        {
            get
            {
                return GetParamInt32(ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed) != 0;
            }
            set
            {
                SetParam(ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed, value ? 0 : 1);
            }
        }

        [Obsolete("Use ReverseLimitSwitchNormallyOpen property instead.")]
        public void ConfigRevLimitSwitchNormallyOpen(bool value) { ReverseLimitSwitchNormallyOpen = value; }

        public bool ReverseLimitSwitchNormallyOpen
        {
            get
            {
                return GetParamInt32(ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed) != 0;
            }
            set
            {
                SetParam(ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed, value ? 0 : 1);
            }
        }

        public void ConfigPeakOutputVoltage(double forwardVoltage, double reverseVoltage)
        {
            if (forwardVoltage > 12)
                forwardVoltage = 12;
            else if (forwardVoltage < 0)
                forwardVoltage = 0;
            if (reverseVoltage > 0)
                reverseVoltage = 0;
            else if (reverseVoltage < -12)
                reverseVoltage = -12;
            SetParam(ParamID.ePeakPosOutput, 1023 * forwardVoltage / 12.0);
            SetParam(ParamID.ePeakNegOutput, 1023 * reverseVoltage / 12.0);
        }

        public void ConfigNominalOutputVoltage(double forwardVoltage, double reverseVoltage)
        {
            if (forwardVoltage > 12)
                forwardVoltage = 12;
            else if (forwardVoltage < 0)
                forwardVoltage = 0;
            if (reverseVoltage > 0)
                reverseVoltage = 0;
            else if (reverseVoltage < -12)
                reverseVoltage = -12;
            SetParam(ParamID.eNominalPosOutput, 1023 * forwardVoltage / 12.0);
            SetParam(ParamID.eNominalNegOutput, 1023 * reverseVoltage / 12.0);
        }


        [Obsolete("Use ConfigNeutralMode instead")]
        public void EnableBrakeMode(bool brake)
        {
            C_TalonSRX_SetOverrideBrakeType(m_talonPointer, brake ? 2 : 1);
        }

        public int FaultOverTemp
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_OverTemp(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultUnderVoltage
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_UnderVoltage(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultForwardLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_ForLim(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultReverseLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_RevLim(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultHardwareFailure
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_HardwareFailure(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_ForSoftLim(m_talonPointer, ref val);
                return val;
            }
        }

        public int FaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetFault_RevSoftLim(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of over temperature sticky faults.
        /// </summary>
        public int StickyFaultOverTemp
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_OverTemp(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of under voltage sticky faults.
        /// </summary>
        public int StickyFaultUnderVoltage
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_UnderVoltage(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of forward limit sticky faults.
        /// </summary>
        public int StickyFaultForwardLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_ForLim(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of reverse limit sticky faults.
        /// </summary>
        public int StickyFaultReverseLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_RevLim(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of forward soft limit sticky faults.
        /// </summary>
        public int StickyFaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_ForSoftLim(m_talonPointer, ref val);
                return val;
            }
        }

        /// <summary>
        /// Gets the number of reverse soft limit sticky faults.
        /// </summary>
        public int StickyFaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                C_TalonSRX_GetStckyFault_RevSoftLim(m_talonPointer, ref val);
                return val;
            }
        }

        ///<inheritdoc/>
        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }

        ///<inheritdoc/>
        public bool Alive => m_safetyHelper.Alive;

        ///<inheritdoc/>
        [Obsolete("Set the ControlEnabled to false.")]
        public void StopMotor()
        {
            ControlEnabled = false;
        }

        ///<inheritdoc/>
        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        ///<inheritdoc/>
        public string Description => $"CAN TalonSRX ID {DeviceID}";

        /// <inheritdoc/>
        public void PidWrite(double value)
        {
            if (m_controlMode == ControlMode.PercentVbus)
            {
                Set(value);
            }
            else
            {
                throw new InvalidOperationException("PID on RoboRIO only supported in Voltage Bus (PWM-like) mode");
            }
        }

        /// <inheritdoc/>
        public double PidGet()
        {
            return GetPosition();
        }

        ///<inheritdoc/>
        public PIDSourceType PIDSourceType { get; set; } = PIDSourceType.Displacement;

        ///<inheritdoc/>
        public void Set(double value)
        {
            m_safetyHelper.Feed();
            if (m_controlEnabled)
            {
                m_setPoint = value;
                CTR_Code status = CTR_Code.CTR_OKAY;
                switch (m_controlMode)
                {
                    case ControlMode.PercentVbus:
                        C_TalonSRX_SetDemand(m_talonPointer, Inverted ? -((int)(value * 1023)) : ((int)(value * 1023)));
                        status = CTR_Code.CTR_OKAY;
                        break;
                    case ControlMode.Voltage:
                        int volts = (int)(value * 256);
                        status = C_TalonSRX_SetDemand(m_talonPointer, Inverted ? -volts : volts);
                        break;
                    case ControlMode.Position:
                        status = C_TalonSRX_SetDemand(m_talonPointer, ScaleRotationsToNativeUnits(m_feedbackDevice, value));
                        break;
                    case ControlMode.Speed:
                        status = C_TalonSRX_SetDemand(m_talonPointer,
                            ScaleVelocityToNativeUnits(m_feedbackDevice, (Inverted ? -value : value)));
                        break;
                    case ControlMode.Follower:
                        status = C_TalonSRX_SetDemand(m_talonPointer, (int)value);
                        break;
                    case ControlMode.Current:
                        double milliamperes = (Inverted ? -value : value) * 1000.0;
                        status = C_TalonSRX_SetDemand(m_talonPointer, (int)milliamperes);
                        break;
                    default:
                        status = CTR_Code.CTR_OKAY;
                        break;
                }
                CheckStatus((int)status);
                status = C_TalonSRX_SetModeSelect(m_talonPointer, (int)MotorControlMode);
                CheckStatus((int)status);

            }
        }

        ///<inheritdoc/>
        public double Get()
        {
            double retVal = 0.0;
            int value = 0;
            switch (m_controlMode)
            {
                case ControlMode.Voltage:
                    retVal = GetOutputVoltage();
                    break;
                case ControlMode.Current:
                    retVal = GetOutputCurrent();
                    break;
                case ControlMode.Speed:
                    C_TalonSRX_GetSensorVelocity(m_talonPointer, ref value);
                    retVal = ScaleNativeUnitsToRpm(m_feedbackDevice, value);
                    break;
                case ControlMode.Position:
                    C_TalonSRX_GetSensorPosition(m_talonPointer, ref value);
                    retVal = ScaleNativeUnitsToRotations(m_feedbackDevice, value);
                    break;
                case ControlMode.Follower:
                case ControlMode.PercentVbus:
                default:
                    C_TalonSRX_GetAppliedThrottle(m_talonPointer, ref value);
                    retVal = (double)value / 1023.0;
                    break;
            }
            return retVal;
        }

        ///<inheritdoc/>
        [Obsolete("This is only here to make CAN Jaguars happy")]
        public void Set(double value, byte syncGroup)
        {
            Set(value);
        }

        internal double GetNativeUnitsPerRotationScalar(FeedbackDevice devToLookup)
        {
            double retVal = 0;
            bool scalingAvail = false;
            switch (devToLookup)
            {
                case FeedbackDevice.QuadEncoder:
                    int qeiPulsePerCount = 4;
                    switch (m_feedbackDevice)
                    {
                        case FeedbackDevice.CtreMagEncoderRelative:
                        case FeedbackDevice.CtreMagEncoderAbsolute:
                            retVal = NativePwdUnitsPerRotation;
                            scalingAvail = true;
                            break;
                        case FeedbackDevice.EncoderRising:
                        case FeedbackDevice.EncoderFalling:
                            qeiPulsePerCount = 1;
                            break;

                    }
                    if (scalingAvail)
                    {

                    }
                    else
                    {
                        if (0 == m_codesPerRev)
                        {

                        }
                        else
                        {
                            retVal = 4 * m_codesPerRev;
                            scalingAvail = true;
                        }
                    }
                    break;
                case FeedbackDevice.AnalogPotentiometer:
                case FeedbackDevice.AnalogEncoder:
                    if (0 == m_numPotTurns)
                    {

                    }
                    else
                    {
                        retVal = (double)NativeAdcUnitsPerRotation / m_numPotTurns;
                        scalingAvail = true;
                    }
                    break;
                case FeedbackDevice.EncoderRising:
                case FeedbackDevice.EncoderFalling:
                    if (0 == m_codesPerRev)
                    {

                    }
                    else
                    {
                        retVal = 1 * m_codesPerRev;
                        scalingAvail = true;
                    }
                    break;
                case FeedbackDevice.CtreMagEncoderRelative:
                case FeedbackDevice.CtreMagEncoderAbsolute:
                case FeedbackDevice.PulseWidth:
                    retVal = NativePwdUnitsPerRotation;
                    scalingAvail = true;
                    break;
            }
            return !scalingAvail ? 0 : retVal;
        }

        internal int ScaleRotationsToNativeUnits(FeedbackDevice devToLookup, double fullRotations)
        {
            int retVal = (int)fullRotations;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = (int)(fullRotations * scalar);
            }
            return retVal;
        }

        internal int ScaleVelocityToNativeUnits(FeedbackDevice devToLookup, double rpm)
        {
            int retVal = (int)rpm;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = (int)(rpm * MinutesPer100MsUnits * scalar);
            }
            return retVal;
        }

        internal double ScaleNativeUnitsToRotations(FeedbackDevice devToLookup, int nativePos)
        {
            double retVal = (double)nativePos;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = ((double)nativePos) / scalar;
            }
            return retVal;
        }

        internal double ScaleNativeUnitsToRpm(FeedbackDevice devToLookup, long nativeVel)
        {
            double retVal = (double)nativeVel;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = ((double)nativeVel) / (scalar * MinutesPer100MsUnits);
            }
            return retVal;
        }

        /// <summary>
        /// Enables Talon SRX to automatically zero the Sensor Position whenever an edge is detected
        /// on the index signal.
        /// </summary>
        /// <param name="enable">Enable or Disable the feature.</param>
        /// <param name="risingEdge">True to clear on rising edge, false for walling edge.</param>
        public void EnableZeroSensorPositionOnIndex(bool enable, bool risingEdge)
        {
            if (enable)
            {
                SetParam(ParamID.eQuadIdxPolarity, risingEdge ? 1 : 0);
                SetParam(ParamID.eClearPositionOnIdx, 1);
            }
            else
            {
                SetParam(ParamID.eClearPositionOnIdx, 0);
                SetParam(ParamID.eQuadIdxPolarity, risingEdge ? 1 : 0);
            }
        }

        ///<inheritdoc/>
        public void Reset()
        {
            Disable();
            ClearIAccum();
        }

        ///<inheritdoc/>
        public bool Enabled => ControlEnabled;

        ///<inheritdoc/>
        public double GetError() => GetClosedLoopError();

        /// <summary>
        /// Selects the profile slot on the Talon. 
        /// </summary>
        /// <param name="slotIdx">The profile to set (0 or 1).</param>
        public void SelectProfileSlot(int slotIdx)
        {
            m_profile = (slotIdx == 0) ? 0 : 1;
            CTR_Code status = C_TalonSRX_SetProfileSlotSelect(m_talonPointer, m_profile);
            if (status != CTR_Code.CTR_OKAY)
                CheckStatus((int)status);
        }

        /// <inheritdoc/>
        public void Disable()
        {
            ControlEnabled = false;
        }

        ///<inheritdoc/>
        public void UpdateTable()
        {
            if (Table != null)
            {
                Table.PutString("~TYPE~", SmartDashboardType);
                Table.PutString("Type", nameof(CANTalon)); // "CANTalon", "CANJaguar" 	
                Table.PutNumber("Mode", (int)MotorControlMode);
                if (MotorControlMode.IsPID())
                {

                    // CANJaguar throws an exception if you try to get its PID constants 	144
                    // when it's not in a PID-compatible mode 	
                    Table.PutNumber("p", P);
                    Table.PutNumber("i", I);
                    Table.PutNumber("d", D);
                    Table.PutNumber("f", F);
                }

                Table.PutBoolean("Enabled", Enabled);
                Table.PutNumber("Value", Get());
            }
        }

        ///<inheritdoc/>
        public void StartLiveWindowMode()
        {
            Set(0.0);
            Table.AddTableListener(this, true);
        }

        ///<inheritdoc/>
        public void StopLiveWindowMode()
        {
            Set(0.0);
            Table.RemoveTableListener(this);
        }

        ///<inheritdoc/>
        public string SmartDashboardType => "CANSpeedController";

        ///<inheritdoc/>
        public void ValueChanged(ITable source, string key, object value, NotifyFlags flags)
        {
            switch (key)
            {
                case "Enabled":
                    if ((bool)value)
                        Enable();
                    else
                        Disable();
                    break;
                case "Value":
                    Set((double)value);
                    break;
                case "Mode":
                    MotorControlMode = (ControlMode)(int)((double)value);
                    break;
            }
            if (MotorControlMode.IsPID())
            {
                switch (key)
                {
                    case "p":
                        P = (double)value;
                        break;
                    case "i":
                        I = (double)value;
                        break;
                    case "d":
                        D = (double)value;
                        break;
                    case "f":
                        F = (double)value;
                        break;
                }
            }
        }

        ///<inheritdoc/>
        public void InitTable(ITable subtable)
        {
            Table = subtable;
            UpdateTable();
        }

        ///<inheritdoc/>
        public ITable Table { get; private set; }
    }
}
