using System;
using System.Collections.Generic;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestClass]
    public class TestHALSim
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            TestBase.StartCode();
        }

        [TestMethod]
        public void TestNotifyDictAdditonInitialNotify()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", Arg.AnyObject)).OccursOnce();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, true);

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestNotifyDictAdditonNoInitialNotify()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", Arg.AnyObject)).OccursNever();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, false);

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestNotifyDictAssignement()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", 1.25)).OccursOnce();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, false);

            HAL.halData["analog_in"][0]["value"] = 1.25;

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestNotifyDictHalUpdate()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", 1.25)).OccursOnce();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, false);

            Dictionary<dynamic, dynamic> inDict = new Dictionary<dynamic, dynamic>()
            {
                {
                    "analog_in", new List<dynamic>
                    {
                        new Dictionary<dynamic, dynamic>()
                        {
                            {"value", 1.25 }
                        }
                    }
                },
            };

            SimData.UpdateHalData(inDict, HAL.halData);

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestHalInNotifyDictUpdate()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", 1.25)).OccursOnce();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, false);

            HAL.halInData["analog_in"][0]["value"] = 1.25;

            SimData.UpdateHalData(HAL.halInData, HAL.halData);

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestNotifyDictRemoveCallback()
        {
            var delegateMock = Mock.Create<Action<dynamic, dynamic>>();

            Mock.Arrange(() => delegateMock("value", 1.25)).OccursOnce();

            SimData.ResetHALData();

            HAL.halData["analog_in"][0].Register("value", delegateMock, false);

            HAL.halData["analog_in"][0]["value"] = 1.25;

            HAL.halData["analog_in"][0].Cancel("value", delegateMock);

            HAL.halData["analog_in"][0]["value"] = 13.84;

            Mock.Assert(delegateMock);
        }

        [TestMethod]
        public void TestHALDeepCopy()
        {
            SimData.ResetHALData();

            HAL.halInData["analog_in"][0]["avg_value"] = 1.352;

            HAL.halData["analog_in"][0]["avg_value"] = 454.57;

            Assert.AreNotEqual(HAL.halInData["analog_in"][0]["avg_value"], HAL.halData["analog_in"][0]["avg_value"]);
        }

        [TestMethod]
        public void TestHAL()
        {
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

        [TestMethod]
        public void TestHalUpdate()
        {
            Dictionary<dynamic, dynamic> inDict = new Dictionary<dynamic, dynamic>
            {
                ["pcm"] = new Dictionary<dynamic, dynamic>()
            };
            inDict["pcm"][0] = new Dictionary<dynamic, dynamic>
            {
                {
                    "compressor", new Dictionary<dynamic, dynamic>()
                    {
                        {"on", true }
                    }
                },
            };

            Assert.IsFalse(HAL.halData["pcm"][0]["compressor"]["on"]);

            SimData.UpdateHalData(inDict, HAL.halData);

            Assert.IsTrue(HAL.halData["pcm"][0]["compressor"]["on"]);
        }
    }
}
