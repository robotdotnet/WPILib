namespace WPILib;

public record struct CANStatus(double PercentBusUtilization, int BusOffCount, int TxFullCount, int ReceiveErrorCount, int TransmitErrorCount);
