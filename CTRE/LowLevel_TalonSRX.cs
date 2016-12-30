/**
 * @brief CAN TALON SRX driver.
 *
 * The TALON SRX is designed to instrument all runtime signals periodically.
 * The default periods are chosen to support 16 TALONs with 10ms update rate
 * for control (throttle or setpoint).  However these can be overridden with
 * SetStatusFrameRate. @see SetStatusFrameRate
 * The getters for these unsolicited signals are auto generated at the bottom
 * of this module.
 *
 * Likewise most control signals are sent periodically using the fire-and-forget
 * CAN API.  The setters for these unsolicited signals are auto generated at the
 * bottom of this module.
 *
 * Signals that are not available in an unsolicited fashion are the Close Loop
 * gains.  For teams that have a single profile for their TALON close loop they
 * can use either the webpage to configure their TALONs once or set the PIDF,
 * Izone, CloseLoopRampRate, etc... once in the robot application.  These
 * parameters are saved to flash so once they are loaded in the TALON, they
 * will persist through power cycles and mode changes.
 *
 * For teams that have one or two profiles to switch between, they can use the
 * same strategy since there are two slots to choose from and the
 * ProfileSlotSelect is periodically sent in the 10 ms control frame.
 *
 * For teams that require changing gains frequently, they can use the soliciting
 * API to get and set those parameters.  Most likely they will only need to set
 * them in a periodic fashion as a function of what motion the application is
 * attempting.  If this API is used, be mindful of the CAN utilization reported
 * in the driver station.
 *
 * If calling application has used the config routines to configure the
 * selected feedback sensor, then all positions are measured in floating point
 * precision rotations.  All sensor velocities are specified in floating point
 * precision RPM.
 * @see ConfigPotentiometerTurns
 * @see ConfigEncoderCodesPerRev
 * HOWEVER, if calling application has not called the config routine for
 * selected feedback sensor, then all getters/setters for position/velocity use
 * the native engineering units of the Talon SRX firm (just like in 2015).
 * Signals explained below.
 *
 * Encoder position is measured in encoder edges.  Every edge is counted
 * (similar to roboRIO 4X mode).  Analog position is 10 bits, meaning 1024
 * ticks per rotation (0V => 3.3V).  Use SetFeedbackDeviceSelect to select
 * which sensor type you need.  Once you do that you can use GetSensorPosition()
 * and GetSensorVelocity().  These signals are updated on CANBus every 20ms (by
 * default).  If a relative sensor is selected, you can zero (or change the
 * current value) using SetSensorPosition.
 *
 * Analog Input and quadrature position (and velocity) are also explicitly
 * reported in GetEncPosition, GetEncVel, GetAnalogInWithOv, GetAnalogInVel.
 * These signals are available all the time, regardless of what sensor is
 * selected at a rate of 100ms.  This allows easy instrumentation for "in the
 * pits" checking of all sensors regardless of modeselect.  The 100ms rate is
 * overridable for teams who want to acquire sensor data for processing, not
 * just instrumentation.  Or just select the sensor using
 * SetFeedbackDeviceSelect to get it at 20ms.
 *
 * Velocity is in position ticks / 100ms.
 *
 * All output units are in respect to duty cycle (throttle) which is -1023(full
 * reverse) to +1023 (full forward).  This includes demand (which specifies
 * duty cycle when in duty cycle mode) and rampRamp, which is in throttle units
 * per 10ms (if nonzero).
 *
 * Pos and velocity close loops are calc'd as
 *   err = target - posOrVel.
 *   iErr += err;
 *   if(   (IZone!=0)  and  abs(err) > IZone)
 *       ClearIaccum()
 *   output = P X err + I X iErr + D X dErr + F X target
 *   dErr = err - lastErr
 * P, I, and D gains are always positive. F can be negative.
 * Motor direction can be reversed using SetRevMotDuringCloseLoopEn if
 * sensor and motor are out of phase. Similarly feedback sensor can also be
 * reversed (multiplied by -1) if you prefer the sensor to be inverted.
 *
 * P gain is specified in throttle per error tick.  For example, a value of 102
 * is ~9.9% (which is 102/1023) throttle per 1 ADC unit(10bit) or 1 quadrature
 * encoder edge depending on selected sensor.
 *
 * I gain is specified in throttle per integrated error. For example, a value
 * of 10 equates to ~0.99% (which is 10/1023) for each accumulated ADC unit
 * (10 bit) or 1 quadrature encoder edge depending on selected sensor.
 * Close loop and integral accumulator runs every 1ms.
 *
 * D gain is specified in throttle per derivative error. For example a value of
 * 102 equates to ~9.9% (which is 102/1023) per change of 1 unit (ADC or
 * encoder) per ms.
 *
 * I Zone is specified in the same units as sensor position (ADC units or
 * quadrature edges).  If pos/vel error is outside of this value, the
 * integrated error will auto-clear...
 *   if(   (IZone!=0)  and  abs(err) > IZone)
 *       ClearIaccum()
 * ...this is very useful in preventing integral windup and is highly
 * recommended if using full PID to keep stability low.
 *
 * CloseLoopRampRate is in throttle units per 1ms.  Set to zero to disable
 * ramping.  Works the same as RampThrottle but only is in effect when a close
 * loop mode and profile slot is selected.
 *
 */
using System;
using HAL.Base;

namespace CTRE
{
    using TALON_Control_6_MotProfAddTrajPoint_huff0_t = UInt64;
    using TALON_Control_6_MotProfAddTrajPoint_t = UInt64;

    public class LowLevel_TalonSrx : CANBusDevice, IDisposable
    {
        const UInt32 STATUS_1 = 0x02041400;
        const UInt32 STATUS_2 = 0x02041440;
        const UInt32 STATUS_3 = 0x02041480;
        const UInt32 STATUS_4 = 0x020414C0;
        const UInt32 STATUS_5 = 0x02041500;
        const UInt32 STATUS_6 = 0x02041540;
        const UInt32 STATUS_7 = 0x02041580;
        const UInt32 STATUS_8 = 0x020415C0;
        const UInt32 STATUS_9 = 0x02041600;
        const UInt32 STATUS_10 = 0x02041640;

        const UInt32 CONTROL_1 = 0x02040000;
        const UInt32 CONTROL_2 = 0x02040040;
        const UInt32 CONTROL_3 = 0x02040080;
        const UInt32 CONTROL_5 = 0x02040100;
        const UInt32 CONTROL_6 = 0x02040140;

        const UInt32 PARAM_REQUEST = 0x02041800;
        const UInt32 PARAM_RESPONSE = 0x02041840;
        const UInt32 PARAM_SET = 0x02041880;

        const UInt32 kParamArbIdValue = PARAM_RESPONSE;
        const UInt32 kParamArbIdMask = 0xFFFFFFFF;

        const float FLOAT_TO_FXP_10_22 = (float)0x400000;
        const float FXP_TO_FLOAT_10_22 = 0.0000002384185791015625f;

        const float FLOAT_TO_FXP_0_8 = (float)0x100;
        const float FXP_TO_FLOAT_0_8 = 0.00390625f;

