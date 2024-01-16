using UnitsNet.NumberExtensions.NumberToAngle;
using WPIMath.Geometry;
using WPIMath.Proto;
using Xunit;

namespace WPIMath.Test.Geometry.Proto;

public class Rotation2dProtoTest
{
    private static readonly Rotation2d ExpectedData = new(1.91.Radians());

    [Fact]
    public void TestRoundTrip()
    {
        ProtobufRotation2d proto = Rotation2d.Proto.CreateMessage();
        Rotation2d.Proto.Pack(proto, ExpectedData);

        Rotation2d data = Rotation2d.Proto.Unpack(proto);
        Assert.Equal(data, ExpectedData);
    }

    [Fact]
    public void TestRoundTripEscapedInterface()
    {
        Rotation2d data = GenericHelpers.ProtoRoundTrip(ExpectedData);
        Assert.Equal(data, ExpectedData);
    }

    [Fact]
    public void TestRoundTripTypedInterface()
    {
        Rotation2d data = GenericHelpers.ProtoTypedRoundTrip<Rotation2d, ProtobufRotation2d>(ExpectedData);
        Assert.Equal(data, ExpectedData);
    }
}
