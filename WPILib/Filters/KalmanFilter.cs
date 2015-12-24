using WPILib.Interfaces;

namespace WPILib.Filters
{
    /// <summary>
    /// Implements a Kalman filter for single input filtering
    /// </summary>
    public class KalmanFilter : Filter
    {
        /// <summary>
        /// Gets or sets the processs noise gain.
        /// </summary>
        public double Q { get; set; } = 0.0;

        /// <summary>
        /// Gets or sets the measurement noise gain.
        /// </summary>
        public double R { get; set; } = 0.0;

        private double m_xHat;
        private double m_P;
        private bool m_firstRun = true;

        private double m_dt;

        private readonly Timer m_timer = new Timer();


        /// <summary>
        /// Creates a new <see cref="KalmanFilter"/>.
        /// </summary>
        /// <param name="source">The <see cref="IPIDSource"/> object that is
        /// used to get values.</param>
        public KalmanFilter(IPIDSource source) : base(source)
        {
            m_timer.Start();
            Reset();
        }

        /// <inheritdoc/>
        public override double Get() => m_xHat;

        /// <inheritdoc/>
        public override void Reset()
        {
            m_xHat = 0.0;
            m_P = 0.0;
            m_firstRun = true;
            m_dt = 0.0;
            m_timer.Reset();
        }

        /// <inheritdoc/>
        public override double PidGet()
        {
            // Gets the current dt since the last call to update()
            m_dt = m_timer.Get();

            if (m_firstRun)
            {
                m_xHat = PidGetSource();
                m_firstRun = false;
            }

            m_P += Q * m_dt;

            double k = m_P / (m_P + R);

            m_xHat += k * (PidGetSource() - m_xHat);

            m_P -= k * m_P;

            if ((!(m_xHat > 0.0)) && (!(m_xHat < 0.0)))
            {
                m_xHat = 0.0;
            }

            m_timer.Reset();

            return m_xHat;
        }
    }
}
