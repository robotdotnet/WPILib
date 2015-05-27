using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkTablesDotNet.Tables;
using WPILib.livewindow;
using WPILib.Interfaces;
using HAL_Base;
using WPILib.Util;

namespace WPILib
{
    public class CANJaguar : MotorSafety, SpeedController, ITableListener, LiveWindowSendable
    {
        public static int kMaxMessageDataSize = 8;

        public static int kControllerRate = 1000;
        public static double kApproxBusVoltage = 12.0;

        private MotorSafetyHelper m_safetyHelper;
        private static Resource allocated = new Resource(63);

        private static int kFullMessageIDMask = HALCAN.CAN_MSGID_API_M | HALCAN.CAN_MSGID_MFR_M | HALCAN.CAN_MSGID_DTYPE_M;
        private static int kSendMessagePeriod = 20;

        private enum EncoderTag
        {
            Encoder,
            QuadEncoder,
            Potentiometer,
        }


        public enum ControlMode
        {
            PercentVbus,
            Current,
            Speed,
            Position,
            Voltage,
        }

        public static int kCurrentFault = 1;
        public static int kTemeperatureFault = 2;
        public static int kButVoltageFault = 4;
        public static int kGateDriverFault = 8;

        public static int kForwardLimit = 1;
        public static int kReverseLimit = 2;

        public enum NeutralMode
        {
            Jumper,
            Break,
            Coast,
        }

        public enum LimitMode
        {
            SwitchInputsOnly,
            SoftPositionLimits,
        }

        public CANJaguar(int deviceNumber)
        {
            try
            {
                allocated.Allocate(deviceNumber - 1);
            }
            catch (CheckedAllocationException e1)
            {
                throw new AllocationException("CANJaguar device " + e1.Message + "(increment index by one)");
            }

            m_deviceNumber = (byte)deviceNumber;
            m_controlMode = ControlMode.PercentVbus;

            m_safetyHelper = new MotorSafetyHelper(this);

            bool recievedFirmwareVersion = false;

            byte[] data = new byte[8];


        }

        public void Free()
        {
            allocated.Free(m_deviceNumber - 1);
            m_safetyHelper = null;
            int status = 0;

            int messageID;

            switch (m_controlMode)
            {
                case ControlMode.PercentVbus:
                    messageID = (int)m_controlMode | HALCAN.LM_API_VOLT_T_SET;
                    break;
                case ControlMode.Speed:
                    messageID = (int)m_controlMode | HALCAN.LM_API_SPD_T_SET;
                    break;
                case ControlMode.Position:
                    messageID = (int)m_controlMode | HALCAN.LM_API_POS_T_SET;
                    break;
                case ControlMode.Current:
                    messageID = (int)m_controlMode | HALCAN.LM_API_ICTRL_T_SET;
                    break;
                case ControlMode.Voltage:
                    messageID = (int)m_controlMode | HALCAN.LM_API_VCOMP_T_SET;
                    break;
                default:
                    return;

            }

            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, null, 0,
                HALCAN.CAN_SEND_PERIOD_STOP_REPEATING, ref status);
        }

        int GetDeviceNumber()
        {
            return m_deviceNumber;
        }


        byte m_deviceNumber;
        double m_value = 0.0f;

        // Parameters/configuration
        ControlMode m_controlMode;
        int m_speedReference = HALCAN.LM_REF_NONE;
        int m_positionReference = HALCAN.LM_REF_NONE;
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

        static readonly int kReceiveStatusAttempts = 50;

        bool m_controlEnabled = true;

        public void SetExpiration(double timeout)
        {
            throw new NotImplementedException();
        }

        public double GetExpiration()
        {
            throw new NotImplementedException();
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }

        public void StopMotor()
        {
            throw new NotImplementedException();
        }

