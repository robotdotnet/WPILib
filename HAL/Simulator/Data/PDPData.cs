namespace HAL.Simulator.Data
{
    /// <summary>
    /// PDP Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class PDPData : DataBase
    {
        /// <inheritdoc/>
        public override void ResetData()
        {
            for (int i = 0; i < Current.Length; i++)
            {
                Current[i] = 0.0;
            }
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
