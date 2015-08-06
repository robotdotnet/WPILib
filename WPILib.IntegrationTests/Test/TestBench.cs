using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib.IntegrationTests.Fixtures;

namespace WPILib.IntegrationTests.Test
{
    static class TestBench
    {
        public const double MOTOR_STOP_TIME = 0.25;

        public const int kTalonChannel = 10;
        public const int kVictorChannel = 1;
        public const int kJaguarChannel = 2;

        /*TiltPanCamera Channels */
        public const int kGyroChannel = 0;
        public const double kGyroSensitivity = 0.013;
        public const int kTiltServoChannel = 9;
        public const int kPanServoChannel = 8;


        /* PowerDistributionPanel channels */
        public const int kJaguarPDPChannel = 6;
        public const int kVictorPDPChannel = 8;
        public const int kTalonPDPChannel = 11;
        public const int kCANJaguarPDPChannel = 5;

        /* CAN ASSOICATED CHANNELS */
        public const int kCANRelayPowerCycler = 1;
        public const int kFakeJaguarPotentiometer = 1;
        public const int kFakeJaguarForwardLimit = 20;
        public const int kFakeJaguarReverseLimit = 21;
        public const int kCANJaguarID = 2;

        //THESE MUST BE IN INCREMENTING ORDER
        public const int DIOCrossConnectB2 = 9;
        public const int DIOCrossConnectB1 = 8;
        public const int DIOCrossConnectA2 = 7;
        public const int DIOCrossConnectA1 = 6;


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
