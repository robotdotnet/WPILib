using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using WPIUtil.ILGeneration;
using Xunit;

namespace wpiutil.test
{
    public unsafe class InterfaceGeneratorTest
    {
        internal static void StatusCheck(int status)
        {

        }

        private delegate byte I1FuncFirstFunc(int* val);
        public interface I1Func
        {
            byte FirstFunc(int* val);
        }

        private class MockFPLoader : IFunctionPointerLoader
        {
            private readonly Dictionary<string, (Delegate, IntPtr)> fpMap = new Dictionary<string, (Delegate, IntPtr)>();

            public void AddDelegate<T>(string name, T del) where T : Delegate
            {
                fpMap.Add(name, (del, Marshal.GetFunctionPointerForDelegate(del)));
            }

            public IntPtr GetProcAddress(string name)
            {
                return fpMap[name].Item2;
            }
        }

        [Fact]
        public void TestInterfaceGeneration1Func()
        {
            MockFPLoader fpLoader = new MockFPLoader();
            fpLoader.AddDelegate<I1FuncFirstFunc>("FirstFunc", (i) =>
            {
                *i = *i + 1;
                return 2;
            });

            var ilGenerator = new CalliILGenerator();
            InterfaceGenerator iGenerator = new InterfaceGenerator(fpLoader, ilGenerator);
            var impl = (I1Func)iGenerator.GenerateImplementations(new Type[] { typeof(I1Func) }, typeof(InterfaceGeneratorTest).GetMethod(nameof(StatusCheck)))[0];

            Assert.NotNull(impl);
            int x = 5;
            byte r = impl!.FirstFunc(&x);
            Assert.Equal(6, x);
            Assert.Equal(2, r);
        }
    }
}
