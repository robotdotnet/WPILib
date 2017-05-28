using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WPILib.Interfaces;

namespace WPILib.Tests.MotorControllers
{
    [TestFixture]
    public class TestMotorControllerConstructors
    {
        [Test]
        public void TestAllISpeedControllersHaveSingleParameterConstructor()
        {
            var instances = from t in typeof(ISpeedController).GetTypeInfo().Assembly.GetTypes()
                            where t.GetInterfaces().Contains(typeof(ISpeedController))
                                    && !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsInterface
                                     //&& t.GetConstructor(new [] { typeof(int)}) != null
                            select t;

            List<string> toThrow = (from instance in instances
                                    where instance.GetConstructor(new[] {typeof (int)}) == null
                                    select $"Speed controller class: {instance} does not contain a constructor that takes a single int.").ToList();

            if (toThrow.Count == 0)
            {
                Assert.Pass();
                return;
            }

            foreach (string s in toThrow)
            {
                Console.WriteLine(s);
            }
            Assert.Fail();
        }
    }
}
