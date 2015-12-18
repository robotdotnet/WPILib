using System;
using HAL.Base;
using NetworkTables;
using NetworkTables.Tables;
using WPILib.Interfaces;
using static HAL.Base.HALCanTalonSRX;
using static HAL.Base.HALCanTalonSRX.Constants;
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

        /// <summary>
        /// Enumerated types for Motion Control Set Values
        /// </summary>
        /// <remarks>
        /// When in Motion Profile control mode, these constants are passed into <see cref="Set(double)"/> 
        /// to manipulate the motion profile executer. When changing modes, be sure to read the value 
        /// back using <see cref="GetMotionProfileStatus()"/> to ensure changes in output will take 
        /// effect before performing buffering actions. <para/>Disable will signal Talon to put motor 
        /// output into neutral drive. Talon will stop processing motion profile points.  This means 
        /// the buffer is effectively disconnected from the executer, allowing the robot to gracefully 
        /// clear and push new traj points. <see cref="IsUnderrun"/> will get cleared. The active trajectory is also cleared.
        /// <para/>
        /// Enable will signal Talon to pop a trajectory point from it's buffer and process it. If the 
        /// active trajectory is empty, Talon will shift in the next point. If the active traj is empty, 
        /// and so is the buffer, the motor drive is neutral and <see cref="IsUnderrun"/> is set. 
        /// When active traj times out, and buffer has at least one point, Talon shifts in the next one, 
        /// and <see cref="IsUnderrun"/> is cleared. When active traj times out, and buffer is empty, 
        /// Talon keeps processing active traj and sets <see cref="IsUnderrun"/>
        /// <para/>
        /// Hold will signal Talon to keep processing the Active Trajectory indefinitely. 
        /// If the active traj is cleared, Talon will neutral motor drive. Otherwise Talon 
        /// will kepp processing the active traj but it will not shift in points from the buffer. 
        /// This means the buffer is effectively disconnected from the executer, allowing the robot 
        /// to gracefully clear and push new traj points. <see cref="IsUnderrun"/> is set if active 
        /// traj is empty, otherwise it is cleared.  <see cref="IsLast"/> signal is also cleared.
        /// <para>
        /// </para>
        /// Typical Workflow:
        /// <list class="bullet">
        /// <listItem><para>Set(disable)</para></listItem>
        /// <listItem><para>Confirm disable takes effect.</para></listItem>
        /// <listItem><para>Clear buffer and push buffer points.</para></listItem>
        /// <listItem><para>Set(enable) when enough points have been pushed to ensure no underruns,
        /// wait for MP to finish or decide abort.</para></listItem>
        /// </list>
        /// </remarks>
        public enum SetValueMotionProfile
        {
            /// <summary>
            /// Disable the motion profile controller
            /// </summary>
            Disable = 0,
            /// <summary>
            /// Enable the motion profile controller
            /// </summary>
            Enable = 1,
            /// <summary>
            /// Hold the motion profile controller in the last valid state.
            /// </summary>
            Hold = 2
        }

        public struct TrajectoryPoint
        {
            public double Position { get; set; }
            public double Velocity { get; set; }
            public int TimeDurMs { get; set; }
            public int ProfileSlotSelect { get; set; }
            public bool VelocityOnly { get; set; }
            public bool IsLastPoint { get; set; }
            public bool ZeroPos { get; set; }
        }

        public struct MotionProfileStatus
        {
            public int TopBufferRem { get; }
            public int TopBufferCnt { get; }
            public int BtmBufferCnt { get; }
            public bool HasUnderrun { get; }
            /// <summary>
            /// 
            /// </summary>
            public bool IsUnderrrun { get; }
            public bool ActivePointValid { get; }
            public TrajectoryPoint ActivePoint { get; }
            public SetValueMotionProfile OutputEnable { get; }

            public MotionProfileStatus(int topRem, int topCnt, int btmCnt, bool hasUnder,
                bool isUnder, bool activeValid, TrajectoryPoint activePoint, SetValueMotionProfile outEnable)
            {
                TopBufferRem = topRem;
                TopBufferCnt = topCnt;
                BtmBufferCnt = btmCnt;
                HasUnderrun = hasUnder;
                IsUnderrrun = isUnder;
                ActivePointValid = activeValid;
                ActivePoint = activePoint;
                OutputEnable = outEnable;
            }
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

        private const int DefaultControlPeriodMs = 10; //!< default control update rate is 10ms.
        private const int DefaultEnablePeriodMs = 50;  //!< default enable update rate is 50ms (when using the new control5 frame).


        /// <summary>
        /// The max Id allowed for a CAN Talon
        /// </summary>
        public const int TalonIds = 62;

        /// <summary>
        /// Resource list of all the talons that the program knows of and has control of.
        /// </summary>
        protected static readonly Resource s_talonIds = new Resource(TalonIds);


        /// <summary>
        /// Constructs a CANTalon object.
        /// </summary>
        /// <param name="deviceNumber">The id of the Talon SRX this object will communicate with.</param>
        /// <param name="controlPeriodMs">The update period to the Talon SRX.  Defaults to 10ms.</param>
        /// <param name="enablePeriodMs">The period in ms to send the enable control frame.</param>
        public CANTalon(int deviceNumber, int controlPeriodMs = DefaultControlPeriodMs, int enablePeriodMs = DefaultEnablePeriodMs)
        {
            if (deviceNumber < 0 || deviceNumber >= TalonIds)
            {
                throw new ArgumentOutOfRangeException(nameof(deviceNumber), "Talon IDs must be between 0 and 62 inclusive.");
            }

            s_talonIds.Allocate(deviceNumber, $"CAN TalonSRX ID {deviceNumber} is already allocated.");

            DeviceID = deviceNumber;
            m_talonPointer = C_TalonSRX_Create3(deviceNumber, controlPeriodMs, enablePeriodMs);
            m_safetyHelper = new MotorSafetyHelper(this);
            m_controlEnabled = true;
            m_setPoint = 0;
            Profile = 0;
            m_codesPerRev = 0;
            m_numPotTurns = 0;
            m_feedbackDevice = FeedbackDevice.QuadEncoder;
            ApplyControlMode(ControlMode.PercentVbus);
            LiveWindow.LiveWindow.AddActuator("CANTalonSRX", deviceNumber, this);
            HAL.Base.HAL.Report(ResourceType.kResourceType_CANTalonSRX, (byte)(deviceNumber + 1), (byte)m_controlMode);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            s_talonIds.Deallocate(DeviceID);
            C_TalonSRX_Destroy(m_talonPointer);
        }

        public void SetParameter(ParamID paramEnum, double value)
        {
            CTR_Code status = C_TalonSRX_SetParam(m_talonPointer, (int)paramEnum, value);
            CheckCTRStatus(status);
        }

        /// <summary>
        /// Sets whether to reverse the input sensor.
        /// </summary>
        /// <param name="flip">True to reverse, false to not</param>
        public void ReverseSensor(bool flip)
        {
            CTR_Code status = C_TalonSRX_SetRevFeedbackSensor(m_talonPointer, flip ? 1 : 0);
            CheckCTRStatus(status);
        }

        /// <summary>
        /// Sets whether to reverse the output.
        /// </summary>
        /// <param name="flip">True to reverse, false to not.</param>
        public void ReverseOutput(bool flip)
        {
            CTR_Code status = C_TalonSRX_SetRevMotDuringCloseLoopEn(m_talonPointer, flip ? 1 : 0);
            CheckCTRStatus(status);
        }

        /// <summary>
        /// Gets the current encoder position.
        /// </summary>
        /// <returns>The current encoder position.</returns>
        public int GetEncoderPosition()
        {
            int pos = 0;
            CTR_Code status = C_TalonSRX_GetEncPosition(m_talonPointer, ref pos);
            CheckCTRStatus(status);
            return pos;
        }

        /// <summary>
        /// Resets the encoder position to a specified point.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetEncoderPostition(int newPosition)
        {
            SetParameter(ParamID.eEncPosition, newPosition);
        }

        /// <summary>
        /// Gets the current encoder velocity.
        /// </summary>
        /// <returns>The current encoder velocity</returns>
        public int GetEncoderVelocity()
        {
            int vel = 0;
            CTR_Code status = C_TalonSRX_GetEncVel(m_talonPointer, ref vel);
            CheckCTRStatus(status);
            return vel;
        }

        /// <summary>
        /// Gets the pulse width postion.
        /// </summary>
        /// <returns>The pulse width position</returns>
        public int GetPulseWidthPosition()
        {
            int val = 0;
            CTR_Code status = C_TalonSRX_GetPulseWidthPosition(m_talonPointer, ref val);
            CheckCTRStatus(status);
            return val;
        }

        /// <summary>
        /// Resets the pulse width position to a specified point.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetPulseWidthPosition(int newPosition)
        {
            SetParameter(ParamID.ePwdPosition, newPosition);
        }

        /// <summary>
        /// Gets the pulse width velocity.
        /// </summary>
        /// <returns>The pulse width velocity.</returns>
        public int GetPulseWidthVelocity()
        {
            int val = 0;
            CTR_Code status = C_TalonSRX_GetPulseWidthVelocity(m_talonPointer, ref val);
            CheckCTRStatus(status);
            return val;
        }

        /// <summary>
        /// Gets the pulse width rise to fall time.
        /// </summary>
        /// <returns>The pulse width time in microseconds.</returns>
        public int GetPulseWidthRiseToFallUs()
        {
            int val = 0;
            CTR_Code status = C_TalonSRX_GetPulseWidthRiseToFallUs(m_talonPointer, ref val);
            CheckCTRStatus(status);
            return val;
        }

        /// <summary>
        /// Gets the pulse width rise to rise time.
        /// </summary>
        /// <returns>The pulse width time in microseconds.</returns>
        public int GetPulseWidthRiseToRiseUs()
        {
            int val = 0;
            CTR_Code status = C_TalonSRX_GetPulseWidthRiseToRiseUs(m_talonPointer, ref val);
            CheckCTRStatus(status);
            return val;
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
                    int value = 0;
                    CTR_Code status = C_TalonSRX_IsPulseWidthSensorPresent(m_talonPointer, ref value);
                    CheckCTRStatus(status);
                    if (value == 0)
                    {
                        retVal = FeedbackDeviceStatus.FeedbackStatusNotPresent;
                    }
                    else
                    {
                        retVal = FeedbackDeviceStatus.FeedbackStatusPresent;
                    }
                    break;
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
            CTR_Code status = C_TalonSRX_GetEncIndexRiseEvents(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature A pin.
        /// </summary>
        /// <returns>The state of the A pin</returns>
        public int GetPinStateQuadA()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetQuadApin(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature B pin.
        /// </summary>
        /// <returns>The state of the B pin</returns>
        public int GetPinStateQuadB()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetQuadBpin(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state;
        }

        /// <summary>
        /// Gets the state of the quadrature index pin.
        /// </summary>
        /// <returns>The state of the index pin</returns>
        public int GetPinStateQuadIdx()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetQuadIdxpin(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state;
        }

        /// <summary>
        /// Resets the analog position to a new position.
        /// </summary>
        /// <param name="newPosition">The new position to reset to.</param>
        public void SetAnalogPosition(int newPosition)
        {
            SetParameter(ParamID.eAinPosition, newPosition);
        }

        /// <summary>
        /// Gets the analog input position, regardless of whether it is in the current feedback device.
        /// </summary>
        /// <returns>The 24 bit analog position. The bottom 10 bits are the ADC value, 
        /// the upper 14 bits track the overflows and underflows.</returns>
        public int GetAnalogInPosition()
        {
            int position = 0;
            CTR_Code status = C_TalonSRX_GetAnalogInWithOv(m_talonPointer, ref position);
            CheckCTRStatus(status);
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
            CTR_Code status = C_TalonSRX_GetAnalogInVel(m_talonPointer, ref velocity);
            CheckCTRStatus(status);
            return velocity;
        }

        /// <summary>
        /// Gets the current difference between the setpoint and the sensor value.
        /// </summary>
        /// <returns>The error in the PID Controller.</returns>
        public int GetClosedLoopError()
        {
            int error = 0;
            CTR_Code status = C_TalonSRX_GetCloseLoopErr(m_talonPointer, ref error);
            CheckCTRStatus(status);
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
                SetParameter(ParamID.eProfileParamSlot0_AllowableClosedLoopErr, allowableCloseLoopError);
            }
            else
            {
                SetParameter(ParamID.eProfileParamSlot1_AllowableClosedLoopErr, allowableCloseLoopError);
            }
        }

        /// <summary>
        /// Gets the value of the forward limit switch.
        /// </summary>
        /// <returns>True if the limit switch is closed, otherwise false.</returns>
        public bool IsForwardLimitSwitchClosed()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetLimitSwitchClosedFor(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state != 0;
        }

        /// <summary>
        /// Gets the value of the reverse limit switch.
        /// </summary>
        /// <returns>True if the limit switch is closed, otherwise false.</returns>
        public bool IsReverseLimitSwitchClosed()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetLimitSwitchClosedRev(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state != 0;
        }

        /// <summary>
        /// Returns whether the brake is enabled during neutral.
        /// </summary>
        /// <returns>True if brake mode, false if coast mode.</returns>
        public bool IsBrakeEnabledDuringNeutral()
        {
            int state = 0;
            CTR_Code status = C_TalonSRX_GetBrakeIsEnabled(m_talonPointer, ref state);
            CheckCTRStatus(status);
            return state != 0;
        }

        /// <inheritdoc/>
        public int EncoderCodesPerRev
        {
            set
            {
                m_codesPerRev = value;
                SetParameter(ParamID.eNumberEncoderCPR, m_codesPerRev);
            }
        }

        /// <inheritdoc/>
        public int PotentiometerTurns
        {
            set
            {
                m_numPotTurns = value;
                SetParameter(ParamID.eNumberPotTurns, m_numPotTurns);
            }
        }

        /// <inheritdoc/>
        public double GetTemperature()
        {
            double temp = 0.0;
            CTR_Code status = C_TalonSRX_GetTemp(m_talonPointer, ref temp);
            CheckCTRStatus(status);
            return temp;
        }

        /// <inheritdoc/>
        public double GetOutputCurrent()
        {
            double current = 0.0;
            CTR_Code status = C_TalonSRX_GetCurrent(m_talonPointer, ref current);
            CheckCTRStatus(status);
            return current;
        }

        /// <inheritdoc/>
        public double GetOutputVoltage()
        {
            int throttle = 0;
            CTR_Code status = C_TalonSRX_GetAppliedThrottle(m_talonPointer, ref throttle);
            CheckCTRStatus(status);
            return GetBusVoltage() * (throttle / 1023.0);
        }

        /// <inheritdoc/>
        public double GetBusVoltage()
        {
            double voltage = 0.0;
            CTR_Code status = C_TalonSRX_GetBatteryV(m_talonPointer, ref voltage);
            CheckCTRStatus(status);
            return voltage;
        }

        /// <inheritdoc/>
        public double GetPosition()
        {
            int pos = 0;
            CTR_Code status = C_TalonSRX_GetSensorPosition(m_talonPointer, ref pos);
            CheckCTRStatus(status);
            return ScaleNativeUnitsToRotations(m_feedbackDevice, pos);
        }

        /// <summary>
        /// Sets the position of the encoder or potentiometer
        /// </summary>
        /// <param name="pos">The new position of the sensor providing feedback.</param>
        public void SetPosition(double pos)
        {
            int nativePos = ScaleRotationsToNativeUnits(m_feedbackDevice, pos);
            CTR_Code status = C_TalonSRX_SetSensorPosition(m_talonPointer, nativePos);
            CheckCTRStatus(status);
        }

        /// <inheritdoc/>
        public double GetSpeed()
        {
            int vel = 0;
            CTR_Code status = C_TalonSRX_GetSensorVelocity(m_talonPointer, ref vel);
            CheckCTRStatus(status);
            return ScaleNativeUnitsToRpm(m_feedbackDevice, vel);
        }

        /// <inheritdoc/>
        public bool GetForwardLimitOk()
        {
            int limSwitch = FaultForwardLimit;
            int softLim = FaultForwardSoftLimit;
            return softLim == 0 && limSwitch == 0;
        }

        /// <inheritdoc/>
        public bool GetReverseLimitOk()
        {
            int limSwitch = FaultReverseLimit;
            int softLim = FaultReverseSoftLimit;
            return softLim == 0 && limSwitch == 0;
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
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.TemperatureFault : 0;

            //Voltage
            val = 0;
            status = C_TalonSRX_GetFault_UnderVoltage(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.BusVoltageFault : 0;

            //Fwd Limit Switch
            val = 0;
            status = C_TalonSRX_GetFault_ForLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.FwdLimitSwitch : 0;

            //Rev Limit Switch
            val = 0;
            status = C_TalonSRX_GetFault_RevLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.RevLimitSwitch : 0;

            //Fwd Soft Limit
            val = 0;
            status = C_TalonSRX_GetFault_ForSoftLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.FwdSoftLimit : 0;

            //Rev Soft Limit
            val = 0;
            status = C_TalonSRX_GetFault_RevSoftLim(m_talonPointer, ref val);

            if (status != CTR_Code.CTR_OKAY)
            {
                CheckCTRStatus(status);
            }

            retVal |= val != 0 ? Faults.RevSoftLimit : 0;

            return retVal;
        }

        private void ApplyControlMode(ControlMode value)
        {
            m_controlMode = value;
            if (value == ControlMode.Disabled)
                m_controlEnabled = false;
            C_TalonSRX_SetModeSelect(m_talonPointer, (int)ControlMode.Disabled);
        }

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
                CTR_Code status = C_TalonSRX_GetFeedbackDeviceSelect(m_talonPointer, ref device);
                CheckCTRStatus(status);
                return (FeedbackDevice)device;
            }
            set
            {
                m_feedbackDevice = value;
                CTR_Code status = C_TalonSRX_SetFeedbackDeviceSelect(m_talonPointer, (int)value);
                CheckCTRStatus(status);
            }
        }

        /// <inheritdoc/>
        public void Enable() => ControlEnabled = true;

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
                    CTR_Code status = C_TalonSRX_SetModeSelect(m_talonPointer, (int)ControlMode.Disabled);
                    CheckCTRStatus(status);
                    m_controlEnabled = false;
                }
                else
                {
                    m_controlEnabled = true;
                }
            }
        }

        /// <inheritdoc/>
        public double P
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_P);
                    CheckCTRStatus(status);
                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_P);
                    CheckCTRStatus(status);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                double retVal = 0;
                status = C_TalonSRX_GetPgain(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetPgain(m_talonPointer, m_profile, value);
                CheckCTRStatus(status);

            }
        }

        /// <inheritdoc/>
        public double I
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_I);

                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_I);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                double retVal = 0;
                status = C_TalonSRX_GetIgain(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetIgain(m_talonPointer, m_profile, value);
                CheckCTRStatus(status);
            }
        }

        /// <inheritdoc/>
        public double D
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_D);
                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_D);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                double retVal = 0;
                status = C_TalonSRX_GetDgain(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetDgain(m_talonPointer, m_profile, value);
                CheckCTRStatus(status);
            }
        }

        /// <inheritdoc/>
        public double F
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_F);
                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_F);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                double retVal = 0;
                status = C_TalonSRX_GetFgain(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetFgain(m_talonPointer, m_profile, value);
                CheckCTRStatus(status);
            }
        }

        public int IZone
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_IZone);
                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_IZone);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                int retVal = 0;
                status = C_TalonSRX_GetIzone(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetIzone(m_talonPointer, m_profile, value);
                CheckCTRStatus(status);
            }
        }

        public double GetIaccum()
        {
            CTR_Code status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.ePidIaccum);
            CheckCTRStatus(status);
            Timer.Delay(DelayForSolicitedSignals);

            int val = 0;
            status = C_TalonSRX_GetParamResponseInt32(m_talonPointer, (int)ParamID.ePidIaccum, ref val);
            CheckCTRStatus(status);
            return val;
        }

        public void ClearIAccum()
        {
            CTR_Code status = C_TalonSRX_SetParam(m_talonPointer, (int)ParamID.ePidIaccum, 0);
            CheckCTRStatus(status);
        }

        public double CloseLoopRampRate
        {
            get
            {
                CTR_Code status;
                if (m_profile == 0)
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot0_CloseLoopRampRate);
                }
                else
                {
                    status = C_TalonSRX_RequestParam(m_talonPointer, (int)ParamID.eProfileParamSlot1_CloseLoopRampRate);
                }
                CheckCTRStatus(status);

                Timer.Delay(DelayForSolicitedSignals);

                int retVal = 0;
                status = C_TalonSRX_GetCloseLoopRampRate(m_talonPointer, m_profile, ref retVal);
                CheckCTRStatus(status);
                return retVal / 1023.0 * 12.0 * 1000.0;
            }
            set
            {
                int rate = (int) (value * 1023.0 / 12.0 / 1000.0);
                CTR_Code status = C_TalonSRX_SetCloseLoopRampRate(m_talonPointer, m_profile, rate);
                CheckCTRStatus(status);
            }
        }

        /// <summary>
        /// Sets the PID and extra constants of the controler.
        /// </summary>
        /// <param name="p"></param>
        /// <param name="i"></param>
        /// <param name="d"></param>
        /// <param name="f"></param>
        /// <param name="izone"></param>
        /// <param name="closeLoopRampRate"></param>
        /// <param name="profile"></param>
        public void SetPID(double p, double i, double d, double f, int izone, double closeLoopRampRate, int profile)
        {
            if (profile != 0 && profile != 1)
                throw new ArgumentOutOfRangeException(nameof(profile), "Talon PID profile must be 0 or 1.");
            Profile = profile;
            P = p;
            I = i;
            D = d;
            F = f;
            IZone = izone;
            CloseLoopRampRate = closeLoopRampRate;
        }

        /// <inheritdoc/>
        public void SetPID(double p, double i, double d)
        {
            SetPID(p, i, d, 0, 0, 0, m_profile);
        }

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

        public int Profile
        {
            get { return m_profile; }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException(nameof(value), "Talon PID profile must be 0 or 1.");
                m_profile = value;
                CTR_Code status = C_TalonSRX_SetProfileSlotSelect(m_talonPointer, m_profile);
                CheckCTRStatus(status);
            }
        }

        public double VoltageCompensationRampRate
        {
            get
            {
                double retVal = 0;
                CTR_Code status = C_TalonSRX_GetParamResponse(m_talonPointer, (int)ParamID.eProfileParamVcompRate, ref retVal);
                CheckCTRStatus(status);
                return retVal * 1000;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetVoltageCompensationRate(m_talonPointer, value / 1000.0);
                CheckCTRStatus(status);
            }
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
                    CheckCTRStatus(status);
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
            set { ForwardSoftLimit = (int)value; }
        }

        /// <inheritdoc/>
        public double ReverseLimit
        {
            set { ReverseSoftLimit = (int)value; }
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
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetParamResponseInt32(m_talonPointer, (int)ParamID.eRampThrottle, ref retVal);
                CheckCTRStatus(status);
                return retVal / 1023.0 * 12.0 * 1000.0;
            }
            set
            {
                int rate = (int)(value * 1023.0 / 12.0 / 100.0);
                CTR_Code status = C_TalonSRX_SetRampThrottle(m_talonPointer, rate);
                CheckCTRStatus(status);
            }
        }

        /// <inheritdoc/>
        public uint FirmwareVersion
        {
            get
            {
                int version = 0;
                CTR_Code status = C_TalonSRX_GetFirmVers(m_talonPointer, ref version);
                CheckCTRStatus(status);
                return (uint)version;
            }
        }

        public int DeviceID { get; }

        public int ForwardSoftLimit
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetForwardSoftLimit(m_talonPointer, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                int nativeLimitPos = ScaleRotationsToNativeUnits(m_feedbackDevice, value);
                CTR_Code status = C_TalonSRX_SetForwardSoftLimit(m_talonPointer, nativeLimitPos);
                CheckCTRStatus(status);
            }
        }

        public bool ForwardSoftLimitEnabled
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetForwardSoftEnable(m_talonPointer, ref retVal);
                CheckCTRStatus(status);
                return retVal != 0;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetForwardSoftEnable(m_talonPointer, value ? 1 : 0);
                CheckCTRStatus(status);
            }
        }

        public int ReverseSoftLimit
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetReverseSoftLimit(m_talonPointer, ref retVal);
                CheckCTRStatus(status);
                return retVal;
            }
            set
            {
                int nativeLimitPos = ScaleRotationsToNativeUnits(m_feedbackDevice, value);
                CTR_Code status = C_TalonSRX_SetReverseSoftLimit(m_talonPointer, nativeLimitPos);
                CheckCTRStatus(status);
            }
        }

        public bool ReverseSoftLimitEnabled
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetReverseSoftEnable(m_talonPointer, ref retVal);
                CheckCTRStatus(status);
                return retVal != 0;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetReverseSoftEnable(m_talonPointer, value ? 1 : 0);
                CheckCTRStatus(status);
            }
        }

        public void ClearStickyFaults()
        {
            CTR_Code status = C_TalonSRX_ClearStickyFaults(m_talonPointer);
            CheckCTRStatus(status);
        }

        public void EnableLimitSwitches(bool forward, bool reverse)
        {
            int mask = 1 << 2 | (forward ? 1 : 0) << 1 | (reverse ? 1 : 0);
            CTR_Code status = C_TalonSRX_SetOverrideLimitSwitchEn(m_talonPointer, mask);
            CheckCTRStatus(status);
            if (status != CTR_Code.CTR_OKAY)
                CheckCTRStatus(status);
        }

        public bool ForwardLimitSwitchNormallyOpen
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetParamResponseInt32(m_talonPointer,
                    (int) ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed, ref retVal);
                CheckCTRStatus(status);
                return retVal != 0;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetParam(m_talonPointer, (int)ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed, value ? 0 : 1);
                CheckCTRStatus(status);
            }
        }

        public bool ReverseLimitSwitchNormallyOpen
        {
            get
            {
                int retVal = 0;
                CTR_Code status = C_TalonSRX_GetParamResponseInt32(m_talonPointer,
                    (int)ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed, ref retVal);
                CheckCTRStatus(status);
                return retVal != 0;
            }
            set
            {
                CTR_Code status = C_TalonSRX_SetParam(m_talonPointer, (int)ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed, value ? 0 : 1);
                CheckCTRStatus(status);
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
            SetParameter(ParamID.ePeakPosOutput, 1023 * forwardVoltage / 12.0);
            SetParameter(ParamID.ePeakNegOutput, 1023 * reverseVoltage / 12.0);
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
            SetParameter(ParamID.eNominalPosOutput, 1023 * forwardVoltage / 12.0);
            SetParameter(ParamID.eNominalNegOutput, 1023 * reverseVoltage / 12.0);
        }

        public void EnableBrakeMode(bool brake)
        {
            CTR_Code status = C_TalonSRX_SetOverrideBrakeType(m_talonPointer, brake ? 2 : 1);
            CheckCTRStatus(status);
        }

        public int FaultOverTemp
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_OverTemp(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultUnderVoltage
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_UnderVoltage(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultForwardLimit
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_ForLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultReverseLimit
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_RevLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultHardwareFailure
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_HardwareFailure(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultForwardSoftLimit
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_ForSoftLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
                return val;
            }
        }

        public int FaultReverseSoftLimit
        {
            get
            {
                int val = 0;
                CTR_Code status = C_TalonSRX_GetFault_RevSoftLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_OverTemp(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_UnderVoltage(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_ForLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_RevLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_ForSoftLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
                CTR_Code status = C_TalonSRX_GetStckyFault_RevSoftLim(m_talonPointer, ref val);
                CheckCTRStatus(status);
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
        public virtual double PidGet()
        {
            return GetPosition();
        }

        ///<inheritdoc/>
        public PIDSourceType PIDSourceType { get; set; } = PIDSourceType.Displacement;

        ///<inheritdoc/>
        public virtual void Set(double value)
        {
            m_safetyHelper.Feed();
            if (m_controlEnabled)
            {
                m_setPoint = value;
                CTR_Code status = CTR_Code.CTR_OKAY;
                switch (m_controlMode)
                {
                    case ControlMode.PercentVbus:
                        C_TalonSRX_Set(m_talonPointer, Inverted ? -value : value);
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
                            ScaleVelocityToNativeUnits(m_feedbackDevice, Inverted ? -value : value));
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
                CheckCTRStatus(status);
                status = C_TalonSRX_SetModeSelect(m_talonPointer, (int)MotorControlMode);
                CheckCTRStatus(status);

            }
        }

        ///<inheritdoc/>
        public virtual double Get()
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
                    retVal = value / 1023.0;
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
            double retVal = nativePos;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = nativePos / scalar;
            }
            return retVal;
        }

        internal double ScaleNativeUnitsToRpm(FeedbackDevice devToLookup, long nativeVel)
        {
            double retVal = nativeVel;
            double scalar = GetNativeUnitsPerRotationScalar(devToLookup);
            if (scalar > 0)
            {
                retVal = nativeVel / (scalar * MinutesPer100MsUnits);
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
                SetParameter(ParamID.eQuadIdxPolarity, risingEdge ? 1 : 0);
                SetParameter(ParamID.eClearPositionOnIdx, 1);
            }
            else
            {
                SetParameter(ParamID.eClearPositionOnIdx, 0);
                SetParameter(ParamID.eQuadIdxPolarity, risingEdge ? 1 : 0);
            }
        }

        public void ChangeMotionControlFramePeriod(int periodMs)
        {
            C_TalonSRX_ChangeMotionControlFramePeriod(m_talonPointer, periodMs);
        }

        public void ClearMotionProfileTrajectories()
        {
            C_TalonSRX_ClearMotionProfileTrajectories(m_talonPointer);
        }

        public int GetMotionProfileTopLevelBufferCount()
        {
            return C_TalonSRX_GetMotionProfileTopLevelBufferCount(m_talonPointer);
        }

        public bool PushMotionProfileTrajectory(TrajectoryPoint trajPt)
        {
            if (IsMotionProfileTopLevelBufferFull())
                return false;
            int targPos = ScaleRotationsToNativeUnits(m_feedbackDevice, trajPt.Position);
            int targVel = ScaleRotationsToNativeUnits(m_feedbackDevice, trajPt.Velocity);

            int profileSlotSelect = trajPt.ProfileSlotSelect > 0 ? 1 : 0;
            int timeDurMs = trajPt.TimeDurMs;

            if (timeDurMs > 255)
                timeDurMs = 255;
            if (timeDurMs < 0)
                timeDurMs = 0;
            CTR_Code status = C_TalonSRX_PushMotionProfileTrajectory(m_talonPointer, targPos, targVel, profileSlotSelect, timeDurMs, trajPt.VelocityOnly ? 1 : 0,
                trajPt.IsLastPoint ? 1 : 0, trajPt.ZeroPos ? 1 : 0);
            CheckCTRStatus(status);
            return true;
        }

        public bool IsMotionProfileTopLevelBufferFull()
        {
            return C_TalonSRX_IsMotionProfileTopLevelBufferFull(m_talonPointer) != 0;
        }

        public void ProcessMotionProfileBuffer()
        {
            C_TalonSRX_ProcessMotionProfileBuffer(m_talonPointer);
        }

        public MotionProfileStatus GetMotionProfileStatus()
        {
            int flags = 0;
            int profileSelect = 0;
            int pos = 0;
            int vel = 0;
            int topRem = 0;
            int topCnt = 0;
            int btmCnt = 0;
            int outEnable = 0;
            CTR_Code status = C_TalonSRX_GetMotionProfileStatus(m_talonPointer, ref flags, ref profileSelect,
                ref pos, ref vel, ref topRem, ref topCnt, ref btmCnt, ref outEnable);
            CheckCTRStatus(status);
            bool hasUnderrun = (flags & kMotionProfileFlag_HasUnderrun) > 0 ? true : false;
            bool isUnderrun = (flags & kMotionProfileFlag_IsUnderrun) > 0 ? true : false;
            bool activePointValid = (flags & kMotionProfileFlag_ActTraj_IsValid) > 0 ? true : false;
            bool isLastPoint = (flags & kMotionProfileFlag_ActTraj_IsLast) > 0 ? true : false;
            bool isVelocityOnly = (flags & kMotionProfileFlag_ActTraj_VelOnly) > 0 ? true : false;

            double position = ScaleNativeUnitsToRotations(m_feedbackDevice, pos);
            double velocity = ScaleNativeUnitsToRpm(m_feedbackDevice, vel);

            SetValueMotionProfile outputEnable = (SetValueMotionProfile)outEnable;

            bool zeroPos = false;
            int timeDurMs = 0;

            TrajectoryPoint activePoint = new TrajectoryPoint()
            {
                IsLastPoint = isLastPoint,
                VelocityOnly = isVelocityOnly,
                Position = position,
                Velocity = velocity,
                ProfileSlotSelect = profileSelect,
                ZeroPos = zeroPos,
                TimeDurMs = timeDurMs
            };

            return new MotionProfileStatus(topRem, topCnt, btmCnt, hasUnderrun, isUnderrun,
                activePointValid, activePoint, outputEnable);
        }

        public void ClearMotionProfileHasUnderrun()
        {
            SetParameter(ParamID.eMotionProfileHasUnderrunErr, 0);
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
            m_profile = slotIdx == 0 ? 0 : 1;
            CTR_Code status = C_TalonSRX_SetProfileSlotSelect(m_talonPointer, m_profile);
            if (status != CTR_Code.CTR_OKAY)
                CheckCTRStatus(status);
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
                    MotorControlMode = (ControlMode)(int)(double)value;
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
