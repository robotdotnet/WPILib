using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;
using static HAL_Simulator.DriverStationHelper;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestDriverStation : TestBase
    {
        [TestFixtureSetUp]
        public static void TestSetup()
        {
            StopDSLoop();
        }

        [TestFixtureTearDown]
        public static void TestTeardown()
        {
            StartDSLoop();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL.halData;
        }

        [Test]
        public void TestControlWordTrue()
        {
            HalData()["control"]["enabled"] = true;
            HalData()["control"]["autonomous"] = true;
            HalData()["control"]["test"] = true;
            HalData()["control"]["eStop"] = true;
            HalData()["control"]["ds_attached"] = true;
            HalData()["control"]["fms_attached"] = true;

            var ct = HAL.GetControlWord();

            Assert.IsTrue(ct.GetEnabled());
            Assert.IsTrue(ct.GetAutonomous());
            Assert.IsTrue(ct.GetTest());
            Assert.IsTrue(ct.GetEStop());
            Assert.IsTrue(ct.GetDSAttached());
            Assert.IsTrue(ct.GetFMSAttached());
        }

        [Test]
        public void TestControlWordFalse()
        {
            HalData()["control"]["enabled"] = false;
            HalData()["control"]["autonomous"] = false;
            HalData()["control"]["test"] = false;
            HalData()["control"]["eStop"] = false;
            HalData()["control"]["ds_attached"] = false;
            HalData()["control"]["fms_attached"] = false;

            var ct = HAL.GetControlWord();

            Assert.IsFalse(ct.GetEnabled());
            Assert.IsFalse(ct.GetAutonomous());
            Assert.IsFalse(ct.GetTest());
            Assert.IsFalse(ct.GetEStop());
            Assert.IsFalse(ct.GetDSAttached());
            Assert.IsFalse(ct.GetFMSAttached());
        }

        [Test]
        public void TestGetBatteryVoltage()
        {
            HalData()["power"]["vin_voltage"] = 7.928;
            Assert.AreEqual(7.928, DriverStation.Instance.GetBatteryVoltage(), 0.0001);
        }

        [Test]
        public void TestEnabled()
        {
            SetEnabledState(EnabledState.Enabled);
            UpdateData();
            Assert.IsTrue(DriverStation.Instance.Enabled);
        }

        [Test]
        public void TestDisabled()
        {
            SetEnabledState(EnabledState.Disabled);
            UpdateData();
            Assert.IsTrue(DriverStation.Instance.Disabled);
        }

        [Test]
        public void TestAutonomous()
        {
            SetRobotMode(RobotMode.Autonomous);
            UpdateData();
            Assert.IsTrue(DriverStation.Instance.Autonomous);
        }

        [Test]
        public void TestTeleop()
        {
            SetRobotMode(RobotMode.Teleop);
            UpdateData();
            Assert.IsTrue(DriverStation.Instance.OperatorControl);
        }

        [Test]
        public void TestTestMode()
        {
            SetRobotMode(RobotMode.Test);
            UpdateData();
            Assert.IsTrue(DriverStation.Instance.Test);
        }

        [Test]
        public void TestSysActive()
        {
            HAL.HALGetSystemActive = (ref int status) =>
            {
                status = 0;
                return false;
            };
            Assert.IsFalse(DriverStation.Instance.SysActive);

            HAL.HALGetSystemActive = HAL_Simulator.HAL.HALGetSystemActive;

            Assert.IsTrue(DriverStation.Instance.SysActive);

        }

        [Test]
        public void TestBrownedOut()
        {
            HAL.HALGetBrownedOut = (ref int status) =>
            {
                status = 0;
                return true;
            };
            Assert.IsTrue(DriverStation.Instance.BrownedOut);

            HAL.HALGetBrownedOut = HAL_Simulator.HAL.HALGetBrownedOut;

            Assert.IsFalse(DriverStation.Instance.BrownedOut);

        }

        [Test]
        [TestCase(DriverStation.Alliance.Red, HALAllianceStationID.HALAllianceStationID_red1)]
        [TestCase(DriverStation.Alliance.Red, HALAllianceStationID.HALAllianceStationID_red2)]
        [TestCase(DriverStation.Alliance.Red, HALAllianceStationID.HALAllianceStationID_red3)]
        [TestCase(DriverStation.Alliance.Blue, HALAllianceStationID.HALAllianceStationID_blue1)]
        [TestCase(DriverStation.Alliance.Blue, HALAllianceStationID.HALAllianceStationID_blue2)]
        [TestCase(DriverStation.Alliance.Blue, HALAllianceStationID.HALAllianceStationID_blue3)]
        public void TestGetAlliance(DriverStation.Alliance returnAlliance, HALAllianceStationID station)
        {
            SetAllianceStation(station);
            UpdateData();
            Assert.AreEqual(returnAlliance, DriverStation.Instance.GetAlliance());
        }

        [TestCase(1, HALAllianceStationID.HALAllianceStationID_red1)]
        [TestCase(2, HALAllianceStationID.HALAllianceStationID_red2)]
        [TestCase(3, HALAllianceStationID.HALAllianceStationID_red3)]
        [TestCase(1, HALAllianceStationID.HALAllianceStationID_blue1)]
        [TestCase(2, HALAllianceStationID.HALAllianceStationID_blue2)]
        [TestCase(3, HALAllianceStationID.HALAllianceStationID_blue3)]
        public void TestGetStation(int returnStation, HALAllianceStationID station)
        {
            SetAllianceStation(station);
            UpdateData();
            Assert.AreEqual(returnStation, DriverStation.Instance.GetLocation());
        }

        [Test]
        public void TestGetAllianceDefault()
        {
            HAL.HALGetAllianceStation = (ref HALAllianceStationID station) =>
            {
                station = (HALAllianceStationID) 9;
                return 0;
            };

            Assert.AreEqual(DriverStation.Alliance.Invalid, DriverStation.Instance.GetAlliance());

            HAL.HALGetAllianceStation = HAL_Simulator.HAL.HALGetAllianceStation;
        }

        [Test]
        public void TestGetLocationDefault()
        {
            HAL.HALGetAllianceStation = (ref HALAllianceStationID station) =>
            {
                station = (HALAllianceStationID)9;
                return 0;
            };

            Assert.AreEqual(0, DriverStation.Instance.GetLocation());

            HAL.HALGetAllianceStation = HAL_Simulator.HAL.HALGetAllianceStation;
        }
        
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestFMSAttached(bool attached)
        {
            SetFMSAttached(attached);
            UpdateData();
            Assert.AreEqual(attached, DriverStation.Instance.FMSAttached);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TestDSAttached(bool attached)
        {
            SetDSAttached(attached);
            UpdateData();
            Assert.AreEqual(attached, DriverStation.Instance.DSAtached);
        }

        [Test]
        public void TestGetMatchTime()
        {
            HAL.HALGetMatchTime = (ref float time) =>
            {
                time = 5.85f;
                return 0;
            };

            Assert.AreEqual(5.85, DriverStation.Instance.GetMatchTime(), 0.00001);

            HAL.HALGetMatchTime = HAL_Simulator.HAL.HALGetMatchTime;
        }
        /*
        [Test]
        [Ignore("Ignoring because it checks for joystick being connected. This needs to be fixed. Passes if we don't check for joystick")]
        public void TestJoystickGetName([Range(0, 5)]int stick)
        {
            SetJoystickName(stick, stick.ToString());
            UpdateData();
            Assert.AreEqual(stick.ToString(), DriverStation.Instance.GetJoystickName(stick));
        }
        */
    }
}