        /* status frame rate types */
        const int kStatusFrame_General = 0;
        const int kStatusFrame_Feedback = 1;
        const int kStatusFrame_Encoder = 2;
        const int kStatusFrame_AnalogTempVbat = 3;
        const int kStatusFrame_PulseWidthMeas = 4;
        const int kStatusFrame_MotionProfile = 5;
        const int kStatusFrame_MotionMagic = 6;
        /* Motion Profile status bits */
        const int kMotionProfileFlag_ActTraj_IsValid = 0x1;
        const int kMotionProfileFlag_HasUnderrun = 0x2;
        const int kMotionProfileFlag_IsUnderrun = 0x4;
        const int kMotionProfileFlag_ActTraj_IsLast = 0x8;
        const int kMotionProfileFlag_ActTraj_VelOnly = 0x10;
        /* Motion Profile Set Output */
        // Motor output is neutral, Motion Profile Executer is not running.
        const int kMotionProf_Disabled = 0;
        // Motor output is updated from Motion Profile Executer, MPE will
        // process the buffered points.
        const int kMotionProf_Enable = 1;
        // Motor output is updated from Motion Profile Executer, MPE will
        // stay processing current trajectory point.
        const int kMotionProf_Hold = 2;

        const int kDefaultControl6PeriodMs = 10;

        /**
         * Signal enumeration for generic signal access.
         * Although every signal is enumerated, only use this for traffic that must
         * be solicited.
         * Use the auto generated getters/setters at bottom of this header as much as
         * possible.
         */
        public enum ParamEnum
        {
            eProfileParamSlot0_P = 1,
            eProfileParamSlot0_I = 2,
            eProfileParamSlot0_D = 3,
            eProfileParamSlot0_F = 4,
            eProfileParamSlot0_IZone = 5,
            eProfileParamSlot0_CloseLoopRampRate = 6,
            eProfileParamSlot1_P = 11,
            eProfileParamSlot1_I = 12,
            eProfileParamSlot1_D = 13,
            eProfileParamSlot1_F = 14,
            eProfileParamSlot1_IZone = 15,
            eProfileParamSlot1_CloseLoopRampRate = 16,
            eProfileParamSoftLimitForThreshold = 21,
            eProfileParamSoftLimitRevThreshold = 22,
            eProfileParamSoftLimitForEnable = 23,
            eProfileParamSoftLimitRevEnable = 24,
            eOnBoot_BrakeMode = 31,
            eOnBoot_LimitSwitch_Forward_NormallyClosed = 32,
            eOnBoot_LimitSwitch_Reverse_NormallyClosed = 33,
            eOnBoot_LimitSwitch_Forward_Disable = 34,
            eOnBoot_LimitSwitch_Reverse_Disable = 35,
            eFault_OverTemp = 41,
            eFault_UnderVoltage = 42,
            eFault_ForLim = 43,
            eFault_RevLim = 44,
            eFault_HardwareFailure = 45,
            eFault_ForSoftLim = 46,
            eFault_RevSoftLim = 47,
            eStckyFault_OverTemp = 48,
            eStckyFault_UnderVoltage = 49,
            eStckyFault_ForLim = 50,
            eStckyFault_RevLim = 51,
            eStckyFault_ForSoftLim = 52,
            eStckyFault_RevSoftLim = 53,
            eAppliedThrottle = 61,
            eCloseLoopErr = 62,
            eFeedbackDeviceSelect = 63,
            eRevMotDuringCloseLoopEn = 64,
            eModeSelect = 65,
            eProfileSlotSelect = 66,
            eRampThrottle = 67,
            eRevFeedbackSensor = 68,
            eLimitSwitchEn = 69,
            eLimitSwitchClosedFor = 70,
            eLimitSwitchClosedRev = 71,
            eSensorPosition = 73,
            eSensorVelocity = 74,
            eCurrent = 75,
            eBrakeIsEnabled = 76,
            eEncPosition = 77,
            eEncVel = 78,
            eEncIndexRiseEvents = 79,
            eQuadApin = 80,
            eQuadBpin = 81,
            eQuadIdxpin = 82,
            eAnalogInWithOv = 83,
            eAnalogInVel = 84,
            eTemp = 85,
            eBatteryV = 86,
            eResetCount = 87,
            eResetFlags = 88,
            eFirmVers = 89,
            eSettingsChanged = 90,
            eQuadFilterEn = 91,
            ePidIaccum = 93,
            eStatus1FrameRate = 94,  // TALON_Status_1_General_10ms_t
            eStatus2FrameRate = 95,  // TALON_Status_2_Feedback_20ms_t
            eStatus3FrameRate = 96,  // TALON_Status_3_Enc_100ms_t
            eStatus4FrameRate = 97,  // TALON_Status_4_AinTempVbat_100ms_t
            eStatus6FrameRate = 98,  // TALON_Status_6_Eol_t
            eStatus7FrameRate = 99,  // TALON_Status_7_Debug_200ms_t
            eClearPositionOnIdx = 100,
            // reserved,
            // reserved,
            // reserved,
            ePeakPosOutput = 104,
            eNominalPosOutput = 105,
            ePeakNegOutput = 106,
            eNominalNegOutput = 107,
            eQuadIdxPolarity = 108,
            eStatus8FrameRate = 109,  // TALON_Status_8_PulseWid_100ms_t
            eAllowPosOverflow = 110,
            eProfileParamSlot0_AllowableClosedLoopErr = 111,
            eNumberPotTurns = 112,
            eNumberEncoderCPR = 113,
            ePwdPosition = 114,
            eAinPosition = 115,
            eProfileParamVcompRate = 116,
            eProfileParamSlot1_AllowableClosedLoopErr = 117,
            eStatus9FrameRate = 118,  // TALON_Status_9_MotProfBuffer_100ms_t
            eMotionProfileHasUnderrunErr = 119,
            eReserved120 = 120,
            eLegacyControlMode = 121,
            eMotMag_Accel = 122,
            eMotMag_VelCruise = 123,
            eStatus10FrameRate = 124, // TALON_Status_10_MotMag_100ms_t 
        };

        private UInt64 _cache;
        private UInt32 _len;
        private UInt32 _can_h = 0;
        private int _can_stat = 0;
        //--------------------- Buffering Motion Profile ---------------------------//

        /**
         * To keep buffers from getting out of control, place a cap on the top level
         * buffer.  Calling application
         * can stream addition points as they are fed to Talon.
         * Approx memory footprint is this capacity X 8 bytes.
         */
        const int kMotionProfileTopBufferCapacity = 512;
        TrajectoryBuffer _motProfTopBuffer = new TrajectoryBuffer(kMotionProfileTopBufferCapacity);
        /**
         * Flow control for streaming trajectories.
         */
        Int32 _motProfFlowControl = -1;

        Object _mutMotProf = new Object();

        private int _controlPeriodMs;
        private int _enablePeriodMs;
        private uint _controlFrameArbId;


        /**
         * Frame Period of the motion profile control6 frame.
         */
        uint _control6PeriodMs = kDefaultControl6PeriodMs;

        private System.Collections.Hashtable _sigs = new System.Collections.Hashtable();

