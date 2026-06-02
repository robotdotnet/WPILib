using UnitsNet;
using WPIUtil.Serialization.Struct;
using Xunit;

namespace WPIMath.Test.Kinematics.Struct
{
    public class ChassisSpeedsStructTest
    {
        readonly ChassisSpeeds data = new(Speed.FromMetersPerSecond(1.0), Speed.FromMetersPerSecond(2.0), RotationalSpeed.FromRadiansPerSecond(3.0));

        [Fact]
        public void TestRoundtrip()
        {
            StructPacker packer = new(new byte[ChassisSpeeds.Struct.Size]);
            ChassisSpeeds.Struct.Pack(ref packer, data);

            StructUnpacker unpacker = new StructUnpacker(packer.Filled);
            ChassisSpeeds unpackedData = ChassisSpeeds.Struct.Unpack(ref unpacker);

            Assert.Equal(data, unpackedData);
        }
    }
}
