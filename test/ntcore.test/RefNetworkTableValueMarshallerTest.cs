using System.Runtime.CompilerServices;
using System.Text;
using NetworkTables.Natives;
using WPIUtil.Marshal;
using Xunit;

namespace NetworkTables.Test;

public class RefNetworkTableValueMarshallerTest
{
    private unsafe delegate void DataInDelegate(NetworkTableValueMarshaller.NativeNetworkTableValue* value, void* pinned);

    private static unsafe void HandleInMarshal(in RefNetworkTableValue value, DataInDelegate callback)
    {
        NetworkTableValueMarshaller.NativeNetworkTableValue __value_native = default;
        // Setup - Perform required setup.
        scoped RefNetworkTableValueMarshaller __value_native__marshaller = new();
        try
        {
            // Marshal - Convert managed data to native data.
            __value_native__marshaller.FromManaged(value, stackalloc byte[RefNetworkTableValueMarshaller.BufferSize]);
            // Pin - Pin data in preparation for calling the P/Invoke.
            fixed (void* __value_native__unused = __value_native__marshaller)
            {
                // PinnedMarshal - Convert managed data to native data that requires the managed data to be pinned.
                __value_native = __value_native__marshaller.ToUnmanaged();
                callback(&__value_native, __value_native__unused);
            }
        }
        finally
        {
            // CleanupCallerAllocated - Perform cleanup of caller allocated resources.
            __value_native__marshaller.Free();
        }

    }

