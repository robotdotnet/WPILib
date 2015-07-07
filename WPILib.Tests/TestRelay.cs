using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestClass]
    public class TestRelay
    {

        [TestMethod]
        public void TestCreateAll()
        {
            List<Relay> relays = new List<Relay>();
            for (int i = 0; i < WPILib.SensorBase.RelayChannels; i++)
            {
                Relay r = new Relay(i);
                relays.Add(r);
            }

            foreach (var relay in relays)
            {
                relay.Dispose();
            }
        }

        [TestMethod]
        public void TestCreateInvalid()
        {
            try
            {
                Relay r = new Relay(SensorBase.RelayChannels);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

        }

        [TestClass]
        public class RelayBoth
        {
            //Test with Both
            [TestMethod]
            public void TestForward()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestReverse()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestOff()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            private Dictionary<dynamic,dynamic> GetSimData()
            {
                return HAL_Base.HAL.halData;
            }
        }



        [TestClass]
        public class RelayForward
        {
            //Test with Both
            [TestMethod]
            public void TestForward()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestReverse()
            {
                Relay relay = new Relay(0, Relay.Direction.Forward);
                try
                {
                    relay.Set(Relay.Value.Reverse);
                    Assert.Fail("No exception thrown. Should Throw an InvalidValueException.");
                }
                catch (InvalidValueException)
                {
                }
                finally
                {
                    relay.Dispose();
                }
            }

            [TestMethod]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestOff()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            private Dictionary<dynamic, dynamic> GetSimData()
            {
                return HAL_Base.HAL.halData;
            }
        }

        [TestClass]
        public class RelayReverse
        {
            [TestMethod]
            public void TestForward()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                try
                {
                    relay.Set(Relay.Value.Forward);
                    Assert.Fail("No exception thrown. Should Throw an InvalidValueException.");
                }
                catch (InvalidValueException)
                {
                }
                finally
                {
                    relay.Dispose();
                }
            }

            [TestMethod]
            public void TestReverse()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [TestMethod]
            public void TestOff()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            private Dictionary<dynamic, dynamic> GetSimData()
            {
                return HAL_Base.HAL.halData;
            }
        }
    }
}
