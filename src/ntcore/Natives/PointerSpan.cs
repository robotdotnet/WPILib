using System;
using System.Collections.Generic;
using System.Text;

namespace NetworkTables.Natives
{
    internal unsafe readonly ref struct PointerSpan<T> where T : unmanaged
    {
        internal readonly T* Pointer;
        internal readonly int Length;

        internal PointerSpan(T* pointer, int length)
        {
            Pointer = pointer;
            Length = length;
        }
    }
}
