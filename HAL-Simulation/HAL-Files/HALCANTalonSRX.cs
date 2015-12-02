using System;
using System.Runtime.InteropServices;
using HAL;
using static HAL_Simulator.PortConverters;

// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
#pragma warning disable 1591


namespace HAL_Simulator
{
    ///<inheritdoc cref="HAL"/>
    internal class HALCanTalonSRX
    {
        internal static void Initialize(IntPtr library, ILibraryLoader loader)
        {
            global::HAL.HALCanTalonSRX.C_TalonSRX_Create = c_TalonSRX_Create;
            global::HAL.HALCanTalonSRX.C_TalonSRX_Destroy = c_TalonSRX_Destroy;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetParam = c_TalonSRX_SetParam;
            global::HAL.HALCanTalonSRX.C_TalonSRX_RequestParam = c_TalonSRX_RequestParam;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponse = c_TalonSRX_GetParamResponse;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetParamResponseInt32 = c_TalonSRX_GetParamResponseInt32;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetStatusFrameRate = c_TalonSRX_SetStatusFrameRate;
            global::HAL.HALCanTalonSRX.C_TalonSRX_ClearStickyFaults = c_TalonSRX_ClearStickyFaults;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_OverTemp = c_TalonSRX_GetFault_OverTemp;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_UnderVoltage = c_TalonSRX_GetFault_UnderVoltage;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForLim = c_TalonSRX_GetFault_ForLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevLim = c_TalonSRX_GetFault_RevLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_HardwareFailure = c_TalonSRX_GetFault_HardwareFailure;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_ForSoftLim = c_TalonSRX_GetFault_ForSoftLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFault_RevSoftLim = c_TalonSRX_GetFault_RevSoftLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_OverTemp = c_TalonSRX_GetStckyFault_OverTemp;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_UnderVoltage = c_TalonSRX_GetStckyFault_UnderVoltage;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForLim = c_TalonSRX_GetStckyFault_ForLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevLim = c_TalonSRX_GetStckyFault_RevLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_ForSoftLim = c_TalonSRX_GetStckyFault_ForSoftLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetStckyFault_RevSoftLim = c_TalonSRX_GetStckyFault_RevSoftLim;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAppliedThrottle = c_TalonSRX_GetAppliedThrottle;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetCloseLoopErr = c_TalonSRX_GetCloseLoopErr;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFeedbackDeviceSelect = c_TalonSRX_GetFeedbackDeviceSelect;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetModeSelect = c_TalonSRX_GetModeSelect;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchEn = c_TalonSRX_GetLimitSwitchEn;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedFor = c_TalonSRX_GetLimitSwitchClosedFor;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetLimitSwitchClosedRev = c_TalonSRX_GetLimitSwitchClosedRev;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorPosition = c_TalonSRX_GetSensorPosition;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetSensorVelocity = c_TalonSRX_GetSensorVelocity;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetCurrent = c_TalonSRX_GetCurrent;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetBrakeIsEnabled = c_TalonSRX_GetBrakeIsEnabled;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncPosition = c_TalonSRX_GetEncPosition;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncVel = c_TalonSRX_GetEncVel;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetEncIndexRiseEvents = c_TalonSRX_GetEncIndexRiseEvents;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadApin = c_TalonSRX_GetQuadApin;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadBpin = c_TalonSRX_GetQuadBpin;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetQuadIdxpin = c_TalonSRX_GetQuadIdxpin;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInWithOv = c_TalonSRX_GetAnalogInWithOv;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetAnalogInVel = c_TalonSRX_GetAnalogInVel;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetTemp = c_TalonSRX_GetTemp;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetBatteryV = c_TalonSRX_GetBatteryV;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetCount = c_TalonSRX_GetResetCount;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetResetFlags = c_TalonSRX_GetResetFlags;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetFirmVers = c_TalonSRX_GetFirmVers;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetDemand = c_TalonSRX_SetDemand;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideLimitSwitchEn = c_TalonSRX_SetOverrideLimitSwitchEn;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetFeedbackDeviceSelect = c_TalonSRX_SetFeedbackDeviceSelect;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevMotDuringCloseLoopEn = c_TalonSRX_SetRevMotDuringCloseLoopEn;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetOverrideBrakeType = c_TalonSRX_SetOverrideBrakeType;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect = c_TalonSRX_SetModeSelect;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetModeSelect2 = c_TalonSRX_SetModeSelect2;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetProfileSlotSelect = c_TalonSRX_SetProfileSlotSelect;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRampThrottle = c_TalonSRX_SetRampThrottle;
            global::HAL.HALCanTalonSRX.C_TalonSRX_SetRevFeedbackSensor = c_TalonSRX_SetRevFeedbackSensor;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthPosition = CTalonSRXGetPulseWidthPosition;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthVelocity = CTalonSRXGetPulseWidthVelocity;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToFallUs = CTalonSRXGetPulseRiseToFallUs;
            global::HAL.HALCanTalonSRX.C_TalonSRX_GetPulseWidthRiseToRiseUs = CTalonSRXGetPulseRiseToRiseUs;
            global::HAL.HALCanTalonSRX.C_TalonSRX_IsPulseWidthSensorPresent = CTalonSRXIsPulseWidthSensorPresent;
        }

