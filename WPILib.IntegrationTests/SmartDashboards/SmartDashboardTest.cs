using System;
using NetworkTables;
using NetworkTables.Native.Exceptions;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;
using WPILib.SmartDashboards;

namespace WPILib.IntegrationTests.SmartDashboards
{
    [TestFixture]
    public class SmartDashboardTest : AbstractComsSetup
    {
        private static NetworkTable table;

        [TestFixtureSetUp]
        public static void SetUpBeforeClass()
        {
            if (RobotBase.IsReal)
            {
                table = NetworkTable.GetTable("SmartDashboard");
            }
        }

        [Test]
        public void TestGetBadValue()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            Assert.Throws<TableKeyNotDefinedException>(() =>
            {
                SmartDashboard.GetString("_404_STRING_KEY_SHOULD_NOT_BE_FOUND_");
            });
        }

        [Test]
        public void TestPutString()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            string key = "testPutString";
            string value = "thisIsAValue";
            SmartDashboard.PutString(key, value);
            Assert.AreEqual(value, SmartDashboard.GetString(key));
            Assert.AreEqual(value, table.GetString(key));
        }

        [Test]
        public void TestPutNumber()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            string key = "testPutNumber";
            int value = 2147483647;
            SmartDashboard.PutNumber(key, value);
            Assert.AreEqual(value, SmartDashboard.GetNumber(key), 0.01);
            Assert.AreEqual(value, table.GetNumber(key), 0.01);
        }

        [Test]
        public void TestPutBoolean()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            string key = "testPutBoolean";
            bool value = true;
            SmartDashboard.PutBoolean(key, value);
            Assert.AreEqual(value, SmartDashboard.GetBoolean(key));
            Assert.AreEqual(value, table.GetBoolean(key));
        }

        [Test]
        public void TestReplaceString()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            string key = "testReplaceString";
            string valueOld = "oldValue";
            string valueNew = "newValue";
            SmartDashboard.PutString(key, valueOld);
            Assert.AreEqual(valueOld, SmartDashboard.GetString(key));
            Assert.AreEqual(valueOld, table.GetString(key));

            SmartDashboard.PutString(key, valueNew);
            Assert.AreEqual(valueNew, SmartDashboard.GetString(key));
            Assert.AreEqual(valueNew, table.GetString(key));
        }

        [Test]
        public void TestPutStringNullKey()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            Assert.Throws<ArgumentNullException>(() =>
            {
                SmartDashboard.PutString(null, "This should not work");
            });
        }

        public void TestPutStringNullValue()
        {
            if (RobotBase.IsSimulation)
            {
                Assert.Pass();
                return;
            }
            Assert.Throws<ArgumentNullException>(() =>
            {
                SmartDashboard.PutString("KEY_SHOULD_NOT_BE_STORED", null);
            });
        }
    }
}
