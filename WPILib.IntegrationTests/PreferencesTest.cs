using System;
using System.IO;
using NetworkTables;
using NUnit.Framework;
using WPILib.IntegrationTests.Test;

namespace WPILib.IntegrationTests
{
    [TestFixture]
    public class PreferencesTest : AbstractComsSetup
    {
        private NetworkTable prefTable;
        private Preferences pref;
        private long check;

        [SetUp]
        public void SetUp()
        {
            //We can't run NT or File based tests on the simulator. Just auto pass them.
            if (RobotBase.IsSimulation)
            {
                return;
            }
            try
            {
                string file = "/home/lvuser/wpilib-preferences.ini";
                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                using (StreamWriter writer = new StreamWriter(file))
                {
                    writer.Write("checkedValueInt = 2\ncheckedValueDouble = .2\ncheckedValueFloat = 3.14\ncheckedValueLong = 172\ncheckedValueString =\"hello \nHow are you ?\"\ncheckedValueBoolean = false");
                }

            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
            }

            pref = Preferences.Instance;
            prefTable = NetworkTable.GetTable("Preferences");
            check = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void Remove()
        {
            pref.Remove("checkedValueLong");
            pref.Remove("checkedValueDouble");
            pref.Remove("checkedValueString");
            pref.Remove("checkedValueInt");
            pref.Remove("checkedValueFloat");
            pref.Remove("checkedValueBoolean");
        }

        public void AddCheckedValue()
        {
            pref.PutLong("checkedValueLong", check);
            pref.PutDouble("checkedValueDouble", 1);
            pref.PutString("checkedValueString", "checked");
            pref.PutInt("checkedValueInt", 1);
            pref.PutFloat("checkedValueFloat", 1);
            pref.PutBoolean("checkedValueBoolean", true);
        }

        [Test]
        public void TestAddRemoveSave()
        {
            //We can't run NT or File based tests on the simulator. Just auto pass them.
            if (RobotBase.IsSimulation)
            {
                return;
            }
            Assert.AreEqual(pref.GetLong("checkedValueLong", 0), 172L);
            Assert.AreEqual(pref.GetDouble("checkedValueDouble", 0), .2, 0);
            Assert.AreEqual(pref.GetString("checkedValueString", ""), "hello \nHow are you ?");
            Assert.AreEqual(pref.GetInt("checkedValueInt", 0), 2);
            Assert.AreEqual(pref.GetFloat("checkedValueFloat", 0), 3.14, .001);
            Assert.IsFalse(pref.GetBoolean("checkedValueBoolean", true));
            Remove();
            Assert.AreEqual(pref.GetLong("checkedValueLong", 0), 0);
            Assert.AreEqual(pref.GetDouble("checkedValueDouble", 0), 0, 0);
            Assert.AreEqual(pref.GetString("checkedValueString", ""), "");
            Assert.AreEqual(pref.GetInt("checkedValueInt", 0), 0);
            Assert.AreEqual(pref.GetFloat("checkedValueFloat", 0), 0, 0);
            Assert.IsFalse(pref.GetBoolean("checkedValueBoolean", false));
            AddCheckedValue();
            pref.Save();
            Assert.AreEqual(check, pref.GetLong("checkedValueLong", 0));
            Assert.AreEqual(pref.GetDouble("checkedValueDouble", 0), 1, 0);
            Assert.AreEqual(pref.GetString("checkedValueString", ""), "checked");
            Assert.AreEqual(pref.GetInt("checkedValueInt", 0), 1);
            Assert.AreEqual(pref.GetFloat("checkedValueFloat", 0), 1, 0);
            Assert.IsTrue(pref.GetBoolean("checkedValueBoolean", false));
        }

        [Test]
        public void TestPreferencesToNetworkTables()
        {
            //We can't run NT or File based tests on the simulator. Just auto pass them.
            if (RobotBase.IsSimulation)
            {
                return;
            }
            string networkedNumber = "networkCheckedValue";
            int networkNumberValue = 100;
            pref.PutInt(networkedNumber, networkNumberValue);
            Assert.AreEqual(networkNumberValue.ToString(), prefTable.GetString(networkedNumber));
            pref.Remove(networkedNumber);
        }
    }
}
