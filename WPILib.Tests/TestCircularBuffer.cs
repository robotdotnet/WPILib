using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCircularBuffer
    {
        private double[] values = {751.848, 766.366, 342.657, 234.252, 716.126,
                             132.344, 445.697, 22.727, 421.125, 799.913};
        private double[] pushFrontOut = {799.913, 421.125, 22.727, 445.697, 132.344,
                              716.126, 234.252, 342.657};
        private double[] pushBackOut = {342.657, 234.252, 716.126, 132.344, 445.697,
                                  22.727, 421.125, 799.913};

        private CircularBuffer<double> queue = new CircularBuffer<double>(8);

        [Test]
        public void PushFrontTest()
        {
            queue.Reset();

            foreach(double value in values)
            {
                queue.PushFront(value);
            }

            for (int i = 0; i < pushFrontOut.Length; i++)
            {
                Assert.That(queue[i], Is.EqualTo(pushFrontOut[i]).Within(0.00005));
            }
        }

        [Test]
        public void PushBackTest()
        {
            queue.Reset();

            foreach (double value in values)
            {
                queue.PushBack(value);
            }

            for (int i = 0; i < pushBackOut.Length; i++)
            {
                Assert.That(queue[i], Is.EqualTo(pushBackOut[i]).Within(0.00005));
            }
        }

        [Test]
        public void TestPushPop()
        {
            queue = new CircularBuffer<double>(3);

            queue.PushBack(1.0);
            queue.PushBack(2.0);
            queue.PushBack(3.0);

            Assert.That(queue[0], Is.EqualTo(1.0).Within(0.00005));
            Assert.That(queue[1], Is.EqualTo(2.0).Within(0.00005));
            Assert.That(queue[2], Is.EqualTo(3.0).Within(0.00005));

            //The buffer is now full.

            queue.PushBack(4.0);

            Assert.That(queue[0], Is.EqualTo(2.0).Within(0.00005));
            Assert.That(queue[1], Is.EqualTo(3.0).Within(0.00005));
            Assert.That(queue[2], Is.EqualTo(4.0).Within(0.00005));

            queue.PushBack(5.0);

            Assert.That(queue[0], Is.EqualTo(3.0).Within(0.00005));
            Assert.That(queue[1], Is.EqualTo(4.0).Within(0.00005));
            Assert.That(queue[2], Is.EqualTo(5.0).Within(0.00005));

            queue.PopBack();
            Assert.That(queue[0], Is.EqualTo(3.0).Within(0.00005));
            Assert.That(queue[1], Is.EqualTo(4.0).Within(0.00005));

            queue.PopFront();

            Assert.That(queue[0], Is.EqualTo(4.0).Within(0.00005));

        }
    }
}
