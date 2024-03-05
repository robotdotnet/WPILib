using UnitsNet;
using WPIMath.Proto;
using Xunit;

namespace WPIMath.Test.Kinematics.Proto
{
    public class ChassisSpeedsProtoTest
    {
        readonly ChassisSpeeds data = new(Speed.FromMetersPerSecond(1.0), Speed.FromMetersPerSecond(2.0), RotationalSpeed.FromRadiansPerSecond(3.0));

        [Fact]
        public void TestRoundtrip()
        {
            ProtobufChassisSpeeds proto = ChassisSpeeds.Proto.CreateMessage();
            ChassisSpeeds.Proto.Pack(proto, data);
            ChassisSpeeds unpacked = ChassisSpeeds.Proto.Unpack(proto);

            Assert.Equal(data.Vx, unpacked.Vx);
            Assert.Equal(data.Vy, unpacked.Vy);
            Assert.Equal(data.Omega, unpacked.Omega);
        }
    }
}
