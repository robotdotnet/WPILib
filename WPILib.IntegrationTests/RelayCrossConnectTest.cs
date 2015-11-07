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
        private static NetworkTable s_table;
        private RelayCrossConnectFixture m_relayFixture;

        [TestFixtureSetUp]
        public static void ClassSetup()
        {
            if (RobotBase.IsReal)
            {
                s_table = NetworkTable.GetTable("_RELAY_CROSS_CONNECT_TEST_");
            }
        }

        [SetUp]
        public void Setup()
        {
            m_relayFixture = TestBench.GetRelayCrossConnectFixture();
            m_relayFixture.Setup();
            if (s_table != null)
            {
                m_relayFixture.GetRelay().InitTable(s_table);
            }
        }

        [TearDown]
        public void TearDown()
        {
            m_relayFixture.Reset();
            m_relayFixture.Teardown();
        }

        [Test]
        public void TestBothHigh()
        {
            m_relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            m_relayFixture.GetRelay().Set(Relay.Value.On);
            m_relayFixture.GetRelay().UpdateTable();
            Assert.IsTrue(m_relayFixture.GetInputOne().Get(), "Input one was not high when relay set both high.");
            Assert.IsTrue(m_relayFixture.GetInputTwo().Get(), "Input two was not high when relay set both high.");
            if (s_table != null)
            {
                Assert.AreEqual("On", s_table.GetString("Value"));
            }
        }

        [Test]
        public void TestFirstHigh()
        {
            m_relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            m_relayFixture.GetRelay().Set(Relay.Value.Forward);
            m_relayFixture.GetRelay().UpdateTable();
            Assert.IsFalse(m_relayFixture.GetInputOne().Get(), "Input one was not low when relay set Value.Forward.");
            Assert.IsTrue(m_relayFixture.GetInputTwo().Get(), "Input two was not high when relay set Value.Forward.");
            if (s_table != null)
            {
                Assert.AreEqual("Forward", s_table.GetString("Value"));
            }
        }

        [Test]
        public void TestSecondHigh()
        {
            m_relayFixture.GetRelay().SetDirection(Relay.Direction.Both);
            m_relayFixture.GetRelay().Set(Relay.Value.Reverse);
            m_relayFixture.GetRelay().UpdateTable();
            Assert.IsTrue(m_relayFixture.GetInputOne().Get(), "Input one was not high when relay set Value.Reverse.");
            Assert.False(m_relayFixture.GetInputTwo().Get(), "Input two was not low when relay set Value.Reverse.");
            if (s_table != null)
            {
                Assert.AreEqual("Reverse", s_table.GetString("Value"));
            }
        }

        [Test]
        public void TestSetValueForwardWithDirectionReverseThrowingException()
        {
            m_relayFixture.GetRelay().SetDirection(Relay.Direction.Forward);
            Assert.Throws<InvalidValueException>(() =>
            {
                m_relayFixture.GetRelay().Set(Relay.Value.Reverse);
            });
        }

        [Test]
        public void TestSetValueReverseWithDirectionForwardThrowingException()
        {
            m_relayFixture.GetRelay().SetDirection(Relay.Direction.Reverse);
            Assert.Throws<InvalidValueException>(() =>
            {
                m_relayFixture.GetRelay().Set(Relay.Value.Forward);
            });
        }

        [Test]
        public void TestInitialSettings()
        {
            Assert.AreEqual(Relay.Value.Off, m_relayFixture.GetRelay().Get());

            Assert.IsFalse(m_relayFixture.GetInputOne().Get());
            Assert.IsFalse(m_relayFixture.GetInputTwo().Get());
            if (s_table != null)
            {
                Assert.AreEqual("Off", s_table.GetString("Value"));
            }
        }
    }
}
