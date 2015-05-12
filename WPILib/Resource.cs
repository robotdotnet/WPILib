using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Util;

namespace WPILib
{
    public class Resource
    {
        private static Resource resourceList = null;

        private readonly bool[] numAllocated;
        private readonly int size;
        private readonly Resource nextResource;


        public static void Reset()
        {
            for (Resource r = Resource.resourceList; r != null; r = r.nextResource)
            {
                for (int i = 0; i < r.size; i++)
                {
                    r.numAllocated[i] = false;
                }
            }
        }

        public Resource(int size)
        {
            this.size = size;
            numAllocated = new bool[size];

            for (int i = 0; i < this.size; i++)
            {
                numAllocated[i] = false;
            }

            nextResource = Resource.resourceList;
            Resource.resourceList = this;
        }

        public int Allocate()
        {
            for (int i = 0; i < size; i++)
            {
                if (!numAllocated[i])
                {
                    numAllocated[i] = true;
                    return i;
                }
            }
            throw new CheckedAllocationException("No available resources");
        }

        public int Allocate(int index)
        {
            if (index >= size || index < 0)
            {
                throw new CheckedAllocationException("Index " + index + " out of range");
            }
            if (numAllocated[index] == true)
            {
                throw new CheckedAllocationException("Resource at index " + index + " already allocated");
            }
            numAllocated[index] = true;
            return index;
        }

        public void Free(int index)
        {
            if (!numAllocated[index])
            {
                throw new AllocationException("No resource available to be freed");
            }
            numAllocated[index] = false;
        }
    }
}
