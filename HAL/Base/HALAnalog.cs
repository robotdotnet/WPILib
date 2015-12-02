namespace HAL.Base
{
    public partial class HALAnalog
    {
        internal const uint Timebase = 40000000;
        internal const int DefaultOversampleBits = 0;
        internal const double DefaultSampleRate = 50000.0;
        internal const int AnalogInputPins = 8;
        internal const int AnalogOutputPins = 2;
        internal const int AccumulatorNumChannels = 2;
        internal const int DefaultAverageBits = 7;
        internal const long DefaultLSBWeight = 1220703;
        internal const int DefaultOffset = 0;
    }
}
