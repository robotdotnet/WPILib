using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace WPILib.Tests
{
    [TestClass]
    public class TestNotifier
    {
        [ClassInitialize]
        static public void Init(TestContext c)
        {
            TestBase.StartCode();
        }

        [TestMethod]
        public void TestSingle()
        {
            var delegateMock = Mock.Create<Action>();

            Mock.Arrange(() => delegateMock()).OccursOnce();

            using (Notifier nt = new Notifier(delegateMock))
            {
                nt.StartSingle(0.25);

                Thread.Sleep(750);

                Mock.Assert(delegateMock);
            }
        }
        /*
        [TestMethod]
        public void TestMultiple()
        {
            var delegateMock = Mock.Create<Action>();

            Mock.Arrange(() => delegateMock()).OccursOnce();

            using (Notifier nt = new Notifier(delegateMock))
            {
                nt.StartPeriodic(0.1);

                Thread.Sleep(500);

                Mock.Assert(delegateMock);
            }
        }
        */

        /*
        [TestMethod]
        public void TestSingle10ms()
        {

            this._autoResetEvent = new AutoResetEvent(false);

            DateTime start = DateTime.Now;
            Notifier nt = new Notifier(() =>
            {
                DateTime end = DateTime.Now;
                TimeSpan total = end - start;
                Console.WriteLine($"Total Time: {total}");
                Console.WriteLine("Milliseconds: " + total.TotalMilliseconds);
                Assert.AreEqual(total.TotalMilliseconds, 50, 2);
                this._autoResetEvent.Set();
            });
            start = DateTime.Now;
            nt.StartSingle(0.05);
            Assert.IsTrue(this._autoResetEvent.WaitOne());
        }

        public void TestSingle50ms()
        {
            
        }

        public void TestSingle1s()
        {
            
        }
        */
        //Need to figure out how were are going to do this, because you can't trust the first loop.

        public void TestMultiple10ms()
        {
            
        }

        public void TestMultiple50ms()
        {
            
        }

        public void TestMultiple1s()
        {
            
        }
    }
}
