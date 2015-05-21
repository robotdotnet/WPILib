
using System;
using System.Linq;
using System.Reflection;

namespace HAL_Base
{
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

    public enum TalonSRXParam
    {

    }
    public partial class HALCanTalonSRX
    {
        public const int kDefaultContorlPeriodMs = 10;

        public static CTR_Code C_TalonSRX_SetParamEnum(System.IntPtr handle, TalonSRXParam paramEnum, double value)
        {
            return C_TalonSRX_SetParam(handle, (int)paramEnum, value);
        }
    }
}
