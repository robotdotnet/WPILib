using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace NetworkTables.Natives;

public static unsafe partial class ValueFree
{
    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValueArray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DisposeValueArray(NtValue* arr, nuint count);

    [LibraryImport("ntcore", EntryPoint = "NT_DisposeValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void DisposeValue(NtValue* arr);
}


[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ManagedToUnmanagedOut, typeof(ReturnFrom))]
[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ManagedToUnmanagedIn, typeof(PassIn))]
[CustomMarshaller(typeof(NetworkTableValue), MarshalMode.ElementOut, typeof(ReturnInArray))]
public static unsafe class NtValueMarshaller
{
    public static class ReturnFrom
    {
        public static NetworkTableValue ConvertToManaged(in NtValue unmanaged)
        {
            return ReturnInArray.ConvertToManaged(unmanaged);
        }

        public static void Free(in NtValue unmanaged)
        {
            fixed (NtValue* ptr = &unmanaged)
            {
                ValueFree.DisposeValue(ptr);
            }
        }
    }

    public static class PassIn
    {
        public static NetworkTableValue ConvertToManaged(in NtValue unmanaged)
        {
            throw new NotImplementedException();
        }

        public static NtValue ConvertToUnmanaged(in NetworkTableValue managed)
        {
            throw new NotImplementedException();
        }

        public static void Free(in NtValue unmanaged)
        {
            throw new NotImplementedException();
        }
    }

    public static class ReturnInArray
    {
        public static NetworkTableValue ConvertToManaged(in NtValue unmanaged)
        {
            throw new NotImplementedException();
        }

        public static NtValue ConvertToUnmanaged(in NetworkTableValue managed)
        {
            throw new NotImplementedException();
        }
    }
}

[CustomMarshaller(typeof(NetworkTableValue[]), MarshalMode.ManagedToUnmanagedOut, typeof(NtValueArrayMarshaller<,>))]
[ContiguousCollectionMarshaller]
public unsafe struct NtValueArrayMarshaller<GenericPlaceholder, TUnmanagedElement> where TUnmanagedElement : unmanaged
{
    private TUnmanagedElement* unmanagedStorage;
    private int? length;
    private NetworkTableValue[]? managedStorage;

    public NtValueArrayMarshaller()
    {
        if (typeof(TUnmanagedElement) != typeof(NtValue))
        {
            throw new InvalidOperationException("Unmanaged type must be topic");
        }
    }

    public ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(int numElements)
    {
        length = numElements;
        return new ReadOnlySpan<TUnmanagedElement>(unmanagedStorage, numElements);
    }

    public Span<NetworkTableValue> GetManagedValuesDestination(int numElements)
    {
        length = numElements;
        managedStorage = new NetworkTableValue[numElements];
        return managedStorage;
    }

    public readonly void Free()
    {
        if (unmanagedStorage != null && length.HasValue)
        {
            ValueFree.DisposeValueArray((NtValue*)unmanagedStorage, (nuint)length.Value);
        }
    }

    public void FromUnmanaged(TUnmanagedElement* unmanaged)
    {
        unmanagedStorage = unmanaged;
    }

    public readonly NetworkTableValue[] ToManaged()
    {
        return managedStorage!;
    }
}

[StructLayout(LayoutKind.Sequential)]
public partial struct NtValue
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