        public LowLevel_TalonSrx(int deviceNumber, int controlPeriodMs, int enablePeriodMs)
            : base((uint)deviceNumber)
        {
            _controlPeriodMs = controlPeriodMs;
            _enablePeriodMs = enablePeriodMs;

            /* bound period to be within [1 ms,95 ms] */
            if (_controlPeriodMs < 1)
                _controlPeriodMs = 1;
            else if (_controlPeriodMs > 95)
                _controlPeriodMs = 95;
            if (_enablePeriodMs < 1)
                _enablePeriodMs = 1;
            else if (_enablePeriodMs > 95)
                _enablePeriodMs = 95;

            RegisterRx(STATUS_1 | (uint)deviceNumber);
            RegisterRx(STATUS_2 | (uint)deviceNumber);
            RegisterRx(STATUS_3 | (uint)deviceNumber);
            RegisterRx(STATUS_4 | (uint)deviceNumber);
            RegisterRx(STATUS_5 | (uint)deviceNumber);
            RegisterRx(STATUS_6 | (uint)deviceNumber);
            RegisterRx(STATUS_7 | (uint)deviceNumber);
            /* use the legacy command frame until we have evidence we can use the new
             * frame.
             */
            RegisterTx(CONTROL_1 | (uint)deviceNumber, (uint)_controlPeriodMs);
            RegisterTx(CONTROL_5 | (uint)deviceNumber, (uint)_enablePeriodMs);
            _controlFrameArbId = CONTROL_1;
            /* the only default param that is nonzero is limit switch.
             * Default to using the flash settings.
             */
            SetOverrideLimitSwitchEn(1);
            /* Check if we can upgrade the control framing */
        }

        public void Dispose()
        {
            RegisterTx(CONTROL_1 | (uint)GetDeviceNumber(), 0);
            RegisterTx(CONTROL_5 | (uint)GetDeviceNumber(), 0);
            /* free the stream we used for SetParam/GetParamResponse */
            if (_can_h != 0)
            {
                HALCAN.FRC_NetworkCommunication_CANSessionMux_closeStreamSession(_can_h);
                _can_h = 0;
            }
        }

        private void OpenSessionIfNeedBe()
        {
            _can_stat = 0;
            if (_can_h == 0)
            {
                /* bit30 - bit8 must match $000002XX.  Top bit is not masked to get remote frames */
                uint arbId = kParamArbIdValue | GetDeviceNumber();
                HALCAN.FRC_NetworkCommunication_CANSessionMux_openStreamSession(ref _can_h, arbId, kParamArbIdMask,
                    20, ref _can_stat);
                if (_can_stat == 0)
                {
                    /* success */
                }
                else
                {
                    /* something went wrong, try again later */
                    _can_h = 0;
                }
            }
        }

        private CANStreamMessage[] _msgBuff = new CANStreamMessage[20];

        private void ProcessStreamMessages()
        {
            if (0 == _can_h) OpenSessionIfNeedBe();
            /* process receive messages */
            UInt32 i;
            uint messagesToRead = 20;
            uint messagesRead = 0;
            /* read out latest bunch of messages */
            _can_stat = 0;
            if (_can_h != 0)
            {
                HALCAN.FRC_NetworkCommunication_CANSessionMux_readStreamSession(_can_h, _msgBuff, messagesToRead,
                    ref messagesRead, ref _can_stat);
            }
            /* loop thru each message of interest */
            for (i = 0; i < messagesRead; ++i)
            {
                CANStreamMessage msg = _msgBuff[i];
                if (msg.messageID == (PARAM_RESPONSE | GetDeviceNumber()))
                {
                    var data = msg.data;
                    byte paramEnum = (byte)(data & 0xFF);
                    data >>= 8;
                    /* save latest signal */
                    _sigs[(uint)paramEnum] = (uint)data;
                }
            }
        }
        public void Set(float value, Int32 controlMode)
        {
            if (value > 1)
                value = 1;
            else if (value < -1)
                value = -1;
            SetDemand24((int)(1023 * value), controlMode); /* must be within [-1023,1023] */
        }
        /*---------------------setters and getters that use the param
         * request/response-------------*/
        /**
         * Send a one shot frame to set an arbitrary signal.
         * Most signals are in the control frame so avoid using this API unless you have
         * to.
         * Use this api for...
         * -A motor controller profile signal eProfileParam_XXXs.  These are backed up
         * in flash.  If you are gain-scheduling then call this periodically.
         * -Default brake and limit switch signals... eOnBoot_XXXs.  Avoid doing this,
         * use the override signals in the control frame.
         * Talon will automatically send a PARAM_RESPONSE after the set, so
         * GetParamResponse will catch the latest value after a couple ms.
         */
        public int SetParamRaw(ParamEnum paramEnum, int rawBits, uint timeoutMs = 0)
        {
            /* caller is using param API.  Open session if it hasn'T been done. */
            if (0 == _can_h) OpenSessionIfNeedBe();
            /* wait for response frame */
            if (timeoutMs != 0)
            {
                /* remove stale entry if caller wants to wait for response. */
                _sigs.Remove((uint)paramEnum);
            }
            /* frame set request and send it */
            UInt64 frame = ((UInt64)rawBits) & 0xFFFFFFFF;
            frame <<= 8;
            frame |= (byte)paramEnum;
            int status = 0;
            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage(
                PARAM_SET | GetDeviceNumber(), ref frame, 5, 0, ref status);
            /* wait for response frame */
            if (timeoutMs > 0)
            {
                int readBits;
                /* loop until timeout or receive if caller wants to check */
                while (timeoutMs > 0)
                {
                    /* wait a bit */
                    System.Threading.Thread.Sleep(1);
                    /* see if response was received */
                    if (0 == GetParamResponseRaw(paramEnum, out readBits))
                        break; /* leave inner loop */
                    /* decrement */
                    --timeoutMs;
                }
                /* if we get here then we timed out */
                if (timeoutMs == 0)
                    status = (int)Codes.CTR_SigNotUpdated;
            }
            return status;
        }
        /**
         * Checks cached CAN frames and updating solicited signals.
         */
        public int GetParamResponseRaw(ParamEnum paramEnum, out Int32 rawBits)
        {
            int retval = 0;
            /* process received param events. We don't expect many since this API is not
             * used often. */
            ProcessStreamMessages();
            /* grab the solicited signal value */
            if (_sigs.Contains((uint)paramEnum) == false)
            {
                retval = (int)Codes.CTR_SigNotUpdated;
                rawBits = 0; /* default value if signal was not received */
            }
            else
            {
                Object value = _sigs[(uint)paramEnum];
                uint temp = (uint)value;
                rawBits = (int)temp;
            }
            return retval;
        }
        /**
         * Asks TALON to immedietely respond with signal value.  This API is only used
         * for signals that are not sent periodically.
         * This can be useful for reading params that rarely change like Limit Switch
         * settings and PIDF values.
          * @param param to request.
         */
        public CTR_Code RequestParam(ParamEnum paramEnum)
        {
            /* process received param events. We don't expect many since this API is not
             * used often. */
            ProcessStreamMessages();
            ulong data = (ulong)paramEnum;
            int status = 0;
            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage(PARAM_REQUEST | GetDeviceNumber(), ref data, 1, 0,
                ref status);
            if (status != 0) return CTR_Code.CTR_TxFailed;
            return CTR_Code.CTR_OKAY;
        }

