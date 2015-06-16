namespace WPILib
{
    /// <summary>
    /// Structure for holding the values stored in an accumulator
    /// </summary>
    public class AccumulatorResult
    {
        /// <summary>
        /// The total value accumulated
        /// </summary>
        public long Value { get; set; }
        /// <summary>
        /// The number of samples value was accumulated over
        /// </summary>
        public long Count { get; set; }
    }
}
