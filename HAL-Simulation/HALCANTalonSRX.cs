using System;
using System.Runtime.InteropServices;
using HAL_Base;
using static HAL_Simulator.PortConverters;
using static HAL_Simulator.SimData;
// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming


namespace HAL_Simulator
{
    public class HALCanTalonSRX
    {
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
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_P] = 1.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_I] = 2.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_D] = 3.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_F] = 4.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_IZone] = 5.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot0_CloseLoopRampRate] = 6.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_P] = 11.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_I] = 12.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_D] = 13.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_F] = 14.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_IZone] = 15.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSlot1_CloseLoopRampRate] = 16.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForThreshold] = 21.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevThreshold] = 22.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitForEnable] = 23.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileParamSoftLimitRevEnable] = 24.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_BrakeMode] = 31.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_NormallyClosed] = 32.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_NormallyClosed] = 33.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Forward_Disable] = 34.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eOnBoot_LimitSwitch_Reverse_Disable] = 35.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_OverTemp] = 41.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_UnderVoltage] = 42.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_ForLim] = 43.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_RevLim] = 44.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_HardwareFailure] = 45.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_ForSoftLim] = 46.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFault_RevSoftLim] = 47.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_OverTemp] = 48.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_UnderVoltage] = 49.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForLim] = 50.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevLim] = 51.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_ForSoftLim] = 52.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eStckyFault_RevSoftLim] = 53.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eAppliedThrottle] = 61.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eCloseLoopErr] = 62.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFeedbackDeviceSelect] = 63.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eRevMotDuringCloseLoopEn] = 64.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eModeSelect] = 65.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eProfileSlotSelect] = 66.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eRampThrottle] = 67.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eRevFeedbackSensor] = 68.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchEn] = 69.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedFor] = 70.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eLimitSwitchClosedRev] = 71.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eSensorPosition] = 73.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eSensorVelocity] = 74.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eCurrent] = 75.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eBrakeIsEnabled] = 76.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eEncPosition] = 77.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eEncVel] = 78.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eEncIndexRiseEvents] = 79.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eQuadApin] = 80.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eQuadBpin] = 81.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eQuadIdxpin] = 82.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eAnalogInWithOv] = 83.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eAnalogInVel] = 84.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eTemp] = 85.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eBatteryV] = 86.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eResetCount] = 87.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eResetFlags] = 88.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eFirmVers] = 89.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eSettingsChanged] = 90.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.eQuadFilterEn] = 91.0,
                    [(int)HAL_Base.HALCanTalonSRX.ParamID.ePidIaccum] = 93.0,
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


            TalonSRX srx = new TalonSRX {deviceNumber = deviceNumber};
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(srx));
            Marshal.StructureToPtr(srx, ptr, true);
            return ptr;
        }

        public static void c_TalonSRX_Destroy(IntPtr handle)
        {
            halData["CAN"].Remove(GetTalonSRX(handle));
        }

        public static CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value)
        {
            halData["CAN"][GetTalonSRX(handle)]["params"][paramEnum] = value;
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum)
        {
            var param = halData["CAN"][GetTalonSRX(handle)]["params"];

            if (!param.ContainsKey(paramEnum))
            {
                return CTR_Code.CTR_InvalidParamValue;
            }
            return CTR_Code.CTR_OKAY;
        }



        public static CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value)
        {
            var param = halData["CAN"][GetTalonSRX(handle)]["params"];
            if (!param.ContainsKey(paramEnum))
            {
                value = 0;
                return CTR_Code.CTR_InvalidParamValue;
            }
            value = (double) param[paramEnum];
            return CTR_Code.CTR_OKAY;
        }

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

        public static CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs)
        {
            return CTR_Code.CTR_OKAY;
        }

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


        public static CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_overtemp"];
            return CTR_Code.CTR_OKAY;
        }

        public static CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)halData["CAN"][GetTalonSRX(handle)]["fault_undervoltage"];
            return CTR_Code.CTR_OKAY;
        }


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_HardwareFailure")]
        public static extern CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAppliedThrottle")]
        public static extern CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetCloseLoopErr")]
        public static extern CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetModeSelect")]
        public static extern CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchClosedFor")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchClosedRev")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetSensorPosition")]
        public static extern CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetSensorVelocity")]
        public static extern CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetCurrent")]
        public static extern CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetBrakeIsEnabled")]
        public static extern CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncPosition")]
        public static extern CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncVel")]
        public static extern CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncIndexRiseEvents")]
        public static extern CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadApin")]
        public static extern CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadBpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadIdxpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAnalogInWithOv")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAnalogInVel")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetTemp")]
        public static extern CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetBatteryV")]
        public static extern CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetResetCount")]
        public static extern CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetResetFlags")]
        public static extern CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFirmVers")]
        public static extern CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetDemand")]
        public static extern CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetOverrideLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRevMotDuringCloseLoopEn")]
        public static extern CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetOverrideBrakeType")]
        public static extern CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetModeSelect")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///modeSelect: int
        ///demand: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetModeSelect2")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetProfileSlotSelect")]
        public static extern CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRampThrottle")]
        public static extern CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param);


        public static CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param)
        {
            halData["CAN"][GetTalonSRX(handle)]["rev_feedback_sensor"] = param;
            return CTR_Code.CTR_OKAY;
        }
    }
}
