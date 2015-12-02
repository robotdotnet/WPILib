using HAL;
using WPILib.LiveWindows;

namespace WPILib
{
    /// <summary>
    /// Alias for counter class.
    /// </summary>
    /// <remarks>Implement the gear tooth sensor supplied by FIRST. 
    /// Currently there is no reverse sensing on the gear tooth sensor, 
    /// but in future versions we might implement the necessary timing in 
    /// the FPGA to sense direction.</remarks>
    public class GearTooth : Counter
    {
        private const double GearToothThreshold = 55e-6;

        /// <summary>
        /// Sets whether the GearTooth sensor is direction sensing.
        /// </summary>
        public bool DirectionSensing
        {
            set
            {
                if (value)
                {
                    SetPulseLengthMode(GearToothThreshold);
                }
            }
        }

        /// <summary>
        /// Construct a <see cref="GearTooth"/> sensor given a channel.
        /// </summary>
        /// <remarks>No direction sensing is assumed.</remarks>
        /// <param name="channel">The GPIO channel that the sensor is
        /// connected to.</param>
        public GearTooth(int channel) : this(channel, false)
        {
        }

        /// <summary>
        /// Construct a <see cref="GearTooth"/> sensor given a channel.
        /// </summary>
        /// <param name="channel">The GPIO channel that the sensor is
        /// connected to.</param>
        /// <param name="directionSensitive">True to enable direction sensing.</param>
        public GearTooth(int channel, bool directionSensitive) : base(channel)
        {
            DirectionSensing = directionSensitive;
            if (directionSensitive)
            {
                HAL.HAL.Report(ResourceType.kResourceType_GearTooth, (byte) channel, 0, "D");
            }
            else
            {
                HAL.HAL.Report(ResourceType.kResourceType_GearTooth, (byte)channel);
            }
            LiveWindow.LiveWindow.AddSensor("GearTooth", channel, this);
        }

        /// <summary>
        /// Construct a <see cref="GearTooth"/> sensor given a <see cref="DigitalSource"/>.
        /// </summary>
        /// <remarks>This should be used when sharing digital inputs</remarks>
        /// <param name="source">An existing <see cref="DigitalSource"/> object 
        /// (such as a <see cref="DigitalInput"/></param>
        /// <param name="directionSensitive">True to enable direction sensing.</param>
        public GearTooth(DigitalSource source, bool directionSensitive) : base(source)
        {
            DirectionSensing = directionSensitive;
        }

        /// <summary>
        /// Construct a <see cref="GearTooth"/> sensor given a <see cref="DigitalSource"/>.
        /// </summary>
        /// <remarks>No direction sensing is assumed.</remarks>
        /// <param name="source">An existing <see cref="DigitalSource"/> object 
        /// (such as a <see cref="DigitalInput"/></param>
        public GearTooth(DigitalSource source) : this(source, false)
        {
            
        }
    }
}
