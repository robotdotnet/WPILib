using System;
using System.Collections.Generic;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;
// ReSharper disable UnusedVariable

namespace WPILib.Tests
{
    [TestFixture]
    public class TestRelay : TestBase
    {


        [Test]
        public void TestCreateAll()
        {
            List<Relay> relays = new List<Relay>();
            for (int i = 0; i < RelayChannels; i++)
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
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Relay r = new Relay(RelayChannels);
            });
        }

        [Test]
        public void TestCreateInvalidLower()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
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

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestReverse()
            {

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(true, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestOn()
            {

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, SimData.Relay[0].Forward);
                Assert.AreEqual(true, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestOff()
            {

                Relay relay = new Relay(0, Relay.Direction.Both);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
                relay.Dispose();
            }
        }



        [TestFixture]
        public class TestRelayForward
        {
            //Test with Both
            [Test]
            public void TestForward()
            {

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.Forward);
                Assert.AreEqual(true, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
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

                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(true, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestOff()
            {
                Relay relay = new Relay(0, Relay.Direction.Forward);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
                relay.Dispose();
            }
        }

        [TestFixture]
        public class TestRelayReverse
        {
            [Test]
            public void TestForward()
            {

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

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.Reverse);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(true, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestOn()
            {

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.On);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(true, SimData.Relay[0].Reverse);
                relay.Dispose();
            }

            [Test]
            public void TestOff()
            {

                Relay relay = new Relay(0, Relay.Direction.Reverse);
                relay.Set(Relay.Value.Off);
                Assert.AreEqual(false, SimData.Relay[0].Forward);
                Assert.AreEqual(false, SimData.Relay[0].Reverse);
                relay.Dispose();
            }
        }
    }
}
