using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HAL_Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WPILib.Tests
{
    [TestClass]
    public class TestEncoders
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            TestBase.StartCode();
        }

        [ClassCleanup]
        public static void Kill()
        {
            DriverStation.Instance.Release();
        }

        private Dictionary<dynamic, dynamic> HalData()
        {
            return HAL_Base.HAL.halData;
        }

        public void CheckConfig(dynamic config, uint aMod, uint aPin, bool aAtr, uint bMod, uint bPin, bool bAtr)
        {
            Assert.AreEqual(aMod, config["ASource_Module"]);
            Assert.AreEqual(aPin, config["ASource_Channel"]);
            Assert.AreEqual(aAtr, config["ASource_AnalogTrigger"]);
            Assert.AreEqual(bMod, config["BSource_Module"]);
            Assert.AreEqual(bPin, config["BSource_Channel"]);
            Assert.AreEqual(bAtr, config["BSource_AnalogTrigger"]);

        }

        [TestMethod]
        public void TestChannelChannelInit()
        {
            using (Encoder x = new Encoder(1, 2))
            {
                Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                CheckConfig(HalData()["encoder"][0]["config"], 0 ,1 , false, 0 ,2, false);
                Assert.IsFalse(HalData()["encoder"][0]["reverse_direction"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestChannelChannelReverseInit()
        {
            using (Encoder x = new Encoder(1, 2, true))
            {
                Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                Assert.IsTrue(HalData()["encoder"][0]["reverse_direction"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestChannelChannelReverseTypeInit()
        {
            using (Encoder x = new Encoder(1, 2, true, EncodingType.K4X))
            {
                Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                Assert.IsTrue(HalData()["encoder"][0]["reverse_direction"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestChannelChannelChannelReverseInit()
        {
            using (Encoder x = new Encoder(1, 2, 3, true))
            {
                Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                Assert.IsTrue(HalData()["encoder"][0]["reverse_direction"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                Assert.IsTrue(HalData()["dio"][3]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            Assert.IsFalse(HalData()["dio"][3]["initialized"]);
        }

        [TestMethod]
        public void TestChannelChannelChannelInit()
        {
            using (Encoder x = new Encoder(1, 2, 3))
            {
                Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                Assert.IsFalse(HalData()["encoder"][0]["reverse_direction"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                Assert.IsTrue(HalData()["dio"][3]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            Assert.IsFalse(HalData()["dio"][3]["initialized"]);
        }

        [TestMethod]
        public void TestSourceSourceInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var x = new Encoder(s1, s2))
                    {
                        Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                        CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                        Assert.IsFalse(HalData()["encoder"][0]["reverse_direction"]);
                    }
                    Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                    Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                    Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                }
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestSourceSourceReverseTypeInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var x = new Encoder(s1, s2, true, EncodingType.K4X))
                    {
                        Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                        CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                        Assert.IsTrue(HalData()["encoder"][0]["reverse_direction"]);
                    }
                    Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                    Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                    Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                }
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestSourceSourceSourceReverseInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var s3 = new DigitalInput(3))
                    {
                        using (var x = new Encoder(s1, s2, s3, true))
                        {
                            Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                            CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                            Assert.IsTrue(HalData()["encoder"][0]["reverse_direction"]);
                        }
                        Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][3]["initialized"]);
                    }
                }
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsFalse(HalData()["dio"][2]["initialized"]);
                Assert.IsFalse(HalData()["dio"][3]["initialized"]);
            }
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            Assert.IsFalse(HalData()["dio"][3]["initialized"]);
        }

        [TestMethod]
        public void TestSourceSourceSourceInit()
        {
            using (var s1 = new DigitalInput(1))
            {
                using (var s2 = new DigitalInput(2))
                {
                    using (var s3 = new DigitalInput(3))
                    {
                        using (var x = new Encoder(s1, s2, s3))
                        {
                            Assert.IsTrue(HalData()["encoder"][0]["initialized"]);
                            CheckConfig(HalData()["encoder"][0]["config"], 0, 1, false, 0, 2, false);
                            Assert.IsFalse(HalData()["encoder"][0]["reverse_direction"]);
                        }
                        Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][2]["initialized"]);
                        Assert.IsTrue(HalData()["dio"][3]["initialized"]);
                    }
                }
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsFalse(HalData()["dio"][2]["initialized"]);
                Assert.IsFalse(HalData()["dio"][3]["initialized"]);
            }
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
            Assert.IsFalse(HalData()["dio"][3]["initialized"]);
        }

        [TestMethod]
        public void TestEncoderCreate2x()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K2X))
            {
                Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                Assert.IsTrue(HalData()["counter"][0]["initialized"]);
                Assert.AreEqual(2, HalData()["counter"][0]["average_size"]);

                Assert.AreEqual(true, HalData()["counter"][0]["up_rising_edge"]);
                Assert.AreEqual(true, HalData()["counter"][0]["up_falling_edge"]);

                Assert.AreEqual(false, HalData()["counter"][0]["down_rising_edge"]);
                Assert.AreEqual(true, HalData()["counter"][0]["down_falling_edge"]);
                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["counter"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestEncoderCreate1x()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K1X))
            {
                Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
                Assert.IsTrue(HalData()["counter"][0]["initialized"]);
                Assert.AreEqual(1, HalData()["counter"][0]["average_size"]);

                Assert.AreEqual(true, HalData()["counter"][0]["up_rising_edge"]);
                Assert.AreEqual(false, HalData()["counter"][0]["up_falling_edge"]);

                Assert.AreEqual(false, HalData()["counter"][0]["down_rising_edge"]);
                Assert.AreEqual(true, HalData()["counter"][0]["down_falling_edge"]);

                Assert.IsTrue(HalData()["dio"][1]["initialized"]);
                Assert.IsTrue(HalData()["dio"][2]["initialized"]);
            }
            Assert.IsFalse(HalData()["encoder"][0]["initialized"]);
            Assert.IsFalse(HalData()["counter"][0]["initialized"]);
            Assert.IsFalse(HalData()["dio"][1]["initialized"]);
            Assert.IsFalse(HalData()["dio"][2]["initialized"]);
        }

        [TestMethod]
        public void TestEncoderGet4x()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K4X))
            {
                HalData()["encoder"][0]["count"] = 1234;
                Assert.AreEqual(1234 / 4, x.Get());
            }
        }

        [TestMethod]
        public void TestEncoderGet2x()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K2X))
            {
                
                HalData()["counter"][0]["count"] = 1234;
                Assert.AreEqual(1234 / 2, x.Get());
            }
        }

        [TestMethod]
        public void TestEncoderGet1x()
        {
            using (var x = new Encoder(1, 2, false, EncodingType.K1X))
            {
                HalData()["counter"][0]["count"] = 1234;
                Assert.AreEqual(1234, x.Get());
            }
        }
    }
}
