namespace HAL.Simulator.Data
{
    /// <summary>
    /// MXP Sim Data
    /// </summary>
    /// <seealso cref="DataBase" />
    public class MXPData : DataBase
    {
        private bool m_initialized = false;

        internal MXPData() { }

        /// <inheritdoc/>
        public override void ResetData()
        {
            m_initialized = false;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MXPData"/> is initialized.
        /// </summary>
        /// <value>
        ///   <c>true</c> if initialized; otherwise, <c>false</c>.
        /// </value>
        public bool Initialized
        {
            get { return m_initialized; }
            internal set
            {
                m_initialized = value;
            }
        }
    }
}
