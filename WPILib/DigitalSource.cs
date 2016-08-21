using System;
using static HAL.Base.HALDigital;
using static WPILib.Utility;

namespace WPILib
{
    /// <summary>
    /// DigitalSource interface
    /// </summary>
    /// <remarks>The DigitalSource represents all the possible inputs
    /// for a counter or a quadrature encoder.The source may be either a digital
    /// input or an analog input.If the caller just provides a channel, then a
    /// digital input will be constructed and freed when finished for the source. The
    /// source can either be a digital input or analog trigger but not both.</remarks>
    public abstract class DigitalSource : InterruptableSensorBase
    {
        /// <summary>
        /// Is this an Analog Trigger
        /// </summary>
        public abstract bool AnalogTrigger { get; }

        /// <summary>
        /// Get the Channel for this source
        /// </summary>
        public abstract int Channel { get; }
    }
}
