using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(RefNetworkTableValue), MarshalMode.ManagedToUnmanagedIn, typeof(RefNetworkTableValueMarshaller))]
public unsafe ref struct RefNetworkTableValueMarshaller
{
    public static int BufferSize => 256;
    private ref readonly byte m_toPin;

    private byte** m_toAssignPin;

    private bool m_doAssignment;

    private NetworkTableValueMarshaller.NativeNetworkTableValue m_nativeValue;

    public void FromManaged(in RefNetworkTableValue managed, Span<byte> callerAllocatedBuffer)
    {
        m_nativeValue.type = managed.Type;
        m_nativeValue.lastChange = managed.Time;
        m_nativeValue.serverTime = 0;

        switch (managed.Type)
        {
            case NetworkTableType.Boolean:
                m_nativeValue.data.valueBoolean = managed.m_structValue.boolValue ? 1 : 0;
                break;
            case NetworkTableType.Double:
                m_nativeValue.data.valueDouble = managed.m_structValue.doubleValue;
                break;
            case NetworkTableType.Integer:
                m_nativeValue.data.valueInt = managed.m_structValue.longValue;
                break;
            case NetworkTableType.Float:
                m_nativeValue.data.valueFloat = managed.m_structValue.floatValue;
                break;
            case NetworkTableType.Raw:
                m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.valueRaw.data)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.valueRaw.size = (nuint)managed.m_byteSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.DoubleArray:
                m_toPin = ref MemoryMarshal.AsBytes(managed.m_doubleSpan).GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.arrDouble.arr)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.arrDouble.size = (nuint)managed.m_doubleSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.IntegerArray:
                m_toPin = ref MemoryMarshal.AsBytes(managed.m_longSpan).GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.arrInt.arr)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.arrInt.size = (nuint)managed.m_longSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.FloatArray:
                m_toPin = ref MemoryMarshal.AsBytes(managed.m_floatSpan).GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.arrFloat.arr)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.arrFloat.size = (nuint)managed.m_floatSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.BooleanArray:
                Span<int> boolArraySpan = MemoryMarshal.Cast<byte, int>(callerAllocatedBuffer);
                if (boolArraySpan.Length < managed.m_boolSpan.Length)
                {
                    boolArraySpan = new int[managed.m_boolSpan.Length];
                }
                for (int i = 0; i < boolArraySpan.Length; i++)
                {
                    boolArraySpan[i] = managed.m_boolSpan[i] ? 1 : 0;
                }
                m_toPin = ref MemoryMarshal.AsBytes(boolArraySpan).GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.arrBoolean.arr)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.arrBoolean.size = (nuint)managed.m_boolSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.String:
                if (managed.m_stringValue == null)
                {
                    // String is stored as utf-8 in raw span
                    m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                    m_nativeValue.data.valueString = new(null, (nuint)managed.m_byteSpan.Length);
                    fixed (void* ptr = &m_nativeValue.data.valueString.Str)
                    {
                        m_toAssignPin = (byte**)ptr;
                    }
                    m_doAssignment = true;
                }
                else
                {
                    // Is string, convert to UTF-8
                    int byteCount = Encoding.UTF8.GetByteCount(managed.m_stringValue!);
                    Span<byte> stringSpan = callerAllocatedBuffer;
                    if (byteCount > stringSpan.Length)
                    {
                        stringSpan = new byte[byteCount];
                    } else {
                        stringSpan = stringSpan[..byteCount];
                    }
                    int exactBytes = Encoding.UTF8.GetBytes(managed.m_stringValue!, stringSpan);
                    Debug.Assert(exactBytes == byteCount);
                    m_toPin = ref stringSpan.GetPinnableReference();
                    m_nativeValue.data.valueString = new(null, (nuint)stringSpan.Length);
                    fixed (void* ptr = &m_nativeValue.data.valueString.Str)
                    {
                        m_toAssignPin = (byte**)ptr;
                    }
                    m_doAssignment = true;
                }
                break;
            case NetworkTableType.StringArray:
                WpiStringMarshaller.WpiStringNative[] strings = new WpiStringMarshaller.WpiStringNative[managed.m_stringSpan.Length];

                for (int i = 0; i < strings.Length; i++)
                {
                    int len = Encoding.UTF8.GetByteCount(managed.m_stringSpan[i]);
                    byte* mem = (byte*)NativeMemory.Alloc((nuint)len);
                    int exactLen = Encoding.UTF8.GetBytes(managed.m_stringSpan[i], new Span<byte>(mem, len));
                    Debug.Assert(exactLen == len);
                    strings[i] = new WpiStringMarshaller.WpiStringNative(mem, (nuint)len);
                }

                m_toPin = ref MemoryMarshal.AsBytes(strings.AsSpan()).GetPinnableReference();
                fixed (void* ptr = &m_nativeValue.data.arrString.arr)
                {
                    m_toAssignPin = (byte**)ptr;
                }
                m_nativeValue.data.arrString.size = (nuint)managed.m_stringSpan.Length;
                m_doAssignment = true;
                break;
            default:
                break;
        }
    }

    public readonly ref readonly byte GetPinnableReference()
    {
        return ref m_toPin;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public NetworkTableValueMarshaller.NativeNetworkTableValue ToUnmanaged()
    {
        if (m_doAssignment)
        {
            *m_toAssignPin = (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in m_toPin));
        }
        return m_nativeValue;
    }

    public void Free()
    {
        if (m_nativeValue.type == NetworkTableType.StringArray)
        {
            int len = (int)m_nativeValue.data.arrString.size;
            for (int i = 0; i < len; i++)
            {
                NativeMemory.Free(m_nativeValue.data.arrString.arr[i].Str);
            }
        }
    }
}
