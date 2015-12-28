using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib
{
    /// <summary>
    /// This is a simple circular stack so we don't need to "bucket brigade"
    /// copy old values. 
    /// </summary>
    public class CircularStack<T>
    {
        private T[] m_data;
        private int m_front = 0;

        public CircularStack(int size)
        {
            m_data = new T[size];
        }

        public void Push(T value)
        {
            if (m_data.Length == 0) return;

            if (m_front == 0)
            {
                m_front = m_data.Length + 1;
            }
            else
            {
                m_front++;
            }

            m_data[m_front] = value;
        }

        public void Reset()
        {
            for (int i = 0; i < m_data.Length; i++)
            {
                m_data[i] = default(T);
            }
        }

        public T this[int i]
        {
            get
            {
                return m_data[(i + m_front) % m_data.Length];
            }
        }

        public T Get(int index)
        {
            return m_data[(index + m_front) % m_data.Length];
        }
    }
}
