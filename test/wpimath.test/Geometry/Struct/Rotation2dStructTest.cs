using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Geometry;
using WPIUtil.Serialization.Struct;
using Xunit;

namespace WPIMath.Test.Geometry.Struct;

public class Rotation2dStructTest
{
    private static readonly Rotation2d ExpectedData = new(1.91.Radians());

    [Fact]
    public void TestRoundTrip()
    {
        StructPacker packer = new(new byte[Rotation2d.Struct.Size]);
        Rotation2d.Struct.Pack(ref packer, ExpectedData);
        StructUnpacker unpacker = new StructUnpacker(packer.Filled);
        Rotation2d data = Rotation2d.Struct.Unpack(ref unpacker);
        Assert.Equal(data, ExpectedData);
    }

    [Fact]
    public void TestRoundTripEscapedInterface()
    {
        Rotation2d data = GenericHelpers.StructRoundTrip(ExpectedData);
        Assert.Equal(data, ExpectedData);
    }
}
