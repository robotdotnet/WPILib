using System;
using System.Collections.Generic;
using System.Text;

namespace Hal
{
    public enum CANDeviceType
    {
        kBroadcast = 0,
        kRobotController = 1,
        kMotorController = 2,
        kRelayController = 3,
        kGyroSensor = 4,
        kAccelerometer = 5,
        kUltrasonicSensor = 6,
        kGearToothSensor = 7,
        kPowerDistribution = 8,
        kPneumatics = 9,
        kMiscellaneous = 10,
        kFirmwareUpdate = 31
    }

    public enum CANManufacturer
    {
        kBroadcast = 0,
        kNI = 1,
        kLM = 2,
        kDEKA = 3,
        kCTRE = 4,
        kREV = 5,
        kGrapple = 6,
        kMS = 7,
        kTeamUse = 8,
        kKauaiLabs = 9,
        kCopperforge = 10,
        kPWF = 11,
        kStudica = 12
    }
}
