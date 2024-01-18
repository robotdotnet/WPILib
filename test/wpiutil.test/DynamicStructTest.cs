using WPIUtil.Serialization.Struct;
using Xunit;

namespace WPIUtil.Test;

public class DynamicStructTest
{
    [Fact]
    public void TestEmpty()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "");
        Assert.Equal("test", desc.Name);
        Assert.Equal("", desc.Schema);
        Assert.Empty(desc.Fields);
        Assert.True(desc.IsValid);
        Assert.Equal(0, desc.Size);
    }

    [Fact]
    public void TestNestedStruct()
    {
        var db = new StructDescriptorDatabase();
        var desc = db.Add("test", "int32 a");
        Assert.True(desc.IsValid);
        var desc2 = db.Add("test2", "test a");
        Assert.True(desc2.IsValid);
        Assert.Equal(4, desc2.Size);
    }
}
