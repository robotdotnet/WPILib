using System;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.SimData;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591


namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCANTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            HAL_Base.HALCANTalonSRX.C_TalonSRX_Create = c_TalonSRX_Create;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_Destroy = c_TalonSRX_Destroy;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetParam = c_TalonSRX_SetParam;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_RequestParam = c_TalonSRX_RequestParam;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetParamResponse = c_TalonSRX_GetParamResponse;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetParamResponseInt32 = c_TalonSRX_GetParamResponseInt32;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetStatusFrameRate = c_TalonSRX_SetStatusFrameRate;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_ClearStickyFaults = c_TalonSRX_ClearStickyFaults;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_OverTemp = c_TalonSRX_GetFault_OverTemp;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_UnderVoltage = c_TalonSRX_GetFault_UnderVoltage;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_ForLim = c_TalonSRX_GetFault_ForLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_RevLim = c_TalonSRX_GetFault_RevLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_HardwareFailure = c_TalonSRX_GetFault_HardwareFailure;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_ForSoftLim = c_TalonSRX_GetFault_ForSoftLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFault_RevSoftLim = c_TalonSRX_GetFault_RevSoftLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = c_TalonSRX_GetStckyFault_OverTemp;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = c_TalonSRX_GetStckyFault_UnderVoltage;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_ForLim = c_TalonSRX_GetStckyFault_ForLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_RevLim = c_TalonSRX_GetStckyFault_RevLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = c_TalonSRX_GetStckyFault_ForSoftLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = c_TalonSRX_GetStckyFault_RevSoftLim;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetAppliedThrottle = c_TalonSRX_GetAppliedThrottle;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetCloseLoopErr = c_TalonSRX_GetCloseLoopErr;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = c_TalonSRX_GetFeedbackDeviceSelect;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetModeSelect = c_TalonSRX_GetModeSelect;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetLimitSwitchEn = c_TalonSRX_GetLimitSwitchEn;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = c_TalonSRX_GetLimitSwitchClosedFor;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = c_TalonSRX_GetLimitSwitchClosedRev;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetSensorPosition = c_TalonSRX_GetSensorPosition;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetSensorVelocity = c_TalonSRX_GetSensorVelocity;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetCurrent = c_TalonSRX_GetCurrent;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetBrakeIsEnabled = c_TalonSRX_GetBrakeIsEnabled;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetEncPosition = c_TalonSRX_GetEncPosition;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetEncVel = c_TalonSRX_GetEncVel;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = c_TalonSRX_GetEncIndexRiseEvents;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetQuadApin = c_TalonSRX_GetQuadApin;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetQuadBpin = c_TalonSRX_GetQuadBpin;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetQuadIdxpin = c_TalonSRX_GetQuadIdxpin;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetAnalogInWithOv = c_TalonSRX_GetAnalogInWithOv;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetAnalogInVel = c_TalonSRX_GetAnalogInVel;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetTemp = c_TalonSRX_GetTemp;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetBatteryV = c_TalonSRX_GetBatteryV;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetResetCount = c_TalonSRX_GetResetCount;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetResetFlags = c_TalonSRX_GetResetFlags;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_GetFirmVers = c_TalonSRX_GetFirmVers;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetDemand = c_TalonSRX_SetDemand;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = c_TalonSRX_SetOverrideLimitSwitchEn;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = c_TalonSRX_SetFeedbackDeviceSelect;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = c_TalonSRX_SetRevMotDuringCloseLoopEn;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetOverrideBrakeType = c_TalonSRX_SetOverrideBrakeType;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetModeSelect = c_TalonSRX_SetModeSelect;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetModeSelect2 = c_TalonSRX_SetModeSelect2;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetProfileSlotSelect = c_TalonSRX_SetProfileSlotSelect;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetRampThrottle = c_TalonSRX_SetRampThrottle;
            HAL_Base.HALCANTalonSRX.C_TalonSRX_SetRevFeedbackSensor = c_TalonSRX_SetRevFeedbackSensor;
        }


        [CalledSimFunction]
        public static IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs)
        {
            if (halData["CAN"].ContainsKey(deviceNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceNumber), "Device Already Allocated.");
            }
            halData["CAN"][deviceNumber] = new NotifyDict<dynamic, dynamic>()
            {
                ["type"] = "talonsrx",
                ["value"] = 0,
                ["params"] = new NotifyDict<dynamic, dynamic>()
                {
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_P] = 1.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_I] = 2.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_D] = 3.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_F] = 4.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_IZone] = 5.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate] = 6.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_P] = 11.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_I] = 12.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_D] = 13.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_F] = 14.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_IZone] = 15.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate] = 16.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSoftLimitForThreshold] = 21.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold] = 22.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSoftLimitForEnable] = 23.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileParamSoftLimitRevEnable] = 24.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eOnBoot_BrakeMode] = 31.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed] = 32.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed] = 33.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable] = 34.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable] = 35.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_OverTemp] = 41.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_UnderVoltage] = 42.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_ForLim] = 43.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_RevLim] = 44.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_HardwareFailure] = 45.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_ForSoftLim] = 46.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFault_RevSoftLim] = 47.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_OverTemp] = 48.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_UnderVoltage] = 49.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_ForLim] = 50.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_RevLim] = 51.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_ForSoftLim] = 52.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eStckyFault_RevSoftLim] = 53.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eAppliedThrottle] = 61.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eCloseLoopErr] = 62.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFeedbackDeviceSelect] = 63.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eRevMotDuringCloseLoopEn] = 64.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eModeSelect] = 65.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eProfileSlotSelect] = 66.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eRampThrottle] = 67.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eRevFeedbackSensor] = 68.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eLimitSwitchEn] = 69.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eLimitSwitchClosedFor] = 70.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eLimitSwitchClosedRev] = 71.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eSensorPosition] = 73.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eSensorVelocity] = 74.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eCurrent] = 75.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eBrakeIsEnabled] = 76.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eEncPosition] = 77.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eEncVel] = 78.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eEncIndexRiseEvents] = 79.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eQuadApin] = 80.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eQuadBpin] = 81.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eQuadIdxpin] = 82.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eAnalogInWithOv] = 83.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eAnalogInVel] = 84.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eTemp] = 85.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eBatteryV] = 86.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eResetCount] = 87.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eResetFlags] = 88.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eFirmVers] = 89.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eSettingsChanged] = 90.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.eQuadFilterEn] = 91.0,
                    [(int)HAL_Base.HALCANTalonSRX.ParamID.ePidIaccum] = 93.0,
                },

                ["fault_overtemp"] = 0,
                ["fault_undervoltage"] = 0,
                ["fault_forlim"] = 0,
                ["fault_revlim"] = 0,
                ["fault_hwfailure"] = 0,
                ["fault_forsoftlim"] = 0,
                ["fault_revsoftlim"] = 0,
                ["stickyfault_overtemp"] = 0,
                ["stickyfault_undervoltage"] = 0,
                ["stickyfault_forlim"] = 0,
                ["stickyfault_revlim"] = 0,
                ["stickyfault_forsoftlim"] = 0,
                ["stickyfault_revsoftlim"] = 0,
                ["applied_throttle"] = 0,
                ["closeloop_err"] = 0,
                ["feedback_device_select"] = 0,
                ["mode_select"] = 0,
                ["limit_switch_en"] = 0,
                ["limit_switch_closed_for"] = 0,
                ["limit_switch_closed_rev"] = 0,
                ["sensor_position"] = 0,
                ["sensor_velocity"] = 0,
                ["current"] = 0,
                ["brake_enabled"] = 0,
                ["enc_position"] = 0,
                ["enc_velocity"] = 0,
                ["enc_index_rise_events"] = 0,
                ["quad_apin"] = 0,
                ["quad_bpin"] = 0,
                ["quad_idxpin"] = 0,
                ["analog_in_with_ov"] = 0,
                ["analog_in_vel"] = 0,
                ["temp"] = 0,
                ["battery"] = 0,
                ["reset_count"] = 0,
                ["reset_flags"] = 0,
                ["firmware_version"] = 0,
                ["override_limit_switch"] = 0,

                ["feedback_device"] = null,
                ["rev_motor_during_close_loop"] = null,
                ["override_braketype"] = null,
                ["profile_slot_select"] = null,
                ["ramp_throttle"] = null,
                ["rev_feedback_sensor"] = null
            };


            TalonSRX srx = new TalonSRX { deviceNumber = deviceNumber };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(srx));
            Marshal.StructureToPtr(srx, ptr, true);
            return ptr;
        }

        [CalledSimFunction]
        public static void c_TalonSRX_Destroy(IntPtr handle)
        {
            halData["CAN"].Remove(GetTalonSRX(handle));
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value)
        {
            halData["CAN"][GetTalonSRX(handle)]["params"][paramEnum] = value;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum)
        {
            var param = halData["CAN"][GetTalonSRX(handle)]["params"];

            if (!param.ContainsKey(paramEnum))
            {
                return CTR_Code.CTR_InvalidParamValue;
            }
            return CTR_Code.CTR_OKAY;
        }



        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value)
        {
            var param = halData["CAN"][GetTalonSRX(handle)]["params"];
            if (!param.ContainsKey(paramEnum))
            {
                value = 0;
                return CTR_Code.CTR_InvalidParamValue;
            }
            value = (double)param[paramEnum];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value)
        {
            var param = halData["CAN"][GetTalonSRX(handle)]["params"];
            if (!param.ContainsKey(paramEnum))
            {
                value = 0;
                return CTR_Code.CTR_InvalidParamValue;
            }
            value = (int)param[paramEnum];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs)
        {
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle)
        {
            halData["CAN"][GetTalonSRX(handle)]["sticky_overtemp"] = 0;
            halData["CAN"][GetTalonSRX(handle)]["stickyfault_undervoltage"] = 0;
            halData["CAN"][GetTalonSRX(handle)]["stickyfault_forlim"] = 0;
            halData["CAN"][GetTalonSRX(handle)]["stickyfault_revlim"] = 0;
            halData["CAN"][GetTalonSRX(handle)]["stickyfault_forsoftlim"] = 0;
            halData["CAN"][GetTalonSRX(handle)]["stickyfault_revsoftlim"] = 0;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_overtemp"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_undervoltage"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_forlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_revlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_hwfailure"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_forsoftlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_revsoftlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_overtemp"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_undervoltage"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_forlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_revlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_forsoftlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["stickyfault_revsoftlim"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["applied_throttle"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["closeloop_err"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["feedback_device_select"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["mode_select"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["limit_switch_en"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["limit_switch_closed_for"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["limit_switch_closed_rev"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["sensor_position"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["sensor_velocity"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param)
        {
            param = (double)halData["CAN"][GetTalonSRX(handle)]["current"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["brake_enabled"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["enc_position"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["enc_velocity"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["enc_index_rise_events"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["quad_apin"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["quad_bpin"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["quad_idxpin"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["analog_in_with_ov"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["analog_in_vel"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param)
        {
            param = (double)halData["CAN"][GetTalonSRX(handle)]["temp"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param)
        {
            param = halData["CAN"][GetTalonSRX(handle)]["battery"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["reset_count"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["reset_flags"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["firmware_version"];
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["value"] = (double)param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["override_limit_switch"] = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["feedback_device"] = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["rev_motor_during_close_loop"] = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["override_braketype"] = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["mode_select"] = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand)
        {
            halData["CAN"][GetTalonSRX(handle)]["mode_select"] = modeSelect;
            halData["CAN"][GetTalonSRX(handle)]["value"] = demand;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["profile_slot_select"] = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["ramp_throttle"] = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["rev_feedback_sensor"] = param;
            return CTR_Code.CTR_OKAY;
        }
    }
}
