using System;
using HAL_Base;
using NetworkTables.Tables;
using WPILib.CAN;
using WPILib.Exceptions;
using WPILib.LiveWindows;
using static HAL_Base.HALCAN;
using static HAL_Base.HALCAN.Constants;

namespace WPILib
{
    public class CANJaguar : IMotorSafety, ICANSpeedController, ITableListener, ILiveWindowSendable, IDisposable
    {

// ReSharper disable InconsistentNaming
        public static readonly int kMaxMessageDataSize = 8;


        public static readonly int kControllerRate = 1000;
        public static readonly double kApproxBusVoltage = 12.0;

        private MotorSafetyHelper m_safetyHelper;
        private static readonly Resource s_allocated = new Resource(63);

        private static readonly int kFullMessageIDMask = CAN_MSGID_API_M | CAN_MSGID_MFR_M | CAN_MSGID_DTYPE_M;
        private const int kSendMessagePeriod = 20;

// ReSharper restore InconsistentNaming

        public struct EncoderTag
        {

        }

        public struct QuadEncoderTag
        {

        }

        public struct PotentiometerTag
        {

        }

// ReSharper disable InconsistentNaming
        public static readonly EncoderTag kEncoder = new EncoderTag();


        public readonly static QuadEncoderTag kQuadEncoder = new QuadEncoderTag();

        public readonly static PotentiometerTag kPotentiometer = new PotentiometerTag();

        public static readonly int kCurrentFault = 1;
        public static readonly int kTemeperatureFault = 2;
        public static readonly int kButVoltageFault = 4;
        public static readonly int kGateDriverFault = 8;

        public static int kForwardLimit = 1;
        public static int kReverseLimit = 2;
// ReSharper restore InconsistentNaming

        public CANJaguar(int deviceNumber)
        {
            try
            {
                s_allocated.Allocate(deviceNumber - 1);
            }
            catch (CheckedAllocationException e1)
            {
                throw new AllocationException("CANJaguar device " + e1.Message + "(increment index by one)");
            }

            m_deviceNumber = (byte)deviceNumber;
            m_controlMode = ControlMode.PercentVbus;

            m_safetyHelper = new MotorSafetyHelper(this);

            bool receivedFirmwareVersion = false;

            byte[] data = new byte[8];

            RequestMessage(CAN_IS_FRAME_REMOTE | CAN_MSGID_API_FIRMVER);
            RequestMessage(LM_API_HWVER);

            for (int i = 0; i < kReceiveStatusAttempts; i++)
            {
                Timer.Delay(0.001);
                SetupPeriodicStatus();
                UpdatePeriodicStatus();

                if (!receivedFirmwareVersion)
                {
                    try
                    {
                        GetMessage(CAN_MSGID_API_FIRMVER, CAN_MSGID_FULL_M, data);
                        m_firmwareVersion = UnpackINT32(data);
                        receivedFirmwareVersion = true;
                    }
                    catch (CANMessageNotFoundException)
                    {

                    }
                }

                if (m_receivedStatusMessage0 &&
                    m_receivedStatusMessage1 &&
                    m_receivedStatusMessage2 &&
                    receivedFirmwareVersion)
                {
                    break;
                }
            }

            if (!m_receivedStatusMessage0 ||
                !m_receivedStatusMessage1 ||
                !m_receivedStatusMessage2 ||
                !receivedFirmwareVersion)
            {
                Dispose();
                throw new CANMessageNotFoundException();
            }

            try
            {
                GetMessage(LM_API_HWVER, CAN_MSGID_FULL_M, data);
                m_hardwareVersion = data[0];
            }
            catch (CANMessageNotFoundException)
            {
                m_hardwareVersion = 0;
            }

            if (m_firmwareVersion >= 3330 || m_firmwareVersion < 108)
            {
                if (m_firmwareVersion < 3330)
                {
                    DriverStation.ReportError("Jag " + m_deviceNumber + " firmware " + m_firmwareVersion + " is too old (must be at least version 108 of the FIRST approved firmware)", false);
                }
                else
                {
                    DriverStation.ReportError("Jag" + m_deviceNumber + " firmware " + m_firmwareVersion + " is not FIRST approved (must be at least version 108 of the FIRST approved firmware)", false);
                }
                return;
            }


        }

        public void Dispose()
        {
            s_allocated.Dispose(m_deviceNumber - 1);
            m_safetyHelper = null;
            int status = 0;

            int messageID;

            switch (m_controlMode)
            {
                case ControlMode.PercentVbus:
                    messageID = (int)m_controlMode | LM_API_VOLT_T_SET;
                    break;
                case ControlMode.Speed:
                    messageID = (int)m_controlMode | LM_API_SPD_T_SET;
                    break;
                case ControlMode.Position:
                    messageID = (int)m_controlMode | LM_API_POS_T_SET;
                    break;
                case ControlMode.Current:
                    messageID = (int)m_controlMode | LM_API_ICTRL_T_SET;
                    break;
                case ControlMode.Voltage:
                    messageID = (int)m_controlMode | LM_API_VCOMP_T_SET;
                    break;
                default:
                    return;

            }

            FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, null, 0,
                CAN_SEND_PERIOD_STOP_REPEATING, ref status);
        }

        public int DeviceNumber => m_deviceNumber;


        byte m_deviceNumber;
        double m_value = 0.0f;

        // Parameters/configuration
        ControlMode m_controlMode;
        int m_speedReference = LM_REF_NONE;
        int m_positionReference = LM_REF_NONE;
        double m_p = 0.0;
        double m_i = 0.0;
        double m_d = 0.0;
        NeutralMode m_neutralMode = NeutralMode.Jumper;
        short m_encoderCodesPerRev = 0;
        short m_potentiometerTurns = 0;
        LimitMode m_limitMode = LimitMode.SwitchInputsOnly;
        double m_forwardLimit = 0.0;
        double m_reverseLimit = 0.0;
        double m_maxOutputVoltage = kApproxBusVoltage;
        double m_voltageRampRate = 0.0;
        float m_faultTime = 0.0f;

        // Which parameters have been verified since they were last set?
        bool m_controlModeVerified = true;
        bool m_speedRefVerified = true;
        bool m_posRefVerified = true;
        bool m_pVerified = true;
        bool m_iVerified = true;
        bool m_dVerified = true;
        bool m_neutralModeVerified = true;
        bool m_encoderCodesPerRevVerified = true;
        bool m_potentiometerTurnsVerified = true;
        bool m_forwardLimitVerified = true;
        bool m_reverseLimitVerified = true;
        bool m_limitModeVerified = true;
        bool m_maxOutputVoltageVerified = true;
        bool m_voltageRampRateVerified = true;
        bool m_faultTimeVerified = true;

