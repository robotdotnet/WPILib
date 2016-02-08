using HAL.Simulator;
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
            Assert.AreEqual(2015, Utility.GetFPGAVersion());
        }

        [Test]
        public void TestFPGAGetRevision()
        {
            Assert.AreEqual(0, Utility.GetFPGARevision());
        }


        [Test]
        public void TestGetUserButton()
        {
            SimData.RoboRioData.FPGAButton = true;
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
        //bool isError, int errorCode, bool isLVCode, [HALAllowNonBlittable]string details,
          //  [HALAllowNonBlittable]string location, [HALAllowNonBlittable]string callStack, bool printMsg

        [Test]
        public void TestCheckStatusReportingError()
        {
            var oldDelegate = HAL.Base.HAL.HALSendError;
            bool error = false;
            int ec = 0;
            bool isLV = true;
            string dets = "";
            string location = "";
            string stackTrace = "";
            bool printMsg = false;

            HAL.Base.HAL.HALSendError = (isError, errorCode, isLVCode, details, loc, stack, print) =>
            {
                error = isError;
                ec = errorCode;
                isLV = isLVCode;
                dets = details;
                location = loc;
                stackTrace = stack;
                printMsg = print;
                return 0;
            };

            ReportErrorTesting test = new ReportErrorTesting();
            test.ThrowError(10);

            Assert.That(error, Is.True);
            Assert.That(ec, Is.EqualTo(10));
            Assert.That(isLV, Is.False);
            Assert.That(dets, Is.EqualTo(HAL.Base.HAL.GetHALErrorMessage(ec)));
            Console.WriteLine(location);
            var split = location.Split('\n');
            Assert.That(split.Length, Is.EqualTo(3));
            Assert.That(split[0].StartsWith("WPILib: ThrowError,"));
            Assert.That(split[1].StartsWith("Caller: RobotCode.Tests.TestUtilityStatus.TestCheckStatusReportingError"));
            Assert.That(stackTrace, Is.Not.Null.Or.Empty);
            Assert.That(printMsg, Is.True);


            HAL.Base.HAL.HALSendError = oldDelegate;
        }
    }
}


