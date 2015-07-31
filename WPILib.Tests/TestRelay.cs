using System;
using System.Collections.Generic;
using HAL_Base;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestRelay
    {
        [TestFixtureSetUp]
        public static void Initialize()
        {
            TestBase.StartCode();
        }

        [TestFixtureTearDown]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        [Test]
        public void TestCreateAll()
        {
            List<Relay> relays = new List<Relay>();
            for (int i = 0; i < TestBase.RelayChannels; i++)
            {
                Relay r = new Relay(i);
                relays.Add(r);
            }

            foreach (var relay in relays)
            {
                relay.Dispose();
            }
        }

        [Test]
        public void TestCreateInvalid()
        {
            try
            {
                Relay r = new Relay(TestBase.RelayChannels);
                Assert.Fail();
            }
            catch (IndexOutOfRangeException)
            {
            }

        }

        [TestFixture]
        public class TestRelayBoth
        {
            //Test with Both
            [Test]
            public void TestForward()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
            public void TestReverse()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
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
                return HAL.halData;
            }
        }



        [TestFixture]
        public class TestRelayForward
        {
            //Test with Both
            [Test]
            public void TestForward()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
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

            [Test]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, data["relay"][0]["fwd"]);
                Assert.AreEqual(false, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
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
                return HAL.halData;
            }
        }

        [TestFixture]
        public class TestRelayReverse
        {
            [Test]
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

            [Test]
            public void TestReverse()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
            public void TestOn()
            {
                var data = GetSimData();

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(false, data["relay"][0]["fwd"]);
                Assert.AreEqual(true, data["relay"][0]["rev"]);
                relay.Dispose();
            }

            [Test]
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
                return HAL.halData;
            }
        }
    }
}