        // Status data
        double m_busVoltage = 0.0f;
        double m_outputVoltage = 0.0f;
        double m_outputCurrent = 0.0f;
        double m_temperature = 0.0f;
        double m_position = 0.0;
        double m_speed = 0.0;
        byte m_limits = (byte)0;
        short m_faults = (short)0;
        int m_firmwareVersion = 0;
        byte m_hardwareVersion = (byte)0;

        // Which periodic status messages have we received at least once?
        bool m_receivedStatusMessage0 = false;
        bool m_receivedStatusMessage1 = false;
        bool m_receivedStatusMessage2 = false;

// ReSharper disable once InconsistentNaming
        private const int kReceiveStatusAttempts = 50;

        bool m_controlEnabled = true;

        public double Expiration
        {
            set { m_safetyHelper.Expiration = value; }
            get { return m_safetyHelper.Expiration; }
        }

        public bool Alive => m_safetyHelper.Alive;

        public void StopMotor()
        {
            DisableControl();
        }

        public bool SafetyEnabled
        {
            set { m_safetyHelper.SafetyEnabled = value; }
            get { return m_safetyHelper.SafetyEnabled; }
        }

        public string Description => "CANJaguar ID " + m_deviceNumber;

        public void PidWrite(double value)
        {
            if (m_controlMode == ControlMode.PercentVbus)
            {
                Set(value);
            }
            else
            {
                throw new InvalidOperationException("PID only supported in PercentVbus mode");
            }
        }

        private void SetSpeedReference(int reference)
        {
            SendMessage(LM_API_SPD_REF, new byte[] { (byte)reference }, 1);
            m_speedReference = reference;
            m_speedRefVerified = false;
        }

        public void EnableControl(double encoderInitialPosition)
        {
            switch (m_controlMode)
            {
                case ControlMode.PercentVbus:
                    SendMessage(LM_API_VOLT_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Speed:
                    SendMessage(LM_API_SPD_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Position:
                    byte[] data = new byte[8];
                    int dataSize = PackFXP16_16(data, encoderInitialPosition);
                    SendMessage(LM_API_POS_T_EN, data, dataSize);
                    break;

                case ControlMode.Current:
                    SendMessage(LM_API_ICTRL_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Voltage:
                    SendMessage(LM_API_VCOMP_T_EN, new byte[0], 0);
                    break;
            }

            m_controlEnabled = true;
        }

        public void EnableControl()
        {
            EnableControl(0.0);
        }

        public void DisableControl()
        {
            // Disable all control modes.
            SendMessage(LM_API_VOLT_DIS, new byte[0], 0);
            SendMessage(LM_API_SPD_DIS, new byte[0], 0);
            SendMessage(LM_API_POS_DIS, new byte[0], 0);
            SendMessage(LM_API_ICTRL_DIS, new byte[0], 0);
            SendMessage(LM_API_VCOMP_DIS, new byte[0], 0);

            // Stop all periodic setpoints
            SendMessage(LM_API_VOLT_T_SET, new byte[0], 0, CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(LM_API_SPD_T_SET, new byte[0], 0, CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(LM_API_POS_T_SET, new byte[0], 0, CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(LM_API_ICTRL_T_SET, new byte[0], 0, CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(LM_API_VCOMP_T_SET, new byte[0], 0, CAN_SEND_PERIOD_STOP_REPEATING);

            m_controlEnabled = false;
        }

        public void Set(double value)
        {
            Set(value, 0);
        }

        public double Get()
        {
            return m_value;
        }

        public void Set(double value, byte syncGroup)
        {
            int messageID;
            byte[] data = new byte[8];
            byte dataSize;

            if (m_controlEnabled)
            {
                switch (m_controlMode)
                {
                    case ControlMode.PercentVbus:
                        messageID = LM_API_VOLT_T_SET;
                        dataSize = PackPercentage(data, value);
                        break;
                    case ControlMode.Speed:
                        messageID = LM_API_SPD_T_SET;
                        dataSize = PackFXP16_16(data, value);
                        break;
                    case ControlMode.Position:
                        messageID = LM_API_POS_T_SET;
                        dataSize = PackFXP16_16(data, value);
                        break;
                    case ControlMode.Current:
                        messageID = LM_API_ICTRL_T_SET;
                        dataSize = PackFXP8_8(data, value);
                        break;
                    case ControlMode.Voltage:
                        messageID = LM_API_VCOMP_T_SET;
                        dataSize = PackFXP8_8(data, value);
                        break;
                    default:
                        return;

                }

                if (syncGroup != 0)
                {
                    data[dataSize++] = syncGroup;
                }

                SendMessage(messageID, data, dataSize, kSendMessagePeriod);
                m_safetyHelper?.Feed();
            }
            m_value = value;
            Verify();
        }

        protected void Verify()
        {
            byte[] data = new byte[8];

            try
            {
                GetMessage(LM_API_STATUS_POWER, CAN_MSGID_FULL_M, data);
                bool powerCycled = data[0] != 0;

                if (powerCycled)
                {
                    data[0] = 1;
                    SendMessage(LM_API_STATUS_POWER, data, 1);

                    m_controlModeVerified = false;
                    m_speedRefVerified = false;
                    m_posRefVerified = false;
                    m_neutralModeVerified = false;
                    m_encoderCodesPerRevVerified = false;
                    m_potentiometerTurnsVerified = false;
                    m_forwardLimitVerified = false;
                    m_reverseLimitVerified = false;
                    m_limitModeVerified = false;
                    m_maxOutputVoltageVerified = false;
                    m_faultTimeVerified = false;

                    if (m_controlMode == ControlMode.PercentVbus || m_controlMode == ControlMode.Voltage)
                    {
                        m_voltageRampRateVerified = false;
                    }
                    else
                    {
                        m_pVerified = false;
                        m_iVerified = false;
                        m_dVerified = false;
                    }

                    m_receivedStatusMessage0 = false;
                    m_receivedStatusMessage1 = false;
                    m_receivedStatusMessage2 = false;

                    int[] messages = new int[]
                    {
                        LM_API_SPD_REF, LM_API_POS_REF,
                        LM_API_SPD_PC, LM_API_POS_PC,
                        LM_API_ICTRL_PC, LM_API_SPD_IC,
                        LM_API_POS_IC, LM_API_ICTRL_IC,
                        LM_API_SPD_DC, LM_API_POS_DC,
                        LM_API_ICTRL_DC, LM_API_CFG_ENC_LINES,
                        LM_API_CFG_POT_TURNS, LM_API_CFG_BRAKE_COAST,
                        LM_API_CFG_LIMIT_MODE, LM_API_CFG_LIMIT_REV,
                        LM_API_CFG_MAX_VOUT, LM_API_VOLT_SET_RAMP,
                        LM_API_VCOMP_COMP_RAMP, LM_API_CFG_FAULT_TIME,
                        LM_API_CFG_LIMIT_FWD
                    };

                    foreach (int message in messages)
                    {
                        try
                        {
                            GetMessage(message, CAN_MSGID_FULL_M, data);
                        }
                        catch (CANMessageNotFoundException)
                        {

                        }
                    }
                }
            }
            catch (CANMessageNotFoundException)
            {
                RequestMessage(LM_API_STATUS_POWER);
            }

            if (!m_controlModeVerified && m_controlEnabled)
            {
                try
                {
                    GetMessage(LM_API_STATUS_CMODE, CAN_MSGID_FULL_M, data);
                    ControlMode mode = (ControlMode)data[0];

                    if (m_controlMode == mode)
                    {
                        m_controlModeVerified = true;
                    }
                    else
                    {
                        EnableControl();
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    RequestMessage(LM_API_STATUS_CMODE);
                }
            }

            if (!m_speedRefVerified)
            {
                try
                {
                    GetMessage(LM_API_SPD_REF, CAN_MSGID_FULL_M, data);

                    int speedRef = data[0];

                    if (m_speedReference == speedRef)
                    {
                        m_speedRefVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetSpeedReference(m_speedReference);
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_SPD_REF);
                }
            }

            if (!m_posRefVerified)
            {
                try
                {
                    GetMessage(LM_API_POS_REF, CAN_MSGID_FULL_M, data);

                    int posRef = data[0];

                    if (m_positionReference == posRef)
                    {
                        m_posRefVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetPositionReference(m_positionReference);
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_POS_REF);
                }
            }

            if (!m_pVerified)
            {
                int message = 0;

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        message = LM_API_SPD_PC;
                        break;

                    case ControlMode.Position:
                        message = LM_API_POS_PC;
                        break;

                    case ControlMode.Current:
                        message = LM_API_ICTRL_PC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, CAN_MSGID_FULL_M, data);

                    double p = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_p, p))
                    {
                        m_pVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        P = m_p;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(message);
                }
            }

            if (!m_iVerified)
            {
                int message = 0;

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        message = LM_API_SPD_IC;
                        break;

                    case ControlMode.Position:
                        message = LM_API_POS_IC;
                        break;

                    case ControlMode.Current:
                        message = LM_API_ICTRL_IC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, CAN_MSGID_FULL_M, data);

                    double i = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_i, i))
                    {
                        m_iVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        I = m_i;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(message);
                }
            }

            if (!m_dVerified)
            {
                int message = 0;

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        message = LM_API_SPD_DC;
                        break;

                    case ControlMode.Position:
                        message = LM_API_POS_DC;
                        break;

                    case ControlMode.Current:
                        message = LM_API_ICTRL_DC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, CAN_MSGID_FULL_M, data);

                    double d = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_d, d))
                    {
                        m_dVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        D = m_d;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(message);
                }
            }

