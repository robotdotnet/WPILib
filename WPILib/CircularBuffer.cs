namespace WPILib
{
    /// <summary>
    /// This is a simple circular buffer so we don't need to "bucket brigade"
    /// copy old values. 
    /// </summary>
    public class CircularBuffer<T>
    {
        private readonly T[] m_data;
        private int m_front = 0;
        private int m_length = 0;

        public CircularBuffer(int size)
        {
            m_data = new T[size];
            for (int i = 0; i < size; i++)
            {
                m_data[i] = default(T);
            }
        }

        /// <summary>
        /// Push new value onto front of the buffer. The value at the back
        /// is overriden if the buffer is full.
        /// </summary>
        /// <param name="value">The value to push to the front.</param>
        public void PushFront(T value)
        {
            if (m_data.Length == 0) return;

            m_front = ModuloDec(m_front);

            m_data[m_front] = value;

            if (m_length < m_data.Length)
            {
                m_length++;
            }
        }

        /// <summary>
        /// Push new value onto the back of the buffer. The value at the front is
        /// overriden if the buffer is full.
        /// </summary>
        /// <param name="value"></param>
        public void PushBack(T value)
        {
            if (m_data.Length == 0) return;

            m_data[(m_front + m_length) % m_data.Length] = value;

            if (m_length < m_data.Length)
            {
                m_length++;
            }
            else
            {
                //Increment front if buffer is full to maintain size.
                m_front = ModuloInc(m_front);
            }
        }

        /// <summary>
        /// Pop value at front of buffer.
        /// </summary>
        /// <returns>Value at front of buffer</returns>
        public T PopFront()
        {
            // If there are no elements in the buffer, do nothing 	
            if (m_length == 0)
            {

                return default(T);
            }


            T temp = m_data[m_front];
            m_front = ModuloInc(m_front);
            m_length--;
            return temp;
        }

        /// <summary>
        /// Pop value at back of buffer.
        /// </summary>
        /// <returns>The value at back of buffer.</returns>
        public T PopBack()
        {
            // If there are no elements in the buffer, do nothing 	
            if (m_length == 0)
            {
                return default(T);
            }

            m_length--;
            return m_data[(m_front + m_length) % m_data.Length];
        }

        /// <summary>
        /// Resets the buffer back to default values.
        /// </summary>
        public void Reset()
        {
            for (int i = 0; i < m_data.Length; i++)
            {
                m_data[i] = default(T);
            }

            m_front = 0;
            m_length = 0;
        }

        /// <summary>
        /// Gets the element at index starting from front of buffer.
        /// </summary>
        /// <param name="i">The index to get</param>
        /// <returns>The value at index.</returns>
        public T this[int i] => m_data[(m_front + i) % m_data.Length];

        /// <summary>
        /// Gets the element at index starting from front of buffer.
        /// </summary>
        /// <param name="index">The index to get</param>
        /// <returns>The value at index.</returns>
        public T Get(int index) => this[index];

        private int ModuloInc(int index)
        {
            return (index + 1) % m_data.Length;
        }

        private int ModuloDec(int index)
        {
            if (index == 0)
            {
                return m_data.Length - 1;
            }
            else
            {
                return index - 1;
            }
        }
    }
}
