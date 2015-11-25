namespace HAL_Simulator.Data
{
    /// <summary>
    /// PDP Sim Data
    /// </summary>
    /// <seealso cref="HAL_Simulator.Data.DataBase" />
    public class PDPData : DataBase
    {
        /// <inheritdoc/>
        public override void ResetData()
        {
            for (int i = 0; i < Current.Length; i++)
            {
                Current[i] = 0.0;
            }
            HasSource = false;
            Temperature = 0.0;
            Voltage = 0.0;
            TotalEnergy = 0.0;
        }

        internal PDPData()
        {
            for (int i = 0; i < Current.Length; i++)
            {
                Current[i] = 0.0;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        public bool HasSource { get; set; } = false;
        /// <summary>
        /// Gets or sets the temperature.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public double Temperature { get; set; } = 0.0;
        /// <summary>
        /// Gets or sets the voltage.
        /// </summary>
        /// <value>
        /// The voltage.
        /// </value>
        public double Voltage { get; set; } = 0.0;
        /// <summary>
        /// Gets the current.
        /// </summary>
        /// <value>
        /// The current.
        /// </value>
        public double[] Current { get; } = new double[16];
        /// <summary>
        /// Gets or sets the total energy.
        /// </summary>
        /// <value>
        /// The total energy.
        /// </value>
        public double TotalEnergy { get; set; } = 0.0;
    }
}
