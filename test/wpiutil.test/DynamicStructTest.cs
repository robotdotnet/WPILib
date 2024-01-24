using WPIUtil.Serialization.Struct;
using Xunit;

namespace WPIUtil.Test;

public class DynamicStructTest
{
    [Fact]
    public void TestEmpty()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", ""u8);
        Assert.Equal("test", desc.Name);
        Assert.Empty(desc.Fields);
        Assert.True(desc.IsValid);
        Assert.Equal(0, desc.Size);
    }

    [Fact]
    public void TestNestedStruct()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "int32 a"u8);
        Assert.True(desc.IsValid);
        var desc2 = db.Add("test2", "test a"u8);
        Assert.True(desc2.IsValid);
        Assert.Equal(4, desc2.Size);
    }

    [Fact]
    public void TestStringAllZeros()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[32]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.Equal("", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTrip()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[32]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.True(dynamic.SetStringField(field, "abc"));
        Assert.Equal("abc", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripEmbeddedNull()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[32]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.True(dynamic.SetStringField(field, "ab\0c"));
        Assert.Equal("ab\0c", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripStringTooLong()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[2]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "abc"));
        Assert.Equal("ab", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial2ByteUtf8()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[2]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\u0234"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTrip2ByteUtf8()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[3]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.True(dynamic.SetStringField(field, "a\u0234"));
        Assert.Equal("a\u0234", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial3ByteUtf8FirstByte()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[2]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\u1234"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial3ByteUtf8SecondByte()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[3]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\u1234"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTrip3ByteUtf8()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[4]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.True(dynamic.SetStringField(field, "a\u1234"));
        Assert.Equal("a\u1234", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial4ByteUtf8FirstByte()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[2]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\uD83D\uDC00"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial4ByteUtf8SecondByte()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[3]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\uD83D\uDC00"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTripPartial4ByteUtf8ThirdByte()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[4]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.False(dynamic.SetStringField(field, "a\uD83D\uDC00"));
        Assert.Equal("a", dynamic.GetStringField(field));
    }

    [Fact]
    public void TestStringRoundTrip4ByteUtf8()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "char a[5]"u8);
        var dynamic = DynamicStruct.Allocate(desc);
        var field = desc.FindFieldByName("a");
        Assert.NotNull(field);
        Assert.True(dynamic.SetStringField(field, "a\uD83D\uDC00"));
        Assert.Equal("a\uD83D\uDC00", dynamic.GetStringField(field));
    }
}
