using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib
{
    /// <summary>
    /// This is a simple circular buffer so we don't need to "bucket brigade"
    /// copy old values. 
    /// </summary>
    public class CircularBuffer<T>
    {
        private T[] m_data;
        private int m_front = 0;
        private int m_back = 0;
        private int m_size = 0;

        public CircularBuffer(int size)
        {
            m_data = new T[size];
            for(int i = 0; i < size; i++)
            {
                m_data[i] = default(T);
            }
        }

        public void PushFront(T value)
        {
            if (m_data.Length == 0) return;

            // If buffer is full, decrement back index so front doesn't underrun it.
            if (m_front == m_back && m_size > 0)
            {
                if (m_front == 0)
                {
                    m_front = m_data.Length - 1;
                }
                else
                {
                    m_front--;
                }

                m_back = m_front;
            }
            else
            {
                if (m_front == 0)
                {
                    m_front = m_data.Length - 1;
                }
                else
                {
                    m_front--;
                }

                m_size++;
            }

            m_data[m_front] = value;
        }

        public void PushBack(T value)
        {
            if (m_data.Length == 0) return;

            m_data[m_back] = value;

            //If buffer is full, advance front index so back doesn't overrun it.
            if (m_front == m_back && m_size > 0)
            {
                m_front = (m_front + 1) % m_data.Length;
                m_back = m_front;
            }
            else
            {
                m_back = (m_back + 1) % m_data.Length;
                m_size++;
            }
        }

        public T PopFront()
        {
            if (m_size == 0) return default(T);

            m_size--;

            T temp = m_data[m_front];
            m_front = (m_front + 1) & m_data.Length;
            return temp;
        }

        public T PopBack()
        {
            if (m_size == 0) return default(T);

            m_size--;

            if (m_back == 0)
            {
                m_back = m_data.Length - 1;
            }
            else
            {
                m_back--;
            }

            return m_data[m_back];
        }

        public int Size()
        {
            return m_size;
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
                return m_data[(m_front + i) % m_data.Length];
            }
        }

        public T Get(int index)
        {
            return m_data[(m_front + index) % m_data.Length];
        }
    }
}