            if (!m_neutralModeVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_BRAKE_COAST, CAN_MSGID_FULL_M, data);

                    NeutralMode mode = (NeutralMode)data[0];

                    if (mode == m_neutralMode)
                    {
                        m_neutralModeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigNeutralMode = m_neutralMode;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_BRAKE_COAST);
                }
            }

            if (!m_encoderCodesPerRevVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_ENC_LINES, CAN_MSGID_FULL_M, data);

                    short codes = UnpackINT16(data);

                    if (codes == m_encoderCodesPerRev)
                    {
                        m_encoderCodesPerRevVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        EncoderCodesPerRev = m_encoderCodesPerRev;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_ENC_LINES);
                }
            }

            if (!m_potentiometerTurnsVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_POT_TURNS, CAN_MSGID_FULL_M, data);

                    short turns = UnpackINT16(data);

                    if (turns == m_potentiometerTurns)
                    {
                        m_potentiometerTurnsVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        PotentiometerTurns = m_potentiometerTurns;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_POT_TURNS);
                }
            }

            if (!m_limitModeVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_LIMIT_MODE, CAN_MSGID_FULL_M, data);

                    LimitMode mode = (LimitMode)data[0];

                    if (mode == m_limitMode)
                    {
                        m_limitModeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        LimitMode = m_limitMode;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_LIMIT_MODE);
                }
            }

            if (!m_forwardLimitVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_LIMIT_FWD, CAN_MSGID_FULL_M, data);

                    double limit = UnpackFXP16_16(data);

                    if (FXP16_EQ(limit, m_forwardLimit))
                    {
                        m_forwardLimitVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ForwardLimit = m_forwardLimit;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_LIMIT_FWD);
                }
            }

            if (!m_reverseLimitVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_LIMIT_REV, CAN_MSGID_FULL_M, data);

                    double limit = UnpackFXP16_16(data);

                    if (FXP16_EQ(limit, m_reverseLimit))
                    {
                        m_reverseLimitVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ReverseLimit = m_reverseLimit;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_LIMIT_REV);
                }
            }

            if (!m_maxOutputVoltageVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_MAX_VOUT, CAN_MSGID_FULL_M, data);

                    double voltage = UnpackFXP8_8(data);

                    // The returned max output voltage is sometimes slightly higher
                    // or lower than what was sent.  This should not trigger
                    // resending the message.
                    if (Math.Abs(voltage - m_maxOutputVoltage) < 0.1)
                    {
                        m_maxOutputVoltageVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        MaxOutputVoltage = m_maxOutputVoltage;
                    }

                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_MAX_VOUT);
                }
            }

            if (!m_voltageRampRateVerified)
            {
                if (m_controlMode == ControlMode.PercentVbus)
                {
                    try
                    {
                        GetMessage(LM_API_VOLT_SET_RAMP, CAN_MSGID_FULL_M, data);

                        double rate = UnpackPercentage(data);

                        if (FXP16_EQ(rate, m_voltageRampRate))
                        {
                            m_voltageRampRateVerified = true;
                        }
                        else
                        {
                            // It's wrong - set it again
                            VoltageRampRate = (m_voltageRampRate);
                        }

                    }
                    catch (CANMessageNotFoundException)
                    {
                        // Verification is needed but not available - request it again.
                        RequestMessage(LM_API_VOLT_SET_RAMP);
                    }
                }
            }
            else if (m_controlMode == ControlMode.Voltage)
            {
                try
                {
                    GetMessage(LM_API_VCOMP_COMP_RAMP, CAN_MSGID_FULL_M, data);

                    double rate = UnpackFXP8_8(data);

                    if (FXP8_EQ(rate, m_voltageRampRate))
                    {
                        m_voltageRampRateVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        VoltageRampRate = (m_voltageRampRate);
                    }

                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_VCOMP_COMP_RAMP);
                }
            }

            if (!m_faultTimeVerified)
            {
                try
                {
                    GetMessage(LM_API_CFG_FAULT_TIME, CAN_MSGID_FULL_M, data);

                    int faultTime = UnpackINT16(data);

                    if ((int)(m_faultTime * 1000.0) == faultTime)
                    {
                        m_faultTimeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        FaultTime = m_faultTime;
                    }
                }
                catch (CANMessageNotFoundException)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(LM_API_CFG_FAULT_TIME);
                }
            }

            if (!m_receivedStatusMessage0 ||
                    !m_receivedStatusMessage1 ||
                    !m_receivedStatusMessage2)
            {
                // If the periodic status messages haven't been verified as received,
                // request periodic status messages again and attempt to unpack any
                // available ones.
                SetupPeriodicStatus();
                //The properties are called just to update their periodic status.
                var tmp = Temperature;
                var tmp2 = Position;
                var tmp3 = Faults;
            }
        }

        private static void SendMessageHelper(int messageID, byte[] data, int dataSize, int period)
        {
            int[] kTrustedMessages = {
                LM_API_VOLT_T_EN, LM_API_VOLT_T_SET, LM_API_SPD_T_EN, LM_API_SPD_T_SET,
                LM_API_VCOMP_T_EN, LM_API_VCOMP_T_SET, LM_API_POS_T_EN, LM_API_POS_T_SET,
                LM_API_ICTRL_T_EN, LM_API_ICTRL_T_SET
            };
            int status = 0;

            for (byte i = 0; i < kTrustedMessages.Length; i++)
            {
                if ((kFullMessageIDMask & messageID) == kTrustedMessages[i])
                {
                    if (dataSize > kMaxMessageDataSize - 2)
                    {
                        throw new SystemException("CAN message has too much data.");
                    }

                    byte[] trustedData = new byte[dataSize + 2];
                    trustedData[0] = 0;
                    trustedData[1] = 0;
                    for (byte j = 0; j < dataSize; j++)
                    {
                        trustedData[j + 2] = data[j];
                    }

                    FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, trustedData, (byte)(dataSize + 2), period, ref status);
                    if (status < 0)
                    {
                        CANExceptionFactory.CheckStatus(status, messageID);
                    }

                    return;
                }
            }

            FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, data, (byte)dataSize, period, ref status);

            if (status < 0)
            {
                CANExceptionFactory.CheckStatus(status, messageID);
            }


        }

        protected void SendMessage(int messageID, byte[] data, int dataSize, int period)
        {
            SendMessageHelper(messageID | m_deviceNumber, data, dataSize, period);
        }

        protected void SendMessage(int messageID, byte[] data, int dataSize)
        {
            SendMessage(messageID, data, dataSize, CAN_SEND_PERIOD_NO_REPEAT);
        }

        protected void RequestMessage(int messageID, int period)
        {
            SendMessageHelper(messageID | m_deviceNumber, null, 0, period);
        }

        protected void RequestMessage(int messageID)
        {
            RequestMessage(messageID, CAN_SEND_PERIOD_NO_REPEAT);
        }

        protected void GetMessage(int messageID, int messageMask, byte[] data)
        {
            uint messageIdu = (uint)messageID;
            messageIdu |= m_deviceNumber;
            messageIdu &= (uint)CAN_MSGID_FULL_M;
            byte dataSize = 0;
            uint timeStamp = 0;
            int status = 0;

            FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref messageIdu, (uint)messageMask, data, ref dataSize, ref timeStamp, ref status);

            if (status < 0)
            {
                CANExceptionFactory.CheckStatus(status, messageID);
            }

        }

        protected void SetupPeriodicStatus()
        {
            byte[] data = new byte[8];
            int dataSize;

            byte[] kMessage0Data = new byte[] {
            (byte)LM_PSTAT_VOLTBUS_B0, (byte)LM_PSTAT_VOLTBUS_B1,
            (byte)LM_PSTAT_VOLTOUT_B0, (byte)LM_PSTAT_VOLTOUT_B1,
            (byte)LM_PSTAT_CURRENT_B0, (byte)LM_PSTAT_CURRENT_B1,
            (byte)LM_PSTAT_TEMP_B0, (byte)LM_PSTAT_TEMP_B1
            };

            byte[] kMessage1Data = new byte[] {
            (byte)LM_PSTAT_POS_B0, (byte)LM_PSTAT_POS_B1, (byte)LM_PSTAT_POS_B2, (byte)LM_PSTAT_POS_B3,
            (byte)LM_PSTAT_SPD_B0, (byte)LM_PSTAT_SPD_B1, (byte)LM_PSTAT_SPD_B2, (byte)LM_PSTAT_SPD_B3
        };

            byte[] kMessage2Data = new byte[] {
            (byte)LM_PSTAT_LIMIT_CLR,
            (byte)LM_PSTAT_FAULT,
            (byte)LM_PSTAT_END,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
            (byte)0,
        };

            dataSize = PackINT16(data, (short)(kSendMessagePeriod));
            SendMessage(LM_API_PSTAT_PER_EN_S0, data, dataSize);
            SendMessage(LM_API_PSTAT_PER_EN_S1, data, dataSize);
            SendMessage(LM_API_PSTAT_PER_EN_S2, data, dataSize);

            dataSize = 8;
            SendMessage(LM_API_PSTAT_CFG_S0, kMessage0Data, dataSize);
            SendMessage(LM_API_PSTAT_CFG_S1, kMessage1Data, dataSize);
            SendMessage(LM_API_PSTAT_CFG_S2, kMessage2Data, dataSize);
        }

        protected void UpdatePeriodicStatus()
        {
            byte[] data = new byte[8];

            // Check if a new bus voltage/output voltage/current/temperature message
            // has arrived and unpack the values into the cached member variables
            try
            {
                GetMessage(LM_API_PSTAT_DATA_S0, CAN_MSGID_FULL_M, data);

                m_busVoltage = UnpackFXP8_8(new byte[] { data[0], data[1] });
                m_outputVoltage = UnpackPercentage(new byte[] { data[2], data[3] }) * m_busVoltage;
                m_outputCurrent = UnpackFXP8_8(new byte[] { data[4], data[5] });
                m_temperature = UnpackFXP8_8(new byte[] { data[6], data[7] });

                m_receivedStatusMessage0 = true;
            }
            catch (CANMessageNotFoundException) { }

            // Check if a new position/speed message has arrived and do the same
            try
            {
                GetMessage(LM_API_PSTAT_DATA_S1, CAN_MSGID_FULL_M, data);

                m_position = UnpackFXP16_16(new byte[] { data[0], data[1], data[2], data[3] });
                m_speed = UnpackFXP16_16(new byte[] { data[4], data[5], data[6], data[7] });

                m_receivedStatusMessage1 = true;
            }
            catch (CANMessageNotFoundException) { }

            // Check if a new limits/faults message has arrived and do the same
            try
            {
                GetMessage(LM_API_PSTAT_DATA_S2, CAN_MSGID_FULL_M, data);
                m_limits = data[0];
                m_faults = data[1];

                m_receivedStatusMessage2 = true;
            }
            catch (CANMessageNotFoundException) { }
        }

        public static void UpdateSyncGroup(byte syncGroup)
        {
            byte[] data = new byte[8];

            data[0] = syncGroup;

            SendMessageHelper(CAN_MSGID_API_SYNC, data, 1, CAN_SEND_PERIOD_NO_REPEAT);
        }

        private static void Swap16(int x, byte[] buffer)
        {
            var tmpBuffer = BitConverter.GetBytes(x);
            buffer[0] = tmpBuffer[0];
            buffer[1] = tmpBuffer[1];
            return;
        }

        private static void Swap32(int x, byte[] buffer)
        {
            var tmpBuffer = BitConverter.GetBytes(x);
            buffer[0] = tmpBuffer[0];
            buffer[1] = tmpBuffer[1];
            buffer[2] = tmpBuffer[2];
            buffer[3] = tmpBuffer[3];
        }

        private static byte PackPercentage(byte[] buffer, double value)
        {
            if (value < -1.0) value = -1.0;
            if (value > 1.0) value = 1.0;
            short intValue = (short)(value * 32767.0);
            Swap16(intValue, buffer);
            return 2;
        }

        private static byte PackFXP8_8(byte[] buffer, double value)
        {
            short intValue = (short)(value * 256.0);
            Swap16(intValue, buffer);
            return 2;
        }

        private static byte PackFXP16_16(byte[] buffer, double value)
        {
            int intValue = (int)(value * 65536.0);
            Swap32(intValue, buffer);
            return 4;
        }