        private static CTR_Code CTalonSRXIsPulseWidthSensorPresent(IntPtr handle, ref int i)
        {
            throw new NotImplementedException();
        }

        private static CTR_Code CTalonSRXGetPulseRiseToRiseUs(IntPtr handle, ref int i)
        {
            throw new NotImplementedException();
        }

        private static CTR_Code CTalonSRXGetPulseRiseToFallUs(IntPtr handle, ref int i)
        {
            throw new NotImplementedException();
        }

        private static CTR_Code CTalonSRXGetPulseWidthVelocity(IntPtr handle, ref int i)
        {
            throw new NotImplementedException();
        }

        private static CTR_Code CTalonSRXGetPulseWidthPosition(IntPtr handle, ref int i)
        {
            throw new NotImplementedException();
        }


        [CalledSimFunction]
        public static IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs)
        {
            if (!SimData.InitializeCanTalon(deviceNumber))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceNumber), "Device Already Allocated.");
            }


            TalonSRX srx = new TalonSRX { deviceNumber = deviceNumber };
            IntPtr ptr = Marshal.AllocHGlobal(Marshal.SizeOf(srx));
            Marshal.StructureToPtr(srx, ptr, true);
            return ptr;
        }

        [CalledSimFunction]
        public static void c_TalonSRX_Destroy(IntPtr handle)
        {
            SimData.RemoveCanTalon(GetTalonSRX(handle));
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).SetParam((global::HAL.HALCanTalonSRX.ParamID)paramEnum, value);
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum)
        {
            try
            {
                SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((global::HAL.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                return CTR_Code.CTR_InvalidParamValue;
            }
        }



        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value)
        {
            try
            {
                value = SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((global::HAL.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                value = 0.0;
                return CTR_Code.CTR_InvalidParamValue;
            }
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value)
        {
            try
            {
                value = (int)SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((global::HAL.HALCanTalonSRX.ParamID)paramEnum);
                return CTR_Code.CTR_OKAY;
            }
            catch (ArgumentOutOfRangeException)
            {
                value = 0;
                return CTR_Code.CTR_InvalidParamValue;
            }
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs)
        {
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_OverTemp = 0;
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_UnderVoltage = 0;
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_ForLim = 0;
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_RevLim = 0;
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_ForSoftLim = 0;
            SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_ForSoftLim = 0;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_OverTemp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_UnderVoltage;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_ForLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_RevLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_HardwareFailure(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_HardwareFailure;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_ForSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).Fault_RevSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_OverTemp(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_OverTemp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_UnderVoltage;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_ForLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_RevLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_ForSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).StckyFault_RevSoftLim;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAppliedThrottle(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).AppliedThrottle;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCloseLoopErr(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).CloseLoopErr;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).FeedbackDeviceSelect;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetModeSelect(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).ModeSelect;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchEn(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).LimitSwitchEn;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(IntPtr handle, ref int param)
        {
            param = SimData.GetCanTalon(GetTalonSRX(handle)).LimitSwitchClosedFor ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param)
        {
            param = SimData.GetCanTalon(GetTalonSRX(handle)).LimitSwitchClosedRev ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorPosition(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).SensorPosition;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetSensorVelocity(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).SensorVelocity;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetCurrent(IntPtr handle, ref double param)
        {
            param = (double)SimData.GetCanTalon(GetTalonSRX(handle)).Current;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBrakeIsEnabled(IntPtr handle, ref int param)
        {
            param = SimData.GetCanTalon(GetTalonSRX(handle)).BrakeIsEnabled ? 1 : 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncPosition(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).EncPosition;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncVel(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).EncVel;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetEncIndexRiseEvents(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).EncIndexRiseEvents;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadApin(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).QuadApin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadBpin(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).QuadBpin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetQuadIdxpin(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).QuadIdxpin;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInWithOv(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).AnalogInWithOv;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetAnalogInVel(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).AnalogInVel;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetTemp(IntPtr handle, ref double param)
        {
            param = (double)SimData.GetCanTalon(GetTalonSRX(handle)).Temp;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetBatteryV(IntPtr handle, ref double param)
        {
            param = SimData.GetCanTalon(GetTalonSRX(handle)).BatteryV;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetCount(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).ResetCount;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetResetFlags(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).ResetFlags;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetFirmVers(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).FirmVers;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetDemand(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).Demand = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).OverrideLimitSwitch = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).FeedbackDeviceSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).RevMotDuringCloseLoopEn = param != 0;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetOverrideBrakeType(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).OverrideBrakeType = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).ModeSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetModeSelect2(IntPtr handle, int modeSelect, int demand)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).ModeSelect = modeSelect;
            SimData.GetCanTalon(GetTalonSRX(handle)).Demand = demand;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetProfileSlotSelect(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).ProfileSlotSelect = param;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRampThrottle(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).RampThrottle = param;
            return CTR_Code.CTR_OKAY;
        }


        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param)
        {
            SimData.GetCanTalon(GetTalonSRX(handle)).RevFeedbackSensor = param != 0;
            return CTR_Code.CTR_OKAY;
        }
    }
}
