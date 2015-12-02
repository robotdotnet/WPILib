namespace HAL.Simulator.Data
{
    /// <summary>
    /// RoboRIO Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class RoboRioData : DataBase
    {
        internal RoboRioData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            FPGAButton = false;

            HasSource = false;

            VInVoltage = 0.0f;
            VInCurrent = 0.0f;
            UserVoltage6V = 6.0f;
            UserCurrent6V = 0.0f;
            UserActive6V = false;
            UserVoltage5V = 5.0f;
            UserCurrent5V = 0.0f;
            UserActive5V = false;
            UserVoltage3V3 = 3.3f;
            UserCurrent3V3 = 0.0f;
            UserActive3V3 = false;
            UserFaults6V = 0;
            UserFaults5V = 0;
            UserFaults3V3 = 0;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [fpga button].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [fpga button]; otherwise, <c>false</c>.
        /// </value>
        public bool FPGAButton { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether this instance has source.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has source; otherwise, <c>false</c>.
        /// </value>
        public bool HasSource { get; set; } = false;

        /// <summary>
        /// Gets or sets the v in voltage.
        /// </summary>
        /// <value>
        /// The v in voltage.
        /// </value>
        public float VInVoltage { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets the v in current.
        /// </summary>
        /// <value>
        /// The v in current.
        /// </value>
        public float VInCurrent { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets the user voltage6 v.
        /// </summary>
        /// <value>
        /// The user voltage6 v.
        /// </value>
        public float UserVoltage6V { get; set; } = 6.0f;
        /// <summary>
        /// Gets or sets the user current6 v.
        /// </summary>
        /// <value>
        /// The user current6 v.
        /// </value>
        public float UserCurrent6V { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets a value indicating whether [user active6 v].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user active6 v]; otherwise, <c>false</c>.
        /// </value>
        public bool UserActive6V { get; set; } = false;
        /// <summary>
        /// Gets or sets the user voltage5 v.
        /// </summary>
        /// <value>
        /// The user voltage5 v.
        /// </value>
        public float UserVoltage5V { get; set; } = 5.0f;
        /// <summary>
        /// Gets or sets the user current5 v.
        /// </summary>
        /// <value>
        /// The user current5 v.
        /// </value>
        public float UserCurrent5V { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets a value indicating whether [user active5 v].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user active5 v]; otherwise, <c>false</c>.
        /// </value>
        public bool UserActive5V { get; set; } = false;
        /// <summary>
        /// Gets or sets the user voltage3 v3.
        /// </summary>
        /// <value>
        /// The user voltage3 v3.
        /// </value>
        public float UserVoltage3V3 { get; set; } = 3.3f;
        public float UserCurrent3V3 { get; set; } = 0.0f;
        /// <summary>
        /// Gets or sets a value indicating whether [user active3 v3].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [user active3 v3]; otherwise, <c>false</c>.
        /// </value>
        public bool UserActive3V3 { get; set; } = false;
        /// <summary>
        /// Gets or sets the user faults6 v.
        /// </summary>
        /// <value>
        /// The user faults6 v.
        /// </value>
        public int UserFaults6V { get; set; } = 0;
        /// <summary>
        /// Gets or sets the user faults5 v.
        /// </summary>
        /// <value>
        /// The user faults5 v.
        /// </value>
        public int UserFaults5V { get; set; } = 0;
        /// <summary>
        /// Gets or sets the user faults3 v3.
        /// </summary>
        /// <value>
        /// The user faults3 v3.
        /// </value>
        public int UserFaults3V3 { get; set; } = 0;

    }
}
