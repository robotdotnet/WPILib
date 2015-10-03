using System;
using System.Runtime.InteropServices;
using HAL_Base;
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
            SimData.GetCanTalon(GetTalonSRX(handle)).SetParam((HAL_Base.HALCanTalonSRX.ParamID)paramEnum, value);
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum)
        {
            try
            {
                SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((HAL_Base.HALCanTalonSRX.ParamID)paramEnum);
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
                value = SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((HAL_Base.HALCanTalonSRX.ParamID)paramEnum);
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
                value = (int)SimData.GetCanTalon(GetTalonSRX(handle)).GetParam((HAL_Base.HALCanTalonSRX.ParamID)paramEnum);
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
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).LimitSwitchClosedFor;
            return CTR_Code.CTR_OKAY;
        }

        [CalledSimFunction]
        public static CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(IntPtr handle, ref int param)
        {
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).LimitSwitchClosedRev;
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
            param = (int)SimData.GetCanTalon(GetTalonSRX(handle)).BrakeIsEnabled;
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
            SimData.GetCanTalon(GetTalonSRX(handle)).RevMotDuringCloseLoopEn = param;
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
            SimData.GetCanTalon(GetTalonSRX(handle)).RevFeedbackSensor = param;
            return CTR_Code.CTR_OKAY;
        }
    }
}
