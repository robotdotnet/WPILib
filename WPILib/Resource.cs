using WPILib.Util;

namespace WPILib
{
    public class Resource
    {
        private static Resource s_resourceList = null;

        private readonly bool[] m_numAllocated;
        private readonly int m_size;
        private readonly Resource m_nextResource;


        public static void Reset()
        {
            for (Resource r = s_resourceList; r != null; r = r.m_nextResource)
            {
                for (int i = 0; i < r.m_size; i++)
                {
                    r.m_numAllocated[i] = false;
                }
            }
        }

        public Resource(int size)
        {
            m_size = size;
            m_numAllocated = new bool[size];

            for (int i = 0; i < m_size; i++)
            {
                m_numAllocated[i] = false;
            }

            m_nextResource = s_resourceList;
            s_resourceList = this;
        }

        public int Allocate()
        {
            for (int i = 0; i < m_size; i++)
            {
                if (!m_numAllocated[i])
                {
                    m_numAllocated[i] = true;
                    return i;
                }
            }
            throw new CheckedAllocationException("No available resources");
        }

        public int Allocate(int index)
        {
            if (index >= m_size || index < 0)
            {
                throw new CheckedAllocationException("Index " + index + " out of range");
            }
            if (m_numAllocated[index] == true)
            {
                throw new CheckedAllocationException("Resource at index " + index + " already allocated");
            }
            m_numAllocated[index] = true;
            return index;
        }

        public void Dispose(int index)
        {
            if (!m_numAllocated[index])
            {
                throw new AllocationException("No resource available to be freed");
            }
            m_numAllocated[index] = false;
        }
    }
}
