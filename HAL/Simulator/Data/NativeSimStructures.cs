using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HAL.Simulator.Data
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HAL_NotifyCallback(IntPtr name, IntPtr param, ref HAL_Value value);

    public delegate void NotifyCallback(string name, HAL_Value value);

    public enum HAL_Type : int
    {
        HAL_UNASSIGNED = 0,
        HAL_BOOLEAN = 0x01,
        HAL_DOUBLE = 0x02,
        HAL_ENUM = 0x16,
        HAL_INT = 0x32,
        HAL_LONG = 0x64,
    };


    [StructLayout(LayoutKind.Explicit)]
    public struct HAL_Value
    {
        [FieldOffset(0)]
        private int v_boolean;
        [FieldOffset(0)]
        private int v_enum;
        [FieldOffset(0)]
        private int v_int;
        [FieldOffset(0)]
        private long v_long;
        [FieldOffset(0)]
        private double v_double;
        [FieldOffset(8)]
        private HAL_Type type;

        public HAL_Type Type => type;

        public bool IsBoolean() => Type == HAL_Type.HAL_BOOLEAN;

        public bool IsDouble() => Type == HAL_Type.HAL_DOUBLE;

        public bool IsEnum() => Type == HAL_Type.HAL_ENUM;

        public bool IsInt() => Type == HAL_Type.HAL_INT;

        public bool IsLong() => Type == HAL_Type.HAL_LONG;

        public bool IsUnassigned => Type == HAL_Type.HAL_UNASSIGNED;

        public bool GetBoolean()
        {
            if (!IsBoolean()) throw new Exception();
            return v_boolean != 0;
        }

        public double GetDouble()
        {
            if (!IsDouble()) throw new Exception();
            return v_double;
        }

        public int GetInt()
        {
            if (!IsInt()) throw new Exception();
            return v_int;
        }

        public int GetEnum()
        {
            if (!IsEnum()) throw new Exception();
            return v_enum;
        }

        public long GetLong()
        {
            if (!IsLong()) throw new Exception();
            return v_long;
        }
    }
}
