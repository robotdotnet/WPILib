using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPILib.Tests
{
    [TestFixture]
    public class TestCircularStack
    {
        private double[] values = {751.848, 766.366, 342.657, 234.252, 716.126,
                             132.344, 445.697, 22.727, 421.125, 799.913};
        private double[] output = {799.913, 421.125, 22.727, 445.697, 132.344,
                             716.126, 234.252, 342.657};

        private CircularStack<double> queue = new CircularStack<double>(8);

        [Test]
        public void InputOutputMatchIndexer()
        {
            foreach(double value in values)
            {
                queue.Push(value);
            }

            for (int i = 0; i < output.Length; i++)
            {
                Assert.That(queue[i], Is.EqualTo(output[i]).Within(0.00005));
            }
        }

        [Test]
        public void InputOutputMatchGetter()
        {
            foreach (double value in values)
            {
                queue.Push(value);
            }

            for (int i = 0; i < output.Length; i++)
            {
                Assert.That(queue.Get(i), Is.EqualTo(output[i]).Within(0.00005));
            }
        }
    }
}