    [Fact]
    public unsafe void TestBool()
    {
        HandleInMarshal(RefNetworkTableValue.MakeBoolean(false), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Boolean, v->type);
            Assert.Equal(0, v->data.valueBoolean);
            Assert.True(pinned is null);
        });

        HandleInMarshal(RefNetworkTableValue.MakeBoolean(true), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Boolean, v->type);
            Assert.Equal(1, v->data.valueBoolean);
            Assert.True(pinned is null);
        });
    }

    [Fact]
    public unsafe void TestInt()
    {
        HandleInMarshal(RefNetworkTableValue.MakeInteger(42), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Integer, v->type);
            Assert.Equal(42, v->data.valueInt);
            Assert.True(pinned is null);
        });

        HandleInMarshal(RefNetworkTableValue.MakeInteger(0), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Integer, v->type);
            Assert.Equal(0, v->data.valueInt);
            Assert.True(pinned is null);
        });
    }

    [Fact]
    public unsafe void TestDouble()
    {
        HandleInMarshal(RefNetworkTableValue.MakeDouble(42.0), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Double, v->type);
            Assert.Equal(42.0, v->data.valueDouble);
            Assert.True(pinned is null);
        });

        HandleInMarshal(RefNetworkTableValue.MakeDouble(56.5), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Double, v->type);
            Assert.Equal(56.5, v->data.valueDouble);
            Assert.True(pinned is null);
        });
    }

    [Fact]
    public unsafe void TestFloat()
    {
        HandleInMarshal(RefNetworkTableValue.MakeFloat(42.0f), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Float, v->type);
            Assert.Equal(42.0f, v->data.valueFloat, 1e-9);
            Assert.True(pinned is null);
        });

        HandleInMarshal(RefNetworkTableValue.MakeFloat(56.5f), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Float, v->type);
            Assert.Equal(56.5f, v->data.valueFloat, 1e-9);
            Assert.True(pinned is null);
        });
    }

    [Fact]
    public unsafe void TestUnassigned()
    {
        HandleInMarshal(RefNetworkTableValue.MakeUnassigned(), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Unassigned, v->type);
            Assert.True(pinned is null);
        });

        HandleInMarshal(RefNetworkTableValue.MakeUnassigned(), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Unassigned, v->type);
            Assert.True(pinned is null);
        });
    }

    [Fact]
    public unsafe void TestRaw()
    {
        Span<byte> raw = stackalloc byte[3];
        "abc"u8.CopyTo(raw);
        void* ptr = Unsafe.AsPointer(ref raw.GetPinnableReference());
        HandleInMarshal(RefNetworkTableValue.MakeRaw(raw), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Raw, v->type);
            Assert.Equal((nuint)3, v->data.valueRaw.size);
            ReadOnlySpan<byte> consumed = new ReadOnlySpan<byte>(v->data.valueRaw.data, (int)v->data.valueRaw.size);
            Assert.True(consumed.SequenceEqual("abc"u8));
            Assert.True(pinned == ptr);
            Assert.True(pinned == v->data.valueRaw.data);
        });

        HandleInMarshal(RefNetworkTableValue.MakeRaw(new()), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.Raw, v->type);
            Assert.Equal((nuint)0, v->data.valueRaw.size);
            Assert.True(pinned is null);
            Assert.True(v->data.valueRaw.data.IsNull);
        });
    }

    [Fact]
    public unsafe void TestString()
    {
        Span<byte> raw = stackalloc byte[3];
        "abc"u8.CopyTo(raw);
        void* ptr = Unsafe.AsPointer(ref raw.GetPinnableReference());
        HandleInMarshal(RefNetworkTableValue.MakeString(raw), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.String, v->type);
            Assert.Equal((nuint)3, v->data.valueString.Len);
            ReadOnlySpan<byte> consumed = new ReadOnlySpan<byte>(v->data.valueString.Str, (int)v->data.valueString.Len);
            Assert.True(consumed.SequenceEqual("abc"u8));
            Assert.True(pinned == ptr);
            Assert.True(pinned == v->data.valueString.Str);
        });

        HandleInMarshal(RefNetworkTableValue.MakeString("string"), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.String, v->type);
            Assert.Equal((nuint)6, v->data.valueString.Len);
            ReadOnlySpan<byte> consumed = new ReadOnlySpan<byte>(v->data.valueString.Str, (int)v->data.valueString.Len);
            Assert.True(consumed.SequenceEqual("string"u8));
            Assert.True(pinned == v->data.valueString.Str);
        });

        var longString = new string('a', 512);
        HandleInMarshal(RefNetworkTableValue.MakeString(longString), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.String, v->type);
            Assert.Equal((nuint)512, v->data.valueString.Len);
            ReadOnlySpan<byte> consumed = new ReadOnlySpan<byte>(v->data.valueString.Str, (int)v->data.valueString.Len);
            var lsArray = Encoding.UTF8.GetBytes(longString);
            Assert.True(consumed.SequenceEqual(lsArray));
            Assert.True(pinned == v->data.valueString.Str);
        });

        HandleInMarshal(RefNetworkTableValue.MakeString((string)null!), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.String, v->type);
            Assert.Equal((nuint)0, v->data.valueString.Len);
            Assert.True(pinned is null);
            Assert.True(v->data.valueString.Str.IsNull);
        });
    }

    [Fact]
    public unsafe void TestStringArray()
    {
        HandleInMarshal(RefNetworkTableValue.MakeStringArray(["abc", "12345"]), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.StringArray, v->type);
            Assert.Equal((nuint)2, v->data.arrString.size);

            ReadOnlySpan<WpiStringMarshaller.WpiStringNative> array = new ReadOnlySpan<WpiStringMarshaller.WpiStringNative>(v->data.arrString.arr, (int)v->data.arrString.size);
            Assert.Equal((nuint)3, array[0].Len);
            ReadOnlySpan<byte> consumed = new ReadOnlySpan<byte>(array[0].Str, (int)array[0].Len);
            Assert.True(consumed.SequenceEqual("abc"u8));

            Assert.Equal((nuint)5, array[1].Len);
            consumed = new ReadOnlySpan<byte>(array[1].Str, (int)array[1].Len);
            Assert.True(consumed.SequenceEqual("12345"u8));

            Assert.True(pinned == v->data.arrString.arr);
        });

        HandleInMarshal(RefNetworkTableValue.MakeStringArray(new()), (v, pinned) =>
        {
            Assert.Equal(NetworkTableType.StringArray, v->type);
            Assert.Equal((nuint)0, v->data.arrString.size);
            Assert.True(pinned is null);
            Assert.True(v->data.arrString.arr.IsNull);
        });
    }
}
