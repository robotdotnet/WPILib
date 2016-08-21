using static HAL.Base.HAL;
using static HAL.SimulatorHAL.Handles.Handle;

namespace HAL.SimulatorHAL.Handles
{
    internal class LimitedHandleResource<T> where T : class, new()
    {
        private readonly T[] m_structures;
        private readonly object[] m_handleMutexes;
        private readonly object m_allocateMutex;

        private readonly int m_size;
        private readonly HALHandleEnum m_enumValue;

        public LimitedHandleResource(int size, HALHandleEnum enumValue)
        {
            m_structures = new T[size];
            m_handleMutexes = new object[size];
            for (int i = 0; i < size; i++)
            {
                m_handleMutexes[i] = new object();
            }
            m_allocateMutex = new object();
            m_size = size;
            m_enumValue = enumValue;
        }

        public int Allocate()
        {
            // globally lock to loop through indices
            lock (m_allocateMutex)
            {
                short i;
                for (i = 0; i < m_size; i++)
                {
                    if (m_structures[i] == null)
                    {
                        // if a false index is found, grab its specific
                        // mutex and allocate it
                        lock (m_handleMutexes[i])
                        {
                            m_structures[i] = new T();
                            return CreateHandle(i, m_enumValue);
                        }
                    }

                }
                return HALInvalidHandle;
            }
        }

        public T Get(int handle)
        {
            short index = GetHandleTypedIndex(handle, m_enumValue);
            if (index < 0 || index >= m_size)
            {
                return null;
            }
            lock (m_handleMutexes[index])
            {
                return m_structures[index];
            }
        }

        public void Free(int handle)
        {
            short index = GetHandleTypedIndex(handle, m_enumValue);
            if (index < 0 || index >= m_size) return;
            lock (m_allocateMutex)
            lock (m_handleMutexes[index])
            {
                m_structures[index] = null;
            }
        }
    }
}