// ReSharper disable once InconsistentNaming
        private static byte PackINT16(byte[] buffer, short value)
        {
            Swap16(value, buffer);
            return 2;
        }

// ReSharper disable once InconsistentNaming
        private static byte PackINT32(byte[] buffer, int value)
        {
            Swap32(value, buffer);
            return 4;
        }

        private static short Unpack16(byte[] buffer, int offset)
        {
            return (short)((buffer[offset] & 0xFF) | (short)((buffer[offset + 1] << 8)) & 0xFF00);
        }

        private static int Unpack32(byte[] buffer, int offset)
        {
            return (buffer[offset] & 0xFF) | ((buffer[offset + 1] << 8) & 0xFF00) |
                ((buffer[offset + 2] << 16) & 0xFF0000) | (int)((buffer[offset + 3] << 24) & 0xFF000000);
        }

        private static double UnpackPercentage(byte[] buffer)
        {
            return Unpack16(buffer, 0) / 32767.0;
        }

        private static double UnpackFXP8_8(byte[] buffer)
        {
            return Unpack16(buffer, 0) / 256.0;
        }

        private static double UnpackFXP16_16(byte[] buffer)
        {
            return Unpack32(buffer, 0) / 65536.0;
        }

// ReSharper disable once InconsistentNaming
        private static short UnpackINT16(byte[] buffer)
        {
            return Unpack16(buffer, 0);
        }

