using WPILib.IntegrationTests.Fixtures;

namespace WPILib.IntegrationTests.Test
{
    static class TestBench
    {
        public const double MotorStopTime = 0.25;

        public const int TalonChannel = 10;
        public const int VictorChannel = 1;
        public const int JaguarChannel = 2;

        /*TiltPanCamera Channels */
        public const int GyroChannel = 0;
        public const double GyroSensitivity = 0.013;
        public const int TiltServoChannel = 9;
        public const int PanServoChannel = 8;


        /* PowerDistributionPanel channels */
        public const int JaguarPdpChannel = 6;
        public const int VictorPdpChannel = 8;
        public const int TalonPdpChannel = 11;
        public const int CanJaguarPdpChannel = 5;

        /* CAN ASSOICATED CHANNELS */
        public const int CanRelayPowerCycler = 1;
        public const int FakeJaguarPotentiometer = 1;
        public const int FakeJaguarForwardLimit = 20;
        public const int FakeJaguarReverseLimit = 21;
        public const int CanJaguarId = 2;

        //THESE MUST BE IN INCREMENTING ORDER
        public const int DioCrossConnectB2 = 9;
        public const int DioCrossConnectB1 = 8;
        public const int DioCrossConnectA2 = 7;
        public const int DioCrossConnectA1 = 6;


        public static RelayCrossConnectFixture GetRelayCrossConnectFixture()
        {
            RelayCrossConnectFixture relay = new MockRelayCrossConnectFixture();
            return relay;
        }

        internal class MockRelayCrossConnectFixture : RelayCrossConnectFixture
        {
            protected override Relay GiveRelay()
            {
                return new Relay(0);
            }

            protected override DigitalInput GiveInputOne()
            {
                return new DigitalInput(18);
            }

            protected override DigitalInput GiveInputTwo()
            {
                return new DigitalInput(19);
            }

        }

        internal class MockAnalogCrossConnectFixture : AnalogCrossConnectFixture
        {
            internal override AnalogInput GiveAnalogInput()
            {
                return new AnalogInput(2);
            }

            internal override AnalogOutput GiveAnalogOutput()
            {
                return new AnalogOutput(0);
            }
        }

        public static AnalogCrossConnectFixture GetAnalogCrossConnectFixture()
        {
            return new MockAnalogCrossConnectFixture();
        }
    }
}
