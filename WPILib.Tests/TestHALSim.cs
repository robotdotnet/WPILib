using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        public void TestHAL()
        {
            TestFilteredHAL(HAL.halData);
        }

        public void TestFilteredHAL(Dictionary<dynamic, dynamic> data)
        {
            foreach (var o in data)
            {
                Assert.IsTrue(o.Key is string);

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
            Dictionary<dynamic, dynamic> inDict = new Dictionary<dynamic, dynamic>()
            {
                {
                    "compressor", new Dictionary<dynamic, dynamic>()
                    {
                        {"on", true }
                    }
                },
            };

            Assert.IsFalse(HAL.halData["compressor"]["on"]);

            SimData.UpdateHalData(inDict, HAL.halData);

            Assert.IsTrue(HAL.halData["compressor"]["on"]);
        }
    }
}
