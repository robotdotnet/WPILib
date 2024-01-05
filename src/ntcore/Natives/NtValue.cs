using System.Runtime.InteropServices;

namespace NetworkTables.Natives;

[StructLayout(LayoutKind.Sequential)]
public partial struct NtValue
{
    public NtType type;
    public long lastChange;
    public long serverTime;

    public NtValueUnion data;

    [StructLayout(LayoutKind.Explicit)]
    public struct NtValueUnion
    {
        [FieldOffset(0)]
        public int valueBoolean;

        [FieldOffset(0)]
        public long valueInt;

        [FieldOffset(0)]
        public float valueFloat;

        [FieldOffset(0)]
        public double valueDouble;

        [FieldOffset(0)]
        public NtString valueString;

        [FieldOffset(0)]
        public NtValueRaw valueRaw;

        [FieldOffset(0)]
        public NtValueBooleanArray arrBoolean;

        [FieldOffset(0)]
        public NtValueDoubleArray arrDouble;

        [FieldOffset(0)]
        public NtValueFloatArray arrFloat;

        [FieldOffset(0)]
        public NtValueIntArray arrInt;

        [FieldOffset(0)]
        public NtValueStringArray arrString;

        public unsafe struct NtValueRaw
        {
            public byte* data;
            public nuint size;
        }

        public unsafe struct NtValueBooleanArray
        {
            public int* arr;
            public nuint size;
        }

        public unsafe struct NtValueDoubleArray
        {
            public double* arr;

            public nuint size;
        }

        public unsafe struct NtValueFloatArray
        {
            public float* arr;

            public nuint size;
        }

        public unsafe struct NtValueIntArray
        {
            public long* arr;

            public nuint size;
        }

        public unsafe partial struct NtValueStringArray
        {
            public NtString* arr;
            public nuint size;
        }
    }
}
