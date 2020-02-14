namespace REV.SparkMax.Natives
{
    public struct PeriodicStatus0LowLevel
    {
        public float AppliedOutput;
        public ushort Faults;
        public ushort StickyFaults;
        public MotorType MotorType;
        public byte IsFollower;
        public byte IsInverted;
        public byte Lock;
        public byte RoboRIO;
        public ulong Timestamp;
    }

    public struct PeriodicStatus1LowLevel
    {
        public float SensorVelocity;
        public byte MotorTemperature;
        public float BusVoltage;
        public float OutputCurrrent;
        public ulong Timestamp;
    }

    public struct PeriodicStatus2LowLevel
    {
        public float SensorPostion;
        public float IAccum;
        public ulong Timestamp;
    }

    public struct PeriodicStatus3LowLevel
    {
        public float AnalogVoltage;
        public float AnalogVelocity;
        public float AnalogPosition;
        public ulong Timestamp;
    }

    public struct PeriodicStatus4LowLevel
    {
        public float AltEncoderPosition;
        public float AltEncoderVelocity;
        public ulong Timestamp;
    }
}
