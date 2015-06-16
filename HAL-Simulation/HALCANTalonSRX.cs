
using System;
using System.Runtime.InteropServices;
using HAL_Base;

namespace HAL_Simulator
{
    /*
    public enum CTR_Code
    {
        CTR_OKAY,

        CTR_RxTimeout,

        CTR_TxTimeout,

        CTR_InvalidParamValue,

        CTR_UnexpectedArbId,

        CTR_TxFailed,

        CTR_SigNotUpdated,
    }
     * */
    public class HALCanTalonSRX
    {
        /// Return Type: void*
        ///deviceNumber: int
        ///controlPeriodMs: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_Create")]
        public static extern IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs);


        /// Return Type: void
        ///handle: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_Destroy")]
        public static extern void c_TalonSRX_Destroy(IntPtr handle);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: double
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetParam")]
        public static extern CTR_Code c_TalonSRX_SetParam(IntPtr handle, int paramEnum, double value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_RequestParam")]
        public static extern CTR_Code c_TalonSRX_RequestParam(IntPtr handle, int paramEnum);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: double*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetParamResponse")]
        public static extern CTR_Code c_TalonSRX_GetParamResponse(IntPtr handle, int paramEnum, ref double value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetParamResponseInt32")]
        public static extern CTR_Code c_TalonSRX_GetParamResponseInt32(IntPtr handle, int paramEnum, ref int value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///frameEnum: unsigned int
        ///periodMs: unsigned int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetStatusFrameRate")]
        public static extern CTR_Code c_TalonSRX_SetStatusFrameRate(IntPtr handle, uint frameEnum, uint periodMs);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_ClearStickyFaults")]
        public static extern CTR_Code c_TalonSRX_ClearStickyFaults(IntPtr handle);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetFault_OverTemp(IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetFault_UnderVoltage(IntPtr handle, ref int param);


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


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [DllImport("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRevFeedbackSensor")]
        public static extern CTR_Code c_TalonSRX_SetRevFeedbackSensor(IntPtr handle, int param);
    }
}
