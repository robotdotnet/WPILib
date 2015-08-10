using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HAL_Simulator;
using NUnit.Framework;
using WPILib.Exceptions;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestDigitalInput : TestBase
    {
        public Dictionary<dynamic, dynamic> GetInputDictionary(int pin)
        {
            return SimData.HalData["dio"][pin];
        }

        public Dictionary<dynamic, dynamic> GetInputMXPDictionary(int pin)
        {
            if (pin < 10)
            {
                throw new ArgumentOutOfRangeException(nameof(pin), "Pin is not an MXP pin");
            }
            return SimData.HalData["mxp"][pin - 10];
        }

        public DigitalInput GetDigitalInput(int pin)
        {
            return new DigitalInput(pin);
        }

        [Test]
        public void TestInputCreateUnderLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var Input = GetDigitalInput(-1);
            });
        }

        [Test]
        public void TestInputCreateOverLimit()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var Input = GetDigitalInput(DigitalChannels);
            });
        }

        [Test]
        public void TestInputCreate()
        {
            using (DigitalInput Input = GetDigitalInput(0))
            {
                Assert.IsTrue(GetInputDictionary(0)["initialized"]);
                Assert.IsTrue(GetInputDictionary(0)["is_input"]);
            }
        }

        [Test]
        public void TestInputDoubleCreate()
        {
            using (DigitalInput Input = GetDigitalInput(0))
            {
                Assert.Throws<AllocationException>(() =>
                {
                    var io2 = GetDigitalInput(0);
                });
            }
        }

        [Test]
        public void TestDigitalInputCreateAll()
        {
            List<DigitalInput> Inputs = new List<DigitalInput>();
            for (int i = 0; i < DigitalChannels; i++)
            {
                Inputs.Add(GetDigitalInput(i));
                Assert.IsTrue(GetInputDictionary(0)["initialized"]);
                Assert.IsTrue(GetInputDictionary(0)["is_input"]);
                if (i > 9)
                {
                    Assert.IsTrue(GetInputMXPDictionary(i)["initialized"]);
                }
            }

            foreach (var Input in Inputs)
            {
                Input.Dispose();
            }
        }

        [Test]
        public void TestDigitalInputDispose()
        {
            DigitalInput Input = GetDigitalInput(0);
            Assert.IsTrue(GetInputDictionary(0)["initialized"]);
            Input.Dispose();
            Assert.IsFalse(GetInputDictionary(0)["initialized"]);
            Input = null;
            Input = GetDigitalInput(0);
            Assert.IsTrue(GetInputDictionary(0)["initialized"]);
            Input.Dispose();
        }

        [Test]
        public void TestDigitalInputGet([Range(0, DigitalChannels - 1)]int channel)
        {
            using (DigitalInput input = GetDigitalInput(channel))
            {
                GetInputDictionary(channel)["value"] = false;
                Assert.IsFalse(input.Get());
                GetInputDictionary(channel)["value"] = true;
                Assert.IsTrue(input.Get());
            }
        }
    }
}
