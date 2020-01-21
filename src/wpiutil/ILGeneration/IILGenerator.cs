using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace WPIUtil.ILGeneration
{
    public interface IILGenerator
    {
        void GenerateMethod(ILGenerator generator, Type returnType, Type[] parameters, IntPtr nativeFp, bool isInstance = false);
    }
}