// ReSharper disable once InconsistentNaming
        private static int UnpackINT32(byte[] buffer)
        {
            return Unpack32(buffer, 0);
        }

        /* Compare floats for equality as fixed point numbers */
        public bool FXP8_EQ(double a, double b)
        {
            return (int)(a * 256.0) == (int)(b * 256.0);
        }

        /* Compare floats for equality as fixed point numbers */
        public bool FXP16_EQ(double a, double b)
        {
            return (int)(a * 65536.0) == (int)(b * 65536.0);
        }

        [Obsolete]
        public void Disable()
        {
            DisableControl();
        }

        public void InitTable(ITable subtable)
        {
            m_table = subtable;
            UpdateTable();
        }


        private ITable m_table = null;

        public ITable Table => m_table;

        public string SmartDashboardType => "Speed Controller";

        public void UpdateTable()
        {
            m_table?.PutNumber("Value", Get());
        }

        public void StartLiveWindowMode()
        {
            Set(0.0);
            m_table.AddTableListener("Value", this, true);
        }

        public void ValueChanged(ITable source, string key, object value, bool isNew)
        {
            Set((double)value);
        }

        public void StopLiveWindowMode()
        {
            Set(0.0);
            m_table.RemoveTableListener(this);
        }

        /**
         * Set the reference source device for position controller mode.
         *
         * Choose between using and encoder and using a potentiometer
         * as the source of position feedback when in position control mode.
         *
         * @param reference Specify a position reference.
         */
        private void SetPositionReference(int reference)
        {
            SendMessage(LM_API_POS_REF, new[] { (byte)reference }, 1);

            m_positionReference = reference;
            m_posRefVerified = false;
        }

        /**
        * Set the P constant for the closed loop modes.
        *
        * @param p The proportional gain of the Jaguar's PID controller.
        */

        public double P
        {
            set
            {
                byte[] data = new byte[8];
                byte dataSize = PackFXP16_16(data, value);

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        SendMessage(LM_API_SPD_PC, data, dataSize);
                        break;

                    case ControlMode.Position:
                        SendMessage(LM_API_POS_PC, data, dataSize);
                        break;

                    case ControlMode.Current:
                        SendMessage(LM_API_ICTRL_PC, data, dataSize);
                        break;

                    default:
                        throw new InvalidOperationException(
                            "PID constants only apply in Speed, Position, and Current mode");
                }

                m_p = value;
                m_pVerified = false;
            }
            get
            {
                if (m_controlMode == (ControlMode.PercentVbus) || m_controlMode == (ControlMode.Voltage))
                {
                    throw new InvalidOperationException("PID does not apply in Percent or Voltage control modes");
                }
                return m_p;
            }
        }

        /**
        * Set the I constant for the closed loop modes.
        *
        * @param i The integral gain of the Jaguar's PID controller.
        */

        public double I
        {
            set
            {
                byte[] data = new byte[8];
                byte dataSize = PackFXP16_16(data, value);

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        SendMessage(LM_API_SPD_IC, data, dataSize);
                        break;

                    case ControlMode.Position:
                        SendMessage(LM_API_POS_IC, data, dataSize);
                        break;

                    case ControlMode.Current:
                        SendMessage(LM_API_ICTRL_IC, data, dataSize);
                        break;

                    default:
                        throw new InvalidOperationException(
                            "PID constants only apply in Speed, Position, and Current mode");
                }

                m_i = value;
                m_iVerified = false;
            }
            get
            {
                if (m_controlMode == (ControlMode.PercentVbus) || m_controlMode == (ControlMode.Voltage))
                {
                    throw new InvalidOperationException("PID does not apply in Percent or Voltage control modes");
                }
                return m_i;
            }
        }

        /**
        * Set the D constant for the closed loop modes.
        *
        * @param d The derivative gain of the Jaguar's PID controller.
        */

        public double D
        {
            set
            {
                byte[] data = new byte[8];
                byte dataSize = PackFXP16_16(data, value);

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        SendMessage(LM_API_SPD_DC, data, dataSize);
                        break;

                    case ControlMode.Position:
                        SendMessage(LM_API_POS_DC, data, dataSize);
                        break;

                    case ControlMode.Current:
                        SendMessage(LM_API_ICTRL_DC, data, dataSize);
                        break;

                    default:
                        throw new InvalidOperationException(
                            "PID constants only apply in Speed, Position, and Current mode");
                }

                m_d = value;
                m_dVerified = false;
            }
            get
            {
                if (m_controlMode == (ControlMode.PercentVbus) || m_controlMode == (ControlMode.Voltage))
                {
                    throw new InvalidOperationException("PID does not apply in Percent or Voltage control modes");
                }
                return m_d;
            }
        }

        /**
        * Set the P, I, and D constants for the closed loop modes.
        *
        * @param p The proportional gain of the Jaguar's PID controller.
        * @param i The integral gain of the Jaguar's PID controller.
        * @param d The differential gain of the Jaguar's PID controller.
        */
        public void SetPID(double p, double i, double d)
        {
            P = p;
            I = i;
            D = d;
        }

        /**
        * Get the Proportional gain of the controller.
        *
        * @return The proportional gain.
        */

        /**
        * Get the Integral gain of the controller.
        *
        * @return The integral gain.
        */

        /**
        * Get the Derivative gain of the controller.
        *
        * @return The derivative gain.
        */

        /**
         * Enable controlling the motor voltage as a percentage of the bus voltage
         * without any position or speed feedback.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         */
        public void SetPercentMode()
        {
            ChangeControlMode(ControlMode.PercentVbus);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_NONE);
        }

        /**
         * Enable controlling the motor voltage as a percentage of the bus voltage,
         * and enable speed sensing from a non-quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         */
        public void SetPercentMode(EncoderTag tag, int codesPerRev)
        {
            ChangeControlMode(ControlMode.PercentVbus);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_ENCODER);
            EncoderCodesPerRev = codesPerRev;
        }

        /**
         * Enable controlling the motor voltage as a percentage of the bus voltage,
         * and enable position and speed sensing from a quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kQuadEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         */
        public void SetPercentMode(QuadEncoderTag tag, int codesPerRev)
        {
            ChangeControlMode(ControlMode.PercentVbus);
            SetPositionReference(LM_REF_ENCODER);
            SetSpeedReference(LM_REF_QUAD_ENCODER);
            EncoderCodesPerRev = codesPerRev;
        }

        /**
         * Enable controlling the motor voltage as a percentage of the bus voltage,
         * and enable position sensing from a potentiometer and no speed feedback.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kPotentiometer}
         */
        public void SetPercentMode(PotentiometerTag tag)
        {
            ChangeControlMode(ControlMode.PercentVbus);
            SetPositionReference(LM_REF_POT);
            SetSpeedReference(LM_REF_NONE);
            PotentiometerTurns = 1;
        }

        /**
         * Enable controlling the motor current with a PID loop.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetCurrentMode(double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Current);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_NONE);
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the motor current with a PID loop, and enable speed
         * sensing from a non-quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kEncoder}
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetCurrentMode(EncoderTag tag, int codesPerRev, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Current);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_NONE);
            EncoderCodesPerRev = codesPerRev;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the motor current with a PID loop, and enable speed and
         * position sensing from a quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kQuadEncoder}
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetCurrentMode(QuadEncoderTag tag, int codesPerRev, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Current);
            SetPositionReference(LM_REF_ENCODER);
            SetSpeedReference(LM_REF_QUAD_ENCODER);
            EncoderCodesPerRev = codesPerRev;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the motor current with a PID loop, and enable position
         * sensing from a potentiometer.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kPotentiometer}
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetCurrentMode(PotentiometerTag tag, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Current);
            SetPositionReference(LM_REF_POT);
            SetSpeedReference(LM_REF_NONE);
            PotentiometerTurns = 1;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the speed with a feedback loop from a non-quadrature
         * encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetSpeedMode(EncoderTag tag, int codesPerRev, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Speed);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_ENCODER);
            EncoderCodesPerRev = codesPerRev;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the speed with a feedback loop from a quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kQuadEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetSpeedMode(QuadEncoderTag tag, int codesPerRev, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Speed);
            SetPositionReference(LM_REF_ENCODER);
            SetSpeedReference(LM_REF_QUAD_ENCODER);
            EncoderCodesPerRev = codesPerRev;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the position with a feedback loop using an encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kQuadEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         *
         */
        public void SetPositionMode(QuadEncoderTag tag, int codesPerRev, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Position);
            SetPositionReference(LM_REF_ENCODER);
            EncoderCodesPerRev = codesPerRev;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the position with a feedback loop using a potentiometer.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kPotentiometer}
         * @param p The proportional gain of the Jaguar's PID controller.
         * @param i The integral gain of the Jaguar's PID controller.
         * @param d The differential gain of the Jaguar's PID controller.
         */
        public void SetPositionMode(PotentiometerTag tag, double p, double i, double d)
        {
            ChangeControlMode(ControlMode.Position);
            SetPositionReference(LM_REF_POT);
            PotentiometerTurns = 1;
            SetPID(p, i, d);
        }

        /**
         * Enable controlling the motor voltage without any position or speed feedback.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         */
        public void SetVoltageMode()
        {
            ChangeControlMode(ControlMode.Voltage);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_NONE);
        }

        /**
         * Enable controlling the motor voltage with speed feedback from a
         * non-quadrature encoder and no position feedback.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         */
        public void SetVoltageMode(EncoderTag tag, int codesPerRev)
        {
            ChangeControlMode(ControlMode.Voltage);
            SetPositionReference(LM_REF_NONE);
            SetSpeedReference(LM_REF_ENCODER);
            EncoderCodesPerRev = codesPerRev;
        }

        /**
         * Enable controlling the motor voltage with position and speed feedback from a
         * quadrature encoder.<br>
         * After calling this you must call {@link CANJaguar#enableControl()} or {@link CANJaguar#enableControl(double)} to enable the device.
         *
         * @param tag The constant {@link CANJaguar#kQuadEncoder}
         * @param codesPerRev The counts per revolution on the encoder
         */
        public void SetVoltageMode(QuadEncoderTag tag, int codesPerRev)
        {
            ChangeControlMode(ControlMode.Voltage);
            SetPositionReference(LM_REF_ENCODER);
            SetSpeedReference(LM_REF_QUAD_ENCODER);
            EncoderCodesPerRev = codesPerRev;
        }

        /**
         * Enable controlling the motor voltage with position feedback from a
         * potentiometer and no speed feedback.
         *
         * @param tag The constant {@link CANJaguar#kPotentiometer}
         */
        public void SetVoltageMode(PotentiometerTag tag)
        {
            ChangeControlMode(ControlMode.Voltage);
            SetPositionReference(LM_REF_POT);
            SetSpeedReference(LM_REF_NONE);
            PotentiometerTurns = 1;
        }

        private void ChangeControlMode(ControlMode controlMode)
        {
            // Disable the previous mode
            DisableControl();

            // Update the local mode
            m_controlMode = controlMode;
            m_controlModeVerified = false;
        }

        /**
        * Get the active control mode from the Jaguar.
        *
        * Ask the Jagaur what mode it is in.
        *
        * @return ControlMode that the Jag is in.
        */
        public ControlMode GetControlMode()
        {
            return m_controlMode;
        }

        /**
        * Get the voltage at the battery input terminals of the Jaguar.
        *
        * @return The bus voltage in Volts.
        */

        public double BusVoltage
        {
            get
            {
                UpdatePeriodicStatus();

                return m_busVoltage;
            }
        }

        /**
        * Get the voltage being output from the motor terminals of the Jaguar.
        *
        * @return The output voltage in Volts.
        */

        public double OutputVoltage
        {
            get
            {
                UpdatePeriodicStatus();

                return m_outputVoltage;
            }
        }

        /**
        * Get the current through the motor terminals of the Jaguar.
        *
        * @return The output current in Amps.
        */

        public double OutputCurrent
        {
            get
            {
                UpdatePeriodicStatus();

                return m_outputCurrent;
            }
        }

        /**
        * Get the internal temperature of the Jaguar.
        *
        * @return The temperature of the Jaguar in degrees Celsius.
        */

        public double Temperature
        {
            get
            {
                UpdatePeriodicStatus();

                return m_temperature;
            }
        }

        /**
         * Get the position of the encoder or potentiometer.
         *
         * @return The position of the motor in rotations based on the configured feedback.
         * @see CANJaguar#configPotentiometerTurns(int)
         * @see CANJaguar#configEncoderCodesPerRev(int)
         */

        public double Position
        {
            get
            {
                UpdatePeriodicStatus();

                return m_position;
            }
        }

        /**
        * Get the speed of the encoder.
        *
        * @return The speed of the motor in RPM based on the configured feedback.
        */

        public double Speed
        {
            get
            {
                UpdatePeriodicStatus();

                return m_speed;
            }
        }

        /**
         * Get the status of the forward limit switch.
         *
         * @return true if the motor is allowed to turn in the forward direction.
         */

        public bool ForwardLimitOK
        {
            get
            {
                UpdatePeriodicStatus();

                return (m_limits & kForwardLimit) != 0;
            }
        }

        /**
         * Get the status of the reverse limit switch.
         *
         * @return true if the motor is allowed to turn in the reverse direction.
         */

        public bool ReverseLimitOK
        {
            get
            {
                UpdatePeriodicStatus();

                return (m_limits & kReverseLimit) != 0;
            }
        }

        /**
         * Get the status of any faults the Jaguar has detected.
         *
         * @return A bit-mask of faults defined by the "Faults" constants.
         * @see #kCurrentFault
         * @see #kBusVoltageFault
         * @see #kTemperatureFault
         * @see #kGateDriverFault
         */

        public Faults Faults
        {
            get
            {
                UpdatePeriodicStatus();

                return (Faults) m_faults;
            }
        }

        /**
        * set the maximum voltage change rate.
        *
        * When in PercentVbus or Voltage output mode, the rate at which the voltage changes can
        * be limited to reduce current spikes.  set this to 0.0 to disable rate limiting.
        *
        * @param rampRate The maximum rate of voltage change in Percent Voltage mode in V/s.
        */
        public double VoltageRampRate
        {
            set
            {
                byte[] data = new byte[8];
                int dataSize;
                int message;

                switch (m_controlMode)
                {
                    case ControlMode.PercentVbus:
                        dataSize = PackPercentage(data, value/(m_maxOutputVoltage*kControllerRate));
                        message = LM_API_VOLT_SET_RAMP;
                        break;
                    case ControlMode.Voltage:
                        dataSize = PackFXP8_8(data, value/kControllerRate);
                        message = LM_API_VCOMP_COMP_RAMP;
                        break;
                    default:
                        throw new InvalidOperationException(
                            "Voltage ramp rate only applies in Percentage and Voltage modes");
                }

                SendMessage(message, data, dataSize);
            }
        }

        /**
        * Get the version of the firmware running on the Jaguar.
        *
        * @return The firmware version.  0 if the device did not respond.
        */

        public uint FirmwareVersion => (uint) m_firmwareVersion;

        /**
        * Get the version of the Jaguar hardware.
        *
        * @return The hardware version. 1: Jaguar,  2: Black Jaguar
        */
        public byte GetHardwareVersion()
        {
            return m_hardwareVersion;
        }

        /**
        * Configure what the controller does to the H-Bridge when neutral (not driving the output).
        *
        * This allows you to override the jumper configuration for brake or coast.
        *
        * @param mode Select to use the jumper setting or to override it to coast or brake.
        */

        public NeutralMode ConfigNeutralMode
        {
            set
            {
                byte[] data = new byte[8];

                data[0] = (byte) value;

                SendMessage(LM_API_CFG_BRAKE_COAST, data, 1);

                m_neutralMode = value;
                m_neutralModeVerified = false;
            }
        }

        /**
        * Configure how many codes per revolution are generated by your encoder.
        *
        * @param codesPerRev The number of counts per revolution in 1X mode.
        */

        public int EncoderCodesPerRev
        {
            set
            {
                byte[] data = new byte[8];

                int dataSize = PackINT16(data, (short) value);
                SendMessage(LM_API_CFG_ENC_LINES, data, dataSize);

                m_encoderCodesPerRev = (short) value;
                m_encoderCodesPerRevVerified = false;
            }
        }

        /**
        * Configure the number of turns on the potentiometer.
        *
        * There is no special support for continuous turn potentiometers.
        * Only integer numbers of turns are supported.
        *
        * @param turns The number of turns of the potentiometer
        */

        public int PotentiometerTurns
        {
            set
            {
                byte[] data = new byte[8];

                int dataSize = PackINT16(data, (short) value);
                SendMessage(LM_API_CFG_POT_TURNS, data, dataSize);

                m_potentiometerTurns = (short) value;
                m_potentiometerTurnsVerified = false;
            }
        }

        /**
         * Configure Soft Position Limits when in Position Controller mode.<br>
         *
         * When controlling position, you can add additional limits on top of the limit switch inputs
         * that are based on the position feedback.  If the position limit is reached or the
         * switch is opened, that direction will be disabled.
         *
         * @param forwardLimitPosition The position that, if exceeded, will disable the forward direction.
         * @param reverseLimitPosition The position that, if exceeded, will disable the reverse direction.
         */
        public void ConfigSoftPositionLimits(double forwardLimitPosition, double reverseLimitPosition)
        {
            LimitMode = LimitMode.SoftPositionLimits;
            ForwardLimit = forwardLimitPosition;
            ReverseLimit = reverseLimitPosition;
        }

        /**
         * Disable Soft Position Limits if previously enabled.<br>
         *
         * Soft Position Limits are disabled by default.
         */
        public void DisableSoftPositionLimits()
        {
            LimitMode = LimitMode.SwitchInputsOnly;
        }

        /**
         * Set the limit mode for position control mode.<br>
         *
         * Use {@link #configSoftPositionLimits(double, double)} or {@link #disableSoftPositionLimits()} to set this
         * automatically.
         * @param mode The {@link LimitMode} to use to limit the rotation of the device.
         * @see LimitMode#SwitchInputsOnly
         * @see LimitMode#SoftPositionLimits
         */

        public LimitMode LimitMode
        {
            set
            {
                byte[] data = new byte[8];
                data[0] = (byte) value;
                SendMessage(LM_API_CFG_LIMIT_MODE, data, 1);
            }
        }

        /**
         * Set the position that, if exceeded, will disable the forward direction.
         *
         * Use {@link #configSoftPositionLimits(double, double)} to set this and the {@link LimitMode} automatically.
         * @param forwardLimitPosition The position that, if exceeded, will disable the forward direction.
         */

        public double ForwardLimit
        {
            set
            {
                byte[] data = new byte[8];

                int dataSize = PackFXP16_16(data, value);
                data[dataSize++] = 1;
                SendMessage(LM_API_CFG_LIMIT_FWD, data, dataSize);

                m_forwardLimit = value;
                m_forwardLimitVerified = false;
            }
        }

        /**
         * Set the position that, if exceeded, will disable the reverse direction.
         *
         * Use {@link #configSoftPositionLimits(double, double)} to set this and the {@link LimitMode} automatically.
         * @param reverseLimitPosition The position that, if exceeded, will disable the reverse direction.
         */

        public double ReverseLimit
        {
            set
            {
                byte[] data = new byte[8];

                int dataSize = PackFXP16_16(data, value);
                data[dataSize++] = 1;
                SendMessage(LM_API_CFG_LIMIT_REV, data, dataSize);

                m_reverseLimit = value;
                m_reverseLimitVerified = false;
            }
        }

        /**
        * Configure the maximum voltage that the Jaguar will ever output.
        *
        * This can be used to limit the maximum output voltage in all modes so that
        * motors which cannot withstand full bus voltage can be used safely.
        *
        * @param voltage The maximum voltage output by the Jaguar.
        */

        public double MaxOutputVoltage
        {
            set
            {
                byte[] data = new byte[8];

                int dataSize = PackFXP8_8(data, value);
                SendMessage(LM_API_CFG_MAX_VOUT, data, dataSize);

                m_maxOutputVoltage = value;
                m_maxOutputVoltageVerified = false;
            }
        }

        /**
        * Configure how long the Jaguar waits in the case of a fault before resuming operation.
        *
        * Faults include over temerature, over current, and bus under voltage.
        * The default is 3.0 seconds, but can be reduced to as low as 0.5 seconds.
        *
        * @param faultTime The time to wait before resuming operation, in seconds.
        */

        public float FaultTime
        {
            set
            {
                byte[] data = new byte[8];

                if (value < 0.5f) value = 0.5f;
                else if (value > 3.0f) value = 3.0f;

                int dataSize = PackINT16(data, (short) (value*1000.0));
                SendMessage(LM_API_CFG_FAULT_TIME, data, dataSize);

                m_faultTime = value;
                m_faultTimeVerified = false;
            }
        }
    }
}
