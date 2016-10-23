using HAL.Simulator;
using HAL.Simulator.Data;
using NUnit.Framework;
using System;
using WPILib.Tests;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestUtility : TestBase
    {

        [Test]
        public void TestGetFPGAVersion()
        {
            Assert.AreEqual(2018, Utility.GetFPGAVersion());
        }

        [Test]
        public void TestFPGAGetRevision()
        {
            Assert.AreEqual(0, Utility.GetFPGARevision());
        }


        [Test]
        public void TestGetUserButton()
        {
            SimData.RoboRioData.SetFPGAButton(true);
            Assert.IsTrue(Utility.GetUserButton());
        }
    }

    internal class ReportErrorTesting
    {
        public void ThrowError(int status)
        {
            Utility.CheckStatus(status);
        }
    }
}

//Seperate namespace here, as the next test can't be WPILib.
namespace RobotCode.Tests
{
    [TestFixture]
    public class TestUtilityStatus : TestBase
    {
        /*
        [Test]
        public void TestCheckStatusReportingError()
        {
            SimData.ErrorList.Clear();
            ReportErrorTesting test = new ReportErrorTesting();
            test.ThrowError(10);

            Assert.That(SimData.ErrorList.Count, Is.EqualTo(1));
            ErrorData data = SimData.ErrorList[0];

            Assert.That(data.IsError, Is.True);
            Assert.That(data.ErrorCode, Is.EqualTo(10));
            Assert.That(data.IsLVCode, Is.False);
            Assert.That(data.Details, Is.EqualTo(HAL.Base.HAL.HAL_GetErrorMessage(data.ErrorCode)));
            Console.WriteLine(data.Location);
            var split = data.Location.Split('\n');
            Assert.That(split.Length, Is.EqualTo(3));
            Assert.That(split[0].StartsWith("WPILib: ThrowError,"));
            Assert.That(split[1].StartsWith("Caller: RobotCode.Tests.TestUtilityStatus.TestCheckStatusReportingError"));
            Assert.That(data.StackTrace, Is.Not.Null.Or.Empty);
        }
        */
    }
    
}


