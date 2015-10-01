using System;
using System.Collections.Generic;
using HAL_Simulator;
using NUnit.Framework;

using HAL = HAL_Base.HAL;
using static HAL_Simulator.DriverStationHelper;

namespace WPILib.Tests
{
    /*
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

        public void TestNotifyDictAdditonInitialNotify()
        {

            int count = 0;
            string key = null;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

            Assert.AreEqual(1, count);
            Assert.AreEqual("Voltage", key);

        }

        [Test]
        public void TestNotifyDictAdditonNoInitialNotify()
        {
            int count = 0;
            string key = null;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

            Assert.AreEqual(0, count);
            Assert.AreEqual(null, key);
        }

        [Test]
        public void TestNotifyDictAssignement()
        {
            int count = 0;
            string key = null;
            dynamic value = -5;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
                value = v;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

            SimData.AnalogIn[0].Voltage = 1.25;

            Assert.AreEqual(1, count);
            Assert.AreEqual("Voltage", key);
            Assert.AreEqual(1.25, value);
        }

        public void TestNotifyDictHalUpdate()
        {
            int count = 0;
            string key = null;
            dynamic value = -5;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
                value = v;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

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

            Assert.AreEqual(1, count);
            Assert.AreEqual("voltage", key);
            Assert.AreEqual(1.25, value);
        }

        [Test]
        public void TestHalInNotifyDictUpdate()
        {
            int count = 0;
            string key = null;
            dynamic value = -5;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
                value = v;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

            SimData.AnalogIn[0].Voltage = 1.25;

            SimData.UpdateHalData(HAL.halInData, HAL.halData);

            Assert.AreEqual(1, count);
            Assert.AreEqual("Voltage", key);
            Assert.AreEqual(1.25, value);
        }

        [Test]
        public void TestNotifyDictRemoveCallback()
        {
            int count = 0;
            string key = null;
            dynamic value = -5;
            Action<dynamic, dynamic> mockDelegate = (k, v) =>
            {
                count++;
                key = k;
                value = v;
            };

            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Register("Voltage", mockDelegate);

            SimData.AnalogIn[0].Voltage = 1.25;

            SimData.AnalogIn[0].Cancel("Voltage", mockDelegate);

            SimData.AnalogIn[0].Voltage = 13.84;

            Assert.AreEqual(1, count);
            Assert.AreEqual("Voltage", key);
            Assert.AreEqual(1.25, value);
        }

        public void TestHALDeepCopy()
        {
            SimData.ResetHALData(false);

            SimData.AnalogIn[0].Voltage = 1.352;

            SimData.AnalogIn[0].Voltage = 454.57;

            Assert.AreNotEqual(HAL.halInData["analog_in"][0]["voltage"], HAL.halData["analog_in"][0]["voltage"]);
        }

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
    */
}
