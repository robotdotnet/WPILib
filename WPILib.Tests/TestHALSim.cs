using System;
using System.Collections.Generic;
using HAL_Base;
using HAL_Simulator;
using NUnit.Framework;
using Telerik.JustMock;

using HAL = HAL_Base.HAL;
using static HAL_Simulator.DriverStationHelper;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestHALSim : TestBase
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

        [Test]
        public void TestNotifyDictAdditonInitialNotify()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", Arg.AnyObject)).OccursOnce();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, true);

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestNotifyDictAdditonNoInitialNotify()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", Arg.AnyObject)).OccursNever();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, false);

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestNotifyDictAssignement()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", 1.25)).OccursOnce();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, false);

            HAL.halData["analog_in"][0]["voltage"] = 1.25;

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestNotifyDictHalUpdate()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", 1.25)).OccursOnce();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, false);

            Dictionary<dynamic, dynamic> inDict = new Dictionary<dynamic, dynamic>()
            {
                {
                    "analog_in", new List<dynamic>
                    {
                        new Dictionary<dynamic, dynamic>()
                        {
                            {"voltage", 1.25 }
                        }
                    }
                },
            };

            SimData.UpdateHalData(inDict, HAL.halData);

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestHalInNotifyDictUpdate()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", 1.25)).OccursOnce();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, false);

            HAL.halInData["analog_in"][0]["voltage"] = 1.25;

            SimData.UpdateHalData(HAL.halInData, HAL.halData);

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestNotifyDictRemoveCallback()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("voltage", 1.25)).OccursOnce();

            SimData.ResetHALData(false);

            HAL.halData["analog_in"][0].Register("voltage", delegateMock, false);

            HAL.halData["analog_in"][0]["voltage"] = 1.25;

            HAL.halData["analog_in"][0].Cancel("voltage", delegateMock);

            HAL.halData["analog_in"][0]["voltage"] = 13.84;

            Mock.Assert(delegateMock);
        }

        [Test]
        public void TestHALDeepCopy()
        {
            SimData.ResetHALData(false);

            HAL.halInData["analog_in"][0]["voltage"] = 1.352;

            HAL.halData["analog_in"][0]["voltage"] = 454.57;

            Assert.AreNotEqual(HAL.halInData["analog_in"][0]["voltage"], HAL.halData["analog_in"][0]["voltage"]);
        }

        [Test]
        public void TestHAL()
        {
            SimData.ResetHALData(false);
            TestFilteredHAL(HAL.halData);
        }

        public void TestFilteredHAL(Dictionary<dynamic, dynamic> data)
        {
            foreach (var o in data)
            {
                Assert.IsTrue(o.Key is string || o.Key is int);

                if (o.Value is Dictionary<dynamic, dynamic>)
                {
                    TestFilteredHAL(o.Value);
                }
                else if (o.Value is List<dynamic> || o.Value is Array)
                {
                    foreach (var vv in o.Value)
                    {
                        if (vv is Dictionary<dynamic, dynamic>)
                        {
                            TestFilteredHAL(vv);
                        }
                        else
                        {
                            Assert.IsTrue(vv == null || vv is double || vv is int || vv is string || vv is bool || vv is float || vv is uint || vv is long);
                        }
                    }
                }
                else
                {
                    var vv = o.Value;
                    Assert.IsTrue(vv == null || vv is double || vv is int || vv is string || vv is bool || vv is float || vv is uint || vv is long);
                }
            }
        }

        [Test]
        public void TestHalUpdate()
        {
            Dictionary<dynamic, dynamic> inDict = new Dictionary<dynamic, dynamic>
            {
                ["power"] = new Dictionary<dynamic, dynamic>
                {
                    ["vin_voltage"] = 3.14
                }
            };
            HAL.halData["power"]["vin_voltage"] = 0.0;

            Assert.AreEqual(0.0, HAL.halData["power"]["vin_voltage"]);

            SimData.UpdateHalData(inDict, HAL.halData);

            Assert.AreEqual(3.14, HAL.halData["power"]["vin_voltage"]);
        }
    }
}
