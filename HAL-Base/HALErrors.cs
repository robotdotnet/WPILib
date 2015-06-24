namespace HAL_Base
{
    public class HALErrors
    {
        public const string CTR_RxTimeout_MESSAGE = "CTRE CAN Recieve Timeout";
        public const string CTR_TxTimeout_MESSAGE = "CTRE CAN Transmit Timeout";
        public const string CTR_InvalidParamValue_MESSAGE = "CTRE CAN Invalid Parameter";
        public const string CTR_UnexpectedArbId_MESSAGE = "CTRE Unexpected Arbitration ID (CAN Node ID)";
        public const string CTR_TxFailed_MESSAGE = "CTRE CAN Transmit Error";
        public const string CTR_SigNotUpdated_MESSAGE = "CTRE CAN Signal Not Updated";

        public const string NiFpga_Status_FifoTimeout_MESSAGE = "NIFPGA: FIFO timeout error";
        public const string NiFpga_Status_TransferAborted_MESSAGE = "NIFPGA: Transfer aborted error";
        public const string NiFpga_Status_MemoryFull_MESSAGE = "NIFPGA: Memory Allocation failed, memory full";
        public const string NiFpga_Status_SoftwareFault_MESSAGE = "NIFPGA: Unexepected software error";
        public const string NiFpga_Status_InvalidParameter_MESSAGE = "NIFPGA: Invalid Parameter";
        public const string NiFpga_Status_ResourceNotFound_MESSAGE = "NIFPGA: Resource not found";
        public const string NiFpga_Status_ResourceNotInitialized_MESSAGE = "NIFPGA: Resource not initialized";
        public const string NiFpga_Status_HardwareFault_MESSAGE = "NIFPGA: Hardware Fault";
        public const string NiFpga_Status_IrqTimeout_MESSAGE = "NIFPGA: Interrupt timeout";

        public const string ERR_CANSessionMux_InvalidBuffer_MESSAGE = "CAN: Invalid Buffer";
        public const string ERR_CANSessionMux_MessageNotFound_MESSAGE = "CAN: Message not found";
        public const string WARN_CANSessionMux_NoToken_MESSAGE = "CAN: No token";
        public const string ERR_CANSessionMux_NotAllowed_MESSAGE = "CAN: Not allowed";
        public const string ERR_CANSessionMux_NotInitialized_MESSAGE = "CAN: Not initialized";

        public const int SAMPLE_RATE_TOO_HIGH = 1001;
        public const string SAMPLE_RATE_TOO_HIGH_MESSAGE = "HAL: Analog module sample rate is too high";
        public const int VOLTAGE_OUT_OF_RANGE = 1002;
        public const string VOLTAGE_OUT_OF_RANGE_MESSAGE = "HAL: Voltage to convert to raw value is out of range [0; 5]";
        public const int LOOP_TIMING_ERROR = 1004;
        public const string LOOP_TIMING_ERROR_MESSAGE = "HAL: Digital module loop timing is not the expected value";
        public const int SPI_WRITE_NO_MOSI = 1012;
        public const string SPI_WRITE_NO_MOSI_MESSAGE = "HAL: Cannot write to SPI port with no MOSI output";
        public const int SPI_READ_NO_MISO = 1013;
        public const string SPI_READ_NO_MISO_MESSAGE = "HAL: Cannot read from SPI port with no MISO input";
        public const int SPI_READ_NO_DATA = 1014;
        public const string SPI_READ_NO_DATA_MESSAGE = "HAL: No data available to read from SPI";
        public const int INCOMPATIBLE_STATE = 1015;
        public const string INCOMPATIBLE_STATE_MESSAGE = "HAL: Incompatible State: The operation cannot be completed";
        public const int NO_AVAILABLE_RESOURCES = -1004;
        public const string NO_AVAILABLE_RESOURCES_MESSAGE = "HAL: No available resources to allocate";
        public const int NULL_PARAMETER = -1005;
        public const string NULL_PARAMETER_MESSAGE = "HAL: A pointer parameter to a method is NULL";
        public const int ANALOG_TRIGGER_LIMIT_ORDER_ERROR = -1010;
        public const string ANALOG_TRIGGER_LIMIT_ORDER_ERROR_MESSAGE = "HAL: AnalogTrigger limits error.  Lower limit > Upper Limit";
        public const int ANALOG_TRIGGER_PULSE_OUTPUT_ERROR = -1011;
        public const string ANALOG_TRIGGER_PULSE_OUTPUT_ERROR_MESSAGE = "HAL: Attempted to read AnalogTrigger pulse output.";
        public const int PARAMETER_OUT_OF_RANGE = -1028;
        public const string PARAMETER_OUT_OF_RANGE_MESSAGE = "HAL: A parameter is out of range.";
        public const int RESOURCE_IS_ALLOCATED = -1029;
        public const string RESOURCE_IS_ALLOCATED_MESSAGE = "HAL: Resource already allocated";

        public const string VI_ERROR_SYSTEM_ERROR_MESSAGE = "HAL - VISA: System Error";
        public const string VI_ERROR_INV_OBJECT_MESSAGE = "HAL - VISA: Invalid Object";
        public const string VI_ERROR_RSRC_LOCKED_MESSAGE = "HAL - VISA: Resource Locked";
        public const string VI_ERROR_RSRC_NFOUND_MESSAGE = "HAL - VISA: Resource Not Found";
        public const string VI_ERROR_INV_RSRC_NAME_MESSAGE = "HAL - VISA: Invalid Resource Name";
        public const string VI_ERROR_QUEUE_OVERFLOW_MESSAGE = "HAL - VISA: Queue Overflow";
        public const string VI_ERROR_IO_MESSAGE = "HAL - VISA: General IO Error";
        public const string VI_ERROR_ASRL_PARITY_MESSAGE = "HAL - VISA: Parity Error";
        public const string VI_ERROR_ASRL_FRAMING_MESSAGE = "HAL - VISA: Framing Error";
        public const string VI_ERROR_ASRL_OVERRUN_MESSAGE = "HAL - VISA: Buffer Overrun Error";
        public const string VI_ERROR_RSRC_BUSY_MESSAGE = "HAL - VISA: Resource Busy";
        public const string VI_ERROR_INV_PARAMETER_MESSAGE = "HAL - VISA: Invalid Parameter";
        //public static int PARAMETER_OUT_OF_RANGE = =-1028;
    }
}
