using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NetworkTableValueMarshaller
{
    public static class ReturnFrom
    {
        public static NetworkTableValue ConvertToManaged(in NativeNetworkTableValue unmanaged)
        {
            throw new NotImplementedException();
        }

        public static void Free(NativeNetworkTableValue unmanaged)
        {
            NetworkTableValue.Free(&unmanaged);
        }
    }

    public static class ReturnInArray
    {
        public static NetworkTableValue ConvertToManaged(in NativeNetworkTableValue unmanaged) => ReturnFrom.ConvertToManaged(unmanaged);

        public static NativeNetworkTableValue ConvertToUnmanaged(in NetworkTableValue managed) => throw new NotSupportedException();
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NativeNetworkTableValue
    {
        public NetworkTableType type;
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

}
