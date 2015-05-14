

using System;
using System.Collections.Generic;
using System.Text;
using WPILib.Util;

namespace WPILib
{
    public class Resource
    {
        private static Resource s_resourceList = null;

        private readonly bool[] _numAllocated;
        private readonly int _size;
        private readonly Resource _nextResource;


        public static void Reset()
        {
            for (Resource r = Resource.s_resourceList; r != null; r = r._nextResource)
            {
                for (int i = 0; i < r._size; i++)
                {
                    r._numAllocated[i] = false;
                }
            }
        }

        public Resource(int size)
        {
            _size = size;
            _numAllocated = new bool[size];

            for (int i = 0; i < _size; i++)
            {
                _numAllocated[i] = false;
            }

            _nextResource = Resource.s_resourceList;
            Resource.s_resourceList = this;
        }

        public int Allocate()
        {
            for (int i = 0; i < _size; i++)
            {
                if (!_numAllocated[i])
                {
                    _numAllocated[i] = true;
                    return i;
                }
            }
            throw new CheckedAllocationException("No available resources");
        }

        public int Allocate(int index)
        {
            if (index >= _size || index < 0)
            {
                throw new CheckedAllocationException("Index " + index + " out of range");
            }
            if (_numAllocated[index] == true)
            {
                throw new CheckedAllocationException("Resource at index " + index + " already allocated");
            }
            _numAllocated[index] = true;
            return index;
        }

        public void Free(int index)
        {
            if (!_numAllocated[index])
            {
                throw new AllocationException("No resource available to be freed");
            }
            _numAllocated[index] = false;
        }
    }
}
