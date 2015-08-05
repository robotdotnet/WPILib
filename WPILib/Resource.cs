using WPILib.Exceptions;

namespace WPILib
{
    /// <summary>
    /// Track resources in the program.
    /// </summary>
    /// <remarks>The <see cref="Resource"/> class is a convenient way of keeping track of
    /// allocated arbitrary resources in the program. Resources are just indicies that have
    /// an lower and upper bound that are tracked by this class. In the library they are 
    /// used for tracking allocation of hardware channels but this is purely arbitrary. 
    /// The resource class does not do any actual allocation, but simply tracks if a given 
    /// index is currently in use.
    /// <para>WARNING: this should only be statically allocated. When the program loads 
    /// into memory all the static constructors are called. At that time a linked list of 
    /// all the "Resources" is created. Then when the program actually starts - in the Robot 
    /// constructor, all resources are initialized. This ensures that the program is restartable
    /// in memory without having to unload/reload.</para>
    /// </remarks>
    public class Resource
    {
        private static Resource s_resourceList = null;

        private readonly bool[] m_numAllocated;
        private readonly int m_size;
        private readonly Resource m_nextResource;

        /// <summary>
        /// Clears all allocated resources.
        /// </summary>
        public static void RestartProgram()
        {
            for (Resource r = s_resourceList; r != null; r = r.m_nextResource)
            {
                for (int i = 0; i < r.m_size; i++)
                {
                    r.m_numAllocated[i] = false;
                }
            }
        }

        /// <summary>
        /// Allocate storage for a new instance of <see cref="Resource"/>
        /// </summary>
        /// <remarks>Allocate a bool array of values that will get initialized
        /// to indicate that no resources have been allocated yet. The indicies
        /// of the resources are 0..size-1.</remarks>
        /// <param name="size">The number of blocks to allocate.</param>
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

        /// <summary>
        /// Allocate a resource.
        /// </summary>
        /// <remarks>When a resource is requested, mark it allocated.
        /// In this case, a free resource value within the range is located
        /// and returned after it is marked allocated.</remarks>
        /// <returns>The index of the allocated block.</returns>
        /// /// <exception cref="AllocationException"> If there are no resources
        /// available to be allocated.</exception>
        /// <param name="error">The string to throw when there is an error.</param>
        public int Allocate(string error = null)
        {
            for (int i = 0; i < m_size; i++)
            {
                if (!m_numAllocated[i])
                {
                    m_numAllocated[i] = true;
                    return i;
                }
            }
            if (error == null)
            {
                error = "No available resources";
            }
            throw new AllocationException(error);
        }

        /// <summary>
        /// Allocate a specific resource value.
        /// </summary>
        /// <remarks>The user requests a specific resource value,
        /// i.e. channel number and it is verified unallocated,
        /// then returned.</remarks>
        /// <param name="index">The resource to allocate.</param>
        /// <param name="error">The string to throw when there is an error.</param>
        /// <returns>The index of the allocated block.</returns>
        /// <exception cref="AllocationException"> If there are no resources
        /// available to be allocated.</exception>
        public int Allocate(int index, string error = null)
        {
            if (index >= m_size || index < 0)
            {
                if (error == null)
                {
                    error = $"Index {index} out of range";
                }
                throw new AllocationException(error);
            }
            if (m_numAllocated[index])
            {
                if (error == null)
                {
                    error = $"Resource at index {index} allready allocated";
                }
                throw new AllocationException(error);
            }
            m_numAllocated[index] = true;
            return index;
        }

        /// <summary>
        /// Dispose of an allocated resource.
        /// </summary>
        /// <remarks>
        /// After a resource is no longer needed, for example a destructor is called for a channel assignment class,
        /// Dispose will release the resource value so it can be reused somewhere else in the program.
        /// </remarks>
        /// <param name="index">The index of the resource to free</param>
        public void Deallocate(int index)
        {
            if (!m_numAllocated[index])
            {
                throw new AllocationException("No resource available to be freed");
            }
            m_numAllocated[index] = false;
        }
    }
}
