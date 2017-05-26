namespace WPILib.Interfaces
{
    /// <summary>
    /// Encoding Types for Counters and Encoders.
    /// </summary>
    public enum EncodingType
    {
        /// <summary>
        /// Count only on rising edge of A channel
        /// </summary>
        K1X = 0,
        /// <summary>
        /// Count on rising edge of A and B channel
        /// </summary>
        K2X = 1,
        /// <summary>
        /// Count on rising and falling edges of both A and B channel
        /// </summary>
        K4X = 2,
    }

    /// <summary>
    /// Interface for counting input ticks.
    /// </summary>
    public interface ICounterBase
    {
        /// <summary>
        /// Gets the current count from the encoder.
        /// </summary>
        /// <remarks>The value is scaled by the scale factor to compensate for the decoding type.</remarks>
        /// <returns>The current count from the encoder.</returns>
        int Get();

        /// <summary>
        /// Resets the encoder count to 0.
        /// </summary>
        void Reset();

        /// <summary>
        /// Returns the period of the most recent pulse in seconds.
        /// </summary>
        /// <remarks>This method compensates for the decoding type.</remarks>
        /// <returns>Period in seconds of the most recent pulse.</returns>
        double GetPeriod();

        /// <summary>
        /// Sets the maximum period for stopped detection, in seconds.
        /// </summary>
        /// <remarks>Sets the value that represents the maximum period of the Encoder before 
        /// it will assume that the attached device is stopped. This timeout allows users to 
        /// determine if the wheels or of ther shaft has stopped rotating. This method 
        /// compensates for the decoding type.</remarks>
        double MaxPeriod { set; }

        /// <summary>
        /// Gets if the encoder is stopped.
        /// </summary>
        /// <remarks>
        /// Using the <see cref="MaxPeriod"/> value, returns true if the most recent pulse width exceeded the Max Period.
        /// </remarks>
        /// <returns>True if the encoder is stopped, otherwise false.</returns>
        bool GetStopped();

        /// <summary>
        /// The last direction the encoder value changed.
        /// </summary>
        /// <returns>True if forward, otherwise false.</returns>
        bool GetDirection();
    }
}
