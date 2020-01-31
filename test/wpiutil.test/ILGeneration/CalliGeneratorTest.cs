using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using WPIUtil.ILGeneration;
using Xunit;

namespace wpiutil.test
{
    public unsafe class CalliGeneratorTest
    {

        [Fact]
        public void TestCalliGenerationVoidVoid()
        {
            int callCount = 0;
            Action vvct = () =>
            {
                callCount++;
            };
            var fp = Marshal.GetFunctionPointerForDelegate(vvct);
            CalliILGenerator generator = new CalliILGenerator();
            DynamicMethod method = new DynamicMethod("MyFunc", null, null);
            generator.GenerateMethod(method.GetILGenerator(), null, new Type[0], fp, true);

            method.Invoke(null, null);

            Assert.Equal(1, callCount);
        }

        private delegate void Take1Int(int v);

        [Fact]
        public void TestCalliGenerationVoidInt()
        {
            int callCount = 0;
            int setVal = 0;
            Take1Int vvct = (i) =>
            {
                callCount++;
                setVal = i;
            };
            var fp = Marshal.GetFunctionPointerForDelegate(vvct);
            CalliILGenerator generator = new CalliILGenerator();
            DynamicMethod method = new DynamicMethod("MyFunc", null, new Type[] { typeof(int) });
            generator.GenerateMethod(method.GetILGenerator(), null, new Type[] { typeof(int) }, fp, true);

            method.Invoke(null, new object[] { 5 });

            Assert.Equal(1, callCount);
            Assert.Equal(5, setVal);
        }

        private delegate int TakeReturn1Int(int v);

        [Fact]
        public void TestCalliGenerationIntInt()
        {
            int callCount = 0;
            int setVal = 0;
            TakeReturn1Int vvct = (i) =>
            {
                callCount++;
                setVal = i;
                return i + 1;
            };
            var fp = Marshal.GetFunctionPointerForDelegate(vvct);
            CalliILGenerator generator = new CalliILGenerator();
            DynamicMethod method = new DynamicMethod("MyFunc", typeof(int), new Type[] { typeof(int) });
            generator.GenerateMethod(method.GetILGenerator(), method.ReturnType, new Type[] { typeof(int) }, fp, true);

            var ret = method.Invoke(null, new object[] { 5 });

            Assert.Equal(1, callCount);
            Assert.Equal(5, setVal);
            Assert.Equal(6, ret);
        }

        private delegate void Take1IntStar(int* v);

        [Fact]
        public void TestCalliGenerationVoidIntStar()
        {
            int callCount = 0;
            int setVal = 5;
            Take1IntStar vvct = (i) =>
            {
                callCount++;
                *i = *i + 1;
            };
            var fp = Marshal.GetFunctionPointerForDelegate(vvct);
            CalliILGenerator generator = new CalliILGenerator();
            DynamicMethod method = new DynamicMethod("MyFunc", null, new Type[] { typeof(int*) });
            generator.GenerateMethod(method.GetILGenerator(), null, new Type[] { typeof(int*) }, fp, true);

            method.Invoke(null, new object[] { Pointer.Box(&setVal, typeof(int*)) });

            Assert.Equal(1, callCount);
            Assert.Equal(6, setVal);
        }
    }
}
