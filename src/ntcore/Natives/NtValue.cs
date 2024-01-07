using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ManagedToUnmanagedIn, typeof(PassIn))]
[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NtValueMarshaller
{
    public static class ReturnFrom
    {
        public static NetworkTableValue ConvertToManaged(in NtValue unmanaged)
        {
            return new NetworkTableValue(unmanaged);
        }

        public static void Free(in NtValue unmanaged)
        {
            throw new NotImplementedException();
        }
    }

    public static class PassIn
    {
        public static NtValue ConvertToUnmanaged(in NetworkTableValue managed)
        {
            return new NtValue
            {
                type = managed.Type,
                serverTime = managed.ServerTime,
                lastChange = managed.Time,
                data = managed.GetNativeValueUnion(),
            };
        }

        public static void Free(in NtValue unmanaged)
        {
            switch (unmanaged.type)
            {
                case NetworkTableType.Raw:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.valueRaw.data);
                    break;
                case NetworkTableType.DoubleArray:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.arrDouble.arr);
                    break;
                case NetworkTableType.BooleanArray:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.arrBoolean.arr);
                    break;
                case NetworkTableType.StringArray:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.arrString.arr);
                    break;
                case NetworkTableType.FloatArray:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.arrFloat.arr);
                    break;
                case NetworkTableType.IntegerArray:
                    Marshal.FreeCoTaskMem((nint)unmanaged.data.arrInt.arr);
                    break;
            }
        }
    }

    public static class ReturnInArray
    {
        public static NetworkTableValue ConvertToManaged(in NtValue unmanaged) => ReturnFrom.ConvertToManaged(unmanaged);

        public static NtValue ConvertToUnmanaged(in NetworkTableValue managed) => throw new NotSupportedException();
    }
}

[StructLayout(LayoutKind.Sequential)]
public partial struct NtValue : INativeArrayFree
{
    public NetworkTableType type;
    public long lastChange;
    public long serverTime;

    public NtValueUnion data;

    public static unsafe void FreeArray(void* ptr, int len)
    {
        throw new NotImplementedException();
    }

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
