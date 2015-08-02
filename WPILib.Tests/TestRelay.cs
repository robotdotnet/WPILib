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
        public void TestCreateInvalidUpper()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                Relay r = new Relay(TestBase.RelayChannels);
            });
        }

        [Test]
        public void TestCreateInvalidLower()
        {
            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                Relay r = new Relay(-1);
            });
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
                Assert.Throws<InvalidValueException>(() =>
                {
                    relay.Set(Relay.Value.Reverse);
                });
                relay.Dispose();
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
                Assert.Throws<InvalidValueException>(() =>
                {
                    relay.Set(Relay.Value.Forward);
                });
                relay.Dispose();
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
