using System;
using System.Collections.Generic;
using System.Threading;
using HAL_Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HAL = HAL_Base.HAL;

namespace WPILib.Tests
{
    [TestClass]
    public class TestInterrupt
    {
        [ClassInitialize]
        public static void Initialize(TestContext c)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private static Dictionary<dynamic, dynamic> HalData()
        {
            return HAL_Base.HAL.halData;
        }


        [TestMethod]
        public void TestMethod1()
        {
            //HalData()["time"]["program_start"] = SimHooks.GetTime();
            //SimHooks.GetFPGATimestamp();
            Thread.Sleep(3000);
            //Console.WriteLine(SimHooks.GetTime() - HalData()["time"]["program_start"]);
            Console.WriteLine(SimHooks.GetFPGATimestamp());
        }
    }
}
