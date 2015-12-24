using WPILib.Interfaces;

namespace WPILib.Filters
{
    /// <summary>
    /// Superclass for filters
    /// </summary>
    public abstract class Filter : IPIDSource
    {
        private readonly IPIDSource m_source;

        /// <summary>
        /// Creates a new <see cref="Filter"/>
        /// </summary>
        /// <param name="source">The <see cref="IPIDSource"/> to filter.</param>
        protected Filter(IPIDSource source)
        {
            m_source = source;
        }

        /// <inheritdoc/>
        public abstract double PidGet();

        /// <summary>
        /// Returns the current filter estimate without inserting new data as <see cref="PidGet"/>
        /// would do.
        /// </summary>
        /// <returns>The current filter estimate</returns>
        public abstract double Get();

        /// <summary>
        /// Resets the filter state.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Calls PidGet of source.
        /// </summary>
        /// <returns>Current value of the source</returns>
        protected double PidGetSource()
        {
            return m_source.PidGet();
        }

        /// <inheritdoc/>
        public PIDSourceType PIDSourceType { get; set; }
    }
}
