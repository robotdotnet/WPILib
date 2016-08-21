using static HAL.Base.HALErrors;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.Handles.Handle;

namespace HAL.SimulatorHAL.Handles
{
    internal class IndexedHandleResource<T> where T : class, new()
    {
        private readonly T[] m_structures;
        private readonly object[] m_handleMutexes;

        private readonly int m_size;
        private readonly HALHandleEnum m_enumValue;

        public IndexedHandleResource(int size, HALHandleEnum enumValue)
        {
            m_structures = new T[size];
            m_handleMutexes = new object[size];
            for (int i = 0; i < size; i++)
            {
                m_handleMutexes[i] = new object();
            }
            m_size = size;
            m_enumValue = enumValue;
        }

        public int Allocate(short index, ref int status)
        {
            // don't aquire the lock if we can fail early.
            if (index < 0 || index >= m_size)
            {
                status = RESOURCE_OUT_OF_RANGE;
                return HALInvalidHandle;
            }
            lock (m_handleMutexes[index])
            {
                // check for allocation, otherwise allocate and return a valid handle
                if (m_structures[index] != null)
                {
                    status = RESOURCE_IS_ALLOCATED;
                    return HALInvalidHandle;
                }
                m_structures[index] = new T();
                return CreateHandle(index, m_enumValue);
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
            lock (m_handleMutexes[index])
            {
                m_structures[index] = null;
            }
        }
    }
}
