using WPILib.Interfaces;

namespace WPILib.Filters
{
    /// <summary>
    /// Creates a queue of values and returns the average of the latest values added to it.
    /// </summary>
    public class MovingAverage : Filter
    {
        private readonly double[] m_values;
        private double m_sum;
        private int m_index;
        private int m_size;

        /// <summary>
        /// Creates a rolling average filter.
        /// </summary>
        /// <param name="source">The <see cref="IPIDSource"/> object that is used to get values.</param>
        /// <param name="size">The total number of values to average.</param>
        public MovingAverage(IPIDSource source, int size) : base(source)
        {
            m_values = new double[size];
            for (int i = 0; i < m_values.Length; i++)
            {
                m_values[i] = 0;
            }
        }

        /// <inheritdoc/>
        public override double Get()
        {
            //Prevent divide by 0 errors
            if (m_size != 0)
            {
                return m_sum / m_size;
            }
            else
            {
                return 0.0;
            }
        }

        /// <inheritdoc/>
        public override void Reset()
        {
            for (int i = 0; i < m_values.Length; i++)
            {
                m_values[i] = 0;
            }
            m_sum = 0;
            m_index = 0;
            m_size = 0;
        }

        /// <inheritdoc/>
        public override double PidGet()
        {
            m_index++;
            if (m_index == m_values.Length) m_index = 0;
            if (m_size == m_values.Length)
            {
                m_sum = m_sum - m_values[m_index];
            }
            else
            {
                m_size++;
            }

            double value = PidGetSource();

            m_values[m_index] = value;
            m_sum += value;

            return Get();
        }
    }
}