        public int SetParam(ParamEnum paramEnum, float value, uint timeoutMs = 0)
        {
            Int32 rawbits = 0;
            switch (paramEnum)
            {
                case ParamEnum.eProfileParamSlot0_P: /* unsigned 10.22 fixed pt value */
                case ParamEnum.eProfileParamSlot0_I:
                case ParamEnum.eProfileParamSlot0_D:
                case ParamEnum.eProfileParamSlot1_P:
                case ParamEnum.eProfileParamSlot1_I:
                case ParamEnum.eProfileParamSlot1_D:
                    {
                        UInt32 urawbits;
                        if (value > 1023) /* bounds check doubles that are outside u10.22 */
                            value = 1023;
                        else if (value < 0)
                            value = 0;
                        urawbits = (UInt32)(value * FLOAT_TO_FXP_10_22); /* perform unsign arithmetic */
                        rawbits = (int)urawbits; /* copy bits over.  SetParamRaw just stuffs into CAN frame with no sense of signedness */
                    }
                    break;
                case ParamEnum.eProfileParamSlot1_F: /* signed 10.22 fixed pt value */
                case ParamEnum.eProfileParamSlot0_F:
                    if (value > 512) /* bounds check doubles that are outside s10.22 */
                        value = 512;
                    else if (value < -512)
                        value = -512;
                    rawbits = (Int32)(value * FLOAT_TO_FXP_10_22);
                    break;
                case ParamEnum.eProfileParamVcompRate: /* unsigned 0.8 fixed pt value volts per ms */
                                                       /* within [0,1) volts per ms.
                                                               Slowest ramp is 1/256 VperMilliSec or 3.072 seconds from 0-to-12V.
                                                               Fastest ramp is 255/256 VperMilliSec or 12.1ms from 0-to-12V.
                                                               */
                    if (value <= 0)
                    {
                        /* negative or zero (disable), send raw value of zero */
                        rawbits = 0;
                    }
                    else
                    {
                        /* nonzero ramping */
                        rawbits = (int)(value * FLOAT_TO_FXP_0_8);
                        /* since whole part is cleared, cap to just under whole unit */
                        if (rawbits > (FLOAT_TO_FXP_0_8 - 1))
                            rawbits = (int)(FLOAT_TO_FXP_0_8 - 1);
                        /* since ramping is nonzero, cap to smallest ramp rate possible */
                        if (rawbits == 0)
                        {
                            /* caller is providing a nonzero ramp rate that's too small
                                    to serialize, so cap to smallest possible */
                            rawbits = 1;
                        }
                    }
                    break;
                default: /* everything else is integral */
                    rawbits = (Int32)value;
                    break;
            }
            return SetParamRaw(paramEnum, rawbits, timeoutMs);
        }
        public int GetParamResponse(ParamEnum paramEnum, out float value)
        {
            Int32 rawbits = 0;
            int retval = GetParamResponseRaw(paramEnum, out rawbits);
            switch (paramEnum)
            {
                case ParamEnum.eProfileParamSlot0_P: /* 10.22 fixed pt value */
                case ParamEnum.eProfileParamSlot0_I:
                case ParamEnum.eProfileParamSlot0_D:
                case ParamEnum.eProfileParamSlot0_F:
                case ParamEnum.eProfileParamSlot1_P:
                case ParamEnum.eProfileParamSlot1_I:
                case ParamEnum.eProfileParamSlot1_D:
                case ParamEnum.eProfileParamSlot1_F:
                case ParamEnum.eCurrent:
                case ParamEnum.eTemp:
                case ParamEnum.eBatteryV:
                    value = ((float)rawbits) * FXP_TO_FLOAT_10_22;
                    break;
                case ParamEnum.eProfileParamVcompRate:
                    value = ((float)rawbits) * FXP_TO_FLOAT_0_8;
                    break;
                default: /* everything else is integral */
                    value = (float)rawbits;
                    break;
            }
            return retval;
        }
        public Int32 GetParamResponseInt32(ParamEnum paramEnum, out int value)
        {
            float dvalue = 0;
            int retval = GetParamResponse(paramEnum, out dvalue);
            value = (Int32)dvalue;
            return retval;
        }
        /*----- getters and setters that use param request/response. These signals are backed up in flash and will survive a power cycle. ---------*/
        /*----- If your application requires changing these values consider using both slots and switch between slot0 <=> slot1. ------------------*/
        /*----- If your application requires changing these signals frequently then it makes sense to leverage this API. --------------------------*/
        /*----- Getters don't block, so it may require several calls to get the latest value. --------------------------*/
        public int SetPgain(UInt32 slotIdx, float gain, uint timeoutMs = 0)
        {
            if (slotIdx == 0) return SetParam(ParamEnum.eProfileParamSlot0_P, gain, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_P, gain, timeoutMs);
        }
        public int SetIgain(UInt32 slotIdx, float gain, uint timeoutMs = 0)
        {
            if (slotIdx == 0) return SetParam(ParamEnum.eProfileParamSlot0_I, gain, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_I, gain, timeoutMs);
        }
        public int SetDgain(UInt32 slotIdx, float gain, uint timeoutMs = 0)
        {
            if (slotIdx == 0) return SetParam(ParamEnum.eProfileParamSlot0_D, gain, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_D, gain, timeoutMs);
        }
        public int SetFgain(UInt32 slotIdx, float gain, uint timeoutMs = 0)
        {
            if (slotIdx == 0) return SetParam(ParamEnum.eProfileParamSlot0_F, gain, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_F, gain, timeoutMs);
        }
        public int SetIzone(UInt32 slotIdx, uint zone, uint timeoutMs = 0)
        {
            if (slotIdx == 0) return SetParam(ParamEnum.eProfileParamSlot0_IZone, zone, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_IZone, zone, timeoutMs);
        }
        public int SetCloseLoopRampRate(UInt32 slotIdx, int closeLoopRampRate, uint timeoutMs = 0)
        {
            if (slotIdx == 0)
                return SetParam(ParamEnum.eProfileParamSlot0_CloseLoopRampRate, closeLoopRampRate, timeoutMs);
            return SetParam(ParamEnum.eProfileParamSlot1_CloseLoopRampRate, closeLoopRampRate, timeoutMs);
        }
        public int SetVoltageCompensationRate(float voltagePerMs, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eProfileParamVcompRate, voltagePerMs, timeoutMs);
        }
        public int GetPgain(UInt32 slotIdx, out float gain)
        {
            if (slotIdx == 0) return GetParamResponse(ParamEnum.eProfileParamSlot0_P, out gain);
            return GetParamResponse(ParamEnum.eProfileParamSlot1_P, out gain);
        }
        public int GetIgain(UInt32 slotIdx, out float gain)
        {
            if (slotIdx == 0) return GetParamResponse(ParamEnum.eProfileParamSlot0_I, out gain);
            return GetParamResponse(ParamEnum.eProfileParamSlot1_I, out gain);
        }
        public int GetDgain(UInt32 slotIdx, out float gain)
        {
            if (slotIdx == 0) return GetParamResponse(ParamEnum.eProfileParamSlot0_D, out gain);
            return GetParamResponse(ParamEnum.eProfileParamSlot1_D, out gain);
        }
        public int GetFgain(UInt32 slotIdx, out float gain)
        {
            if (slotIdx == 0) return GetParamResponse(ParamEnum.eProfileParamSlot0_F, out gain);
            return GetParamResponse(ParamEnum.eProfileParamSlot1_F, out gain);
        }
        public int GetIzone(UInt32 slotIdx, out int zone)
        {
            if (slotIdx == 0)
                return GetParamResponseInt32(ParamEnum.eProfileParamSlot0_IZone, out zone);
            return GetParamResponseInt32(ParamEnum.eProfileParamSlot1_IZone, out zone);
        }
        public int GetCloseLoopRampRate(UInt32 slotIdx, out int closeLoopRampRate)
        {
            if (slotIdx == 0)
                return GetParamResponseInt32(ParamEnum.eProfileParamSlot0_CloseLoopRampRate, out closeLoopRampRate);
            return GetParamResponseInt32(ParamEnum.eProfileParamSlot1_CloseLoopRampRate, out closeLoopRampRate);
        }
        public int GetVoltageCompensationRate(out float voltagePerMs)
        {
            return GetParamResponse(ParamEnum.eProfileParamVcompRate, out voltagePerMs);
        }
        public int SetSensorPosition(int pos, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eSensorPosition, pos, timeoutMs);
        }
        public int SetForwardSoftLimit(int forwardLimit, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eProfileParamSoftLimitForThreshold, forwardLimit, timeoutMs);
        }
        public int SetReverseSoftLimit(int reverseLimit, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eProfileParamSoftLimitRevThreshold, reverseLimit, timeoutMs);
        }
        public int SetForwardSoftEnable(int enable, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eProfileParamSoftLimitForEnable, enable, timeoutMs);
        }
        public int SetReverseSoftEnable(int enable, uint timeoutMs = 0)
        {
            return SetParam(ParamEnum.eProfileParamSoftLimitRevEnable, enable, timeoutMs);
        }
        public int GetForwardSoftLimit(out int forwardLimit)
        {
            return GetParamResponseInt32(ParamEnum.eProfileParamSoftLimitForThreshold, out forwardLimit);
        }
        public int GetReverseSoftLimit(out int reverseLimit)
        {
            return GetParamResponseInt32(ParamEnum.eProfileParamSoftLimitRevThreshold, out reverseLimit);
        }
        public int GetForwardSoftEnable(out int enable)
        {
            return GetParamResponseInt32(ParamEnum.eProfileParamSoftLimitForEnable, out enable);
        }
        public int GetReverseSoftEnable(out int enable)
        {
            return GetParamResponseInt32(ParamEnum.eProfileParamSoftLimitRevEnable, out enable);
        }
        /**
         * @param param [out] Rise to fall time period in microseconds.
         */
        public int GetPulseWidthRiseToFallUs(out int param)
        {
            const int BIT12 = 1 << 12;
            int temp = 0;
            int periodUs = 0;
            /* first grab our 12.12 position */
            int retval1 = GetPulseWidthPosition(out temp);
            /* mask off number of turns */
            temp &= 0xFFF;
            /* next grab the waveform period. This value
             * will be zero if we stop getting pulses **/
            int retval2 = GetPulseWidthRiseToRiseUs(out periodUs);
            /* now we have 0.12 position that is scaled to the waveform period.
                    Use fixed pt multiply to scale our 0.16 period into us.*/
            param = (temp * periodUs) / BIT12;
            /* pass the worst error code to caller.
                    Assume largest value is the most pressing error code.*/
            if (retval1 > retval2)
                return retval1;
            return retval2;
        }
        public int IsPulseWidthSensorPresent(out int param)
        {
            int periodUs = 0;
            int retval = GetPulseWidthRiseToRiseUs(out periodUs);
            /* if a nonzero period is present, we are getting good pules.
                    Otherwise the sensor is not present. */
            if (periodUs != 0)
                param = 1;
            else
                param = 0;
            return retval;
        }

        /**
         * Change the periodMs of a TALON's status frame.  See kStatusFrame_* enums for
         * what's available.
         */
        public int SetStatusFrameRate(UInt32 frameEnum, UInt32 periodMs, uint timeoutMs = 0)
        {
            Codes retval = Codes.CAN_OK;
            ParamEnum paramEnum = 0;
            /* bounds check the period */
            if (periodMs < 1)
                periodMs = 1;
            else if (periodMs > 255)
                periodMs = 255;
            byte period = (byte)periodMs;
            /* lookup the correct param enum based on what frame to rate-change */
            switch (frameEnum)
            {
                case kStatusFrame_General:
                    paramEnum = ParamEnum.eStatus1FrameRate;
                    break;
                case kStatusFrame_Feedback:
                    paramEnum = ParamEnum.eStatus2FrameRate;
                    break;
                case kStatusFrame_Encoder:
                    paramEnum = ParamEnum.eStatus3FrameRate;
                    break;
                case kStatusFrame_AnalogTempVbat:
                    paramEnum = ParamEnum.eStatus4FrameRate;
                    break;
                case kStatusFrame_PulseWidthMeas:
                    paramEnum = ParamEnum.eStatus8FrameRate;
                    break;
                case kStatusFrame_MotionProfile:
                    paramEnum = ParamEnum.eStatus9FrameRate;
                    break;
                case kStatusFrame_MotionMagic:
                    paramEnum = ParamEnum.eStatus10FrameRate;
                    break;
                default:
                    /* caller's request is not support, return an error code */
                    retval = Codes.CAN_INVALID_PARAM;
                    break;
            }
            /* if lookup was succesful, send set-request out */
            if (retval == Codes.CAN_OK)
            {
                /* paramEnum is updated, sent it out */
                retval = (Codes)SetParamRaw(paramEnum, period, timeoutMs);
            }
            return (int)retval;
        }
        /**
         * Clear all sticky faults in TALON.
         */
        public CTR_Code ClearStickyFaults()
        {
            ulong bit1 = 0x2;
            ulong data = (ulong)bit1;
            int status = 0;
            HALCAN.FRC_NetworkCommunication_CANSessionMux_sendMessage(CONTROL_3 | GetDeviceNumber(), ref data, 1, 0,
                ref status);
            if (status != 0) return CTR_Code.CTR_TxFailed;
            return CTR_Code.CTR_OKAY;
        }
        /**
            * @return the tx task that transmits Control6 (motion profile control).
            *         If it's not scheduled, then schedule it.  This is part of firing
            *         the MotionProf framing only when needed to save bandwidth.
            */
        private UInt64 GetControl6()
        {
            txTask control6 = GetTx(CONTROL_6 | _deviceNumber);
            if (control6.IsEmpty())
            {
                // Control6 never started, arm it now
                RegisterTx(CONTROL_6 | _deviceNumber, _control6PeriodMs);
                control6 = GetTx(CONTROL_6 | _deviceNumber);

            }
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_6 | _deviceNumber, ref _cache);
            if (retval != 0)
            {
                /* control6 never started, arm it now */
                _cache = 0;
                CTRE.Native.CAN.Send(CONTROL_6 | _deviceNumber, _cache, 8, _control6PeriodMs);
                /* sync flow control */
                _motProfFlowControl = 0;
            }
            return _cache;
        }
        /**
         * Calling application can opt to speed up the handshaking between the robot API
         * and the Talon to increase the download rate of the Talon's Motion Profile.
         * Ideally the period should be no more than half the period of a trajectory
         * point.
         */
        public void ChangeMotionControlFramePeriod(UInt32 periodMs)
        {
            lock (_mutMotProf)
            {
                /* if message is already registered, it will get updated.
                 * Otherwise it will error if it hasn't been setup yet, but that's ok
                 * because the _control6PeriodMs will be used later.
                 * @see GetControl6
                 */
                _control6PeriodMs = periodMs;
                /* apply the change if frame is transmitting */
                int stat = CTRE.Native.CAN.GetSendBuffer(CONTROL_6 | _deviceNumber, ref _cache);
                if (stat == 0)
                {
                    /* control6 already started, change frame rate */
                    CTRE.Native.CAN.Send(CONTROL_6 | _deviceNumber, _cache, 8, _control6PeriodMs);
                }
            }
        }
        /**
         * Clear the buffered motion profile in both Talon RAM (bottom), and in the API
         * (top).
         */
        public void ClearMotionProfileTrajectories()
        {
            lock (_mutMotProf)
            {
                /* clear the top buffer */
                _motProfTopBuffer.Clear();
                /* send signal to clear bottom buffer */
                UInt64 frame = GetControl6();
                frame &= 0xFFFFFFFFFFFFF0FF; /* clear Idx */
                _motProfFlowControl = 0; /* match the transmitted flow control */
                CTRE.Native.CAN.Send(CONTROL_6 | _deviceNumber, frame, 8, 0xFFFFFFFF);
            }
        }
        /**
         * Retrieve just the buffer count for the api-level (top) buffer.
         * This routine performs no CAN or data structure lookups, so its fast and ideal
         * if caller needs to quickly poll the progress of trajectory points being
         * emptied into Talon's RAM. Otherwise just use GetMotionProfileStatus.
         * @return number of trajectory points in the top buffer.
         */
        public UInt32 GetMotionProfileTopLevelBufferCount()
        {
            lock (_mutMotProf)
            {
                UInt32 retval = (UInt32)_motProfTopBuffer.GetNumTrajectories();
                return retval;
            }
        }
        /**
         * Retrieve just the buffer full for the api-level (top) buffer.
         * This routine performs no CAN or data structure lookups, so its fast and ideal
         * if caller needs to quickly poll. Otherwise just use GetMotionProfileStatus.
         * @return number of trajectory points in the top buffer.
         */
        public bool IsMotionProfileTopLevelBufferFull()
        {
            lock (_mutMotProf)
            {
                if (_motProfTopBuffer.GetNumTrajectories() >= kMotionProfileTopBufferCapacity)
                    return true;
                return false;
            }
        }
        /**
         * Push another trajectory point into the top level buffer (which is emptied
         * into the Talon's bottom buffer as room allows).
         * @param targPos  servo position in native Talon units (sensor units).
         * @param targVel  velocity to feed-forward in native Talon units (sensor units
         *                 per 100ms).
         * @param profileSlotSelect  which slot to pull PIDF gains from.  Currently
         *                           supports 0 or 1.
         * @param timeDurMs  time in milliseconds of how long to apply this point.
         * @param velOnly  set to nonzero to signal Talon that only the feed-foward
         *                 velocity should be used, i.e. do not perform PID on position.
         *                 This is equivalent to setting PID gains to zero, but much
         *                 more efficient and synchronized to MP.
         * @param isLastPoint  set to nonzero to signal Talon to keep processing this
         *                     trajectory point, instead of jumping to the next one
         *                     when timeDurMs expires.  Otherwise MP executer will
         *                     eventually see an empty buffer after the last point
         *                     expires, causing it to assert the IsUnderRun flag.
         *                     However this may be desired if calling application
         *                     never wants to terminate the MP.
         * @param zeroPos  set to nonzero to signal Talon to "zero" the selected
         *                 position sensor before executing this trajectory point.
         *                 Typically the first point should have this set only thus
         *                 allowing the remainder of the MP positions to be relative to
         *                 zero.
         * @return CTR_OKAY if trajectory point push ok. CTR_BufferFull if buffer is
         *         full due to kMotionProfileTopBufferCapacity.
         */
        public int PushMotionProfileTrajectory(int targPos,
                                                int targVel,
                                                int profileSlotSelect,
                                                int timeDurMs, int velOnly,
                                                int isLastPoint,
                                                int zeroPos)
        {
            ReactToMotionProfileCall();
            /* create our trajectory point */
            byte b0 = 0;
            byte b1 = 0;
            if (zeroPos != 0)
                b0 |= 0x40;
            if (velOnly != 0)
                b0 |= 0x04;
            if (isLastPoint != 0)
                b0 |= 0x08;
            if (profileSlotSelect != 0)
                b0 |= 0x80;

            if (timeDurMs < 0)
                timeDurMs = 0;
            else if (timeDurMs > 255)
                timeDurMs = 255;

            byte b2 = (byte)(timeDurMs);
            byte b3 = (byte)(targVel >> 0x08);
            byte b4 = (byte)(targVel & 0xFF);
            byte b5 = (byte)(targPos >> 0x10);
            byte b6 = (byte)(targPos >> 0x08);
            byte b7 = (byte)(targPos & 0xFF);

            TALON_Control_6_MotProfAddTrajPoint_huff0_t traj = 0;
            traj |= b7;
            traj <<= 8;
            traj |= b6;
            traj <<= 8;
            traj |= b5;
            traj <<= 8;
            traj |= b4;
            traj <<= 8;
            traj |= b3;
            traj <<= 8;
            traj |= b2;
            traj <<= 8;
            traj |= b1;
            traj <<= 8;
            traj |= b0;

            lock (_mutMotProf)
            {
                if (_motProfTopBuffer.GetNumTrajectories() >= kMotionProfileTopBufferCapacity)
                    return (int)Codes.CAN_OVERFLOW;
                _motProfTopBuffer.Push(traj);
            }
            return (int)Codes.CAN_OK;
        }
        /**
         * Increment our flow control to manage streaming to the Talon.
         * f(x) = { 1,   x = 15,
         *          x+1,  x < 15
         *        }
         */
        private int MotionProf_IncrementSync(int idx)
        {
            return ((idx >= 15) ? 1 : 0) + ((idx + 1) & 0xF);
        }
        /**
         * Update the NextPt signals inside the control frame given the next pt to send.
         * @param control pointer to the CAN frame payload containing control6.  Only
         * the signals that serialize the next trajectory point are updated from the
         * contents of newPt.
         * @param newPt point to the next trajectory that needs to be inserted into
         *        Talon RAM.
         */
        private void CopyTrajPtIntoControl(ref TALON_Control_6_MotProfAddTrajPoint_t control, TALON_Control_6_MotProfAddTrajPoint_t newPt)
        {
            /* Bring over the common signals in the first two bytes:  
                NextPt_ProfileSlotSelect,
                NextPt_ZeroPosition,
                NextPt_VelOnly,
                NextPt_IsLast,
                huffCode
                */
            /* the last six bytes are entirely for hold NextPt's values. */
            control &= 0x0000000000000F30;
            control |= 0xFFFFFFFFFFFFF0CF & newPt;
        }
        /**
         * Caller is either pushing a new motion profile point, or is
         * calling the Process buffer routine.  In either case check our
         * flow control to see if we need to start sending control6.
         */
        private void ReactToMotionProfileCall()
        {
            if (_motProfFlowControl < 0)
            {
                /* we have not yet armed the periodic frame.  We do this lazilly to
                 * save bus utilization since most Talons on the bus probably are not
                 * MP'ing.
                 */
                ClearMotionProfileTrajectories(); /* this moves flow control so only fires
                                         once if ever */
            }
        }
        /**
         * This must be called periodically to funnel the trajectory points from the
         * API's top level buffer to the Talon's bottom level buffer.  Recommendation
         * is to call this twice as fast as the executation rate of the motion profile.
         * So if MP is running with 20ms trajectory points, try calling this routine
         * every 10ms.  All motion profile functions are thread-safe through the use of
         * a mutex, so there is no harm in having the caller utilize threading.
         */
        public void ProcessMotionProfileBuffer()
        {
            ReactToMotionProfileCall();
            /* get the latest status frame */
            int retval = CTRE.Native.CAN.Receive(STATUS_9 | _deviceNumber, ref _cache, ref _len);
            /* lock */
            lock (_mutMotProf)
            {
                int NextID = (int)((_cache >> 0x8) & 0xF);
                /* calc what we expect to receive */
                if (_motProfFlowControl == NextID)
                {
                    /* Talon has completed the last req */
                    if (_motProfTopBuffer.IsEmpty())
                    {
                        /* nothing to do */
                    }
                    else
                    {
                        /* get the latest control frame */
                        UInt64 toFill = GetControl6();
                        UInt64 front = _motProfTopBuffer.Front();
                        CopyTrajPtIntoControl(ref toFill, front);
                        _motProfTopBuffer.Pop();
                        _motProfFlowControl = MotionProf_IncrementSync(_motProfFlowControl);
                        /* insert latest flow control */
                        ulong val = (ulong)_motProfFlowControl;
                        val &= 0xF;
                        val <<= 8;
                        toFill &= 0xFFFFFFFFFFFFF0FF;
                        toFill |= val;
                        CTRE.Native.CAN.Send(CONTROL_6 | _deviceNumber, toFill, 8, 0xFFFFFFFF);
                    }
                }
                else
                {
                    /* still waiting on Talon */
                }
            }
        }
        /**
         * Retrieve all status information.
         * Since this all comes from one CAN frame, its ideal to have one routine to
         * retrieve the frame once and decode everything.
         * @param [out] flags  bitfield for status bools. Starting with least
         *        significant bit: IsValid, HasUnderrun, IsUnderrun, IsLast, VelOnly.
         *
         *        IsValid  set when MP executer is processing a trajectory point,
         *                 and that point's status is instrumented with IsLast,
         *                 VelOnly, targPos, targVel.  However if MP executor is
         *                 not processing a trajectory point, then this flag is
         *                 false, and the instrumented signals will be zero.
         *        HasUnderrun  is set anytime the MP executer is ready to pop
         *                     another trajectory point from the Talon's RAM,
         *                     but the buffer is empty.  It can only be cleared
         *                     by using SetParam(eMotionProfileHasUnderrunErr,0);
         *        IsUnderrun  is set when the MP executer is ready for another
         *                    point, but the buffer is empty, and cleared when
         *                    the MP executer does not need another point.
         *                    HasUnderrun shadows this registor when this
         *                    register gets set, however HasUnderrun stays
         *                    asserted until application has process it, and
         *                    IsUnderrun auto-clears when the condition is
         *                    resolved.
         *        IsLast  is set/cleared based on the MP executer's current
         *                trajectory point's IsLast value.  This assumes
         *                IsLast was set when PushMotionProfileTrajectory
         *                was used to insert the currently processed trajectory
         *                point.
         *        VelOnly  is set/cleared based on the MP executer's current
         *                 trajectory point's VelOnly value.
         *
         * @param [out] profileSlotSelect  The currently processed trajectory point's
         *        selected slot.  This can differ in the currently selected slot used
         *        for Position and Velocity servo modes.
         * @param [out] targPos The currently processed trajectory point's position
         *        in native units.  This param is zero if IsValid is zero.
         * @param [out] targVel The currently processed trajectory point's velocity
         *        in native units.  This param is zero if IsValid is zero.
         * @param [out] topBufferRem The remaining number of points in the top level
         *        buffer.
         * @param [out] topBufferCnt The number of points in the top level buffer to
         *        be sent to Talon.
         * @param [out] btmBufferCnt The number of points in the bottom level buffer
         *        inside Talon.
         * @return CTR error code
         */
        public int GetMotionProfileStatus(out UInt32 flags, out UInt32 profileSlotSelect, out Int32 targPos,
                                out Int32 targVel, out UInt32 topBufferRem, out UInt32 topBufferCnt,
                                out UInt32 btmBufferCnt, out UInt32 outputEnable)
        {
            /* get the latest status frame */
            int retval = CTRE.Native.CAN.Receive(STATUS_9 | _deviceNumber, ref _cache, ref _len);

            /* clear signals in case we never received an update, caller should check return */
            flags = 0;
            profileSlotSelect = 0;
            targPos = 0;
            targVel = 0;
            btmBufferCnt = 0;

            /* these signals are always available */
            topBufferCnt = _motProfTopBuffer.GetNumTrajectories();
            topBufferRem = kMotionProfileTopBufferCapacity - _motProfTopBuffer.GetNumTrajectories();

            /* TODO: make enums or make a better method prototype */
            if ((_cache & 0x01) > 0) flags |= kMotionProfileFlag_ActTraj_IsValid;
            if ((_cache & 0x40) > 0) flags |= kMotionProfileFlag_HasUnderrun;
            if ((_cache & 0x80) > 0) flags |= kMotionProfileFlag_IsUnderrun;
            if ((_cache & 0x08) > 0) flags |= kMotionProfileFlag_ActTraj_IsLast;
            if ((_cache & 0x04) > 0) flags |= kMotionProfileFlag_ActTraj_VelOnly;

            btmBufferCnt = (byte)(_cache >> 0x10);

            targVel = (byte)(_cache >> 0x18);
            targVel <<= 8;
            targVel |= (byte)(_cache >> 0x20);

            targPos = (byte)(_cache >> 0x28);
            targPos <<= 8;
            targPos |= (byte)(_cache >> 0x30);
            targPos <<= 8;
            targPos |= (byte)(_cache >> 0x38);

            if ((_cache & 0x02) > 0)
                profileSlotSelect = 1;
            else
                profileSlotSelect = 0;
            /* decode output enable */
            outputEnable = (uint)((_cache >> 4) & 0x3);
            switch (outputEnable)
            {
                case kMotionProf_Disabled:
                case kMotionProf_Enable:
                case kMotionProf_Hold:
                    break;
                default:
                    /* do now allow invalid values for sake of user-facing enum types */
                    outputEnable = kMotionProf_Disabled;
                    break;
            }
            return retval;
        }
        //------------------------ auto generated ------------------------------------//
        /* This API is optimal since it uses the fire-and-forget CAN interface.
         * These signals should cover the majority of all use cases.
         */
        public int GetFault_OverTemp(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 52);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_UnderVoltage(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 51);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_ForLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 50);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_RevLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 49);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_HardwareFailure(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 48);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_ForSoftLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 28);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetFault_RevSoftLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 27);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_OverTemp(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 53);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_UnderVoltage(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 52);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_ForLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 51);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_RevLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 50);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_ForSoftLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 49);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetStckyFault_RevSoftLim(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 48);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetAppliedThrottle(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 24);
            byte L = (byte)(_cache >> 32);
            H &= 0x7;
            L &= 0xff;
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 11);
            raw >>= (32 - 11);
            param = (int)raw;
            return retval;
        }
        public int GetCloseLoopErr(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte M = (byte)(_cache >> 8);
            byte L = (byte)(_cache >> 16);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetFeedbackDeviceSelect(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 40);
            byte L = (byte)(_cache >> 48);
            H &= 0x1f;
            L &= 0x0;
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw >>= 9;
            param = (int)raw;
            return retval;
        }
        public int GetModeSelect(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 40);
            byte L = (byte)(_cache >> 48);
            H &= 0x1;
            L &= 0xe0;
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw >>= 5;
            param = (int)raw;
            return retval;
        }
        public int GetLimitSwitchEn(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 40);
            byte L = (byte)(_cache >> 48);
            H &= 0xff;
            L &= 0x0;
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw >>= 13;
            param = (int)raw;
            return retval;
        }
        public int GetLimitSwitchClosedFor(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 31);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetLimitSwitchClosedRev(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_1 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 30);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetSensorPosition(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte M = (byte)(_cache >> 8);
            byte L = (byte)(_cache >> 16);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetSensorVelocity(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 24);
            byte L = (byte)(_cache >> 32);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetCurrent(out float param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 40);
            byte L = (byte)(_cache >> 48);
            H &= 0xff;
            L &= 0xc0;
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw >>= 6;
            param = 0.125F * raw + 0F;
            return retval;
        }
        /**
		 * @param param set to nonzero if brake is enabled.
		 */
        public int GetBrakeIsEnabled(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_2 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 63);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetEncPosition(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte M = (byte)(_cache >> 8);
            byte L = (byte)(_cache >> 16);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetEncVel(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 24);
            byte L = (byte)(_cache >> 32);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetEncIndexRiseEvents(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 40);
            byte L = (byte)(_cache >> 48);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetQuadApin(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 63);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetQuadBpin(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 62);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetQuadIdxpin(out bool param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_3 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 61);
            param = (L & 1) > 0;
            return retval;
        }
        public int GetAnalogInWithOv(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_4 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte M = (byte)(_cache >> 8);
            byte L = (byte)(_cache >> 16);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetAnalogInVel(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_4 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 24);
            byte L = (byte)(_cache >> 32);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetTemp(out float param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_4 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 40);
            Int32 raw = 0;
            raw |= L;
            param = 0.645161290322581F * raw + -50F;
            return retval;
        }
        public int GetBatteryV(out float param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_4 | _deviceNumber, ref _cache, ref _len);
            byte L = (byte)(_cache >> 48);
            Int32 raw = 0;
            raw |= L;
            param = 0.05F * raw + 4F;
            return retval;
        }
        public int GetResetCount(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_5 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte L = (byte)(_cache >> 8);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            param = (int)raw;
            return retval;
        }
        public int GetResetFlags(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_5 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 16);
            byte L = (byte)(_cache >> 24);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            param = (int)raw;
            return retval;
        }
        public int GetPulseWidthPosition(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_8 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0);
            byte M = (byte)(_cache >> 8);
            byte L = (byte)(_cache >> 16);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetPulseWidthVelocity(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_8 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 48);
            byte L = (byte)(_cache >> 56);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetPulseWidthRiseToRiseUs(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_8 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 32);
            byte L = (byte)(_cache >> 40);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        /**
		 * @param param holds the version of the Talon.  Talon must be powered cycled at least once.
		 */
        public int GetFirmVers(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_5 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 32);
            byte L = (byte)(_cache >> 40);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        /** @param control mode specific output */
        public int SetDemand24(Int32 param, Int32 controlmode)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            byte H = (byte)(param >> 0x10);
            byte M = (byte)(param >> 0x08);
            byte L = (byte)(param);
            /* demand */
            _cache &= ~(0xFFul << 16);
            _cache &= ~(0xFFul << 24);
            _cache &= ~(0xFFul << 32);
            _cache |= (UInt64)(H) << 16;
            _cache |= (UInt64)(M) << 24;
            _cache |= (UInt64)(L) << 32;
            /* control mode */
            controlmode = controlmode & 0xf;
            _cache &= ~(((UInt64)0xf) << 52);
            _cache |= ((UInt64)controlmode) << 52;

            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetDemand(Int32 param, Int32 controlmode)
        {
            return SetDemand24(param, controlmode);
        }
        /** SetOverrideLimitSwitchEn */
        public int SetOverrideLimitSwitchEn(Int32 param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            param = param & 0x7;
            _cache &= ~(((UInt64)0x7) << 45);
            _cache |= ((UInt64)param) << 45;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetFeedbackDeviceSelect(Int32 param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            param = param & 0xf;
            _cache &= ~(((UInt64)0xf) << 41);
            _cache |= ((UInt64)param) << 41;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetRevMotDuringCloseLoopEn(bool param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            if (param == false)
                _cache &= ~(1ul << 49);
            else
                _cache |= 1ul << 49;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetOverrideBrakeType(Int32 param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            param = param & 0x3;
            _cache &= ~(((UInt64)0x3) << 50);
            _cache |= ((UInt64)param) << 50;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetModeSelect(Int32 param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            param = param & 0xf;
            _cache &= ~(((UInt64)0xf) << 52);
            _cache |= ((UInt64)param) << 52;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetProfileSlotSelect(uint param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            if (param == 0)
                _cache &= ~(1ul << 40);
            else
                _cache |= 1ul << 40;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetRampThrottle(Int32 param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            byte L = (byte)(param);
            _cache &= ~((UInt64)0xFFul << 56);
            _cache |= ((UInt64)L << 56);
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        public int SetRevFeedbackSensor(bool param)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            if (param == false)
                _cache &= ~(1ul << 48);
            else
                _cache |= 1ul << 48;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }
        /**
         * Set the throttle to feedforward. This takes effect during..
         * Closed-loop modes only (and MP).
         */
        public int SetThrottleBump(Int32 throttleBump)
        {
            int retval = CTRE.Native.CAN.GetSendBuffer(CONTROL_5 | _deviceNumber, ref _cache);
            if (retval != 0)
                return retval;
            byte H3 = (byte)((throttleBump >> 0x08) & 7);    /* bits [10:8] */
            byte L = (byte)(throttleBump);                  /* bits [7:0] */
            _cache &= ~(0x07ul);
            _cache &= ~(0xFFul << 8);
            _cache |= (UInt64)(H3);
            _cache |= (UInt64)(L) << 8;
            CTRE.Native.CAN.Send(CONTROL_5 | _deviceNumber, _cache, 8, 0xFFFFFFFF);
            return retval;
        }

        public int GetMotionMagic_ActiveTrajVelocity(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_10 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0x0);
            byte L = (byte)(_cache >> 0x8);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 16); /* sign extend */
            raw >>= (32 - 16); /* sign extend */
            param = (int)raw;
            return retval;
        }
        public int GetMotionMagic_ActiveTrajPosition(out Int32 param)
        {
            int retval = CTRE.Native.CAN.Receive(STATUS_10 | _deviceNumber, ref _cache, ref _len);
            byte H = (byte)(_cache >> 0x10);
            byte M = (byte)(_cache >> 0x18);
            byte L = (byte)(_cache >> 0x20);
            Int32 raw = 0;
            raw |= H;
            raw <<= 8;
            raw |= M;
            raw <<= 8;
            raw |= L;
            raw <<= (32 - 24); /* sign extend */
            raw >>= (32 - 24); /* sign extend */
            param = (int)raw;
            return retval;
        }
    }
}