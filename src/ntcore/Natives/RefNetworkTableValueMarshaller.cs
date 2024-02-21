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

#pragma warning disable IDE0052 // Remove unread private members
    private ref Ptr<byte> m_toAssignPin;
#pragma warning restore IDE0052 // Remove unread private members

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
                m_toAssignPin = ref m_nativeValue.data.valueRaw.data.GetPinnableByteReference();
                m_nativeValue.data.valueRaw.size = (nuint)managed.m_byteSpan.Length;
                break;
            case NetworkTableType.DoubleArray:
                m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                m_toAssignPin = ref m_nativeValue.data.arrDouble.arr.GetPinnableByteReference();
                m_nativeValue.data.arrDouble.size = (nuint)managed.m_byteSpan.Length / 8;
                break;
            case NetworkTableType.IntegerArray:
                m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                m_toAssignPin = ref m_nativeValue.data.arrInt.arr.GetPinnableByteReference();
                m_nativeValue.data.arrInt.size = (nuint)managed.m_byteSpan.Length / 8;
                break;
            case NetworkTableType.FloatArray:
                m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                m_toAssignPin = ref m_nativeValue.data.arrFloat.arr.GetPinnableByteReference();
                m_nativeValue.data.arrFloat.size = (nuint)managed.m_byteSpan.Length / 4;
                break;
            case NetworkTableType.BooleanArray:
                Span<int> boolArraySpan = MemoryMarshal.Cast<byte, int>(callerAllocatedBuffer);
                if (boolArraySpan.Length < managed.m_byteSpan.Length)
                {
                    boolArraySpan = new int[managed.m_byteSpan.Length];
                }
                for (int i = 0; i < boolArraySpan.Length; i++)
                {
                    boolArraySpan[i] = managed.m_byteSpan[i];
                }
                m_toPin = ref MemoryMarshal.AsBytes(boolArraySpan).GetPinnableReference();
                m_toAssignPin = ref m_nativeValue.data.arrBoolean.arr.GetPinnableByteReference();
                m_nativeValue.data.arrBoolean.size = (nuint)managed.m_byteSpan.Length;
                break;
            case NetworkTableType.String:
                if (managed.m_structValue.stringStorageType == RefNetworkTableValue.StringStorageType.Utf8)
                {
                    // String is stored as utf-8 in raw span
                    m_toPin = ref managed.m_byteSpan.GetPinnableReference();
                    m_nativeValue.data.valueString = new(null, (nuint)managed.m_byteSpan.Length);
                    m_toAssignPin = ref m_nativeValue.data.valueString.Str.GetPinnableByteReference();
                }
                else
                {
                    // Is string, convert to UTF-8
                    ReadOnlySpan<char> stringValue = MemoryMarshal.Cast<byte, char>(managed.m_byteSpan);
                    int byteCount = Encoding.UTF8.GetByteCount(stringValue);
                    Span<byte> stringSpan = callerAllocatedBuffer;
                    if (byteCount > stringSpan.Length)
                    {
                        stringSpan = new byte[byteCount];
                    }
                    else
                    {
                        stringSpan = stringSpan[..byteCount];
                    }
                    int exactBytes = Encoding.UTF8.GetBytes(stringValue, stringSpan);
                    Debug.Assert(exactBytes == byteCount);
                    m_toPin = ref stringSpan.GetPinnableReference();
                    m_nativeValue.data.valueString = new(null, (nuint)stringSpan.Length);
                    m_toAssignPin = ref m_nativeValue.data.valueString.Str.GetPinnableByteReference();
                }
                break;
            case NetworkTableType.StringArray:
                WpiStringMarshaller.WpiStringNative[] strings = new WpiStringMarshaller.WpiStringNative[managed.m_stringSpan.Length];

                for (int i = 0; i < strings.Length; i++)
                {
                    int len = Encoding.UTF8.GetByteCount(managed.m_stringSpan[i].AsSpan());
                    byte* mem = (byte*)NativeMemory.Alloc((nuint)len);
                    int exactLen = Encoding.UTF8.GetBytes(managed.m_stringSpan[i].AsSpan(), new Span<byte>(mem, len));
                    Debug.Assert(exactLen == len);
                    strings[i] = new WpiStringMarshaller.WpiStringNative(mem, (nuint)len);
                }

                m_toPin = ref MemoryMarshal.AsBytes(strings.AsSpan()).GetPinnableReference();
                m_toAssignPin = ref m_nativeValue.data.arrString.arr.GetPinnableByteReference();
                m_nativeValue.data.arrString.size = (nuint)managed.m_stringSpan.Length;
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
        if (!Unsafe.IsNullRef(in m_toPin))
        {
            m_toAssignPin = (byte*)Unsafe.AsPointer(ref Unsafe.AsRef(in m_toPin));
        }
        return m_nativeValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Free()
    {
        if (m_nativeValue.type == NetworkTableType.StringArray)
        {
            FreeStringArray();
        }
    }

    private readonly void FreeStringArray()
    {
        int len = (int)m_nativeValue.data.arrString.size;
        for (int i = 0; i < len; i++)
        {
            NativeMemory.Free(m_nativeValue.data.arrString.arr[i].Str);
        }
    }
}
