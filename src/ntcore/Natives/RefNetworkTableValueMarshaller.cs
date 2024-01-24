using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using WPIUtil;
using WPIUtil.Marshal;

namespace NetworkTables.Natives;

[CustomMarshaller(typeof(RefNetworkTableValue), MarshalMode.ManagedToUnmanagedIn, typeof(RefNetworkTableValueMarshaller))]
public unsafe ref struct RefNetworkTableValueMarshaller
{
    private ref byte m_toPin;

    private ref byte* m_toAssignPin;

    private bool m_doAssignment;

    private NetworkTableValueMarshaller.NativeNetworkTableValue m_nativeValue;

    public void FromManaged(in RefNetworkTableValue managed)
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
                m_toPin = managed.m_byteSpan.GetPinnableReference();
                m_toAssignPin = m_nativeValue.data.valueRaw.data;
                m_nativeValue.data.valueRaw.size = (nuint)managed.m_byteSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.DoubleArray:
                m_toPin = MemoryMarshal.AsBytes(managed.m_doubleSpan).GetPinnableReference();
                m_toAssignPin = (byte*)m_nativeValue.data.arrDouble.arr;
                m_nativeValue.data.arrDouble.size = (nuint)managed.m_doubleSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.IntegerArray:
                m_toPin = MemoryMarshal.AsBytes(managed.m_longSpan).GetPinnableReference();
                m_toAssignPin = (byte*)m_nativeValue.data.arrInt.arr;
                m_nativeValue.data.arrInt.size = (nuint)managed.m_longSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.FloatArray:
                m_toPin = MemoryMarshal.AsBytes(managed.m_floatSpan).GetPinnableReference();
                m_toAssignPin = (byte*)m_nativeValue.data.arrFloat.arr;
                m_nativeValue.data.arrFloat.size = (nuint)managed.m_floatSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.BooleanArray:
                int[] boolArrayData = new int[managed.m_boolSpan.Length];
                for (int i = 0; i < boolArrayData.Length; i++)
                {
                    boolArrayData[i] = managed.m_boolSpan[i] ? 1 : 0;
                }
                m_toPin = MemoryMarshal.AsBytes(boolArrayData.AsSpan()).GetPinnableReference();
                m_toAssignPin = (byte*)m_nativeValue.data.arrBoolean.arr;
                m_nativeValue.data.arrBoolean.size = (nuint)managed.m_boolSpan.Length;
                m_doAssignment = true;
                break;
            case NetworkTableType.String:
                byte[] stringArrayData = Encoding.UTF8.GetBytes(managed.m_stringValue!);
                m_toPin = MemoryMarshal.AsBytes(stringArrayData.AsSpan()).GetPinnableReference();
                m_nativeValue.data.valueString = new(null, (nuint)stringArrayData.Length);
                m_toAssignPin = m_nativeValue.data.valueString.Str;
                m_doAssignment = true;
                break;
            case NetworkTableType.StringArray:
                WpiStringMarshaller.WpiStringNative[] strings = new WpiStringMarshaller.WpiStringNative[managed.m_stringSpan.Length];

                for (int i = 0; i < strings.Length; i++)
                {
                    int len = Encoding.UTF8.GetByteCount(managed.m_stringSpan[i]);
                    byte* mem = (byte*)NativeMemory.Alloc((nuint)len);
                    Encoding.UTF8.GetBytes(managed.m_stringSpan[i], new Span<byte>(mem, len));
                    strings[i] = new WpiStringMarshaller.WpiStringNative(mem, (nuint)len);
                }

                m_toPin = MemoryMarshal.AsBytes(strings.AsSpan()).GetPinnableReference();
                m_toAssignPin = (byte*)m_nativeValue.data.arrString.arr;
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

    public NetworkTableValueMarshaller.NativeNetworkTableValue ToUnmanaged()
    {
        if (m_doAssignment)
        {
            m_toAssignPin = (byte*)Unsafe.AsPointer(ref m_toPin);
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
