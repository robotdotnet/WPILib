
using HAL_Base;

namespace HAL_FRC
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
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_Create")]
        public static extern System.IntPtr c_TalonSRX_Create(int deviceNumber, int controlPeriodMs);


        /// Return Type: void
        ///handle: void*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_Destroy")]
        public static extern void c_TalonSRX_Destroy(System.IntPtr handle);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: double
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetParam")]
        public static extern CTR_Code c_TalonSRX_SetParam(System.IntPtr handle, int paramEnum, double value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_RequestParam")]
        public static extern CTR_Code c_TalonSRX_RequestParam(System.IntPtr handle, int paramEnum);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: double*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetParamResponse")]
        public static extern CTR_Code c_TalonSRX_GetParamResponse(System.IntPtr handle, int paramEnum, ref double value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///paramEnum: int
        ///value: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetParamResponseInt32")]
        public static extern CTR_Code c_TalonSRX_GetParamResponseInt32(System.IntPtr handle, int paramEnum, ref int value);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///frameEnum: unsigned int
        ///periodMs: unsigned int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetStatusFrameRate")]
        public static extern CTR_Code c_TalonSRX_SetStatusFrameRate(System.IntPtr handle, uint frameEnum, uint periodMs);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_ClearStickyFaults")]
        public static extern CTR_Code c_TalonSRX_ClearStickyFaults(System.IntPtr handle);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetFault_OverTemp(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetFault_UnderVoltage(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_HardwareFailure")]
        public static extern CTR_Code c_TalonSRX_GetFault_HardwareFailure(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_ForSoftLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetFault_RevSoftLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_OverTemp")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_OverTemp(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_UnderVoltage")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_UnderVoltage(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_ForLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_RevLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_ForSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_ForSoftLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetStckyFault_RevSoftLim")]
        public static extern CTR_Code c_TalonSRX_GetStckyFault_RevSoftLim(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAppliedThrottle")]
        public static extern CTR_Code c_TalonSRX_GetAppliedThrottle(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetCloseLoopErr")]
        public static extern CTR_Code c_TalonSRX_GetCloseLoopErr(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_GetFeedbackDeviceSelect(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetModeSelect")]
        public static extern CTR_Code c_TalonSRX_GetModeSelect(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchEn(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchClosedFor")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedFor(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetLimitSwitchClosedRev")]
        public static extern CTR_Code c_TalonSRX_GetLimitSwitchClosedRev(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetSensorPosition")]
        public static extern CTR_Code c_TalonSRX_GetSensorPosition(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetSensorVelocity")]
        public static extern CTR_Code c_TalonSRX_GetSensorVelocity(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetCurrent")]
        public static extern CTR_Code c_TalonSRX_GetCurrent(System.IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetBrakeIsEnabled")]
        public static extern CTR_Code c_TalonSRX_GetBrakeIsEnabled(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncPosition")]
        public static extern CTR_Code c_TalonSRX_GetEncPosition(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncVel")]
        public static extern CTR_Code c_TalonSRX_GetEncVel(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetEncIndexRiseEvents")]
        public static extern CTR_Code c_TalonSRX_GetEncIndexRiseEvents(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadApin")]
        public static extern CTR_Code c_TalonSRX_GetQuadApin(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadBpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadBpin(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetQuadIdxpin")]
        public static extern CTR_Code c_TalonSRX_GetQuadIdxpin(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAnalogInWithOv")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInWithOv(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetAnalogInVel")]
        public static extern CTR_Code c_TalonSRX_GetAnalogInVel(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetTemp")]
        public static extern CTR_Code c_TalonSRX_GetTemp(System.IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: double*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetBatteryV")]
        public static extern CTR_Code c_TalonSRX_GetBatteryV(System.IntPtr handle, ref double param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetResetCount")]
        public static extern CTR_Code c_TalonSRX_GetResetCount(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetResetFlags")]
        public static extern CTR_Code c_TalonSRX_GetResetFlags(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int*
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_GetFirmVers")]
        public static extern CTR_Code c_TalonSRX_GetFirmVers(System.IntPtr handle, ref int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetDemand")]
        public static extern CTR_Code c_TalonSRX_SetDemand(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetOverrideLimitSwitchEn")]
        public static extern CTR_Code c_TalonSRX_SetOverrideLimitSwitchEn(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetFeedbackDeviceSelect")]
        public static extern CTR_Code c_TalonSRX_SetFeedbackDeviceSelect(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRevMotDuringCloseLoopEn")]
        public static extern CTR_Code c_TalonSRX_SetRevMotDuringCloseLoopEn(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetOverrideBrakeType")]
        public static extern CTR_Code c_TalonSRX_SetOverrideBrakeType(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetModeSelect")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///modeSelect: int
        ///demand: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetModeSelect2")]
        public static extern CTR_Code c_TalonSRX_SetModeSelect2(System.IntPtr handle, int modeSelect, int demand);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetProfileSlotSelect")]
        public static extern CTR_Code c_TalonSRX_SetProfileSlotSelect(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRampThrottle")]
        public static extern CTR_Code c_TalonSRX_SetRampThrottle(System.IntPtr handle, int param);


        /// Return Type: CTR_Code->Anonymous_f52fbd73_f226_4861_9aae_a3b7d481be8b
        ///handle: void*
        ///param: int
        [System.Runtime.InteropServices.DllImportAttribute("libHALAthena_shared.so", EntryPoint = "c_TalonSRX_SetRevFeedbackSensor")]
        public static extern CTR_Code c_TalonSRX_SetRevFeedbackSensor(System.IntPtr handle, int param);
    }
}
