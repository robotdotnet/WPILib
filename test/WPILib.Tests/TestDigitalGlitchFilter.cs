//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using HAL.Simulator;
//using NUnit.Framework;
//using WPILib.Exceptions;

//namespace WPILib.Tests
//{
//    [TestFixture]
//    public class TestDigitalGlitchFilter : TestBase
//    {
//        private struct FilterList
//        {
//            public DigitalInput input;
//            public DigitalGlitchFilter filter;
//            public int index;
//        }

//        [Test]
//        public void TestGlitchFilterValidCreation()
//        {
//            List<FilterList> inputs = new List<FilterList>();

//            try
//            {


//                for (int i = 0; i < 3; i++)
//                {
//                    DigitalInput input = new DigitalInput(i);
//                    DigitalGlitchFilter filter = new DigitalGlitchFilter();
//                    filter.Add(input);

//                    FilterList list = new FilterList
//                    {
//                        input = input,
//                        filter = filter,
//                        index = i
//                    };

//                    inputs.Add(list);

//                    Assert.That(SimData.DIO[i].FilterIndex, Is.EqualTo(i));
//                }

//            }
//            finally
//            {
//                foreach (var filterList in inputs)
//                {
//                    filterList.filter.Remove(filterList.input);
//                    filterList.filter.Dispose();
//                    filterList.input.Dispose();

//                    Assert.That(SimData.DIO[filterList.index].FilterIndex, Is.EqualTo(-1));
//                }    
//            }
//        }

//        [Test]
//        public void TestGlitchFilterOverCreation()
//        {
//            List<FilterList> inputs = new List<FilterList>();

//            try
//            {


//                for (int i = 0; i < 3; i++)
//                {
//                    DigitalInput input = new DigitalInput(i);
//                    DigitalGlitchFilter filter = new DigitalGlitchFilter();
//                    filter.Add(input);

//                    FilterList list = new FilterList
//                    {
//                        input = input,
//                        filter = filter,
//                        index = i
//                    };

//                    inputs.Add(list);

//                    Assert.That(SimData.DIO[i].FilterIndex, Is.EqualTo(i));
//                }

//                Assert.Throws<AllocationException>(() =>
//                {
//                    DigitalGlitchFilter filter = new DigitalGlitchFilter();
//                });

//            }
//            finally
//            {
//                foreach (var filterList in inputs)
//                {
//                    filterList.filter.Remove(filterList.input);
//                    filterList.filter.Dispose();
//                    filterList.input.Dispose();

//                    Assert.That(SimData.DIO[filterList.index].FilterIndex, Is.EqualTo(-1));
//                }
//            }
//        }
//    }
//}
