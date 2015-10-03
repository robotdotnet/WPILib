using NetworkTables;
using NUnit.Framework;
using WPILib.Exceptions;
using WPILib.IntegrationTests.Fixtures;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class RelayCrossConnectTest : AbstractComsSetup
    {
        private static NetworkTable table;
        private RelayCrossConnectFixture relayFixture;

        [TestFixtureSetUp]
        public static void ClassSetup()
        {
            if (RobotBase.IsReal)
            {
                table = NetworkTable.GetTable("_RELAY_CROSS_CONNECT_TEST_");
            }
        }

        [SetUp]
        public void Setup()
        {
            relayFixture = TestBench.GetRelayCrossConnectFixture();
            relayFixture.Setup();
            if (table != null)
            {
                relayFixture.GetRelay().InitTable(table);
            }
        }

        [TearDown]
        public void TearDown()
        {
            relayFixture.Reset();
            relayFixture.Teardown();
        }

        [Test]
        public void TestBothHigh()
        {
            relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            relayFixture.GetRelay().Set(Relay.Value.On);
            relayFixture.GetRelay().UpdateTable();
            Assert.IsTrue(relayFixture.GetInputOne().Get(), "Input one was not high when relay set both high.");
            Assert.IsTrue(relayFixture.GetInputTwo().Get(), "Input two was not high when relay set both high.");
            if (table != null)
            {
                Assert.AreEqual("On", table.GetString("Value"));
            }
        }

        [Test]
        public void TestFirstHigh()
        {
            relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            relayFixture.GetRelay().Set(Relay.Value.Forward);
            relayFixture.GetRelay().UpdateTable();
            Assert.IsFalse(relayFixture.GetInputOne().Get(), "Input one was not low when relay set Value.Forward.");
            Assert.IsTrue(relayFixture.GetInputTwo().Get(), "Input two was not high when relay set Value.Forward.");
            if (table != null)
            {
                Assert.AreEqual("Forward", table.GetString("Value"));
            }
        }

        [Test]
        public void TestSecondHigh()
        {
            relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            relayFixture.GetRelay().Set(Relay.Value.Reverse);
            relayFixture.GetRelay().UpdateTable();
            Assert.IsTrue(relayFixture.GetInputOne().Get(), "Input one was not high when relay set Value.Reverse.");
            Assert.False(relayFixture.GetInputTwo().Get(), "Input two was not low when relay set Value.Reverse.");
            if (table != null)
            {
                Assert.AreEqual("Reverse", table.GetString("Value"));
            }
        }

        [Test]
        public void TestSetValueForwardWithDirectionReverseThrowingException()
        {
            relayFixture.GetRelay().SetDirection(Relay.Direction.Forward);
            Assert.Throws<InvalidValueException>(() =>
            {
                relayFixture.GetRelay().Set(Relay.Value.Reverse);
            });
        }

        [Test]
        public void TestSetValueReverseWithDirectionForwardThrowingException()
        {
            relayFixture.GetRelay().SetDirection(Relay.Direction.Reverse);
            Assert.Throws<InvalidValueException>(() =>
            {
                relayFixture.GetRelay().Set(Relay.Value.Forward);
            });
        }

        [Test]
        public void TestInitialSettings()
        {
            Assert.AreEqual(Relay.Value.Off, relayFixture.GetRelay().Get());

            Assert.IsFalse(relayFixture.GetInputOne().Get());
            Assert.IsFalse(relayFixture.GetInputTwo().Get());
            if (table != null)
            {
                Assert.AreEqual("Off", table.GetString("Value"));
            }
        }
    }
}
