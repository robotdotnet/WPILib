using System.Collections.Generic;
using static HAL.Base.HAL;
using static HAL.SimulatorHAL.Handles.Handle;

namespace HAL.SimulatorHAL.Handles
{
    internal class UnlimitedHandleResource<T> where T : class
    {
        private readonly List<T> m_structures;
        private readonly object m_handleMutex;

        private readonly HALHandleEnum m_enumValue;

        public UnlimitedHandleResource(HALHandleEnum enumValue)
        {
            m_structures = new List<T>();
            m_handleMutex = new object();
            m_enumValue = enumValue;
        }

        public int Allocate(T structure)
        {
            lock (m_handleMutex)
            {
                int i;
                for (i = 0; i < m_structures.Count; i++)
                {
                    if (m_structures[i] == null)
                    {
                        m_structures[i] = structure;
                        return CreateHandle((short)i, m_enumValue);
                    }
                }

                if (i >= short.MaxValue) return HALInvalidHandle;
                
                m_structures.Add(structure);
                return CreateHandle((short)i, m_enumValue);
            }
        }

        public T Get(int handle)
        {
            short index = GetHandleTypedIndex(handle, m_enumValue);
            lock (m_handleMutex)
            {
                if (index < 0 || index >= m_structures.Count)
                    return null;
                return m_structures[index];
            }
        }

        public void Free(int handle)
        {
            short index = GetHandleTypedIndex(handle, m_enumValue);
            lock (m_handleMutex)
            {
                if (index < 0 || index >= m_structures.Count) return;
                m_structures[index] = null;
            }
    }
    }
}
