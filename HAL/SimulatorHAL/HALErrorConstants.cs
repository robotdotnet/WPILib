// ReSharper disable RedundantAssignment
// ReSharper disable InconsistentNaming


#pragma warning disable 1591

namespace HAL.SimulatorHAL
{
    ///<inheritdoc cref="HAL"/>
    internal static class HALErrorConstants
    {
        internal const int CTR_RxTimeout = 1;
        internal const int CTR_TxTimeout = 2;
        internal const int CTR_InvalidParamValue = 3;
        internal const int CTR_UnexpectedArbId = 4;
        internal const int CTR_TxFailed = 5;
        internal const int CTR_SigNotUpdated = 6;

        internal const int NiFpga_Status_FifoTimeout = -50400;
        internal const int NiFpga_Status_TransferAborted = -50405;
        internal const int NiFpga_Status_MemoryFull = -52000;
        internal const int NiFpga_Status_SoftwareFault = -52003;
        internal const int NiFpga_Status_InvalidParameter = -52005;
        internal const int NiFpga_Status_ResourceNotFound = -52006;
        internal const int NiFpga_Status_ResourceNotInitialized = -52010;
        internal const int NiFpga_Status_HardwareFault = -52018;
        internal const int NiFpga_Status_IrqTimeout = -61060;
        internal const int NiFpga_Status_Success = 0;

        internal const int ERR_CANSessionMux_InvalidBuffer = -44408;
        internal const int ERR_CANSessionMux_MessageNotFound = -44087;
        internal const int WARN_CANSessionMux_NoToken = 44087;
        internal const int ERR_CANSessionMux_NotAllowed = -44088;
        internal const int ERR_CANSessionMux_NotInitialized = -44089;

        internal const int SAMPLE_RATE_TOO_HIGH = 1001;
        internal const int VOLTAGE_OUT_OF_RANGE = 1002;
        internal const int LOOP_TIMING_ERROR = 1004;
        internal const int SPI_WRITE_NO_MOSI = 1012;
        internal const int SPI_READ_NO_MISO = 1013;
        internal const int SPI_READ_NO_DATA = 1014;
        internal const int INCOMPATIBLE_STATE = 1015;
        internal const int NO_AVAILABLE_RESOURCES = -1004;
        internal const int NULL_PARAMETER = -1005;
        internal const int ANALOG_TRIGGER_LIMIT_ORDER_ERROR = -1010;
        internal const int ANALOG_TRIGGER_PULSE_OUTPUT_ERROR = -1011;
        internal const int PARAMETER_OUT_OF_RANGE = -1028;
        internal const int RESOURCE_IS_ALLOCATED = -1029;
    }
}