        public void SetSafetyEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        public bool IsSafetyEnabled()
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }

        public void PidWrite(double output)
        {
            if (m_controlMode == ControlMode.PercentVbus)
            {
                Set(output);
            }
            else
            {
                throw new InvalidOperationException("PID only supported in PercentVbus mode");
            }
        }

        private void SetSpeedReference(int reference)
        {
            SendMessage(HALCAN.LM_API_SPD_REF, new byte[] { (byte)reference }, 1);
            m_speedReference = reference;
            m_speedRefVerified = false;
        }

        public void EnableControl(double encoderInitialPosition)
        {
            switch (m_controlMode)
            {
                case ControlMode.PercentVbus:
                    SendMessage(HALCAN.LM_API_VOLT_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Speed:
                    SendMessage(HALCAN.LM_API_SPD_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Position:
                    byte[] data = new byte[8];
                    int dataSize = PackFXP16_16(data, encoderInitialPosition);
                    SendMessage(HALCAN.LM_API_POS_T_EN, data, dataSize);
                    break;

                case ControlMode.Current:
                    SendMessage(HALCAN.LM_API_ICTRL_T_EN, new byte[0], 0);
                    break;

                case ControlMode.Voltage:
                    SendMessage(HALCAN.LM_API_VCOMP_T_EN, new byte[0], 0);
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
            SendMessage(HALCAN.LM_API_VOLT_DIS, new byte[0], 0);
            SendMessage(HALCAN.LM_API_SPD_DIS, new byte[0], 0);
            SendMessage(HALCAN.LM_API_POS_DIS, new byte[0], 0);
            SendMessage(HALCAN.LM_API_ICTRL_DIS, new byte[0], 0);
            SendMessage(HALCAN.LM_API_VCOMP_DIS, new byte[0], 0);

            // Stop all periodic setpoints
            SendMessage(HALCAN.LM_API_VOLT_T_SET, new byte[0], 0, HALCAN.CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(HALCAN.LM_API_SPD_T_SET, new byte[0], 0, HALCAN.CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(HALCAN.LM_API_POS_T_SET, new byte[0], 0, HALCAN.CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(HALCAN.LM_API_ICTRL_T_SET, new byte[0], 0, HALCAN.CAN_SEND_PERIOD_STOP_REPEATING);
            SendMessage(HALCAN.LM_API_VCOMP_T_SET, new byte[0], 0, HALCAN.CAN_SEND_PERIOD_STOP_REPEATING);

            m_controlEnabled = false;
        }

        public double Get()
        {
            return m_value;
        }

        public void Set(double speed, byte syncGroup)
        {
            int messageID;
            byte[] data = new byte[8];
            byte dataSize;

            if (m_controlEnabled)
            {
                switch (m_controlMode)
                {
                    case ControlMode.PercentVbus:
                        messageID = HALCAN.LM_API_VOLT_T_SET;
                        dataSize = PackPercentage(data, speed);
                        break;
                    case ControlMode.Speed:
                        messageID = HALCAN.LM_API_SPD_T_SET;
                        dataSize = PackFXP16_16(data, speed);
                        break;
                    case ControlMode.Position:
                        messageID = HALCAN.LM_API_POS_T_SET;
                        dataSize = PackFXP16_16(data, speed);
                        break;
                    case ControlMode.Current:
                        messageID = HALCAN.LM_API_ICTRL_T_SET;
                        dataSize = PackFXP8_8(data, speed);
                        break;
                    case ControlMode.Voltage:
                        messageID = HALCAN.LM_API_VCOMP_T_SET;
                        dataSize = PackFXP8_8(data, speed);
                        break;
                    default:
                        return;

                }

                if (syncGroup != 0)
                {
                    data[dataSize++] = syncGroup;
                }

                SendMessage(messageID, data, dataSize, kSendMessagePeriod);
                if (m_safetyHelper != null)
                {
                    m_safetyHelper.Feed();
                }
            }
            m_value = speed;
            Verify();
        }

        protected void Verify()
        {
            byte[] data = new byte[8];

            try
            {
                GetMessage(HALCAN.LM_API_STATUS_POWER, HALCAN.CAN_MSGID_FULL_M, data);
                bool powerCycled = data[0] != 0;

                if (powerCycled)
                {
                    data[0] = 1;
                    SendMessage(HALCAN.LM_API_STATUS_POWER, data, 1);

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
                        HALCAN.LM_API_SPD_REF, HALCAN.LM_API_POS_REF,
                        HALCAN.LM_API_SPD_PC, HALCAN.LM_API_POS_PC,
                        HALCAN.LM_API_ICTRL_PC, HALCAN.LM_API_SPD_IC,
                        HALCAN.LM_API_POS_IC, HALCAN.LM_API_ICTRL_IC,
                        HALCAN.LM_API_SPD_DC, HALCAN.LM_API_POS_DC,
                        HALCAN.LM_API_ICTRL_DC, HALCAN.LM_API_CFG_ENC_LINES,
                        HALCAN.LM_API_CFG_POT_TURNS, HALCAN.LM_API_CFG_BRAKE_COAST,
                        HALCAN.LM_API_CFG_LIMIT_MODE, HALCAN.LM_API_CFG_LIMIT_REV,
                        HALCAN.LM_API_CFG_MAX_VOUT, HALCAN.LM_API_VOLT_SET_RAMP,
                        HALCAN.LM_API_VCOMP_COMP_RAMP, HALCAN.LM_API_CFG_FAULT_TIME,
                        HALCAN.LM_API_CFG_LIMIT_FWD
                    };

                    foreach (int message in messages)
                    {
                        try
                        {
                            GetMessage(message, HALCAN.CAN_MSGID_FULL_M, data);
                        }
                        catch (CANMessageNotFoundException e)
                        {

                        }
                    }
                }
            }
            catch (CANMessageNotFoundException e)
            {
                RequestMessage(HALCAN.LM_API_STATUS_POWER);
            }

            if (!m_controlModeVerified && m_controlEnabled)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_STATUS_CMODE, HALCAN.CAN_MSGID_FULL_M, data);
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
                catch (CANMessageNotFoundException e)
                {
                    RequestMessage(HALCAN.LM_API_STATUS_CMODE);
                }
            }

            if (!m_speedRefVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_SPD_REF, HALCAN.CAN_MSGID_FULL_M, data);

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
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_SPD_REF);
                }
            }

            if (!m_posRefVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_POS_REF, HALCAN.CAN_MSGID_FULL_M, data);

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
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_POS_REF);
                }
            }

            if (!m_pVerified)
            {
                int message = 0;

                switch (m_controlMode)
                {
                    case ControlMode.Speed:
                        message = HALCAN.LM_API_SPD_PC;
                        break;

                    case ControlMode.Position:
                        message = HALCAN.LM_API_POS_PC;
                        break;

                    case ControlMode.Current:
                        message = HALCAN.LM_API_ICTRL_PC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, HALCAN.CAN_MSGID_FULL_M, data);

                    double p = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_p, p))
                    {
                        m_pVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetP(m_p);
                    }
                }
                catch (CANMessageNotFoundException e)
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
                        message = HALCAN.LM_API_SPD_IC;
                        break;

                    case ControlMode.Position:
                        message = HALCAN.LM_API_POS_IC;
                        break;

                    case ControlMode.Current:
                        message = HALCAN.LM_API_ICTRL_IC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, HALCAN.CAN_MSGID_FULL_M, data);

                    double i = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_i, i))
                    {
                        m_iVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetI(m_i);
                    }
                }
                catch (CANMessageNotFoundException e)
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
                        message = HALCAN.LM_API_SPD_DC;
                        break;

                    case ControlMode.Position:
                        message = HALCAN.LM_API_POS_DC;
                        break;

                    case ControlMode.Current:
                        message = HALCAN.LM_API_ICTRL_DC;
                        break;

                    default:
                        break;
                }

                try
                {
                    GetMessage(message, HALCAN.CAN_MSGID_FULL_M, data);

                    double d = UnpackFXP16_16(data);

                    if (FXP16_EQ(m_d, d))
                    {
                        m_dVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetD(m_d);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(message);
                }
            }

            if (!m_neutralModeVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_BRAKE_COAST, HALCAN.CAN_MSGID_FULL_M, data);

                    NeutralMode mode = (NeutralMode)data[0];

                    if (mode == m_neutralMode)
                    {
                        m_neutralModeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigNeutralMode(m_neutralMode);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_BRAKE_COAST);
                }
            }

            if (!m_encoderCodesPerRevVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_ENC_LINES, HALCAN.CAN_MSGID_FULL_M, data);

                    short codes = UnpackINT16(data);

                    if (codes == m_encoderCodesPerRev)
                    {
                        m_encoderCodesPerRevVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigEncoderCodesPerRev(m_encoderCodesPerRev);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_ENC_LINES);
                }
            }

            if (!m_potentiometerTurnsVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_POT_TURNS, HALCAN.CAN_MSGID_FULL_M, data);

                    short turns = UnpackINT16(data);

                    if (turns == m_potentiometerTurns)
                    {
                        m_potentiometerTurnsVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigPotentiometerTurns(m_potentiometerTurns);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_POT_TURNS);
                }
            }

            if (!m_limitModeVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_LIMIT_MODE, HALCAN.CAN_MSGID_FULL_M, data);

                    LimitMode mode = (LimitMode)data[0];

                    if (mode == m_limitMode)
                    {
                        m_limitModeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigLimitMode(m_limitMode);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_LIMIT_MODE);
                }
            }

            if (!m_forwardLimitVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_LIMIT_FWD, HALCAN.CAN_MSGID_FULL_M, data);

                    double limit = UnpackFXP16_16(data);

                    if (FXP16_EQ(limit, m_forwardLimit))
                    {
                        m_forwardLimitVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigForwardLimit(m_forwardLimit);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_LIMIT_FWD);
                }
            }

            if (!m_reverseLimitVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_LIMIT_REV, HALCAN.CAN_MSGID_FULL_M, data);

                    double limit = UnpackFXP16_16(data);

                    if (FXP16_EQ(limit, m_reverseLimit))
                    {
                        m_reverseLimitVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigReverseLimit(m_reverseLimit);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_LIMIT_REV);
                }
            }

            if (!m_maxOutputVoltageVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_MAX_VOUT, HALCAN.CAN_MSGID_FULL_M, data);

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
                        ConfigMaxOutputVoltage(m_maxOutputVoltage);
                    }

                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_MAX_VOUT);
                }
            }

            if (!m_voltageRampRateVerified)
            {
                if (m_controlMode == ControlMode.PercentVbus)
                {
                    try
                    {
                        GetMessage(HALCAN.LM_API_VOLT_SET_RAMP, HALCAN.CAN_MSGID_FULL_M, data);

                        double rate = UnpackPercentage(data);

                        if (FXP16_EQ(rate, m_voltageRampRate))
                        {
                            m_voltageRampRateVerified = true;
                        }
                        else
                        {
                            // It's wrong - set it again
                            SetVoltageRampRate(m_voltageRampRate);
                        }

                    }
                    catch (CANMessageNotFoundException e)
                    {
                        // Verification is needed but not available - request it again.
                        RequestMessage(HALCAN.LM_API_VOLT_SET_RAMP);
                    }
                }
            }
            else if (m_controlMode == ControlMode.Voltage)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_VCOMP_COMP_RAMP, HALCAN.CAN_MSGID_FULL_M, data);

                    double rate = UnpackFXP8_8(data);

                    if (FXP8_EQ(rate, m_voltageRampRate))
                    {
                        m_voltageRampRateVerified = true;
                    }
                    else
                    {
                        // It's wrong - set it again
                        SetVoltageRampRate(m_voltageRampRate);
                    }

                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_VCOMP_COMP_RAMP);
                }
            }

            if (!m_faultTimeVerified)
            {
                try
                {
                    GetMessage(HALCAN.LM_API_CFG_FAULT_TIME, HALCAN.CAN_MSGID_FULL_M, data);

                    int faultTime = UnpackINT16(data);

                    if ((int)(m_faultTime * 1000.0) == faultTime)
                    {
                        m_faultTimeVerified = true;
                    }
                    else
                    {
                        //It's wrong - set it again
                        ConfigFaultTime(m_faultTime);
                    }
                }
                catch (CANMessageNotFoundException e)
                {
                    // Verification is needed but not available - request it again.
                    RequestMessage(HALCAN.LM_API_CFG_FAULT_TIME);
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
                GetTemperature();
                GetPosition();
                GetFaults();
            }
        }

        private static void SendMessageHelper(int messageID, byte[] data, int dataSize, int period)
        {
            int[] kTrustedMessages = {
				HALCAN.LM_API_VOLT_T_EN, HALCAN.LM_API_VOLT_T_SET, HALCAN.LM_API_SPD_T_EN, HALCAN.LM_API_SPD_T_SET,
				HALCAN.LM_API_VCOMP_T_EN, HALCAN.LM_API_VCOMP_T_SET, HALCAN.LM_API_POS_T_EN, HALCAN.LM_API_POS_T_SET,
				HALCAN.LM_API_ICTRL_T_EN, HALCAN.LM_API_ICTRL_T_SET
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

                    HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, trustedData, (byte)(dataSize + 2), period, ref status);
                    if (status < 0)
                    {
                        CANExceptionFactory.CheckStatus(status, messageID);
                    }

                    return;
                }
            }

            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage((uint)messageID, data, (byte)dataSize, period, ref status);

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
            SendMessage(messageID, data, dataSize, HALCAN.CAN_SEND_PERIOD_NO_REPEAT);
        }

        protected void RequestMessage(int messageID, int period)
        {
            SendMessageHelper(messageID | m_deviceNumber, null, 0, period);
        }

        protected void RequestMessage(int messageID)
        {
            RequestMessage(messageID, HALCAN.CAN_SEND_PERIOD_NO_REPEAT);
        }

        protected bool GetMessage(int messageID, int messageMask, byte[] data)
        {
            uint messageIDU = (uint)messageID;
            messageIDU |= m_deviceNumber;
            messageIDU &= (uint)HALCAN.CAN_MSGID_FULL_M;
            byte dataSize = 0;
            uint timeStamp = 0;
            int status = 0;

            HALCAN.FRC_NetworkCommunication_CANSessionMux_receiveMessage(ref messageIDU, (uint)messageMask, data, ref dataSize, ref timeStamp, ref status);

            if (status == CANExceptionFactory.ERR_CANSessionMux_MessageNotFound)
            {
                return false;
            }
            else
            {
                Utility.CheckStatus(status);
            }

            return true;

        }

        protected void SetupPeriodicStatus()
        {
            byte[] data = new byte[8];
            int dataSize;

            byte[] kMessage0Data = new byte[] {
			(byte)HALCAN.LM_PSTAT_VOLTBUS_B0, (byte)HALCAN.LM_PSTAT_VOLTBUS_B1,
			(byte)HALCAN.LM_PSTAT_VOLTOUT_B0, (byte)HALCAN.LM_PSTAT_VOLTOUT_B1,
			(byte)HALCAN.LM_PSTAT_CURRENT_B0, (byte)HALCAN.LM_PSTAT_CURRENT_B1,
			(byte)HALCAN.LM_PSTAT_TEMP_B0, (byte)HALCAN.LM_PSTAT_TEMP_B1
		    };

            byte[] kMessage1Data = new byte[] {
			(byte)HALCAN.LM_PSTAT_POS_B0, (byte)HALCAN.LM_PSTAT_POS_B1, (byte)HALCAN.LM_PSTAT_POS_B2, (byte)HALCAN.LM_PSTAT_POS_B3,
			(byte)HALCAN.LM_PSTAT_SPD_B0, (byte)HALCAN.LM_PSTAT_SPD_B1, (byte)HALCAN.LM_PSTAT_SPD_B2, (byte)HALCAN.LM_PSTAT_SPD_B3
		};

            byte[] kMessage2Data = new byte[] {
			(byte)HALCAN.LM_PSTAT_LIMIT_CLR,
			(byte)HALCAN.LM_PSTAT_FAULT,
			(byte)HALCAN.LM_PSTAT_END,
			(byte)0,
			(byte)0,
			(byte)0,
			(byte)0,
			(byte)0,
		};

            dataSize = PackINT16(data, (short)(kSendMessagePeriod));
            SendMessage(HALCAN.LM_API_PSTAT_PER_EN_S0, data, dataSize);
            SendMessage(HALCAN.LM_API_PSTAT_PER_EN_S1, data, dataSize);
            SendMessage(HALCAN.LM_API_PSTAT_PER_EN_S2, data, dataSize);

            dataSize = 8;
            SendMessage(HALCAN.LM_API_PSTAT_CFG_S0, kMessage0Data, dataSize);
            SendMessage(HALCAN.LM_API_PSTAT_CFG_S1, kMessage1Data, dataSize);
            SendMessage(HALCAN.LM_API_PSTAT_CFG_S2, kMessage2Data, dataSize);
        }

        protected void UpdatePeriodicStatus()
        {
            byte[] data = new byte[8];
            int dataSize;

            // Check if a new bus voltage/output voltage/current/temperature message
            // has arrived and unpack the values into the cached member variables
            try
            {
                GetMessage(HALCAN.LM_API_PSTAT_DATA_S0, HALCAN.CAN_MSGID_FULL_M, data);

                m_busVoltage = UnpackFXP8_8(new byte[] { data[0], data[1] });
                m_outputVoltage = UnpackPercentage(new byte[] { data[2], data[3] }) * m_busVoltage;
                m_outputCurrent = UnpackFXP8_8(new byte[] { data[4], data[5] });
                m_temperature = UnpackFXP8_8(new byte[] { data[6], data[7] });

                m_receivedStatusMessage0 = true;
            }
            catch (CANMessageNotFoundException e) { }

            // Check if a new position/speed message has arrived and do the same
            try
            {
                GetMessage(HALCAN.LM_API_PSTAT_DATA_S1, HALCAN.CAN_MSGID_FULL_M, data);

                m_position = UnpackFXP16_16(new byte[] { data[0], data[1], data[2], data[3] });
                m_speed = UnpackFXP16_16(new byte[] { data[4], data[5], data[6], data[7] });

                m_receivedStatusMessage1 = true;
            }
            catch (CANMessageNotFoundException e) { }

            // Check if a new limits/faults message has arrived and do the same
            try
            {
                GetMessage(HALCAN.LM_API_PSTAT_DATA_S2, HALCAN.CAN_MSGID_FULL_M, data);
                m_limits = data[0];
                m_faults = data[1];

                m_receivedStatusMessage2 = true;
            }
            catch (CANMessageNotFoundException e) { }
        }

        public static void UpdateSyncGroup(byte syncGroup)
        {
            byte[] data = new byte[8];

            data[0] = syncGroup;

            SendMessageHelper(HALCAN.CAN_MSGID_API_SYNC, data, 1, HALCAN.CAN_SEND_PERIOD_NO_REPEAT);
        }

        private static void Swap16(int x, byte[] buffer) {
		buffer[0] = (byte)(x & 0xff);
		buffer[1] = (byte)((x>>8) & 0xff);
	}

	private static void Swap32(int x, byte[] buffer) {
		buffer[0] = (byte)(x & 0xff);
		buffer[1] = (byte)((x>>8) & 0xff);
		buffer[2] = (byte)((x>>16) & 0xff);
		buffer[3] = (byte)((x>>24) & 0xff);
	}

	private static byte PackPercentage(byte[] buffer, double value) {
		if(value < -1.0) value = -1.0;
		if(value > 1.0) value = 1.0;
		short intValue = (short) (value * 32767.0);
		Swap16(intValue, buffer);
		return 2;
	}

	private static byte PackFXP8_8(byte[] buffer, double value) {
		short intValue = (short) (value * 256.0);
		Swap16(intValue, buffer);
		return 2;
	}

	private static byte PackFXP16_16(byte[] buffer, double value) {
		int intValue = (int) (value * 65536.0);
		Swap32(intValue, buffer);
		return 4;
	}

	private static byte PackINT16(byte[] buffer, short value) {
		Swap16(value, buffer);
		return 2;
	}

	private static byte PackINT32(byte[] buffer, int value) {
		Swap32(value, buffer);
		return 4;
	}

        private static short Unpack16(byte[] buffer, int offset) {
		return (short) ((buffer[offset] & 0xFF) | (short) ((buffer[offset + 1] << 8)) & 0xFF00);
	}

        private static int Unpack32(byte[] buffer, int offset) {
		return (buffer[offset] & 0xFF) | ((buffer[offset + 1] << 8) & 0xFF00) |
			((buffer[offset + 2] << 16) & 0xFF0000) | (int)((buffer[offset + 3] << 24) & 0xFF000000);
	}

	private static double UnpackPercentage(byte[] buffer) {
		return Unpack16(buffer,0) / 32767.0;
	}

	private static double UnpackFXP8_8(byte[] buffer) {
		return Unpack16(buffer,0) / 256.0;
	}

	private static double UnpackFXP16_16(byte[] buffer) {
		return Unpack32(buffer,0) / 65536.0;
	}

	private static short UnpackINT16(byte[] buffer) {
		return Unpack16(buffer,0);
	}

	private static int UnpackINT32(byte[] buffer) {
		return Unpack32(buffer,0);
	}

	/* Compare floats for equality as fixed point numbers */
	public bool FXP8_EQ(double a, double b) {
		return (int)(a * 256.0) == (int)(b * 256.0);
	}

	/* Compare floats for equality as fixed point numbers */
	public bool FXP16_EQ(double a, double b) {
		return (int)(a * 65536.0) == (int)(b * 65536.0);
	}

        public void Set(double speed)
        {
            Set(speed, 0);
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
        public ITable GetTable()
        {
            return m_table;
        }

        public string GetSmartDashboardType()
        {
            return "Speed Controller";
        }

        public void UpdateTable()
        {
            if (m_table != null)
            {
                m_table.PutNumber("Value", Get());
            }
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
    }
}
